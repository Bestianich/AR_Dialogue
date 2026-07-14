using System;
using UnityEngine;
using UnityEngine.Serialization;

[Serializable]
public class DialogueOption
{
    [FormerlySerializedAs("Dialogue")] public string Text;
    public string PortName;

    public DialogueOption(string text, string portName)
    {
        Text = text;
        PortName = portName;
    }
    
}
