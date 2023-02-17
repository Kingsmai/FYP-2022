using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Dialogue", menuName = "System/Dialogue")]
    public class DialogueScriptableObject : ScriptableObject {
        public string dialogueID;
        public List<Dialogue> dialogues;

        protected virtual void OnValidate() {
            var assetPath = AssetDatabase.GetAssetPath(GetInstanceID());
            dialogueID = Path.GetFileNameWithoutExtension(assetPath);
            
        }
    }

    [Serializable]
    public class Dialogue {
        public Sprite speakerImage;
        public string speakerName;
        [TextArea(10, 10)] public string dialogue;
    }
}
