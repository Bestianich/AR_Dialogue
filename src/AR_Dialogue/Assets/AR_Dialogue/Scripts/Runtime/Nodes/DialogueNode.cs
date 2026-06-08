
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(300)]
public class DialogueNode : ANode
{
    [Input] public NodePort InputPort;
    public Sprite CharacterSprite;
    public string CharacterName;
    [TextArea(5 , 10)]
    public string Speech;
    [Output] public NodePort OutputPort;
    public List<DialogueOption> DialogueOptions = new List<DialogueOption>();
    
    [UsesTextField("Text")]
    public override object Execute()
    { 
        return Speech;
    }
    
    
}
