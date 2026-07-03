using AR_Dialogue.Scripts.Runtime;
using UnityEditor;
using UnityEngine;

namespace AR_DialogueEditor
{
    [CustomPropertyDrawer(typeof(DialogueMemory))]
    public class DialogueMemoryDrawer : PropertyDrawer
    {

        private GUIStyle _centeredStyle = new GUIStyle(EditorStyles.label) {
            alignment = TextAnchor.MiddleCenter,
            fontSize = 14,
            fontStyle = FontStyle.Bold
        };
        
        
        private float _itemHeight = EditorGUIUtility.singleLineHeight * 2f;
        private float _padding = 10f;
        private int _memoryDataCount = 0;


        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            return base.GetPropertyHeight(property, label) + 150 + (_itemHeight * _memoryDataCount);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            //base.OnGUI(position, property, label);
            EditorGUI.BeginProperty(position, label, property);
            Rect headerRect = new Rect(position.x, position.y + _itemHeight, position.width, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(headerRect, "DIALOGUE DATA", _centeredStyle);
            
            
            float nameWidth = position.width * 0.3f;
            float typeWidth = position.width * 0.3f;
            float valueWidth = position.width - nameWidth - typeWidth - _padding;
            
            
            Rect nameRect = new Rect(position.x, headerRect.y + _itemHeight, nameWidth, 10);
            Rect typeRect = new Rect(position.x + nameWidth, headerRect.y + _itemHeight, typeWidth, 10);
            Rect valueRect = new Rect(position.x + (nameWidth + typeWidth) + _padding, headerRect.y + _itemHeight, valueWidth, 10);
            
            EditorGUI.LabelField(nameRect, "NAME", EditorStyles.boldLabel);
            EditorGUI.LabelField(typeRect, "TYPE", EditorStyles.boldLabel);
            EditorGUI.LabelField(valueRect, "VALUE", EditorStyles.boldLabel);
            
            SerializedProperty memoryDatas = property.FindPropertyRelative("MemoryDatas");
            _memoryDataCount = memoryDatas.arraySize;
            
            for (int i = 0; i < _memoryDataCount; i++)
            {
                SerializedProperty memoryData = memoryDatas.GetArrayElementAtIndex(i);
                Rect memoryRect = new Rect(position.x , position.y + nameRect.y + _itemHeight * (i+1), position.width, 100);  
                EditorGUI.PropertyField(memoryRect, memoryData , GUIContent.none);
            }
            
            
            Rect addButtonRect = new Rect(position.x , headerRect.y + _itemHeight * (_memoryDataCount+1) + 50, position.width * 0.5f, 30);
            Rect removeButtonRect = new Rect(position.x + addButtonRect.width , addButtonRect.y, position.width * 0.5f, 30);
            
            //Add new memoryData to the list
            if(GUI.Button(addButtonRect, "ADD"))
            {
                memoryDatas.arraySize++;
                var memoryProperty = memoryDatas.GetArrayElementAtIndex(memoryDatas.arraySize - 1);
                
                var  name = memoryProperty.FindPropertyRelative("Name");
                name.stringValue = "newData#" + memoryDatas.arraySize;
                name.serializedObject.ApplyModifiedProperties();
                name.serializedObject.Update();
            }
            //Remove memoryData to the list
            if (GUI.Button(removeButtonRect, "REMOVE"))
            {
                memoryDatas.DeleteArrayElementAtIndex(memoryDatas.arraySize - 1);
            }
            
            EditorGUI.EndProperty();
        }

        
    }
}
