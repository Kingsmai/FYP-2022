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
        public Texture2D SkinSprite;

        public Vector2 FrameSize = new(40, 40);
        public Vector2 Pivot = new(0.5f, 0);
        public int sampleRate = 16;

        public List<SkinsState> SkinsStatus;

        public SkinsSO() {
            SkinsStatus = new();
            // »ù´¡×´Ì¬£¬¿ÕÏÐ¡¢ÐÐ×ß¡¢ËÀÍö
            SkinsStatus.Add(new SkinsState("DEAD", 1));
            SkinsStatus.Add(new SkinsState("WALK", 8));
            SkinsStatus.Add(new SkinsState("IDLE", 8));
        }

        private void OnValidate() {
            string assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            SkinID = Path.GetFileNameWithoutExtension(assetPath);
        }
    }

    [Serializable]
    public class SkinsState {
        public string StatusID;
        public int FrameLength;

        public SkinsState(string statusID, int frameLength) {
            StatusID = statusID;
            FrameLength = frameLength;
        }
    }
}
