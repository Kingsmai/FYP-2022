using UnityEditor;
using UnityEngine;

namespace CraftsmanHero {
    [CustomPropertyDrawer(typeof(Ingredient))]
    public class IngredientDrawerUIE : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            // Using BeginProperty / EndProperty on the parent property means that
            // prefab override logic works on the entire property.
            EditorGUI.BeginProperty(position, label, property);

            // Draw label
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            // Don't make child fields be indented
            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            // Calculate rects
            var itemRect = new Rect(position.x, position.y, 200, position.height);
            var amountRect = new Rect(position.x + 220, position.y, position.width - 220, position.height);

            // Draw fields - pass GUIContent.none to each so they are drawn without labels
            EditorGUI.PropertyField(itemRect, property.FindPropertyRelative("gameItem"), GUIContent.none);
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);

            // Set indent back to what it was
            EditorGUI.indentLevel = indent;

            EditorGUI.EndProperty();
        }
    }
}
