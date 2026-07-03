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
        
        private DataType _previousDataType;
        private DataType _currentDataType;

        private SerializedProperty _value;

        

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
            _value = property.FindPropertyRelative("Value");
            
            name.stringValue = EditorGUI.TextField(nameRect, name.stringValue);
            
            //Check for when I change the type of the memory data
            EditorGUI.BeginChangeCheck();
            _currentDataType = (DataType)EditorGUI.EnumPopup(typeRect,  (DataType)type.enumValueIndex);
            if (EditorGUI.EndChangeCheck())
            {
                if ((DataType)type.enumValueIndex != _currentDataType)
                {
                    Debug.Log("Cambiatooo");
                    type.intValue = (int)_currentDataType;
                    _currentDataType = (DataType)type.enumValueIndex;
                    type.serializedObject.ApplyModifiedProperties();
                    type.serializedObject.Update();
                    ResetValue(_currentDataType);
                }
            }
            
            

            DataType dataType = (DataType)type.enumValueIndex;
            if (_value == null)
            {
                ResetValue(_currentDataType);
            }
            
            switch (dataType)
            {
                case DataType.INT:
                    int intValue = (int)_value.managedReferenceValue;
                    int newInt = EditorGUI.IntField(valueRect , intValue);
                    if (newInt != intValue)
                    {
                        _value.managedReferenceValue = newInt;
                        _value.serializedObject.ApplyModifiedProperties();
                        _value.serializedObject.Update();
                        _value = property.FindPropertyRelative("Value");
                    }
                    break;
                case DataType.FLOAT:
                    
                    float currentFloat = (float)_value.managedReferenceValue;
                    float newFloat = EditorGUI.FloatField(valueRect, currentFloat);
                    if (newFloat != currentFloat)
                    {
                        _value.managedReferenceValue = newFloat;
                        _value.serializedObject.ApplyModifiedProperties();
                        _value.serializedObject.Update();
                        _value = property.FindPropertyRelative("Value");
                    }
                    break;
                case DataType.STRING:
                    string currentString = _value.managedReferenceValue as string;
                    string newString = EditorGUI.TextField(valueRect, currentString);
                    if (newString != currentString)
                    {
                        _value.managedReferenceValue = newString;
                        _value.serializedObject.ApplyModifiedProperties();
                        _value.serializedObject.Update();
                        _value = property.FindPropertyRelative("Value");
                    }
                    break;
                case DataType.BOOLEAN:
                    bool currentBool = (bool)_value.managedReferenceValue;
                    bool newBool = EditorGUI.Toggle(valueRect, currentBool);
                    if (newBool != currentBool)
                    {
                        _value.managedReferenceValue = newBool;
                        _value.serializedObject.ApplyModifiedProperties();
                        _value.serializedObject.Update();
                        _value = property.FindPropertyRelative("Value");
                    }
                    break;
                case DataType.GAMEOBJECT:
                    ObjectWrapper currentObject = _value.managedReferenceValue as ObjectWrapper;
                    if(currentObject == null)
                        currentObject = new ObjectWrapper();
                    GameObject newObject = EditorGUI.ObjectField(valueRect, currentObject.Target, typeof(GameObject), true) as GameObject;
                    if (newObject != currentObject.Target)
                    {
                        _value.managedReferenceValue = new ObjectWrapper(newObject);
                        _value.serializedObject.ApplyModifiedProperties();
                        _value.serializedObject.Update();
                        _value = property.FindPropertyRelative("Value");
                    }
                    break;
                case DataType.SPRITE:
                    ObjectWrapper currentSprite = _value.managedReferenceValue as ObjectWrapper;
                    if (currentSprite == null)
                        currentSprite = new ObjectWrapper();
                    Sprite newSprite = EditorGUI.ObjectField(valueRect, currentSprite.Target, typeof(Sprite), true) as Sprite;
                    if (newSprite != currentSprite.Target)
                    {
                        _value.managedReferenceValue = new ObjectWrapper(newSprite);
                        _value.serializedObject.ApplyModifiedProperties();
                        _value.serializedObject.Update();
                        _value = property.FindPropertyRelative("Value");
                    }
                    break;
                default:
                    break;
            }


            
            
        }
        private void ResetValue(DataType dataType)
        {
            _value.managedReferenceValue = null;
            switch (dataType)
            {
                case DataType.INT:
                    _value.managedReferenceValue = 0;
                    break;
                case DataType.FLOAT:
                    _value.managedReferenceValue = 0f;
                    break;
                case DataType.STRING:
                    _value.managedReferenceValue = "Blank";
                    break;
                case DataType.BOOLEAN:
                    _value.managedReferenceValue = false;
                    break;
                case DataType.GAMEOBJECT:
                    _value.managedReferenceValue = null;
                    break;
                case DataType.SPRITE:
                    _value.managedReferenceValue = null;
                    break;
                default:
                    break;
            }
            _value.serializedObject.ApplyModifiedProperties();
        }
    }
}