using System;
using System.Collections.Generic;
using CraftsmanHero.Interfaces;
using UnityEngine;

namespace CraftsmanHero {
    public class Player : Entity, IAttackable {
        [Header("Player Properties", order = 1)]
        public WeaponsScriptableObject CurrentWeapon;

        [Range(0.1f, 10f)] public float pickupRadius = 1;

        // 玩家动画相关
        [Header("玩家图片相关")] public Animator SkinAnimator;

        public List<SkinsScriptableObject> Skins;
        public int currentSkin;
        RuntimeAnimatorController defaultController;

        Transform handTransform;
        Weapons HeldWeapon;

        bool isFacingRight = true;

        // Clock
        float _elapsedTime;

        protected override void Awake() {
            base.Awake();

            CreateHand();

            defaultController = SkinAnimator.runtimeAnimatorController;

            // 点击鼠标，攻击
            InputManager.Instance.Input.Player.Fire.performed += ctx => { Attack(); };
        }

        void Update() {
            var angle = InputManager.Instance.GetMouseAngle(handTransform.position);
            Aim(angle);
            Look(angle);

            // Detect game item on ground
            if (_elapsedTime >= 0.1f) {
                DetectItemOnGround();
                _elapsedTime = 0;
            }

            _elapsedTime += Time.deltaTime;
        }

        void FixedUpdate() {
            var moveDir = InputManager.Instance.GetMovementDirection();
            Move(moveDir);
            SkinAnimator.SetBool("walk", moveDir != Vector2.zero);
        }

        public void Attack() {
            HeldWeapon.Fire();
        }

        void CreateHand() {
            // 创建 Hand 子 GameObject
            var hand = new GameObject("hand");
            hand.transform.SetParent(transform, false);
            var handOffset = hand.transform.position;
            handOffset.y = 0.5f;
            hand.transform.position = handOffset;
            handTransform = hand.transform;

            // 创建 Weapon 子 GameObject
            HeldWeapon = Instantiate(CurrentWeapon.weaponPrefab, hand.transform).GetComponent<Weapons>();
        }

        void Aim(float angle) {
            handTransform.rotation = Quaternion.Euler(0, 0, angle);
        }

        void Look(float angle) {
            // -90 ~ 90 = right = false;
            // 鼠标在右侧，但是不是朝向右边
            if (angle >= -90 && angle <= 90 && !isFacingRight) {
                Flip();
            }
            else if ((angle > 90 || angle < -90) && isFacingRight) {
                Flip();
            }
        }

        void Flip() {
            transform.Rotate(0, 180f, 0);
            HeldWeapon.transform.Rotate(180f, 0, 0);
            healthEffectParent.transform.Rotate(0, 180f, 0);
            isFacingRight = !isFacingRight;
        }

        // #REGION: 皮肤相关
        // DEBUG ONLY 切换下一个皮肤
        public void NextSkin() {
            currentSkin = ++currentSkin % Skins.Count;
            RuntimeAnimatorController controller = Skins[currentSkin].AnimatorController;

            if (controller != null) {
                SkinAnimator.runtimeAnimatorController = controller;
            }
            else {
                SkinAnimator.runtimeAnimatorController = defaultController;
            }
        }
        // #ENDREGION

        void DetectItemOnGround() {
            // TODO: 把 game Items 放在一个 game Manager 的数组 / 对象池里，加速访问。
            GameObject[] gameItems = GameObject.FindGameObjectsWithTag("GameItem");
            foreach (var gameItem in gameItems) {
                if (Vector3.Distance(gameItem.transform.position, transform.position) < pickupRadius) {
                    // Fly towards player
                    GameItemScriptableObject itemData = gameItem.GetComponent<GameItemDisplay>().GameItemToDisplay;
                    PickupGameItem(itemData);
                    Destroy(gameItem);
                }
            }
        }
        void PickupGameItem(GameItemScriptableObject gameItem) {
            InventoryItemInfo inventoryItem = null;

            foreach (var item in inventory) {
                if (item.gameItem == gameItem && item.Amount < 99) {
                    inventoryItem = item;
                    break;
                }
            }

            if (inventoryItem == null) {
                inventoryItem = new InventoryItemInfo(gameItem, 1);
                inventory.Add(inventoryItem);
            }
            else {
                inventoryItem.Amount++;
            }
        }

        protected override void Death() {
            // 重写角色死亡操作
            SkinAnimator.SetTrigger("dead");
        }

        void OnDrawGizmos() {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position + Vector3.up, pickupRadius);
        }
    }
}
