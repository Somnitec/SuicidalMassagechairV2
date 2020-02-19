using System;
using System.Collections;
using System.Collections.Generic;

namespace Framework
{
    [Serializable]
    public class Event
    {
    }

    [Serializable]
    public class Events : GenericEvents<Event>
    {
    }


    [Serializable]
    public class GenericEvents<J> : Singleton<GenericEvents<J>>
    {
        public delegate void EventDelegate<T>(T e) where T : J;
        private delegate void EventDelegate(J e);

        private Dictionary<Type, EventDelegate> delegates = new Dictionary<Type, EventDelegate>();
        private Dictionary<Delegate, EventDelegate> delegateLookup = new Dictionary<Delegate, EventDelegate>();

        public void AddListener<T>(EventDelegate<T> del) where T : J
        {
            // Early-out if we've already registered this delegate
            if (delegateLookup.ContainsKey(del))
                return;

            // Create a new non-generic delegate which calls our generic one.
            // This is the delegate we actually invoke.
            EventDelegate internalDelegate = (e) => del((T)e);
            delegateLookup[del] = internalDelegate;

            if (delegates.TryGetValue(typeof(T), out EventDelegate tempDel))
            {
                delegates[typeof(T)] = tempDel += internalDelegate;
            }
            else
            {
                delegates[typeof(T)] = internalDelegate;
            }
        }

        public void AddListener<T>(T type, EventDelegate<T> del) where T : J
        {
            AddListener<T>(del);
        }

        public void RemoveListener<T>(EventDelegate<T> del) where T : J
        {
            if (delegateLookup.TryGetValue(del, out EventDelegate internalDelegate))
            {
                if (delegates.TryGetValue(typeof(T), out EventDelegate tempDel))
                {
                    tempDel -= internalDelegate;
                    if (tempDel == null)
                    {
                        delegates.Remove(typeof(T));
                    }
                    else
                    {
                        delegates[typeof(T)] = tempDel;
                    }
                }

                delegateLookup.Remove(del);
            }
        }

        public void RemoveListener<T>(T type, EventDelegate<T> del) where T : J
        {
            RemoveListener<T>(del);
        }

        public void Raise(J e)
        {
            EventDelegate del;
            if (delegates.TryGetValue(e.GetType(), out del))
            {
                del.Invoke(e);
            }
        }
    }
}
