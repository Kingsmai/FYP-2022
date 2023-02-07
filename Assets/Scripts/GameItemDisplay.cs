using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace CraftsmanHero {
    public class GameItemDisplay : MonoBehaviour {
        public SpriteRenderer iconSpriteRenderer;
        [SerializeField] GameItemScriptableObject gameItemToDisplay;
        Rigidbody2D rb2d;
        Animator anim;
        CircleCollider2D col;

        public GameItemScriptableObject GameItemToDisplay {
            get { return gameItemToDisplay; }
            set {
                gameItemToDisplay = value;
                Setup();
            }
        }

        void Awake() {
            Setup();
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            col = GetComponent<CircleCollider2D>();
            // 随机向某个方向滑动
            Vector2 randomDirection = new Vector2(Random.Range(-1f, 1f), Random.Range(-1f, 1f));
            randomDirection = randomDirection.normalized;
            Debug.Log(randomDirection);
            rb2d.AddForce(randomDirection.normalized * 100);
        }

        void Update() {
            if (rb2d.velocity.x <= 0.5 && rb2d.velocity.y <= 0.5) {
                rb2d.velocity = Vector2.zero;
                anim.SetTrigger("startFloat");
                col.isTrigger = true;
            }
        }

        void Setup() {
            if (gameItemToDisplay != null) {
                iconSpriteRenderer.sprite = GameItemToDisplay.ItemIcon;
            }
        }
    }
}
