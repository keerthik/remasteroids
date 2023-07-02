using UnityEditor;
using UnityEngine;
using System;

namespace EditorAux {
    [CustomPropertyDrawer(typeof(DrawIfAttribute))]
    public class DrawIfPropertyDrawer : PropertyDrawer
    {
    // Reference to the attribute on the property.
        DrawIfAttribute drawIf;
    
        // Field that is being compared.
        SerializedProperty comparedField;
    
        // Height of the property.
        private float propertyHeight;
    
        public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
            return propertyHeight;
        }
    
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Set the global variables.
            drawIf = attribute as DrawIfAttribute;
            comparedField = property.serializedObject.FindProperty(drawIf.comparedPropertyName);
    
            // Get the value of the compared field.
            
            object comparedFieldValue = comparedField.GetValue();
            float numericComparedFieldValue = Convert.ToSingle(comparedFieldValue);
            float numericDrawIfValue = Convert.ToSingle(drawIf.comparedToValue);
            // Is the condition met? Should the field be drawn?
            bool conditionMet = false;
    
            // Compare the values to see if the condition is met.
            switch (drawIf.comparisonType) {
                case ComparisonType.Equals:
                    if (comparedFieldValue.Equals(drawIf.comparedToValue))
                        conditionMet = true;
                    break;
    
                case ComparisonType.NotEqual:
                    if (!comparedFieldValue.Equals(drawIf.comparedToValue))
                        conditionMet = true;
                    break;
    
                case ComparisonType.GreaterThan:
                    if (numericComparedFieldValue > numericDrawIfValue)
                        conditionMet = true;
                    break;
    
                case ComparisonType.LessThan:
                    if (numericComparedFieldValue < numericDrawIfValue)
                        conditionMet = true;
                    break;
    
                case ComparisonType.LesserOrEqual:
                    if (numericComparedFieldValue <= numericDrawIfValue)
                        conditionMet = true;
                    break;
    
                case ComparisonType.GreaterOrEqual:
                    if (numericComparedFieldValue >= numericDrawIfValue)
                        conditionMet = true;
                    break;
            }
    
            // The height of the property should be defaulted to the default height.
            propertyHeight = base.GetPropertyHeight(property, label);
    
            // If the condition is met, simply draw the field. Else...
            if (conditionMet) {
                EditorGUI.PropertyField(position, property);
            } else {
                //...check if the disabling type is read only. If it is, draw it disabled, else, set the height to zero.
                if (drawIf.disablingType == DisablingType.ReadOnly) {
                    GUI.enabled = false;
                    EditorGUI.PropertyField(position, property);
                    GUI.enabled = true;
                } else {
                    propertyHeight = 0f;
                }
            }
        }
    }
}