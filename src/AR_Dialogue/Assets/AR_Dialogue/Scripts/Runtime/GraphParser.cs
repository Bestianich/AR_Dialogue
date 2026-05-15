using UnityEngine;


public class GraphParser : MonoBehaviour
{
    [SerializeField] private DialogueGraph _graph;
    private ANode _currentNode;
    
    public void Parse()
    {
        if (_currentNode == null)
        {
            if (_graph.StartNode == null)
            {
                Debug.LogWarning("Graph doesnt have a StartNode");
                return;
            }
            _currentNode = _graph.StartNode;
        }
        _currentNode.Execute();
        _currentNode = _currentNode.GetNextNode();
    }
}
