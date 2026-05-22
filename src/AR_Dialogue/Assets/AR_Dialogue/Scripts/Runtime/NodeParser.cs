using UnityEngine;
using XNode;

public class NodeParser : MonoBehaviour
{
        [SerializeField] private DialogueGraph _graph;

        public ANode _currentNode;

        private void Awake()
        {
            if (_currentNode == null)
                _currentNode = _graph.StartNode;
        }
        
        [ContextMenu("Parse")]
        public void Parse()
        { 
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
            Debug.Log(port.fieldName);

            if (!port.IsConnected)
            {
                Debug.LogWarning("Outport port is not connected to anything. Is the dialogue graph ended?");
                return;
            }
            
            _currentNode = port.Connection.node as ANode;
            
            
            // var nextNode = port..node as ANode;
            // _currentNode = nextNode;
            
        }
}