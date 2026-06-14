
//[HideInNodeEditor(true)]

using XNode;

[DisallowMultipleNodes(1)]
public class StartNode : ANode
{
    [Output] public int defaultOutput;
    
    public override object Execute()
    {
        return "Starting Node";
    }

    
}
