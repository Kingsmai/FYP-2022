using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public abstract class Entity : MonoBehaviour {
        [Header("Entity Properties", order = 10)]
        private Rigidbody2D rb2d;

        // ������ֵ���
        private GameObject healthEffectParent;
        private FloatingText currentDamageText;
        private FloatingText currentRecoverText;

        // ����ֵ
        public int MaxHealth = 10;
        [SerializeField] private int health;
        public bool IsVulnerable;

        // ����
        public float MoveSpeed = 10f;

        public Sprite ShadowSprite;
        public Sprite ShadowLockSprite;
        protected SpriteRenderer shadowLockRenderer;
        
        protected virtual void Awake() {
            health = MaxHealth;

            rb2d = GetComponent<Rigidbody2D>();
            healthEffectParent = new GameObject("hpEffect");
            healthEffectParent.transform.parent = transform;

            CreateShadow();
        }

        // ����
        public abstract void Attack();

        // ����Ӱ��
        public void CreateShadow() {
            GameObject shadow = new GameObject("shadow");
            shadow.transform.parent = transform;
            SpriteRenderer shadow_sr = shadow.AddComponent<SpriteRenderer>();
            shadow_sr.sortingLayerName = SortingLayerConst.SHADOW;

            GameObject shadowLock = new GameObject("shadow_lock");
            shadowLock.transform.parent = transform;
            shadowLockRenderer = shadowLock.AddComponent<SpriteRenderer>();
            shadowLockRenderer.sortingLayerName = SortingLayerConst.SHADOW;

            shadow_sr.sprite = ShadowSprite;
            shadowLockRenderer.sprite = ShadowLockSprite;
        }

        // ����
        public virtual void GetDamage(int damageAmount) {
            // �۳�����ֵ
            if (!IsVulnerable) {
                health -= damageAmount;
            }

            // ��ʾ�˺���ֵ
            if (currentDamageText == null) {
                currentDamageText = Instantiate(TextsManager.Instance.damageText, healthEffectParent.transform).GetComponent<FloatingText>();
            }
            currentDamageText.UpdateText(damageAmount);

            // �˺���Ч
            if (health < 0) {
                // �л�������ͼ
            } else {
                // ��ͼ��˸
            }
        }

        // ��Ѫ
        public virtual void HealthRecover(int recoverAmount) {
            recoverAmount = health + recoverAmount < MaxHealth ? recoverAmount : Mathf.Abs(health - recoverAmount);
            health += recoverAmount;

            // ��ʾ�ظ���ֵ
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
