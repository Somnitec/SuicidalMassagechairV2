using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NodeSystem.Functions
{
    [Serializable]
    public class CompositeFunction : NodeScriptBaseFunction
    {
        [ValueDropdown("GetKeys"), SerializeField]
        private string key;

        [SerializeField] private bool loop;

        private NodePlayingLogic _nodePlayingLogic = new NodePlayingLogic();

        private Settings settings => SettingsHolder.Instance.Settings;
        private FunctionList functions => settings.compositeFunctionLibrary.Collection[key];

        public override void RaiseEvent()
        {
            if(_nodePlayingLogic == null) _nodePlayingLogic = new NodePlayingLogic();
            
            NodeFunctionRunner.Instance.StartCoroutine(
                _nodePlayingLogic.InvokeFunctionsCoroutine(functions, OnComplete));
        }

        private void OnComplete()
        {
            Debug.Log($"Composite {key} completed");
            if (loop)
            {
                Debug.Log($"Looping {key}");
                RaiseEvent();
            }
        }

        private IEnumerable<string> GetKeys()
        {
            return settings.compositeFunctionLibrary?.Collection?.Keys;
        }
    }
}