using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

public class Actor : MonoBehaviour
{

    [SerializeField] private DialogueGraph _dialogueGraph;
    public ANode CurrentNode;
    [SerializeField] private DialogueMemory _dialogueMemory;


    private void Awake()
    {
        foreach (MemoryData memory in _dialogueMemory.MemoryDatas)
        {
            Debug.Log(memory.Name);
        }

        if (CurrentNode == null)
            CurrentNode = _dialogueGraph.StartNode;
        NextNode("DefaultOutput");
    }

    [ContextMenu("Parse")]
    public void Parse()
    {
        Debug.Log("Node to execute: " + CurrentNode.ToString());
        var method = CurrentNode.GetType().GetMethod("Execute");
        if (method == null)
        {
            Debug.LogError($"Method execute not found");
            return;
        }

        CurrentNode.Init(_dialogueMemory , this);
        CurrentNode.Execute();

        var attribute = CurrentNode.GetType().GetCustomAttribute(typeof(HasDynamicPortsAttribute), false) as HasDynamicPortsAttribute;
        Debug.Log(attribute);
        if (attribute == null)
            NextNode("DefaultOutput");
    }

    public void NextNode(string outPortField)
    {
        CurrentNode.OnNextNode();
        NodePort nodePort = null;


        //First I check the dynamicOutputPorts
        foreach (var dynamicPort in CurrentNode.DynamicOutputs)
        {
            if (dynamicPort.fieldName == outPortField)
            {
                nodePort = dynamicPort;
            }
        }

        //If there are not any then i check the default outputPort
        if (nodePort == null)
            nodePort = CurrentNode.GetPort("DefaultOutput");

        if (nodePort == null)
        {
            Debug.LogError($"Port {outPortField} not found");
            return;
        }

        if (!nodePort.IsConnected)
        {
            Debug.LogWarning("Outport port is not connected to anything. Is the dialogue graph ended?");
            return;
        }

        CurrentNode = nodePort.Connection.node as ANode;

    }


}