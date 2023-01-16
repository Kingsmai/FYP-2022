using System;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
    public class RecipeSO : ScriptableObject {
        public string RecipeID;
        public Ingredient Result;
        [Space]
        public Ingredient[] RecipeIngredient;

        private void OnValidate() {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            RecipeID = Path.GetFileNameWithoutExtension(assetPath);
        }
    }

    [Serializable]
    public class Ingredient {
        public GameItemSO gameItem;
        public int amount = 1;
    }
}
