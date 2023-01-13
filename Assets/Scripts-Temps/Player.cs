using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class Player : Entity {
        [Header("Player Properties", order = 1)]
        public WeaponsSO CurrentWeapon;

        public List<Skins> skins;

        SpriteRenderer playerSpriteRenderer;
        Animator playerAnimator;
        BoxCollider2D movementCollider;

        Transform handTransform;
        GameObject HeldWeapon;
        [SerializeField]
        Transform firePoint;

        bool isFacingRight = true;

        protected override void Awake() {
            base.Awake();

            CreateBodyAndHand();

            // 点击鼠标，攻击
            InputManager.Instance.Input.Player.Fire.performed += (ctx) => {
                Attack();
            };
        }

        private void Update() {
            float angle = InputManager.Instance.GetMouseAngle(handTransform.position);
            Aim(angle);
            Look(angle);
        }

        private void FixedUpdate() {
            Vector2 moveDir = InputManager.Instance.GetMovementDirection();
            Move(moveDir);
            playerAnimator.SetBool("run", moveDir != Vector2.zero);
        }

        private void CreateBodyAndHand() {
            // 创建 Body 子 GameObject
            GameObject body = new GameObject("body");
            body.transform.SetParent(transform, false);
            playerSpriteRenderer = body.AddComponent<SpriteRenderer>();
            playerAnimator = body.AddComponent<Animator>();
            movementCollider = body.AddComponent<BoxCollider2D>();
            playerSpriteRenderer.sortingLayerName = SortingLayerConst.CHARACTER;
            ChangeSkin(skins[0].skinId);
            movementCollider.offset = new Vector2(0, 0.25f);
            movementCollider.size = new Vector2(1, 0.5f);

            // 创建 Hand 子 GameObject
            GameObject hand = new GameObject("hand");
            hand.transform.SetParent(transform, false);
            Vector3 handOffset = hand.transform.position;
            handOffset.y = 0.5f;
            hand.transform.position = handOffset;
            handTransform = hand.transform;

            // 创建 Weapon 子 GameObject
            HeldWeapon = Instantiate(CurrentWeapon.weaponPrefab, hand.transform);
            firePoint = Helper.getChildGameObject(HeldWeapon, "firepoint").GetComponent<Transform>();
        }

        private void Aim(float angle) {
            handTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void Look(float angle) {
            DebugManager.Log(angle);
            // -90 ~ 90 = right = false;
            // 鼠标在右侧，但是不是朝向右边
            if (angle >= -90 && angle <= 90 && !isFacingRight) {
                Flip();
            } else if ((angle > 90 || angle < -90) && isFacingRight) {
                Flip();
            }
            // -90 ~ 90 = right = false;
            //bool flip = !(angle >= -90 && angle <= 90);
            //playerSpriteRenderer.flipX = flip;
            //weaponSpriteRenderer.flipY = flip;
        }

        private void Flip() {
            transform.Rotate(0, 180f, 0);
            HeldWeapon.transform.Rotate(180f, 0, 0);
            isFacingRight = !isFacingRight;
        }

        public override void Attack() {
            Instantiate(CurrentWeapon.bulletPrefab, firePoint.position, firePoint.rotation);
        }

        internal void ChangeSkin(int skinId) {
            RuntimeAnimatorController controller = skins.Find(x => x.skinId == skinId).controller;
            playerAnimator.runtimeAnimatorController = controller;
        }
    }
}
