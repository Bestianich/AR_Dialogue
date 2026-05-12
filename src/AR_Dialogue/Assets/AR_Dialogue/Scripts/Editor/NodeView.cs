using UnityEngine;
using UnityEngine.UIElements;


    public class NodeView 
    {
        public ANode NodeReference;
        public Rect rect;
        public string title;
        
        public GUIStyle style;

        public bool isDragged;

        public NodeView(ANode nodeReference, float width, float height, GUIStyle style , string title)
        {
            this.NodeReference = nodeReference;
            this.rect = new Rect(NodeReference.posInGraph.x, NodeReference.posInGraph.y, width, height);
            this.style = style;
            this.title = title;
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
            NodeReference.posInGraph = rect.position;
        }

        public void Draw()
        {
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
                        Drag(e.delta);
                        e.Use();
                        return true;
                    }
                    break;
            }
           
            
            return false;
        }
        
    }