using System.Collections.Generic;
using System.Linq;
using CraftsmanHero.Interfaces;
using UnityEngine;

namespace CraftsmanHero {
    public class Player : Entity, IAttackable {
        [Header("Player Properties", order = 1)]
        public WeaponsScriptableObject CurrentWeapon;

        // 玩家动画相关
        [Header("玩家图片相关")]
        public Animator SkinAnimator;
        public List<SkinsScriptableObject> Skins;
        RuntimeAnimatorController defaultController;
        public int currentSkin = 0;

        Transform handTransform;
        Weapons HeldWeapon;

        bool isFacingRight = true;

        protected override void Awake() {
            base.Awake();

            CreateHand();

            defaultController = SkinAnimator.runtimeAnimatorController;

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
            SkinAnimator.SetBool("walk", moveDir != Vector2.zero);
        }

        private void CreateHand() {

            // 创建 Hand 子 GameObject
            GameObject hand = new GameObject("hand");
            hand.transform.SetParent(transform, false);
            Vector3 handOffset = hand.transform.position;
            handOffset.y = 0.5f;
            hand.transform.position = handOffset;
            handTransform = hand.transform;

            // 创建 Weapon 子 GameObject
            HeldWeapon = Instantiate(CurrentWeapon.weaponPrefab, hand.transform).GetComponent<Weapons>();
        }

        private void Aim(float angle) {
            handTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void Look(float angle) {
            // -90 ~ 90 = right = false;
            // 鼠标在右侧，但是不是朝向右边
            if (angle >= -90 && angle <= 90 && !isFacingRight) {
                Flip();
            } else if ((angle > 90 || angle < -90) && isFacingRight) {
                Flip();
            }
        }

        private void Flip() {
            transform.Rotate(0, 180f, 0);
            HeldWeapon.transform.Rotate(180f, 0, 0);
            healthEffectParent.transform.Rotate(0, 180f, 0);
            isFacingRight = !isFacingRight;
        }

        public void Attack() {
            HeldWeapon.Fire();
        }

        // #REGION: 皮肤相关
        // DEBUG ONLY 切换下一个皮肤
        public void NextSkin() {
            currentSkin = (++currentSkin) % Skins.Count;
            RuntimeAnimatorController controller = Skins[currentSkin].AnimatorController;
            if (controller != null) {
                SkinAnimator.runtimeAnimatorController = controller;
            } else {
                SkinAnimator.runtimeAnimatorController = defaultController;
            }
        }
        // #ENDREGION
    }
}
