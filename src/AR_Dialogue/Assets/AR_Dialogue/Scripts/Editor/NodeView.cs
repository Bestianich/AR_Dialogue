using UnityEngine;
using UnityEngine.UIElements;


    public class NodeView 
    {
        public ANode NodeReference;
        public Rect rect;
        public string title;
        
        public GUIStyle style;

        public bool isDragged;
        
        public PortView InPortView;
        public PortView OutPortView;

        public NodeView(ANode nodeReference, float width, float height, GUIStyle style , GUIStyle InPortStyle, GUIStyle OutPortStyle , string title)
        {
            this.NodeReference = nodeReference;
            this.rect = new Rect(NodeReference.posInGraph.x, NodeReference.posInGraph.y, width, height);
            this.style = style;
            this.title = title;
            InPortView = new PortView(this, PortType.IN, InPortStyle);
            OutPortView = new PortView(this, PortType.OUT, OutPortStyle);
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
            NodeReference.posInGraph = rect.position;
        }

        public void Draw()
        {
            InPortView.Draw();
            OutPortView.Draw();
            GUI.Box(rect, title, style);
        }

        public bool ProcessEvents(Event e)
        {
            switch (e.type)
            {
                case EventType.MouseDown:
                    if (e.button == 0)
                    {
                        if (rect.Contains(e.mousePosition))
                        {
                            isDragged = true;
                            GUI.changed = true;
                        }
                        else
                            GUI.changed = true;
                    }
                    break;
                case EventType.MouseUp:
                    isDragged = false;
                    break;
                case EventType.MouseDrag:
                    if (e.button == 0 && isDragged)
                    {
                        DialogueGraphWindow.OnNodeSelected?.Invoke(this);
                        if (!isDragged) return true;
                        e.Use();
                        Drag(e.delta);
                        return true;
                    }
                    break;
            }
           
            
            return false;
        }
        
    }