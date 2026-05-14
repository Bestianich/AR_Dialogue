

using System;
using UnityEngine;

public class PortView
{
    public Rect Rect;
    
    public NodeView NodeView;
    public PortType Type;
    
    public GUIStyle Style;


    public PortView(NodeView nodeView, PortType type, GUIStyle style)
    {
        NodeView = nodeView;
        Type = type;
        Style = style;
        Rect = new Rect(0 , 0 , 10f, 20f);
    }
    
    public void Draw()
    {
        Rect.y = NodeView.rect.y + NodeView.rect.height * 0.5f - Rect.height * 0.5f;

        switch (Type)
        {
            case PortType.IN:
                Rect.x = NodeView.rect.x - Rect.width + 8f;
                break;
            case PortType.OUT:
                Rect.x = NodeView.rect.x + Rect.width - 8f;
                break;
        }

        if (GUI.Button(Rect, "", Style))
        {
            
        }
    }
}
