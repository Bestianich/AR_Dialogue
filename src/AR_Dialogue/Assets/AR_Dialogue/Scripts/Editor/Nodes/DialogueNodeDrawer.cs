using System.Collections.Generic;
using System.Linq;
using AR_Dialogue;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

namespace AR_DialogueEditor
{
    [CustomNodeEditor(typeof(DialogueNode))]
    public class DialogueNodeDrawer : NodeEditor
    {
        private DialogueNode _dialogueNode;

        
        private bool _showNodeSettings = true;
        private string _newDialogueOption = "";
        private string _newDialogueOptionOutput = "";
        private int _currentTab = 0;
        private int _portToDelete = 0;
        
        private bool _showDialogueSettings = true;
        
        private bool _showDialogueOptions = true;
        
        public override void OnBodyGUI()
        {
            if(_dialogueNode == null)
                _dialogueNode = target as DialogueNode;
            serializedObject.Update();

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultInput"));
            
            
            _showDialogueSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showDialogueSettings, "Dialogue Settings");
            if (_showDialogueSettings)
            {
                
                float prevWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 150;
                // NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("CharacterName"));
                // EditorGUIUtility.labelWidth = prevWidth;
                
                
                // prevWidth = EditorGUIUtility.labelWidth;
                // EditorGUILayout.PrefixLabel("Character Name");
                // _dialogueNode.CharacterName = EditorGUILayout.TextField(_dialogueNode.CharacterName);
                // EditorGUIUtility.labelWidth = prevWidth;
                
               
                EditorGUIUtility.labelWidth = 150;
                NodeEditorGUILayout.PropertyField( serializedObject.FindProperty("TextMeshReference"));
                EditorGUIUtility.labelWidth = prevWidth;
                
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Text"));
                
                
               
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            
            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultOutput"));
            
        }
    }
}