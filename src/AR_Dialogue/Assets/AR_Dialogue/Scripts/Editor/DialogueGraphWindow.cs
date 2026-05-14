using System;
using System.Collections.Generic;
using System.Linq;
using AR_DialogueEditor;
using NUnit.Framework.Interfaces;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Graphs;
using UnityEngine;
using Object = UnityEngine.Object;

public class DialogueGraphWindow : EditorWindow
{
    
    private DialogueGraph graph;
    private List<NodeView> nodesView;
    
    private GUIStyle nodeStyle;
    private GUIStyle inPortStyle;
    private GUIStyle outPortStyle;

    private NodeView selectedNodeView;
    
    private PortView selectedInPortView;
    private PortView selectedOutPortView;
    
    public static Action<NodeView> OnNodeSelected;

    [OnOpenAsset]
    //Function for when you open an DialogueGraph to show its window 
    public static bool OnOpenAsset(int instanceID, int line)
    {
        var graph = EditorUtility.InstanceIDToObject(instanceID) as DialogueGraph;
        if(graph == null) return false;
        Open(graph);
        return true;    
    }

    public static DialogueGraphWindow Open(DialogueGraph graph)
    {
        if(graph == null) return null;
        DialogueGraphWindow w = GetWindow<DialogueGraphWindow>(graph.name , true);
        w.graph = graph;
        if (w.nodesView == null)
            w.nodesView = new List<NodeView>();
        if(w.graph.Nodes == null) graph.Nodes = new List<ANode>();
        foreach (var node in graph.Nodes)
        {
            w.nodesView.Add(new NodeView(node , 200 , 50 , w.nodeStyle , w.inPortStyle , w.outPortStyle, node.GetName()));
        }
        return w;
    }


    private void OnEnable()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = Texture2D.whiteTexture;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);

        inPortStyle = new GUIStyle();
        inPortStyle.normal.background = Texture2D.redTexture;
        inPortStyle.active.background = Texture2D.redTexture;
        inPortStyle.border = new RectOffset(4, 4, 12, 12);
        
        outPortStyle = new GUIStyle();
        outPortStyle.normal.background = Texture2D.blackTexture;
        outPortStyle.active.background = Texture2D.blackTexture;
        outPortStyle.border = new RectOffset(4, 4, 12, 12);

        OnNodeSelected += OnSelectNodeView;
    }

    private void OnDisable()
    {
        OnNodeSelected -= OnSelectNodeView;
    }
    
    private void OnGUI()
    {
        DrawNodes();
        ProcessNodeEvents(Event.current);
        ProcessGraphEvents(Event.current);
        if(GUI.changed) Repaint();
    }

    private void DrawNodes()
    {
        if (nodesView == null)
            return;
        foreach (var nodeView in nodesView)
        {
            nodeView.Draw();
        }
    }

    private void ProcessNodeEvents(Event e)
    {
        if (nodesView == null)
            return;
        foreach (var nodeView in nodesView)
        {
            bool guiChanged = nodeView.ProcessEvents(e);
            if(guiChanged)
                GUI.changed = true;
        }
    }

    private void ProcessGraphEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 1) 
                    ProcessContextMenu(e.mousePosition);
                break;
            case EventType.KeyDown:
                if (e.keyCode == KeyCode.Delete)
                {
                    Debug.Log("DESSTRO");
                    DeleteNodeView(selectedNodeView);
                }
                break;
        }
        
    }

    private void ProcessContextMenu(Vector2 mousePosition)
    {
        GenericMenu menu = new GenericMenu();
        var nodeTypes = ARDialogueUtilities.GetAllTypeOfANodes();
        if (!nodeTypes.Any())
        {
            menu.AddDisabledItem(new GUIContent("No Nodes Found"));
            menu.ShowAsContext();
            return;
        }

        foreach (var type in nodeTypes)
        {
            if(type == null)
                continue;
            menu.AddItem(new GUIContent(type.Name), false , () => OnClickAddNode(mousePosition , type));
        }
        menu.AddSeparator("");
        menu.AddDisabledItem(new GUIContent("Select Node To ADD"));
        //menu.AddItem(new GUIContent("Add Node") , false , () => OnClickAddNode(mousePosition , AssetDatabase.CreateAsset(ANode , )));
        menu.ShowAsContext();
    }
    
    
    private void OnSelectNodeView(NodeView nodeView)
    {
        selectedNodeView = nodeView;
        Debug.Log(selectedNodeView);
    }
    private void DeleteNodeView(NodeView nodeView)
    {
        if (nodeView == null)
        {
            Debug.LogError("NodeView to delete is null");
            return;
        }
        graph.Nodes.Remove(nodeView.NodeReference);
        nodesView.Remove(nodeView);
        AssetDatabase.RemoveObjectFromAsset(nodeView.NodeReference);
        Repaint();
    }

 
    // Function to add a new Node to the graph
    private void OnClickAddNode(Vector2 pos , Type nodeType)
    {
        Debug.Log("ADDING NODE VIEW TO GRAPH");
        if (graph.Nodes == null)
            graph.Nodes = new List<ANode>();
        
        if(nodesView == null)
            nodesView = new List<NodeView>();
        
        //UnityEngine.Object obj = ScriptableObject.CreateInstance(nodeType);
        var nodeToAdd = Activator.CreateInstance(nodeType) as ANode;

        AssetDatabase.AddObjectToAsset(nodeToAdd , graph);
        
        if (nodeToAdd == null)
        {
            Debug.LogError($"Cannot create node of type {nodeType}");
            return;
        }
        
        nodeToAdd.name = nodeType.Name + "#" + graph.Nodes.Count;
        
        nodeToAdd.posInGraph = pos;
        //AssetDatabase.AddObjectToAsset(nodeToAdd, graph.Nodes[0]);
        //CreateInstance(nodeToAdd);
        graph.Nodes.Add(nodeToAdd);
        Debug.Log(nodeToAdd);
        nodesView.Add(new NodeView(nodeToAdd , 200 , 50 , nodeStyle , inPortStyle , outPortStyle, nodeToAdd.GetName()));
        EditorUtility.SetDirty(graph);
        Debug.Log(nodeToAdd.name);
    }
    

}
