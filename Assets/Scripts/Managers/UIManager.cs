using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

namespace CraftsmanHero {
    public class UIManager : Singleton<UIManager> {
        Player currentPlayer;
        GameManager gameManager;
        InputManager inputManager;

        [Header("生命值相关")] public Slider HealthBar;
        public TextMeshProUGUI HealthAmount;

        [Header("游戏货币相关")] public TextMeshProUGUI GoldAmount;

        [Header("背包和物品栏相关")] public GameObject hotbarSlots;
        [SerializeField] int currentSelectedHotbarSlot;
        public Sprite slotImageNormal;
        public Sprite slotImageSelected;
        [Space] public GameObject backpackSlots;
        public GameObject inventorySlotPrefab;
        [SerializeField] List<InventorySlotDisplay> inventorySlots;

        [Header("设置界面相关")] public GameObject settingPanel;
        Image[] hotbarSlotsImages;
        bool isSettingIsOpen;

        protected override void Awake() {
            base.Awake();
            currentPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
            gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            inputManager = GameObject.Find("InputManager").GetComponent<InputManager>();

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

            currentPlayer.OnInventoryItemChange += idx => {
                inventorySlots[idx].GameItemToDisplay = currentPlayer.inventory[idx].gameItem;
                inventorySlots[idx].Count = currentPlayer.inventory[idx].Amount;
                inventorySlots[idx].Enable();
            };

            inventorySlots = new List<InventorySlotDisplay>();

            CreateInventorySlots();
        }

        void CreateInventorySlots() {
            for (var i = 0; i < currentPlayer.inventory.Count; i++) {
                var currentInventoryItem = currentPlayer.inventory[i];
                InventorySlotDisplay slot = Instantiate(inventorySlotPrefab, backpackSlots.transform)
                    .GetComponent<InventorySlotDisplay>();
                inventorySlots.Add(slot);

                if (currentInventoryItem.gameItem == null) {
                    slot.Disable();
                }
                else {
                    slot.Enable();
                    slot.GameItemToDisplay = currentInventoryItem.gameItem;
                    slot.Count = currentInventoryItem.Amount;
                }
            }
        }
    }
}
