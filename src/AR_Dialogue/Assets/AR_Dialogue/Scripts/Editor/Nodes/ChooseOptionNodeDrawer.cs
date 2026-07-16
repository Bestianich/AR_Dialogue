using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;
using XNode;
using XNodeEditor;

[CustomNodeEditor(typeof(ChooseOptionNode))]
public class ChooseOptionNodeDrawer : NodeEditor
{
    private ChooseOptionNode _chooseNode;
    
    private string _newDialogueOption = "";
    private string _newDialogueOptionOutput = "";
    private int _currentTab = 0;
    private int _portToDelete = 0;
    
    private bool _showCreateDelete = false;
    private bool _showDialogueOptions = false;


    public override void OnBodyGUI()
    {
        if (_chooseNode == null)
            _chooseNode = target as ChooseOptionNode;
        serializedObject.Update();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("DefaultInput"));


        //Node Settings Part
        _showCreateDelete =
            EditorGUILayout.BeginFoldoutHeaderGroup(_showCreateDelete, "Create / Remove Dialogue Options");
        if (_showCreateDelete)
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
                        bool hasSamePort = false;

                        foreach (var port in _chooseNode.DialogueOptions)
                        {
                            if (port.PortName == _newDialogueOptionOutput)
                            {
                                hasSamePort = true;
                                break;
                            }
                        }

                        if (noDialogue)
                        {
                            EditorUtility.DisplayDialog("Error on creating a new Dialogue Option",
                                "Please insert a valid dialogue.", "OK");
                            return;
                        }

                        if (noPort)
                        {
                            EditorUtility.DisplayDialog("Error on creating a new Dialogue Option",
                                "Please insert a valid Output Port.", "OK");
                            return;
                        }

                        if (hasSamePort)
                        {
                            EditorUtility.DisplayDialog("Errore on creating a new Dialogue Option",
                                "Please insert a different output port.", "OK");
                            return;
                        }

                        _chooseNode.AddDynamicOutput(typeof(int), Node.ConnectionType.Multiple,
                            Node.TypeConstraint.None,
                            _newDialogueOptionOutput);
                        _chooseNode.DialogueOptions.Add(
                            new DialogueOption(_newDialogueOption, _newDialogueOptionOutput));
                    }

                    break;
                //Delete Dialogue Option
                case 1:
                    if (_chooseNode.DialogueOptions.Count == 0)
                    {
                        EditorGUILayout.HelpBox("No dialogue options found.", MessageType.Info);
                        break;
                    }

                    EditorGUILayout.PrefixLabel("Choose Port");
                    List<string> outputs = new List<string>();
                    foreach (NodePort port in _chooseNode.DynamicOutputs)
                    {
                        outputs.Add(port.fieldName);
                    }

                    _portToDelete = EditorGUILayout.Popup(_portToDelete, outputs.ToArray());
                    if (GUILayout.Button("Delete Port"))
                    {
                        _chooseNode.DialogueOptions.RemoveAt(_portToDelete);
                        _chooseNode.RemoveDynamicPort(_chooseNode.DynamicOutputs.ElementAt(_portToDelete));
                    }

                    break;
                default:
                    break;
            }

        }

        EditorGUILayout.EndFoldoutHeaderGroup();

        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("ButtonPrefab"));
        NodeEditorGUILayout.PropertyField(serializedObject.FindProperty("RectTransformReference"));

        _showDialogueOptions = EditorGUILayout.BeginFoldoutHeaderGroup(_showDialogueOptions, "Dialogue Options");
        if (_showDialogueOptions)
        {
            foreach (var dialogueOption in _chooseNode.DialogueOptions)
            {
                EditorGUILayout.PrefixLabel("Option Text");
                dialogueOption.Text = EditorGUILayout.TextField(dialogueOption.Text);
                EditorGUILayout.TextField("Output Port: ", dialogueOption.PortName);
                EditorGUILayout.LabelField("", GUI.skin.horizontalSlider);
            } 
        }

        EditorGUILayout.EndFoldoutHeaderGroup();
        //Draws the Ports
        foreach (NodePort port in _chooseNode.DynamicOutputs)
        {
            NodeEditorGUILayout.PortField(port);
        }
    }

}

