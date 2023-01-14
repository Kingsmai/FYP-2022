using System;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Recipe", menuName = "Recipe")]
    public class RecipeSO : ScriptableObject {
        public Ingredient result;
        [Space]
        public Ingredient[] recipeIngredient;
    }

    [Serializable]
    public class Ingredient {
        public GameItemSO gameItem;
        public int amount = 1;
    }
}
