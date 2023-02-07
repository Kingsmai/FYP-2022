using UnityEngine;

namespace CraftsmanHero {
    public class Bullets : MonoBehaviour {
        public int destroyDuration = 3;

        [Space] public int Damage;

        float _elapsedTime;

        protected SpriteRenderer _spriteRenderer;

        void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        void Update() {
            _elapsedTime += Time.deltaTime;

            if (_elapsedTime > destroyDuration) {
                // TODO: 之后实现对象池，要调用对象池的销毁方法
                Destroy(gameObject);
            }
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            if (collision.name.Equals("hpEffect") && !collision.CompareTag("Player")) {
                var entity = collision.GetComponentInParent<Entity>();
                entity.GetDamage(Damage, transform.position);
            }
        }
    }
}
