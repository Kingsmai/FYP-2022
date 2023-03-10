using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CraftsmanHero {
    public class GameManager : Singleton<GameManager> {
        public TextMeshProUGUI coinText;
        public Slider playerHealthBar;
        public Slider playerManaBar;

        public PlayerController player;

        // Player Inventory
        public List<GameItemAmount> inventory;

        // Player properties
        int playerGold;
        int playerHealth;
        int playerMana;
        int playerMaxHealth;
        int playerMaxMana;

        public int PlayerGold {
            get => playerGold;
            private set {
                playerGold = value;
                coinText.text = playerGold.ToString();
            }
        }

        public int PlayerMaxHealth {
            get => playerMaxHealth;
            private set {
                playerMaxHealth = value;
                playerHealthBar.maxValue = playerMaxHealth;
            }
        }

        public int PlayerHealth {
            get => playerHealth;
            private set {
                if (playerHealth <= 0) {
                    playerHealth = 0;
                    player.Dead();
                }

                playerHealth = value;
                playerHealthBar.value = playerHealth;
            }
        }

        public int PlayerMaxMana {
            get => playerMaxMana;
            private set {
                playerMaxMana = value;
                playerManaBar.maxValue = playerMaxMana;
            }
        }

        public int PlayerMana {
            get => playerMana;
            private set {
                playerMana = value;
                playerManaBar.value = playerMana;
            }
        }

        protected override void Awake() {
            base.Awake();
            player = GameObject.Find("Player").GetComponent<PlayerController>();
        }

        public void SetGold(int amount) {
            PlayerGold = amount;
        }

        public void AddGold(int amount) {
            PlayerGold += amount;
        }

        public void SubGold(int amount) {
            PlayerGold -= amount;
        }

        public void SetMaxHealth(int maxHealth) {
            PlayerMaxHealth = maxHealth;
        }

        public void SetHealth(int health) {
            PlayerHealth = health;
        }

        public void RegenerateHealth(int amount) {
            PlayerHealth = PlayerHealth + amount < PlayerMaxHealth ? PlayerHealth + amount : PlayerMaxHealth;
        }

        public void TakeDamage(int damage) {
            PlayerHealth = PlayerHealth - damage > 0 ? PlayerHealth - damage : 0;
        }

        public void SetMaxMana(int maxMana) {
            PlayerMaxMana = maxMana;
        }

        public void SetMana(int mana) {
            PlayerMana = mana;
        }

        public void RegenerateMana(int amount) {
            PlayerMana = PlayerMana + amount < PlayerMaxMana ? PlayerMana + amount : PlayerMaxMana;
        }

        public void SpendMana(int castCost) {
            PlayerMana = PlayerMana - castCost > 0 ? PlayerMana - castCost : 0;
        }

        public void ObtainItem(GameItemScriptableObject gameItem, int amount = 1) {
            // ???????????????????????????????????????
            var stackCount = amount / gameItem.maxStackCount;
            var amountLeft = amount % gameItem.maxStackCount;

            // ??????????????????????????????????????? gameItem
            for (var i = 0; i < inventory.Count; i++)
                if (stackCount > 0 && inventory[i].GameItem == null) {
                    inventory[i] = new GameItemAmount {
                        GameItem = gameItem,
                        Amount = gameItem.maxStackCount
                    };
                    stackCount--;
                }
                else if (amountLeft > 0 && inventory[i].GameItem != null && inventory[i].GameItem.Equals(gameItem) && inventory[i].Amount < gameItem.maxStackCount) {
                    var spaceLeft = gameItem.maxStackCount - inventory[i].Amount;

                    if (spaceLeft > amountLeft) {
                        inventory[i].Amount += amountLeft;
                        amountLeft = 0;
                    }
                    else {
                        inventory[i].Amount = gameItem.maxStackCount;
                        amountLeft -= spaceLeft;
                    }
                }

            // ???????????????????????????
            for (var i = 0; amountLeft > 0 && i < inventory.Count; i++)
                if (inventory[i].GameItem == null) {
                    inventory[i] = new GameItemAmount {
                        GameItem = gameItem,
                        Amount = amountLeft
                    };
                    break;
                }
        }
    }
}
