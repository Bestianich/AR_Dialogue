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
            NextNode("OutputPort");
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
            NextNode("OutputPort");
        }

        public void NextNode(string outPortField)
        {
            //Check if exist OutPort
            if(!_currentNode.HasPort(outPortField))
            {
                Debug.LogError(outPortField + " is not a valid port");
                return;
            }
            
            var port =  _currentNode.GetPort(outPortField);

            if (!port.IsConnected)
            {
                Debug.LogWarning("Outport port is not connected to anything. Is the dialogue graph ended?");
                return;
            }
            
            _currentNode = port.Connection.node as ANode;
            
        }
}