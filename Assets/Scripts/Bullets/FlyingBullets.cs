using UnityEngine;

namespace CraftsmanHero {
    public class FlyingBullets : Bullets {
        public float Speed = 5f;
        Rigidbody2D rb2d;

        void Awake() {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb2d.freezeRotation = true;
            rb2d.velocity = transform.right * Speed;
        }

        protected override void OnTriggerEnter2D(Collider2D collision) {
            base.OnTriggerEnter2D(collision);

            if (collision.name.Equals("hpEffect") && !collision.CompareTag("Player")) {
                Destroy(gameObject);
            }
        }

        public void SetSpeed(float speed) {
            Speed = speed;
            rb2d.velocity = transform.right * Speed;
        }
    }
}
