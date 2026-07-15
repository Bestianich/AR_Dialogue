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
    private bool _waitForPlayerInput = false;
    
    private void Awake()
    {
        if (CurrentNode == null)
            CurrentNode =  Instantiate(_dialogueGraph.StartNode);
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
        
        var attribute = CurrentNode.GetType().GetCustomAttribute(typeof(HasDynamicPortsAttribute), false) as HasDynamicPortsAttribute;
        
        Debug.Log(CurrentNode + "  has been executed: " + CurrentNode.HasBeenExecuted);
        if (CurrentNode.HasBeenExecuted && attribute is not { HasDynamicPorts: true })
            NextNode("DefaultOutput");
            
        
        CurrentNode.Init(_dialogueMemory , this);
        CurrentNode.Execute();

        var waitForPlayerInputAttribute = CurrentNode.GetType().GetCustomAttribute(typeof(WaitForPlayerInputAttribute)) as WaitForPlayerInputAttribute;
        if (waitForPlayerInputAttribute != null)
            _waitForPlayerInput = waitForPlayerInputAttribute.WaitForPlayerInput;
        
        if (attribute is not { HasDynamicPorts: true } &&  !_waitForPlayerInput)
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

        CurrentNode = Instantiate(nodePort.Connection.node as ANode);
        
        var waitForPlayerInputAttribute = CurrentNode.GetType().GetCustomAttribute(typeof(WaitForPlayerInputAttribute)) as WaitForPlayerInputAttribute;
        if (waitForPlayerInputAttribute != null)
            _waitForPlayerInput = waitForPlayerInputAttribute.WaitForPlayerInput;

        if(!_waitForPlayerInput)
            Parse();

        
    }

    #region ARInteractable implementation

    public bool IsBeignInteracted { get; set; }

    public void OnEnterInteraction()
    {
        if(IsBeignInteracted) return;
        IsBeignInteracted = true;
        _mainCanvas.SetActive(true);
        Interact();
    }

    public void Interact()
    {
        if(!IsBeignInteracted)
            return;
        Parse();
    }

    public void OnExitInteraction()
    {
        _mainCanvas.SetActive(false);
        IsBeignInteracted = false;
    }
    #endregion
}