using System.IO;
using UnityEditor;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Game Item/Game Item", order = 1)]
    public class GameItemSO : ScriptableObject {
        public string ItemID;
        public string ItemName;

        protected virtual void OnValidate() {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            ItemID = Path.GetFileNameWithoutExtension(assetPath);
        }
    }
}
