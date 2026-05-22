
public class StartNode : ANode<string>
{
    [Output] public int OutPort;
    
    public override string Execute()
    {
        return "Starting Node";
    }
}
