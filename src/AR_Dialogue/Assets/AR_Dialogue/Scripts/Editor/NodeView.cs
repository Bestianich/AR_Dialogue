using UnityEngine;
using UnityEngine.UIElements;

namespace AR_DialogueEditor
{
    public class NodeView 
    {
        public Rect rect;
        public string title;
        
        public GUIStyle style;

        public NodeView(Vector2 position, float width, float height, GUIStyle style , string title)
        {
            this.rect = new Rect(position.x, position.y, width, height);
            this.style = style;
            this.title = title;
        }

        public void Drag(Vector2 delta)
        {
            rect.position += delta;
        }

        public void Draw()
        {
            GUI.Box(rect, title, style);
        }

        public bool ProcessEvents(Event e)
        {

           
            
            return false;
        }
        
    }
}