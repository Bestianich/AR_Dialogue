
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

[WaitForPlayerInput(true)]
[NodeWidth(300)]
public class DialogueNode : ANode
{
    [Input] public int DefaultInput;
    public Sprite CharacterSprite;
    public string CharacterName;
    public string TextMeshReference;
    [TextArea(5, 10)] public string Speech;
    public List<DialogueOption> DialogueOptions = new List<DialogueOption>();
    [Output] public int DefaultOutput;

    public override void Execute()
    {
        base.Execute();
        TextMeshProUGUI textMesh = _dialogueMemory.Get(TextMeshReference) as TextMeshProUGUI;
        if (textMesh == null)
        {
            Debug.LogWarning($"TextMesh: {TextMeshReference} reference not found!!");
            return;
        }

        textMesh.text = Speech;
    }
    

    public override void OnNextNode()
    {
        TextMeshProUGUI textMesh = _dialogueMemory.Get(TextMeshReference) as TextMeshProUGUI;
        if (textMesh == null)
        {
            Debug.LogWarning($"TextMesh: {TextMeshReference} reference not found!!");
            return;
        }
        textMesh.text = "";
        base.OnNextNode();
    }
    
}
