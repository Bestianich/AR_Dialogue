using System;
using System.Reflection;
using TMPro;
using UnityEngine;
using XNode;

public class NodeParser : MonoBehaviour
{
        [SerializeField] private DialogueGraph _graph;
        [SerializeField] private TextMeshProUGUI _textField;
        
        public ANode _currentNode;

        private void Awake()
        {
            if (_currentNode == null)
                _currentNode = _graph.StartNode;
            NextNode("defaultOutput");
        }
        
        [ContextMenu("Parse")]
        public void Parse()
        {
            Debug.Log("Node to execute: "  + _currentNode.ToString());
            var method = _currentNode.GetType().GetMethod("Execute");
            if (method == null)
            {
                Debug.LogError($"Method execute not found");
                return;
            }
            
            var attributes = method.GetCustomAttributes();
            foreach (var attribute in attributes)
            {
                if (attribute is UsesTextFieldAttribute)
                {
                    //var usesTextField = attribute as UsesTextFieldAttribute;
                    Debug.Log($"UsesTextFieldAttribute: {attribute}");
                    _textField.text = (string)_currentNode.Execute();
                }
            }
            NextNode("prova");
        }

        public void NextNode(string outPortField)
        {
            NodePort nodePort = null;
            
            
            //First I check the dynamicOutputPorts
            foreach (var dynamicPort in _currentNode.DynamicOutputs)
            {
                if (dynamicPort.fieldName == outPortField)
                {
                    nodePort = dynamicPort;
                }
            }

            //If there are not any then i check the default outputPort
            if (nodePort == null)
                nodePort = _currentNode.GetPort("defaultOutput");

            if (nodePort == null)
            {
                Debug.LogError($"Port {outPortField} not found");
                return;
            }
            
            if (!nodePort.IsConnected)
            {
                Debug.LogWarning("Outport port is not connected to anything. Is the dialogue graph ended?");
                return;
            }
            
            _currentNode = nodePort.Connection.node as ANode;
            
        }
}