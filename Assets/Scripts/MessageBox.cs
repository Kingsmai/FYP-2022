using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CraftsmanHero {
    public class MessageBox : MonoBehaviour {
        public GameObject nameBox;
        public TextMeshProUGUI nameText;
        public Image portraitImage;
        public TextMeshProUGUI messageText;

        public void ShowMessage(string message, string speakerName = null, Sprite image = null) {
            if (speakerName == null) {
                nameBox.SetActive(false);
            }
            else {
                nameBox.SetActive(true);
                nameText.text = speakerName;
            }

            if (image != null) {
                portraitImage.sprite = image;
            }

            messageText.text = message;

            gameObject.SetActive(true);
        }

        public void Close() {
            gameObject.SetActive(false);
        }
    }
}
