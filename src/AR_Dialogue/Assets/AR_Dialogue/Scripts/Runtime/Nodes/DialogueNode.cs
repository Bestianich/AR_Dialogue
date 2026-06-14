
using System.Collections.Generic;
using UnityEngine;
using XNode;

[NodeWidth(300)]
public class DialogueNode : ANode
{
    [Input] public int defaultInput;
    public Sprite CharacterSprite;
    public string CharacterName;
    [TextArea(5 , 10)]
    public string Speech;
    public List<DialogueOption> DialogueOptions = new List<DialogueOption>();
    [Output] public int defaultOutput;
    
    [UsesTextField("Text")]
    public override object Execute()
    { 
        return Speech;
    }
    
    
}
