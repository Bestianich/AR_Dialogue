


using System.Collections.Generic;
using UnityEngine;

[HasDynamicPorts(true)]
[NodeWidth(300)]
public class ChooseOptionNode : ANode
{
    [Input] public int DefaultInput;
    public DialogueOptionButton ButtonPrefab;
    public string RectTransformReference;
    public List<DialogueOption> DialogueOptions = new List<DialogueOption>();
    private List<DialogueOptionButton> _buttonInstances = new List<DialogueOptionButton>();

    public override void Execute()
    {
        var rect = _dialogueMemory.Get(RectTransformReference) as RectTransform;
        if (rect == null)
        {
            Debug.LogError($"RectTransform: {RectTransformReference} reference not found!!");
            return;
        }

        foreach (var option in DialogueOptions)
        {
            var button = Instantiate(ButtonPrefab , rect.transform);
            button.Init(option.Text, option.PortName, _actor);
            _buttonInstances.Add(button);
        }
    }

    public override void OnNextNode()
    {
        var rect = _dialogueMemory.Get(RectTransformReference) as RectTransform;
        if (rect == null)
        {
            Debug.LogError($"RectTransform: {RectTransformReference} reference not found!!");
            return;
        }
        for (int i = rect.childCount - 1; i >= 0; i--)
        {
            Destroy(rect.GetChild(i).gameObject);
        }
    }
}