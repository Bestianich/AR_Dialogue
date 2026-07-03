using System.Collections.Generic;
using UnityEngine;
using XNode;



public class TestNode : Node
{
    [Input] public int Entry;
    public int TestInt = 0;
    public float TestFloat = 5f;
    public string TestString = "Hello World";
    public List<float> TestList = new List<float>();
    public Sprite TestSprite;
    public object NonSerializableField;
    [Output] public int Exit;
}
