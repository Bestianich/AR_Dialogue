using System;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class DialogueOption : ANode
{
    public string Dialogue;
    public string PortName;

    public DialogueOption(string dialogue, string portName)
    {
        Dialogue = dialogue;
        PortName = portName;
    }

    public override object Execute()
    {
        throw new NotImplementedException();
    }
}
