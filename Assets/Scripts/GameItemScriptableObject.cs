using System.IO;
using UnityEditor;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Game Item/Game Item", order = 1)]
    public class GameItemScriptableObject : ScriptableObject {
        public string ItemID;
        public string ItemName;
        public Sprite ItemIcon;

        protected virtual void OnValidate() {
            var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            ItemID = Path.GetFileNameWithoutExtension(assetPath);
        }
    }
}
