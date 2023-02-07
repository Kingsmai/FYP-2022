using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
    public class RecipeScriptableObject : ScriptableObject {
        public string RecipeID;
        public Ingredient Result;

        [Space] public Ingredient[] RecipeIngredient;

        void OnValidate() {
            var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            RecipeID = Path.GetFileNameWithoutExtension(assetPath);
        }
    }

    [Serializable]
    public class Ingredient {
        public GameItemScriptableObject gameItem;
        public int amount = 1;
    }
}
