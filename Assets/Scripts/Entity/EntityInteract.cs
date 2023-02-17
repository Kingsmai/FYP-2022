using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class EntityInteract : MonoBehaviour {
        public DialogueScriptableObject chatDialogue;

        void Chat() {
            foreach (var dialogue in chatDialogue.dialogues) {
                Debug.Log($"{dialogue.speakerName}: {dialogue.dialogue}");
            }
        }
    }
}
