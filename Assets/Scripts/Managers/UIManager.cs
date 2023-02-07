using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CraftsmanHero {
    public class UIManager : Singleton<UIManager> {
        [Header("生命值相关")] public Slider HealthBar;
        public TextMeshProUGUI HealthAmount;

        [Header("游戏货币相关")] public TextMeshProUGUI GoldAmount;

        [Header("背包和物品栏相关")] public GameObject hotbarSlots;
        Image[] hotbarSlotsImages;
        [SerializeField] int currentSelectedHotbarSlot = 0;
        public Sprite slotImageNormal;
        public Sprite slotImageSelected;

        [Header("设置界面相关")] public GameObject settingPanel;
        bool isSettingIsOpen;

        protected override void Awake() {
            base.Awake();
            var currentPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            var inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

            hotbarSlotsImages = hotbarSlots.GetComponentsInChildren<Image>();

            currentPlayer.OnMaxHealthChanged += () => { HealthBar.maxValue = currentPlayer.MaxHealth; };

            HealthBar.maxValue = currentPlayer.MaxHealth;

            currentPlayer.OnHealthChanged += () => {
                HealthAmount.text = currentPlayer.Health.ToString();
                HealthBar.value = currentPlayer.Health;
            };


            currentPlayer.OnGoldChanged += () => { GoldAmount.text = currentPlayer.Gold.ToString(); };

            currentPlayer.OnExperienceChanged += () => { };

            inputManager.OnScroll += isScrollingDown => {
                hotbarSlotsImages[currentSelectedHotbarSlot].sprite = slotImageNormal;

                if (isScrollingDown) {
                    currentSelectedHotbarSlot = ++currentSelectedHotbarSlot % hotbarSlotsImages.Length;
                }
                else {
                    currentSelectedHotbarSlot = --currentSelectedHotbarSlot;

                    if (currentSelectedHotbarSlot == -1) {
                        currentSelectedHotbarSlot = hotbarSlotsImages.Length - 1;
                    }
                }

                hotbarSlotsImages[currentSelectedHotbarSlot].sprite = slotImageSelected;
            };

            inputManager.OnCancelPressed += () => {
                // If menu is not open, open the menu. Else; close the menu.
                if (isSettingIsOpen) {
                    settingPanel.SetActive(false);
                    isSettingIsOpen = false;
                }
                else {
                    settingPanel.SetActive(true);
                    isSettingIsOpen = true;
                }
            };
        }
    }
}
