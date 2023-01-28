using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CraftsmanHero {
    public class Player : Entity {
        [Header("Player Properties", order = 1)]
        public WeaponsSO CurrentWeapon;

        // ��Ҷ������
        [Header("���ͼƬ���")]
        public Animator SkinAnimator;
        public List<SkinsSO> Skins;
        RuntimeAnimatorController defaultController;
        public int currentSkin = 0;

        Transform handTransform;
        Weapons HeldWeapon;

        bool isFacingRight = true;

        protected override void Awake() {
            base.Awake();

            CreateHand();

            defaultController = SkinAnimator.runtimeAnimatorController;

            // �����꣬����
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

            // ���� Hand �� GameObject
            GameObject hand = new GameObject("hand");
            hand.transform.SetParent(transform, false);
            Vector3 handOffset = hand.transform.position;
            handOffset.y = 0.5f;
            hand.transform.position = handOffset;
            handTransform = hand.transform;

            // ���� Weapon �� GameObject
            HeldWeapon = Instantiate(CurrentWeapon.weaponPrefab, hand.transform).GetComponent<Weapons>();
        }

        private void Aim(float angle) {
            handTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void Look(float angle) {
            // -90 ~ 90 = right = false;
            // ������Ҳ࣬���ǲ��ǳ����ұ�
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

        public override void Attack() {
            HeldWeapon.Fire();
        }

        // #REGION: Ƥ�����
        // DEBUG ONLY �л���һ��Ƥ��
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
