using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CraftsmanHero {
    public class UIManager : Singleton<UIManager> {
        public Image HealthBar;
        public TextMeshProUGUI HealthAmount;
        public TextMeshProUGUI GoldAmount;

        protected override void Awake() {
            base.Awake();
            var currentPlayer = GameObject.FindWithTag("Player").GetComponent<Player>();
            var gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
            currentPlayer.OnHealthChanged += () => {
                HealthAmount.text = currentPlayer.Health.ToString();
                HealthBar.fillAmount = (float)currentPlayer.Health / currentPlayer.MaxHealth;
            };
            
            // Initialize
            gameManager.OnGoldChange += () => {
                GoldAmount.text = gameManager.GoldAmount.ToString();
            };
        }
    }
}
