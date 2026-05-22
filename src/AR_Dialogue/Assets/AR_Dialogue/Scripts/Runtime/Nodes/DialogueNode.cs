
using UnityEngine;
using XNode;

public class DialogueNode : ANode<string>
{
    [Input] public NodePort InputPort;
    [TextArea(10 , 10)]
    public string Speech;
    [Output] public NodePort OutputPort;
    
    public override string Execute()
    {
        return Speech;
    }
    
    
}
