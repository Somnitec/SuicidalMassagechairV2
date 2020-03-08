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

        private MonoBehaviour coroutineRunner = NodeFunctionRunner.Instance;

        public override void RaiseEvent(MonoBehaviour coroutineRunner)
        {
            if (_nodePlayingLogic == null) _nodePlayingLogic = new NodePlayingLogic();

            this.coroutineRunner = coroutineRunner;

            coroutineRunner.StartCoroutine(
                _nodePlayingLogic.InvokeFunctionsCoroutine(functions, OnComplete, coroutineRunner));
        }

        private void OnComplete()
        {
            Debug.Log($"Composite {key} completed");
            if (loop)
            {
                Debug.Log($"Looping {key}");
                RaiseEvent(coroutineRunner);
            }
        }

        private IEnumerable<string> GetKeys()
        {
            return settings.compositeFunctionLibrary?.Collection?.Keys;
        }
    }
}