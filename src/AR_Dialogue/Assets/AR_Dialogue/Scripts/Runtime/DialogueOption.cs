using System;
using UnityEngine;
using UnityEngine.Serialization;


[Serializable]
public class DialogueOption
{
    public string Dialogue;
    public string PortName;

    public DialogueOption(string dialogue, string portName)
    {
        Dialogue = dialogue;
        PortName = portName;
    }
    
}
