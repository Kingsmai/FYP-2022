using System.IO;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Game Item/Game Item", order = 1)]
    public class GameItemScriptableObject : ScriptableObject {
        public string ItemID;
        public string ItemName;
        public Sprite ItemIcon;

        protected virtual void OnValidate() {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            ItemID = Path.GetFileNameWithoutExtension(assetPath);
        }
    }
}
