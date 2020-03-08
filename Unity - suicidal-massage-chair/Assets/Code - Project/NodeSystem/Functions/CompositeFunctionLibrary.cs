using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace NodeSystem.Functions
{
    [CreateAssetMenu]
    public class CompositeFunctionLibrary : SerializedScriptableObject
    {
        public Dictionary<string, FunctionList> Collection = new Dictionary<string, FunctionList>();
    }
}