using UnityEngine;


[NodeWidth(150)]
public class ExitDialogueNode : ANode
{
    [Input] public int DefaultInput;
    [Output] public int DefaultOutput;

    public override void Execute()
    {
        base.Execute();
        _actor.OnExitInteraction();
    }
    
    
}
