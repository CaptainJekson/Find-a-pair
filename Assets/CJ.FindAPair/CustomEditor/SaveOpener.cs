#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace CJ.FindAPair.CustomEditor
{
    public class SaveOpener : EditorWindow
    {
        [MenuItem("Find a pair/Open save data")]
        private static void ShowWindow()
        {
            EditorWindow.GetWindow(typeof(SaveOpener));
        }

        private void OnGUI()
        {
            if (GUILayout.Button("Open save data"))
            {
                System.Diagnostics.Process.Start(Application.dataPath);
            }
        }
    }
}

#endif
