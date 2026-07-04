using AR_Dialogue.Scripts.Runtime;
using TMPro;
using Unity.Android.Types;
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
            EditorGUI.BeginProperty(position, label, property);
            float nameWidth = position.width * 0.3f;
            float typeWidth = position.width * 0.3f;
            float valueWidth = position.width - nameWidth - typeWidth - _padding;

            Rect nameRect = new Rect(position.x, position.y, nameWidth, EditorGUIUtility.singleLineHeight);
            Rect typeRect = new Rect(nameRect.x + nameWidth + _padding, position.y, typeWidth, EditorGUIUtility.singleLineHeight);
            Rect valueRect = new Rect(typeRect.x + typeWidth + _padding, position.y, valueWidth, EditorGUIUtility.singleLineHeight);
            
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
                    ResetValue(_currentDataType , property);
                }
            }
            
            DataType dataType = (DataType)type.enumValueIndex;
            
            bool changed = false;
            switch (dataType)
            {
                case DataType.INT:
                    EditorGUI.BeginChangeCheck();
                    int intValue = (int)_value.managedReferenceValue;
                    int newInt = EditorGUI.IntField(valueRect , intValue);
                    if(EditorGUI.EndChangeCheck())
                    {   
                        property.FindPropertyRelative("Value").managedReferenceValue = newInt;
                        property.FindPropertyRelative("Value").serializedObject.ApplyModifiedProperties();
                        Debug.Log(property.FindPropertyRelative("Value").managedReferenceValue);
                        changed = true;
                    }
                    
                    break;
                case DataType.FLOAT:
                    EditorGUI.BeginChangeCheck();
                    float currentFloat = (float)_value.managedReferenceValue;
                    float newFloat = EditorGUI.FloatField(valueRect, currentFloat);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (newFloat != currentFloat)
                        {
                            property.FindPropertyRelative("Value").managedReferenceValue = newFloat;
                            _value = property.FindPropertyRelative("Value");
                            changed = true;
                            
                        }
                    }
                    break;
                case DataType.STRING:
                    EditorGUI.BeginChangeCheck();
                    string currentString = property.FindPropertyRelative("Value").managedReferenceValue as string;
                    
                    string newString = EditorGUI.TextField(valueRect, currentString);
                    //Debug.Log(((PrimitiveWrapper)_value.managedReferenceValue));
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        _value.managedReferenceValue = newString;
                        _value.serializedObject.ApplyModifiedProperties();
                        _value = property.FindPropertyRelative("Value");
                        changed = true;
                        
                    }

                    break;
                case DataType.BOOLEAN:
                    EditorGUI.BeginChangeCheck();
                    bool currentBool = (bool)_value.managedReferenceValue; 
                    bool newBool = EditorGUI.Toggle(valueRect, currentBool);
                    if (EditorGUI.EndChangeCheck())
                    {
                        _value.managedReferenceValue = newBool;
                        _value.serializedObject.ApplyModifiedProperties();
                        _value = property.FindPropertyRelative("Value");
                        changed = true;
                    }
                    break;
                case DataType.GAMEOBJECT:
                    EditorGUI.BeginChangeCheck();
                    ObjectWrapper currentObject = _value.managedReferenceValue as ObjectWrapper;
                    if(currentObject == null)
                        currentObject = new ObjectWrapper();
                    GameObject newObject = EditorGUI.ObjectField(valueRect, currentObject.Target, typeof(GameObject), true) as GameObject;
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (newObject != currentObject.Target)
                        {
                            _value.managedReferenceValue = new ObjectWrapper(newObject);
                            _value.serializedObject.ApplyModifiedProperties();
                            _value.serializedObject.Update();
                            _value = property.FindPropertyRelative("Value");
                            changed = true;
                        }
                    }
                    

                    break;
                
                case DataType.TEXTMESHPRO:
                    EditorGUI.BeginChangeCheck();
                    ObjectWrapper currentText = _value.managedReferenceValue as ObjectWrapper;
                    if (currentText == null)
                    {
                        currentText = new ObjectWrapper();
                    }
                    TextMeshProUGUI newText = EditorGUI.ObjectField(valueRect, currentText.Target , typeof(TextMeshProUGUI) , true) as TextMeshProUGUI;
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (newText != currentText.Target as TextMeshProUGUI)
                        {
                            _value.managedReferenceValue = new ObjectWrapper(newText);
                            _value.serializedObject.ApplyModifiedProperties();
                            _value.serializedObject.Update();
                            _value = property.FindPropertyRelative("Value");
                            changed = true;
                        }
                    }

                    break;
                case DataType.SPRITE:
                    EditorGUI.BeginChangeCheck();
                    ObjectWrapper currentSprite = _value.managedReferenceValue as ObjectWrapper;
                    if (currentSprite == null)
                        currentSprite = new ObjectWrapper();
                    Sprite newSprite = EditorGUI.ObjectField(valueRect, currentSprite.Target, typeof(Sprite), true) as Sprite;
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (newSprite != currentSprite.Target)
                        {
                            _value.managedReferenceValue = new ObjectWrapper(newSprite);
                            _value.serializedObject.ApplyModifiedProperties();
                            _value.serializedObject.Update();
                            _value = property.FindPropertyRelative("Value");
                            changed = true;
                            
                        }
                    }

                    break;
                default:
                    break;
            }
            

            
            if (changed)
            {
                property.serializedObject.ApplyModifiedProperties();
                property.serializedObject.Update();
            }

            EditorGUI.EndProperty();
            
            
        }
        private void ResetValue(DataType dataType , SerializedProperty property)
        {
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
            property.serializedObject.ApplyModifiedProperties();
            property.serializedObject.Update();
        }
    }
}