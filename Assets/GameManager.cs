using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

namespace CraftsmanHero {
    public class GameManager : Singleton<GameManager> {
        public TextMeshProUGUI coinText;

        int playerGold;
        
        public int PlayerGold {
            get => playerGold;
            private set {
                playerGold = value;
                coinText.text = playerGold.ToString();
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
    }
}
