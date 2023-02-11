using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CraftsmanHero {
    public class UIManager : Singleton<UIManager> {
        Player currentPlayer;
        InputManager inputManager;

        [Header("生命值相关")] public Slider HealthBar;
        public TextMeshProUGUI HealthAmount;

        [Header("游戏货币相关")] public TextMeshProUGUI GoldAmount;

        [Header("背包和物品栏相关")] public GameObject hotbarSlots;
        Image[] hotbarSlotsImages;
        [SerializeField] int currentSelectedHotbarSlot;
        public Sprite slotImageNormal;
        public Sprite slotImageSelected;
        [Space] public GameObject backpackSlots;
        public GameObject inventorySlotPrefab;
        List<InventorySlotDisplay> inventorySlots;

        [Header("界面开关相关")] public GameObject settingPanel;
        public GameObject inventoryPanel;
        Stack<GameObject> currentOpenedPanel;

        protected override void Awake() {
            base.Awake();
            currentPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
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
                if (currentOpenedPanel.Count != 0) {
                    currentOpenedPanel.Peek().SetActive(false);
                    currentOpenedPanel.Pop();
                }
                else if (!currentOpenedPanel.Contains(settingPanel)) {
                    settingPanel.SetActive(true);
                    currentOpenedPanel.Push(settingPanel);
                }

                if (currentOpenedPanel.Count == 0) {
                    CursorManager.Instance.AimCursor();
                }
            };

            inputManager.OnInventoryPressed += () => {
                if (!currentOpenedPanel.Contains(inventoryPanel)) {
                    inventoryPanel.SetActive(true);
                    currentOpenedPanel.Push(inventoryPanel);
                    CursorManager.Instance.SelectCursor();
                }
                else if (currentOpenedPanel.Peek() == inventoryPanel) {
                    inventoryPanel.SetActive(false);
                    currentOpenedPanel.Pop();
                }

                if (currentOpenedPanel.Count == 0) {
                    CursorManager.Instance.AimCursor();
                }
            };

            currentPlayer.OnInventoryItemChange += idx => {
                inventorySlots[idx].GameItemToDisplay = currentPlayer.inventory[idx].gameItem;
                inventorySlots[idx].Count = currentPlayer.inventory[idx].Amount;
                inventorySlots[idx].Enable();
            };

            inventorySlots = new List<InventorySlotDisplay>();
            currentOpenedPanel = new Stack<GameObject>();

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
