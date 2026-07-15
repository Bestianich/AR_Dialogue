

using UnityEngine;
using XNode;

[WaitForPlayerInput(false)]
[DisallowMultipleNodes(1)]
public class StartNode : ANode
{
    [Output] public int DefaultOutput;
    public override void Execute()
    {
        base.Execute();
        Debug.Log("NodeGraph Started");
    }

    
}
