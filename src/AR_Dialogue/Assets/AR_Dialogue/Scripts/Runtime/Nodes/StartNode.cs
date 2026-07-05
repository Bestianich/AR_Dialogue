

using UnityEngine;
using XNode;

[DisallowMultipleNodes(1)]
public class StartNode : ANode
{
    [Output] public int DefaultOutput;
    public override void Execute()
    {
        Debug.Log("NodeGraph Started");
    }

    
}
