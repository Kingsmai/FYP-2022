using System.IO;
using UnityEditor;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Skin", menuName = "Skins")]
    public class SkinsScriptableObject : ScriptableObject {
        public string SkinID;
        public string SkinName;
        public AnimatorOverrideController AnimatorController;

        void OnValidate() {
            var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            SkinID = Path.GetFileNameWithoutExtension(assetPath);
        }
    }
}
