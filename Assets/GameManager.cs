using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace CraftsmanHero {
    public class GameManager : Singleton<GameManager> {
        public TextMeshProUGUI coinText;
        public Slider playerHealthBar;
        public Slider playerManaBar;

        int playerGold;
        int playerMaxHealth;
        int playerHealth;
        int playerMaxMana;
        int playerMana;

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
    }
}
