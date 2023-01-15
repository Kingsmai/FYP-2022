using UnityEngine;

namespace CraftsmanHero {
    public class Bullets : MonoBehaviour {
        public int Damage;

        protected SpriteRenderer _spriteRenderer;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            if (collision.name.Equals("hpEffect") && !collision.CompareTag("Player")) {
                Entity entity = collision.GetComponentInParent<Entity>();
                entity.GetDamage(Damage, transform.position);
            }
        }
    }
}
