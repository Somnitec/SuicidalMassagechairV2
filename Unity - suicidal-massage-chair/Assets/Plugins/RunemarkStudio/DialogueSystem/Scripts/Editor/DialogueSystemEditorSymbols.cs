namespace Runemark.DialogueSystem
{
    using Runemark.Common;
    using System.Linq;
    using UnityEditor;

    [InitializeOnLoad]
    public class DialogueSystemEditorSymbols : Editor
    {
        private static string _DialogueSystemSymbol = "RUNEMARK_DIALOGUE_SYSTEM";
        private static string _CinemachineSymbol = "CINEMACHINE";
        static DialogueSystemEditorSymbols()
        {
            RunemarkGUI.Textures.LoadFromResources("InspectorTitleIcon", "Editor/DialogueSystemIcon_Dark");

            string symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
            if (!symbols.Split(';').Contains(_DialogueSystemSymbol))
            {
                PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols + ";" + _DialogueSystemSymbol);
            }

            // Add symbol for cinemachine if exists.
            if (TypeUtils.ClassExists("CinemachineCore"))
            {
                symbols = PlayerSettings.GetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup);
                if (!symbols.Split(';').Contains(_CinemachineSymbol))
                {
                    PlayerSettings.SetScriptingDefineSymbolsForGroup(EditorUserBuildSettings.selectedBuildTargetGroup, symbols + ";" + _CinemachineSymbol);
                }
            }
        }
    }
}