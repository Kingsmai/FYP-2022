using UnityEngine;
using UnityEngine.Serialization;

namespace CraftsmanHero {
    public class FlyingBullets : Bullets {
        public float speed = 5f;
        Rigidbody2D rb2d;

        void Awake() {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb2d.freezeRotation = true;
            rb2d.velocity = transform.right * speed;
        }

        protected override void OnTriggerEnter2D(Collider2D collision) {
            base.OnTriggerEnter2D(collision);

            if (collision.name.Equals("hpEffect") && !collision.CompareTag("Player")) {
                Destroy(gameObject);
            }
        }

        public void SetSpeed(float speed) {
            this.speed = speed;
            rb2d.velocity = transform.right * this.speed;
        }
    }
}
