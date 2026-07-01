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
        
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            base.OnGUI(position, property, label);
            EditorGUI.BeginProperty(position, label, property);
            
            Rect headerRect = new Rect(position.x, position.y + _itemHeight, position.width, EditorGUIUtility.singleLineHeight);
            
            EditorGUI.LabelField(headerRect, "MEMORY DATA", _centeredStyle);
            
            
            float nameWidth = position.width * 0.3f;
            float typeWidth = position.width * 0.3f;
            float valueWidth = position.width - nameWidth - typeWidth - _padding;
            
            
            Rect nameRect = new Rect(position.x, headerRect.y + _itemHeight, nameWidth, position.height);
            Rect typeRect = new Rect(position.x + nameWidth, headerRect.y + _itemHeight, typeWidth, position.height);
            Rect valueRect = new Rect(position.x + (nameWidth + typeWidth) + _padding, headerRect.y + _itemHeight, valueWidth, position.height);
            
            EditorGUI.LabelField(nameRect, "NAME", EditorStyles.boldLabel);
            EditorGUI.LabelField(typeRect, "TYPE", EditorStyles.boldLabel);
            EditorGUI.LabelField(valueRect, "VALUE", EditorStyles.boldLabel);
            
            
            SerializedProperty data = property.FindPropertyRelative("memoryData");
            EditorGUILayout.PropertyField(data, false); 
            
        }

        
    }
}
