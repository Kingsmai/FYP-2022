using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Skin", menuName = "Skins")]
    public class SkinsSO : ScriptableObject {
        public string SkinID;
        public string SkinName;
        public AnimatorOverrideController AnimatorController;

        private void OnValidate() {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            SkinID = Path.GetFileNameWithoutExtension(assetPath);
        }
    }
}
