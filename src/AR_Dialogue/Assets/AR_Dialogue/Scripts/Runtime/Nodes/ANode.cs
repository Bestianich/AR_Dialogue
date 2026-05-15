using System;
using UnityEngine;

[CreateAssetMenu(fileName = "New Node", menuName = "New Node")]
public abstract class ANode : ScriptableObject
{
    public Vector2 posInGraph;
    public Port InputPort;
    public Port OutputPort;
    public void Init()
    {
        InputPort = new Port(PortType.IN, this, null);
        OutputPort = new Port(PortType.OUT, this, null);
    }

    public ANode GetNextNode()
    {
        return OutputPort.Connection?.OutPort.Content;
    }

    public ANode GetPreviousNode()
    {
        return InputPort.Connection?.InPort.Content;
    }

    public abstract void Execute();

    public virtual string GetName()
    {
        return "ANode";
    }
    
    
}

