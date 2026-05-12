using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AR_DialogueEditor
{
    [CustomEditor(typeof(DialogueGraph))]
    public class DialogueGraphEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            serializedObject.Update();
            if (GUILayout.Button("Edit DialogueGraph", GUILayout.Height(50)))
            {
                DialogueGraphWindow w = DialogueGraphWindow.Open(target as DialogueGraph);
            }
            DrawDefaultInspector();
        }
    }
}