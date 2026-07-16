
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
    public string TextMeshReference;
    [FormerlySerializedAs("Speech")] [TextArea(5, 10)] public string Text;
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

        textMesh.text = Text;
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
