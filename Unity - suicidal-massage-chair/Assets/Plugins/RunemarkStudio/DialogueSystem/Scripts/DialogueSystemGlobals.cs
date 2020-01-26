namespace Runemark.DialogueSystem
{
    using Runemark.VisualEditor;
    using UnityEngine;

    //[CreateAssetMenu(fileName = "DialogueSystemGlobals", menuName = "RunemarkDeveloper/Dialogue System Globals", order = 1)]
    [System.Serializable]
    public class DialogueSystemGlobals : ScriptableObject
    {
        #region GLOBAL VARIABLES
        public VariableCollection Variables = new VariableCollection();
        #endregion

        private void OnEnable()
        {
            Runemark.Common.RunemarkDebug.Log("Try to reset global variables: {0}", Variables.GetAll().Count);

            foreach (var v in Variables.GetAll())
            {
                Common.RunemarkDebug.Log("{0} {1} reset.", v.Name, (v.Save ? "shouldn't" : "should"));
                if (!v.Save) v.Reset();
            }
        }
    }
}
