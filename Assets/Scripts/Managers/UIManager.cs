using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CraftsmanHero {
    public class UIManager : Singleton<UIManager> {
        [Header("生命值相关")] public Image HealthBar;
        public TextMeshProUGUI HealthAmount;

        [Header("游戏货币相关")] public TextMeshProUGUI GoldAmount;

        [Header("背包和物品栏相关")] public GameObject InventorySlots;
        Image[] inventorySlotsImages;
        [SerializeField]
        int currentSelectedInventorySlot = 0;
        public Sprite slotImageNormal;
        public Sprite slotImageSelected;

        protected override void Awake() {
            base.Awake();
            var currentPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            var inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

            inventorySlotsImages = InventorySlots.GetComponentsInChildren<Image>();
            
            currentPlayer.OnHealthChanged += () => {
                HealthAmount.text = currentPlayer.Health.ToString();
                HealthBar.fillAmount = (float)currentPlayer.Health / currentPlayer.MaxHealth;
            };

            gameManager.OnGoldChange += () => { GoldAmount.text = gameManager.GoldAmount.ToString(); };
            
            inputManager.OnScroll += isScrollingDown => {
                inventorySlotsImages[currentSelectedInventorySlot].sprite = slotImageNormal;
                if (isScrollingDown) {
                    currentSelectedInventorySlot = ++currentSelectedInventorySlot % inventorySlotsImages.Length;
                }
                else {
                    currentSelectedInventorySlot = --currentSelectedInventorySlot;

                    if (currentSelectedInventorySlot == -1) {
                        currentSelectedInventorySlot = inventorySlotsImages.Length - 1;
                    }
                }
                inventorySlotsImages[currentSelectedInventorySlot].sprite = slotImageSelected;
            };
        }
    }
}
