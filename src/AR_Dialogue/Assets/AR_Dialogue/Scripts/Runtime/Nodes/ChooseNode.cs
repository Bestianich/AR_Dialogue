


using System.Collections.Generic;

public class ChooseNode : ANode
{
    [Input] public int DefaultInput;
    public List<DialogueOption> DialogueOptions = new List<DialogueOption>();
    [Output] public int DefaultOutput;
    
    public override void Execute()
    {
        throw new System.NotImplementedException();
    }
}