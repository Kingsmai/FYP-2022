using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CraftsmanHero {
    public class Entity : MonoBehaviour {
        public delegate void EntityEventHandler();

        public event EntityEventHandler OnMaxHealthChanged;
        public event EntityEventHandler OnHealthChanged;
        public event EntityEventHandler OnGoldChanged;
        public event EntityEventHandler OnExperienceChanged;

        Rigidbody2D rb2d;

        public string entityTag = "Untagged";

        // 生命数值相关
        [Header("生命值")] int maxHealth = 10;

        public int MaxHealth {
            get { return maxHealth; }
            set {
                maxHealth = value;
                OnMaxHealthChanged?.Invoke();
            }
        }

        int health;

        public int Health {
            get { return health; }
            private set {
                health = value;
                OnHealthChanged?.Invoke();
            }
        }

        public bool IsVulnerable;
        protected GameObject healthEffectParent;
        BoxCollider2D damageCollider;
        FloatingText currentDamageText;
        FloatingText currentRecoverText;
        public Vector2 damageColliderOffset = new Vector2(0, 0.75f);
        public Vector2 damageColliderSize = new Vector2(1, 1.5f);

        [Header("掉落物")]
        // 最多可以掉落多少金币
        [SerializeField]
        int gold;

        public int Gold {
            get { return gold; }
            set {
                gold = value;
                OnGoldChanged?.Invoke();
            }
        }

        [SerializeField] int experience;

        public int Experience {
            get { return experience; }
            set {
                experience = value;
                OnExperienceChanged?.Invoke();
            }
        }

        public List<InventoryItemInfo> inventory;

        [Header("移动")] public bool isStatic;
        public float MoveSpeed = 10f;

        [Header("影子")] public Sprite ShadowSprite;
        public Sprite ShadowLockSprite;
        protected SpriteRenderer shadowLockRenderer;

        private void OnValidate() {
            if (isStatic) {
                MoveSpeed = 0f;
            }
        }

        protected virtual void Awake() {
            Health = MaxHealth;

            rb2d = GetComponent<Rigidbody2D>();

            CreateDamageCollider();
            CreateShadow();
        }

        // 创建伤害碰撞体
        private void CreateDamageCollider() {
            healthEffectParent = new GameObject("hpEffect");
            healthEffectParent.transform.SetParent(transform, false);
            healthEffectParent.tag = entityTag;
            damageCollider = healthEffectParent.AddComponent<BoxCollider2D>();
            damageCollider.offset = damageColliderOffset;
            damageCollider.size = damageColliderSize;
            damageCollider.isTrigger = true;
        }

        // 创建影子
        public void CreateShadow() {
            if (ShadowSprite != null) {
                GameObject shadow = new GameObject("shadow");
                shadow.transform.SetParent(transform, false);
                SpriteRenderer shadow_sr = shadow.AddComponent<SpriteRenderer>();
                shadow_sr.sortingLayerName = SortingLayerConst.SHADOW;
                shadow_sr.sprite = ShadowSprite;
            }

            GameObject shadowLock = new GameObject("shadow_lock");
            shadowLock.transform.SetParent(transform, false);
            shadowLockRenderer = shadowLock.AddComponent<SpriteRenderer>();
            shadowLockRenderer.sortingLayerName = SortingLayerConst.SHADOW;
            shadowLockRenderer.sprite = ShadowLockSprite;
        }

        // 受伤
        public virtual void GetDamage(int damageAmount, Vector3 position) {
            // 扣除生命值
            if (!IsVulnerable) {
                Health -= damageAmount;
            }

            // 显示伤害数值
            if (currentDamageText == null) {
                currentDamageText = Instantiate(GameManager.Instance.damageTextPrefab, healthEffectParent.transform)
                    .GetComponent<FloatingText>();
            }

            currentDamageText.UpdateText(damageAmount);

            // 伤害特效
            if (Health <= 0) {
                // 切换死亡贴图
                Death();
            }
            else {
                // 贴图闪烁
            }
        }

        // 回血
        public void HealthRecover(int recoverAmount) {
            recoverAmount = health + recoverAmount < MaxHealth ? recoverAmount : Mathf.Abs(health - recoverAmount);
            Health += recoverAmount;

            // 显示回复数值
            if (currentRecoverText == null) {
                currentRecoverText = Instantiate(GameManager.Instance.recoverTextPrefab, healthEffectParent.transform)
                    .GetComponent<FloatingText>();
            }

            currentRecoverText.UpdateText(recoverAmount);
        }

        public void Move(Vector2 direction) {
            rb2d.velocity = direction * MoveSpeed;
        }

        // 获得金币
        public void AddExperience(int expAmount) {
            Experience += expAmount;
        }

        // 获得经验值
        public void ObtainGold(int goldAmount) {
            Gold += goldAmount;
        }

        // 死亡处理
        protected virtual void Death() {
            Destroy(damageCollider);
            // Player Get Money
            var dropGold = Random.Range(0, Gold);
            GameManager.Instance.CurrentPlayer.ObtainGold(dropGold);

            // Drop Items
            foreach (var inventoryItemInfo in inventory) {
                var dropAmount = inventoryItemInfo.Amount;

                if (inventoryItemInfo.randomDrop) {
                    dropAmount = Random.Range(0, inventoryItemInfo.Amount);
                }

                for (var i = 0; i < dropAmount; i++) {
                    var item = Instantiate(GameManager.Instance.gameItemPrefab);
                    item.transform.position = transform.position;
                    var itemDisplay = item.GetComponent<GameItemDisplay>();
                    itemDisplay.GameItemToDisplay = inventoryItemInfo.gameItem;
                }
            }
        }
    }
}
