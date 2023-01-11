using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public abstract class Entity : MonoBehaviour {
        [SerializeField] private Rigidbody2D rb2d;

        public EntityType EntityType = EntityType.SmallEntity;
        protected SpriteRenderer shadowLockRenderer;

        // 生命数值相关
        private GameObject healthEffectParent;
        private FloatingText currentDamageText;
        private FloatingText currentRecoverText;

        // 生命值
        public int MaxHealth = 10;
        [SerializeField] private int health;
        public bool IsVulnerable;

        // 移速
        public float MoveSpeed = 10f;

        protected virtual void Awake() {
            health = MaxHealth;

            rb2d = GetComponent<Rigidbody2D>();
            healthEffectParent = new GameObject("hpEffect");
            healthEffectParent.transform.parent = transform;

            CreateShadow();
        }

        // 攻击
        public abstract void Attack();

        // 创建影子
        public void CreateShadow() {
            GameObject shadow = new GameObject("shadow");
            shadow.transform.parent = transform;
            SpriteRenderer shadow_sr = shadow.AddComponent<SpriteRenderer>();
            shadow_sr.sortingLayerName = SortingLayerConst.SHADOW;
            
            GameObject shadowLock = new GameObject("shadow_lock");
            shadowLock.transform.parent = transform;
            shadowLockRenderer = shadowLock.AddComponent<SpriteRenderer>();
            shadowLockRenderer.sortingLayerName = SortingLayerConst.SHADOW;

            if (EntityType == EntityType.SmallEntity) {
                shadow_sr.sprite = SpriteManager.Instance.smallShadow;
                shadowLockRenderer.sprite = SpriteManager.Instance.smallShadowLock;
            }
        }

        // 受伤
        public virtual void GetDamage(int damageAmount) {
            // 扣除生命值
            if (!IsVulnerable) {
                health -= damageAmount;
            }

            // 显示伤害数值
            if (currentDamageText == null) {
                currentDamageText = Instantiate(TextsManager.Instance.damageText, healthEffectParent.transform).GetComponent<FloatingText>();
            }
            currentDamageText.UpdateText(damageAmount);

            // 伤害特效
            if (health < 0) {
                // 切换死亡贴图
            } else {
                // 贴图闪烁
            }
        }

        // 回血
        public virtual void HealthRecover(int recoverAmount) {
            recoverAmount = health + recoverAmount < MaxHealth ? recoverAmount : Mathf.Abs(health - recoverAmount);
            health += recoverAmount;

            // 显示回复数值
            if (currentRecoverText == null) {
                currentRecoverText = Instantiate(TextsManager.Instance.recoverText, healthEffectParent.transform).GetComponent<FloatingText>();
            }
            currentRecoverText.UpdateText(recoverAmount);
        }

        public virtual void Move(Vector2 direction) {
            rb2d.velocity = direction * MoveSpeed;
        }
    }
}
