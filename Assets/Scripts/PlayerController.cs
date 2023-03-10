using UnityEngine;

namespace CraftsmanHero {
    public class PlayerController : MonoBehaviour {
        Animator anim;

        Rigidbody2D rb2d;

        public float MovementSpeed { get; set; } = 7.5f;

        void Awake() {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        public void Move(Vector2 dir) {
            // Debug.Log($"Move player towards direction: {dir * movementSpeed}");
            rb2d.velocity = dir * MovementSpeed;

            // Play animation
            anim.SetBool("running", rb2d.velocity != Vector2.zero);
        }

        public void Dead() {
            anim.SetTrigger("dead");
        }

        public void Revive() {
            anim.SetTrigger("revive");
        }
    }
}
