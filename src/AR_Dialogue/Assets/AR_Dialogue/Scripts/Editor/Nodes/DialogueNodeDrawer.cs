using System.Collections.Generic;
using System.Linq;
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

            NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("defaultInput"));
            
            
            //Node Settings Part
            _showNodeSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showNodeSettings, "Node Settings");
            if (_showNodeSettings)
            {
                
                _currentTab = GUILayout.Toolbar(_currentTab, new string[] { "Add Dialogue Option", "Remove Option" });

                switch (_currentTab)
                {
                    //Add new Dialogue Option
                    case 0:
                        EditorGUILayout.PrefixLabel("Dialogue");
                        _newDialogueOption = EditorGUILayout.TextField(_newDialogueOption);
                        EditorGUILayout.PrefixLabel("Output Port");
                        _newDialogueOptionOutput = EditorGUILayout.TextField(_newDialogueOptionOutput);

                        if (GUILayout.Button("Create new Dialogue Option"))
                        {
                            bool noDialogue = _newDialogueOption.Length == 0;
                            bool noPort = _newDialogueOptionOutput.Length == 0;
                            if (noDialogue)
                            {
                                EditorUtility.DisplayDialog("Error on creating a new Dialogue Option", "Please insert a valid dialogue.", "OK");
                                return;
                            }

                            if (noPort)
                            {
                                EditorUtility.DisplayDialog("Error on creating a new Dialogue Option", "Please insert a valid Output Port.", "OK");
                                return;
                            }

                            _dialogueNode.AddDynamicOutput(typeof(int), Node.ConnectionType.Multiple, Node.TypeConstraint.None,
                                _newDialogueOptionOutput);
                            _dialogueNode.DialogueOptions.Add(new DialogueOption(_newDialogueOption, _newDialogueOptionOutput));
                        }
                        break;
                    //Delete Dialogue Option
                    case 1:
                        if (_dialogueNode.DialogueOptions.Count == 0)
                        {
                            EditorGUILayout.HelpBox("No dialogue options found.", MessageType.Info);
                            break;
                        } 
                        EditorGUILayout.PrefixLabel("Choose Port");
                        List<string> outputs = new List<string>();
                        foreach (NodePort port in _dialogueNode.DynamicOutputs)
                        {
                            outputs.Add(port.fieldName);
                        }
                        
                        _portToDelete = EditorGUILayout.Popup(_portToDelete, outputs.ToArray());
                        if (GUILayout.Button("Delete Port"))
                        {
                            _dialogueNode.DialogueOptions.RemoveAt(_portToDelete);
                            _dialogueNode.RemoveDynamicPort(_dialogueNode.DynamicOutputs.ElementAt(_portToDelete));
                        }
                        break;
                    default:
                        break;
                }
                
            }
            
            //Dialouge Settings Part
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            _showDialogueSettings = EditorGUILayout.BeginFoldoutHeaderGroup(_showDialogueSettings, "Dialogue Settings");
            if (_showDialogueSettings)
            {
             
                float prevWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 150;
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("CharacterName"));
                EditorGUIUtility.labelWidth = prevWidth;

                prevWidth = EditorGUIUtility.labelWidth;
                EditorGUIUtility.labelWidth = 150;
                _dialogueNode.CharacterSprite = (Sprite)EditorGUILayout.ObjectField("Character Sprite",
                    _dialogueNode.CharacterSprite, typeof(Sprite), false);
                EditorGUIUtility.labelWidth = prevWidth;
                
                
                // prevWidth = EditorGUIUtility.labelWidth;
                // EditorGUILayout.PrefixLabel("Character Name");
                // _dialogueNode.CharacterName = EditorGUILayout.TextField(_dialogueNode.CharacterName);
                // EditorGUIUtility.labelWidth = prevWidth;
                
               
                EditorGUIUtility.labelWidth = 150;
                NodeEditorGUILayout.PropertyField( serializedObject.FindProperty("TextMeshReference"));
                EditorGUIUtility.labelWidth = prevWidth;
                
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("Speech"));
                
                
               
            }
            
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            
            // Check if there are any DialogueOptions, if is false then I draw the defaultOutput of the DialogueNode
            if (!_dialogueNode.DynamicOutputs.Any())
            {
                NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("defaultOutput"));
                return;
            }
            
            _showDialogueOptions = EditorGUILayout.BeginFoldoutHeaderGroup(_showDialogueOptions, "Dialogue Options");
            if (_showDialogueOptions)
            {
                foreach (var dialogueOption in _dialogueNode.DialogueOptions)
                {
                    EditorGUILayout.PrefixLabel(dialogueOption.Dialogue);
                    dialogueOption.Dialogue = EditorGUILayout.TextField(dialogueOption.Dialogue);
                    EditorGUILayout.TextField("Output Port: ",dialogueOption.PortName);
                    EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
                }
            }
            EditorGUILayout.EndFoldoutHeaderGroup();
            
            //Draws the Ports
            foreach (NodePort port in _dialogueNode.DynamicOutputs)
            {
                NodeEditorGUILayout.PortField(port);
            }
            
            
        }
    }
}