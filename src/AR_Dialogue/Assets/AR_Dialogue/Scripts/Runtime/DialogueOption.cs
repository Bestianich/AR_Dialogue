using System;
using UnityEngine;


[Serializable]
public class DialogueOption
{
    public string Dialogue;
    public string Option;

    public DialogueOption(string dialogue, string option)
    {
        Dialogue = dialogue;
        Option = option;
    }
}
