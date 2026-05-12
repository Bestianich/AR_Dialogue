using UnityEditor;
using UnityEditor.Callbacks;
using UnityEditor.Graphs;
using UnityEngine;

public class DialogueGraphWindow : EditorWindow
{
    
    private DialogueGraph graph;
    

    private void OnGUI()
    {
        
    }

    private void DrawNodes()
    {
        
    }

    private void ProcessEvents(Event e)
    {
        
    }

    
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
    }
