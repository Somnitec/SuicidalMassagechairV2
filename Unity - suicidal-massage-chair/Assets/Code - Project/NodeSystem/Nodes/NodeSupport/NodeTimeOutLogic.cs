using System;
using System.Collections;
using Sirenix.OdinInspector;
using UnityEngine;
using XNode;

namespace NodeSystem.Nodes
{
    public class NodeTimeOutLogic
    {
        [HorizontalGroup("TimeOut"), LabelText("TimeOut")]
        public bool HasTimeOut = false;
        [HorizontalGroup("TimeOut"), HideLabel]
        [ShowIf("HasTimeOut")]
        [Range(0,30f)]
        public float TimeOut = 3.0f;
        
        private NodePort TimeOutButtonPort => node?.GetOutputPort("OnTimeOut");

        private Node node;
        
        
        public  void StartTimeOut(Action<NodePort> onTimeOut, MonoBehaviour coroutineHost, Node node)
        {
            this.node = node;
            coroutineHost.StartCoroutine(TimeOutCoroutine(onTimeOut));
        }
        
        private  IEnumerator TimeOutCoroutine(Action<NodePort> onTimeOut)
        {
            yield return new WaitForSeconds(TimeOut);

            if (HasTimeOut)
            {
                onTimeOut(TimeOutButtonPort);
            }
        }
    }
}