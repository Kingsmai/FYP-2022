using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace CraftsmanHero {
    [CustomPropertyDrawer(typeof(InventoryItemInfo))]
    public class InventoryItemDrawerUIE : PropertyDrawer {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
            EditorGUI.BeginProperty(position, label, property);
            position = EditorGUI.PrefixLabel(position, GUIUtility.GetControlID(FocusType.Passive), label);

            var indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;

            var itemRect = new Rect(position.x, position.y, 170, position.height);
            var amountRect = new Rect(position.x + 180, position.y, 50, position.height);
            var randomRect = new Rect(position.x + 240, position.y, 20, position.height);

            EditorGUI.PropertyField(itemRect, property.FindPropertyRelative("gameItem"), GUIContent.none);
            EditorGUI.PropertyField(amountRect, property.FindPropertyRelative("amount"), GUIContent.none);
            EditorGUI.PropertyField(randomRect, property.FindPropertyRelative("randomDrop"), GUIContent.none);

            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }
    }
}
