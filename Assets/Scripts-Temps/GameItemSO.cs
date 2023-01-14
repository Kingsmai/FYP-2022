using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Game Item/Game Item", order = 1)]
    public class GameItemSO : ScriptableObject {
        public string itemId;
        public string itemName;
    }
}
