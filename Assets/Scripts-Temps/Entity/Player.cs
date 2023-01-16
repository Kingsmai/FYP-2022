using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace CraftsmanHero {
    public class Player : Entity {
        [Header("Player Properties", order = 1)]
        public WeaponsSO CurrentWeapon;

        // 玩家动画相关
        [Header("玩家图片相关")]
        public List<SkinsSO> Skins;
        SkinsSO currentSkin;
        public int currentSkinIndex;
        int currentSkinState;
        int currentSkinFrame;
        Sprite[,] skinSpriteCache;
        SpriteRenderer playerSpriteRenderer;
        BoxCollider2D movementCollider;

        Transform handTransform;
        Weapons HeldWeapon;

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
            if (moveDir != Vector2.zero) {
                ChangeSkinState("WALK");
            } else {
                ChangeSkinState("IDLE");
            }
        }

        private void CreateBodyAndHand() {
            // 创建 Body 子 GameObject
            GameObject body = new GameObject("body");
            body.transform.SetParent(transform, false);
            playerSpriteRenderer = body.AddComponent<SpriteRenderer>();
            movementCollider = body.AddComponent<BoxCollider2D>();
            playerSpriteRenderer.sortingLayerName = SortingLayerConst.CHARACTER;
            ChangeSkin(0);
            // 设置碰撞体
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

        public override void Attack() {
            HeldWeapon.Fire();
        }

        // #REGION: 皮肤相关
        // 加载皮肤切片到缓存，提升计算性能
        public void LoadSkinFrameToCache() {
            currentSkin = Skins[currentSkinIndex];
            skinSpriteCache = new Sprite[currentSkin.SkinsStatus.Count, currentSkin.SkinsStatus.Max(state => state.FrameLength)];
            for (int state = 0; state < currentSkin.SkinsStatus.Count; state++) {
                for (int frame = 0; frame < currentSkin.SkinsStatus[state].FrameLength; frame++) {
                    Rect rect = new(
                        frame * currentSkin.FrameSize.x, // 修改 x 值改变帧数
                        state * currentSkin.FrameSize.y, // 修改 y 值改变动作（从下往上）
                        currentSkin.FrameSize.x,
                        currentSkin.FrameSize.y);
                    Sprite skinSprite = Sprite.Create(currentSkin.SkinSprite, rect, currentSkin.Pivot, 16f);
                    skinSpriteCache[state, frame] = skinSprite;
                }
            }
        }

        // 切换到下一帧
        public void NextFrame() {
            currentSkinFrame = (++currentSkinFrame) % currentSkin.SkinsStatus[currentSkinState].FrameLength;
            playerSpriteRenderer.sprite = skinSpriteCache[currentSkinState, currentSkinFrame];
        }

        // 根据 ID 换肤
        public void ChangeSkin(int skinIdx) {
            CancelInvoke();
            currentSkinIndex = skinIdx;
            LoadSkinFrameToCache();
            ChangeSkinState("IDLE");
            InvokeRepeating(nameof(NextFrame), 0f, 1f / currentSkin.sampleRate);
        }

        public void ChangeSkin(string skinId) {
            CancelInvoke();
            currentSkinIndex = Skins.IndexOf(Skins.Find(x => x.SkinID.Equals(skinId)));
            LoadSkinFrameToCache();
            ChangeSkinState("IDLE");
            InvokeRepeating(nameof(NextFrame), 0f, 1f / currentSkin.sampleRate);
        }

        public void ChangeSkinState(string stateName) {
            currentSkinState = currentSkin.SkinsStatus.IndexOf(currentSkin.SkinsStatus.Find(state => state.StatusID.Equals(stateName)));
        }
        // #ENDREGION
    }
}
