
using UnityEngine;
using XNode;

public class DialogueNode : ANode
{
    [Input] public NodePort InputPort;
    [TextArea(10 , 10)]
    public string Speech;
    [Output] public NodePort OutputPort;
    
    [UsesTextField("Text")]
    public override object Execute()
    { 
        return Speech;
    }
    
    
}
