using System;
using System.Collections.Generic;
using System.Linq;
using AR_DialogueEditor;
using NUnit.Framework.Interfaces;
using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Graphs;
using UnityEngine;

public class DialogueGraphWindow : EditorWindow
{
    
    private DialogueGraph graph;
    private List<NodeView> nodesView;
    private GUIStyle nodeStyle;

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
        return w;
    }


    private void OnEnable()
    {
        nodeStyle = new GUIStyle();
        nodeStyle.normal.background = Texture2D.whiteTexture;
        nodeStyle.border = new RectOffset(12, 12, 12, 12);
    }
    private void OnGUI()
    {
        DrawNodes();
        ProcessEvents(Event.current);
        if(GUI.changed) Repaint();
    }

    private void DrawNodes()
    {
        // if (nodesView == null && graph.Nodes != null)
        // {
        //     nodesView = new List<NodeView>();
        //     foreach (var node in graph.Nodes)
        //     {
        //         nodesView.Add(new NodeView(node.posInGraph , 200 , 50 , nodeStyle));
        //     }
        // }
        // else
        //     return;
        if(nodesView == null)
            return;
        foreach (var nodeView in nodesView)
        {
            nodeView.Draw();
        }
    }

    private void ProcessEvents(Event e)
    {
        switch (e.type)
        {
            case EventType.MouseDown:
                if (e.button == 1) 
                    ProcessContextMenu(e.mousePosition);
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

    private void OnClickAddNode(Vector2 pos , Type nodeType)
    {
        Debug.Log("ADDING NODE VIEW TO GRAPH");
        if (graph.Nodes == null)
            graph.Nodes = new List<ANode>();
        
        if(nodesView == null)
            nodesView = new List<NodeView>();
        
        UnityEngine.Object obj = ScriptableObject.CreateInstance(nodeType);
        var nodeToAdd = obj as ANode;

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
        nodesView.Add(new NodeView(pos , 200 , 50 , nodeStyle , nodeToAdd.name));
        EditorUtility.SetDirty(graph);
        Debug.Log(nodeToAdd.name);
    }
    

}
