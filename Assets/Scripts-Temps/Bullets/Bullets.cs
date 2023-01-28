using UnityEngine;
using UnityEngine.Serialization;

namespace CraftsmanHero {
    public class Bullets : MonoBehaviour {
        public int damage;

        protected SpriteRenderer SpriteRenderer;

        void Awake() {
            SpriteRenderer = GetComponent<SpriteRenderer>();
        }

        protected virtual void OnTriggerEnter2D(Collider2D collision) {
            if (collision.name.Equals("hpEffect") && !collision.CompareTag("Player")) {
                var entity = collision.GetComponentInParent<Entity>();
                entity.GetDamage(damage, transform.position);
            }
        }
    }
}
