using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CraftsmanHero
{
    public class InventorySlotDisplay : MonoBehaviour
    {
        public Image iconSpriteRenderer;
        public TextMeshProUGUI countUI;
        [SerializeField] GameItemScriptableObject gameItemToDisplay;
        [SerializeField] int count;

        public GameItemScriptableObject GameItemToDisplay {
            get => gameItemToDisplay;
            set {
                gameItemToDisplay = value;
                Setup();
            }
        }

        public int Count {
            get { return count; }
            set {
                count = value;
                countUI.text = count.ToString();
            }
        }

        void Setup() {
            if (gameItemToDisplay != null) {
                iconSpriteRenderer.sprite = GameItemToDisplay.ItemIcon;
            }
        }

        public void Disable() {
            iconSpriteRenderer.gameObject.SetActive(false);
            countUI.gameObject.SetActive(false);
        }

        public void Enable() {
            iconSpriteRenderer.gameObject.SetActive(true);
            countUI.gameObject.SetActive(true);
        }
    }
}
