using UnityEngine;

namespace CraftsmanHero {
    public abstract class Entity : MonoBehaviour {
        private Rigidbody2D rb2d;

        public string entityTag = "Untagged";

        // ������ֵ���
        [Header("����ֵ")]
        public int MaxHealth = 10;
        [SerializeField] private int health;
        public bool IsVulnerable;
        protected GameObject healthEffectParent;
        private BoxCollider2D damageCollider;
        private FloatingText currentDamageText;
        private FloatingText currentRecoverText;
        public Vector2 damageColliderOffset = new Vector2(0, 0.75f);
        public Vector2 damageColliderSize = new Vector2(1, 1.5f);

        [Header("�ƶ�")]
        public bool isStatic;
        public float MoveSpeed = 10f;

        [Header("Ӱ��")]
        public Sprite ShadowSprite;
        public Sprite ShadowLockSprite;
        protected SpriteRenderer shadowLockRenderer;

        private void OnValidate() {
            if (isStatic) {
                MoveSpeed = 0f;
            }
        }

        protected virtual void Awake() {
            health = MaxHealth;

            rb2d = GetComponent<Rigidbody2D>();

            CreateDamageCollider();
            CreateShadow();
        }

        // ����
        public abstract void Attack();

        // �����˺���ײ��
        private void CreateDamageCollider() {
            healthEffectParent = new GameObject("hpEffect");
            healthEffectParent.transform.SetParent(transform, false);
            healthEffectParent.tag = entityTag;
            damageCollider = healthEffectParent.AddComponent<BoxCollider2D>();
            damageCollider.offset = damageColliderOffset;
            damageCollider.size = damageColliderSize;
            damageCollider.isTrigger = true;
        }

        // ����Ӱ��
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

        // ����
        public virtual void GetDamage(int damageAmount, Vector3 position) {

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
