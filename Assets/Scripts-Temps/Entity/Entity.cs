using UnityEngine;
using UnityEngine.Serialization;

namespace CraftsmanHero {
    public abstract class Entity : MonoBehaviour {
        public string entityTag = "Untagged";

        // 生命数值相关
        [Header("生命值")] public int maxHealth = 10;
        public bool isVulnerable;
        public Vector2 damageColliderOffset = new(0, 0.75f);
        public Vector2 damageColliderSize = new(1, 1.5f);

        [Header("移动")] public bool isStatic;
        public float moveSpeed = 10f;

        [Header("影子")] public Sprite shadowSprite;
        public Sprite shadowLockSprite;
        FloatingText currentDamageText;
        FloatingText currentRecoverText;
        BoxCollider2D damageCollider;
        protected GameObject HealthEffectParent;
        Rigidbody2D rb2d;
        protected SpriteRenderer ShadowLockRenderer;

        [SerializeField] int Health { get; set; }

        protected virtual void Awake() {
            Health = maxHealth;

            rb2d = GetComponent<Rigidbody2D>();

            CreateDamageCollider();
            CreateShadow();
        }

        void OnValidate() {
            if (isStatic) {
                moveSpeed = 0f;
            }
        }

        // 攻击
        public abstract void Attack();

        // 创建伤害碰撞体
        void CreateDamageCollider() {
            HealthEffectParent = new GameObject("hpEffect");
            HealthEffectParent.transform.SetParent(transform, false);
            HealthEffectParent.tag = entityTag;
            damageCollider = HealthEffectParent.AddComponent<BoxCollider2D>();
            damageCollider.offset = damageColliderOffset;
            damageCollider.size = damageColliderSize;
            damageCollider.isTrigger = true;
        }

        // 创建影子
        public void CreateShadow() {
            if (shadowSprite != null) {
                var shadow = new GameObject("shadow");
                shadow.transform.SetParent(transform, false);
                var shadow_sr = shadow.AddComponent<SpriteRenderer>();
                shadow_sr.sortingLayerName = SortingLayerConst.SHADOW;
                shadow_sr.sprite = shadowSprite;
            }

            var shadowLock = new GameObject("shadow_lock");
            shadowLock.transform.SetParent(transform, false);
            ShadowLockRenderer = shadowLock.AddComponent<SpriteRenderer>();
            ShadowLockRenderer.sortingLayerName = SortingLayerConst.SHADOW;
            ShadowLockRenderer.sprite = shadowLockSprite;
        }

        // 受伤
        public virtual void GetDamage(int damageAmount, Vector3 position) {
            // 扣除生命值
            if (!isVulnerable) {
                Health -= damageAmount;
            }

            // 显示伤害数值
            if (currentDamageText == null) {
                currentDamageText = Instantiate(TextsManager.Instance.damageText, HealthEffectParent.transform)
                    .GetComponent<FloatingText>();
            }

            currentDamageText.UpdateText(damageAmount);

            // 伤害特效
            if (Health < 0) {
                // 切换死亡贴图
            }
            // 贴图闪烁
        }

        // 回血
        public virtual void HealthRecover(int recoverAmount) {
            recoverAmount = Health + recoverAmount < maxHealth ? recoverAmount : Mathf.Abs(Health - recoverAmount);
            Health += recoverAmount;

            // 显示回复数值
            if (currentRecoverText == null) {
                currentRecoverText = Instantiate(TextsManager.Instance.recoverText, HealthEffectParent.transform)
                    .GetComponent<FloatingText>();
            }

            currentRecoverText.UpdateText(recoverAmount);
        }

        public virtual void Move(Vector2 direction) {
            rb2d.velocity = direction * moveSpeed;
        }
    }
}
