using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class Player : Entity {
        public List<Skins> skins;
        
        SpriteRenderer playerSpriteRenderer;
        Animator playerAnimator;
        BoxCollider2D movementCollider;

        protected override void Awake() {
            base.Awake();

            CreateBody();
            
            // 点击鼠标，攻击
            InputManager.Instance.Input.Player.Fire.performed += (ctx) => {
                Attack();
            };
        }

        private void FixedUpdate() {
            Vector2 moveDir = InputManager.Instance.GetMovementDirection();
            Move(moveDir);
            playerAnimator.SetBool("run", moveDir != Vector2.zero);
        }

        private void CreateBody() {
            // 创建 Body 子 GameObject
            GameObject body = new GameObject("body");
            body.transform.SetParent(transform, false);
            playerSpriteRenderer = body.AddComponent<SpriteRenderer>();
            playerAnimator = body.AddComponent<Animator>();
            movementCollider = body.AddComponent<BoxCollider2D>();
            ChangeSkin(skins[0].skinId);
            movementCollider.offset = new Vector2(0, 0.25f);
            movementCollider.size = new Vector2(1, 0.5f);
        }

        public override void Attack() {
            DebugManager.Log($"Attacked! at: {Time.time}");
        }

        internal void ChangeSkin(int skinId) {
            RuntimeAnimatorController controller = skins.Find(x => x.skinId == skinId).controller;
            playerAnimator.runtimeAnimatorController = controller;
        }
    }

    [System.Serializable]
    public struct Skins {
        public int skinId;
        public string skinName;
        public RuntimeAnimatorController controller;
    }
}
