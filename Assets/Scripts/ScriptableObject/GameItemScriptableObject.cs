using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Game Item", menuName = "Game Item/Game Item")]
    public class GameItemScriptableObject : ScriptableObject {
        public string itemId;
        public string itemName;
        public Sprite itemIcon;
        public GameObject itemPrefab;
        public int itemPrice;
        [TextArea] public string description;
        public int maxStackCount;
    }
}
