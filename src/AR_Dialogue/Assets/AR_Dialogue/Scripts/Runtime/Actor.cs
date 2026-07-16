using System.Reflection;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using XNode;

public class Actor : MonoBehaviour , ARInteractable
{

    [SerializeField] private DialogueGraph _dialogueGraph;
    public ANode CurrentNode;
    [SerializeField] private GameObject _mainCanvas;
    [SerializeField] private DialogueMemory _dialogueMemory;
    private bool _isDialougeEnded = false;
    
    private void Awake()
    {
        if (CurrentNode == null)
            CurrentNode =  Instantiate(_dialogueGraph.StartNode);
        IsWaitingForInput = false;
        Parse();
        
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
        
        var requireInput = CurrentNode.GetType().GetCustomAttribute(typeof(WaitForPlayerInputAttribute)) as WaitForPlayerInputAttribute;
        if (requireInput is  { WaitForPlayerInput: true })
        {
            IsWaitingForInput = true;
            return;
        }
        
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
            _isDialougeEnded = true;
            return;
        }
        
        CurrentNode = Instantiate(nodePort.Connection.node as ANode);
        
        //var requireInput = CurrentNode.GetType().GetCustomAttribute(typeof(WaitForPlayerInputAttribute)) as WaitForPlayerInputAttribute;
        Parse();
        
        
    }

    #region ARInteractable implementation

    public bool IsWaitingForInput { get; set; }
    public bool IsBeignInteracted { get; set; }

    public void OnEnterInteraction()
    {
        if(IsBeignInteracted) return;
        if(_isDialougeEnded) return;
        IsBeignInteracted = true;
        _mainCanvas.SetActive(true);
        
    }

    public void Interact()
    {
        if(!IsBeignInteracted)
            return;
        if(!IsWaitingForInput) return;
        IsWaitingForInput = false;
        var checkDynamicPorts = CurrentNode.GetType().GetCustomAttribute(typeof(HasDynamicPortsAttribute)) as HasDynamicPortsAttribute;
        if (checkDynamicPorts is {HasDynamicPorts: true})
            return;
        
        NextNode("DefaultOutput");
    }

    public void OnExitInteraction()
    {
        _mainCanvas.SetActive(false);
        IsBeignInteracted = false;
    }
    #endregion
}