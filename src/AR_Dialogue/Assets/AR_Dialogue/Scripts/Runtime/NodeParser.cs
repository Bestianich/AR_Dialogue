using UnityEngine;
using XNode;

public class NodeParser : MonoBehaviour
{
        [SerializeField] private DialogueGraph _graph;

        private ANode _currentNode;
        
        public void Parse()
        { 
            if (_currentNode == null) 
                _currentNode = _graph.StartNode;
            
            NextNode("OutPort");
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
            var nextNode = port.Connection.node as ANode;
            _currentNode = nextNode;
            
        }
}