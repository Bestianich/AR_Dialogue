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
            // if (_value == null)
            // {
            //     ResetValue(_currentDataType);
            // }
            
            bool changed = false;
            switch (dataType)
            {
                case DataType.INT:
                    EditorGUI.BeginChangeCheck();
                    int intValue = (int)_value.managedReferenceValue;
                    int newInt = EditorGUI.IntField(valueRect , intValue);
                    if(EditorGUI.EndChangeCheck())
                    {   
                        if (newInt != intValue)
                        {
                            property.FindPropertyRelative("Value").managedReferenceValue = newInt;
                            _value = property.FindPropertyRelative("Value");
                            changed = true;
                        }
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
                    PrimitiveWrapper currentString = _value.managedReferenceValue as PrimitiveWrapper;
                    if (currentString == null)
                        currentString = new PrimitiveWrapper();
                    
                    string newString = EditorGUI.TextField(valueRect, currentString.Target as string);
                    Debug.Log(currentString.Target as string);
                    //Debug.Log(((PrimitiveWrapper)_value.managedReferenceValue));
                    
                    if (EditorGUI.EndChangeCheck())
                    {
                        
                        if (newString != currentString.Target as string)
                            
                        {
                            _value.managedReferenceValue = new PrimitiveWrapper(newString);
                             _value.serializedObject.ApplyModifiedProperties();
                            _value.serializedObject.Update();
                            _value = property.FindPropertyRelative("Value");
                            changed = true;
                        }
                    }

                    break;
                case DataType.BOOLEAN:
                    EditorGUI.BeginChangeCheck();
                    PrimitiveWrapper currentBool = _value.managedReferenceValue as PrimitiveWrapper;
                    if (currentBool == null)
                        currentBool = new PrimitiveWrapper(false);
                    bool newBool = EditorGUI.Toggle(valueRect, (bool)currentBool.Target);
                    if (EditorGUI.EndChangeCheck())
                    {
                        if (newBool != (bool)currentBool.Target)
                        {
                            _value.managedReferenceValue = new PrimitiveWrapper(newBool);
                            _value.serializedObject.ApplyModifiedProperties();
                            _value.serializedObject.Update();
                            _value = property.FindPropertyRelative("Value");
                            changed = true;
                            
                        }
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
        private void ResetValue(DataType dataType)
        {
            Debug.Log("Resetting Value");
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