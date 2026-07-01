using AR_Dialogue.Scripts.Runtime;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace AR_DialogueEditor
{
    [CustomPropertyDrawer(typeof(MemoryData))]
    public class MemoryDataDrawer : PropertyDrawer
    {
        private float _padding = 10f;
        private float _itemHeight = EditorGUIUtility.singleLineHeight * 2f;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            float nameWidth = position.width * 0.3f;
            float typeWidth = position.width * 0.3f;
            float valueWidth = position.width - nameWidth - typeWidth - _padding;

            Rect nameRect = new Rect(position.x, position.y, nameWidth, EditorGUIUtility.singleLineHeight);
            Rect typeRect = new Rect(nameRect.x + nameWidth, position.y, typeWidth, EditorGUIUtility.singleLineHeight);
            Rect valueRect = new Rect(typeRect.x + typeWidth, position.y, valueWidth, EditorGUIUtility.singleLineHeight);
            
            SerializedProperty name = property.FindPropertyRelative("Name");
            SerializedProperty type = property.FindPropertyRelative("DataType");
            SerializedProperty value = property.FindPropertyRelative("Value");
            
            name.stringValue = EditorGUI.TextField(nameRect, name.stringValue);
            EditorGUI.PropertyField(typeRect , type, GUIContent.none);
            
            Debug.Log(value);
            DataType dataType = (DataType)type.intValue;

            switch (dataType)
            {
                case DataType.INT:
                    if(value.objectReferenceValue == null)
                        value.intValue = 0;
                    value.intValue = EditorGUI.IntField(valueRect , value.intValue);
                    break;
                case DataType.FLOAT:
                    value.floatValue = EditorGUI.FloatField(valueRect, value.floatValue);
                    break;
                case DataType.STRING:
                    value.stringValue = EditorGUI.TextField(valueRect, value.stringValue);
                    break;
                case DataType.BOOLEAN:
                    value.boolValue = EditorGUI.Toggle(valueRect, value.boolValue);
                    break;
                case DataType.GAMEOBJECT:
                    value.objectReferenceValue = EditorGUI.ObjectField(valueRect, value.objectReferenceValue, typeof(UnityEngine.Object), false);
                    break;
                case DataType.SPRITE:
                    value.objectReferenceValue = EditorGUI.ObjectField(valueRect, value.objectReferenceValue, typeof(Sprite), false);
                    break;
                default:
                    break;
            }
            
            
        }
    }
}