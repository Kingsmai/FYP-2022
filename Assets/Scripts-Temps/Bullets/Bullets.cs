using UnityEngine;

namespace CraftsmanHero {
    public class Bullets : MonoBehaviour {
        public int Damage;

        protected SpriteRenderer _spriteRenderer;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Entity")) {
                EntityStrobe strobeHelper = collision.GetComponentInChildren<EntityStrobe>();
                strobeHelper.StrobeColor(1, Color.white);
                EnemyAI ai = collision.GetComponent<EnemyAI>();
                ai.GetDamage(10, GameManager.Instance.CurrentPlayer.transform.position);
            }
        }
    }
}
