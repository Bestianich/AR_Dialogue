
//[HideInNodeEditor(true)]

using XNode;

[DisallowMultipleNodes(1)]
public class StartNode : ANode
{
    [Output] public NodePort OutPort;
    
    public override object Execute()
    {
        return "Starting Node";
    }

    
}
