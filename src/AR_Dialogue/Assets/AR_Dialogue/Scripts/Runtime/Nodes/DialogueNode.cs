
using UnityEngine;
using XNode;

[UsesTextField]
public class DialogueNode : ANode
{
    [Input] public NodePort InputPort;
    [TextArea(10 , 10)]
    public string Speech;
    [Output] public NodePort OutputPort;
    
    public override object Execute()
    { 
        return Speech;
    }
    
    
}
