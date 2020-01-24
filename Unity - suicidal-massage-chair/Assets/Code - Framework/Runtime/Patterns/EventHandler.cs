using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Framework
{
    public abstract class EventHandler<T> : MonoBehaviour where T : GameEvent
    {
        protected abstract void HandleEvent(T e);

        private void OnEnable()
        {
            Events.Instance.AddListener<T>(HandleEvent);
        }

        private void OnDisable()
        {
            Events.Instance.RemoveListener<T>(HandleEvent);
        }
    }
}