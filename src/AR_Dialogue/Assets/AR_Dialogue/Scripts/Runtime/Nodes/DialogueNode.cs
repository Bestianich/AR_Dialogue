
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using XNode;

[NodeWidth(300)]
public class DialogueNode : ANode
{
    [Input] public int defaultInput;
    public Sprite CharacterSprite;
    public string CharacterName;
    public string TextMeshReference;
    [TextArea(5 , 10)]
    public string Speech;
    public List<DialogueOption> DialogueOptions = new List<DialogueOption>();
    [Output] public int defaultOutput;
    
    public override object Execute()
    {
        TextMeshProUGUI textMesh = _dialogueMemory.Get(TextMeshReference) as TextMeshProUGUI;
        if (textMesh == null)
        {
            Debug.LogWarning("TextMesh Reference Not Found!!");
            return null;
        }
        
        textMesh.text = Speech;
        return null;
    }
    
    
}
