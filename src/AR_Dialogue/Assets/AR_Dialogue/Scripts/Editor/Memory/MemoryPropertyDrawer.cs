using AR_Dialogue.Scripts.Runtime;
using UnityEditor;
using UnityEngine;

namespace AR_DialogueEditor
{
    [CustomPropertyDrawer(typeof(DialogueMemory))]
    public class MemoryPropertyDrawer : PropertyDrawer
    {

        private GUIStyle _centeredStyle = new GUIStyle(EditorStyles.label) {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 14,
            fontStyle = FontStyle.Bold
        };
        
        
        private float _itemHeight = EditorGUIUtility.singleLineHeight * 2f;
        private float _padding = 10f;
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);
            
            Rect headerRect = new Rect(position.x, position.y + _itemHeight, position.width, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(headerRect, "MEMORY DATA", _centeredStyle);

            
            float nameWidth = position.width * 0.3f;
            float valueWidth = position.width - nameWidth - _padding;
            
            
            Rect nameRect = new Rect(position.x, headerRect.y + _itemHeight, nameWidth, position.height);
            Rect valueRect = new Rect(position.x + nameWidth + _padding, headerRect.y + _itemHeight, valueWidth, position.height);
            
            EditorGUI.LabelField(nameRect, "NAME", EditorStyles.boldLabel);
            EditorGUI.LabelField(valueRect, "VALUE", EditorStyles.boldLabel);
            
            SerializedProperty memoryDatas = property.FindPropertyRelative("MemoryDatas");
            int count = memoryDatas.arraySize;

            for (int i = 0; i < count; i++)
            {
                SerializedProperty memoryData = memoryDatas.GetArrayElementAtIndex(i);
                SerializedProperty name = memoryData.FindPropertyRelative("Name");
                nameRect.y += _itemHeight;
                name.stringValue = EditorGUI.TextField(nameRect, name.stringValue);
            }
            
            float buttonY = count * _itemHeight + headerRect.y + 100;
            
            Rect addRect = new Rect(position.x + _padding, buttonY, 80, 20);
            Rect removeRect = new Rect(position.x + addRect.width + _padding, buttonY, 80, 20);

            if (GUI.Button(addRect, "ADD", EditorStyles.miniButtonLeft))
            {
                memoryDatas.arraySize++;
            }

            if (GUI.Button(removeRect, "REMOVE", EditorStyles.miniButtonRight))
            {
                memoryDatas.DeleteArrayElementAtIndex(memoryDatas.arraySize - 1);
                Debug.Log("CIAO");
            }
            EditorGUI.EndProperty();
        }

        
    }
}
