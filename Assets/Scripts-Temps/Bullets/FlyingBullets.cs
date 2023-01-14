using UnityEngine;

namespace CraftsmanHero {
    public class FlyingBullets : Bullets {
        private Rigidbody2D rb2d;
        public float Speed = 5f;

        private void Awake() {
            rb2d = gameObject.AddComponent<Rigidbody2D>();
            rb2d.collisionDetectionMode = CollisionDetectionMode2D.Continuous;
            rb2d.freezeRotation = true;
            rb2d.velocity = transform.right * Speed;
        }

        public void SetSpeed(float speed) {
            Speed = speed;
            rb2d.velocity = transform.right * Speed;
        }
    }
}
