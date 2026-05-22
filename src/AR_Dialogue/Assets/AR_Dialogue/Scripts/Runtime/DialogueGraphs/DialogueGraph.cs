using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XNode;

[CreateAssetMenu(fileName = "newDialogueGraph", menuName = "AR_Dialogue/New Dialogue Graph")]
public class DialogueGraph : NodeGraph
{
    public StartNode StartNode;


    //Check to set the StartNode of the DialogueGraph
    public override Node AddNode(Type type)
    {
        if (type == typeof(StartNode))
        {
            StartNode = base.AddNode(type) as StartNode;
            return StartNode;
        }
        return base.AddNode(type);
    }
    
}