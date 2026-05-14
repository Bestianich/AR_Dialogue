using UnityEngine;

[CreateAssetMenu(fileName = "New Node", menuName = "New Node")]
public abstract class ANode : ScriptableObject
{
    public Vector2 posInGraph;

    public virtual string GetName()
    {
        return "ANode";
    }
    
    
}
