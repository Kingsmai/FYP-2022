using UnityEngine;

namespace CraftsmanHero {
    public class PlayerMovement : MonoBehaviour {
        private PlayerControl input;
        private Rigidbody2D rb2d;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        [Range(1f, 10f)]
        public float MoveSpeed = 7.5f;
        private Vector2 move;

        private void Awake() {
            input = new PlayerControl();
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();

            input.Player.Fire.performed += callback => {
                Debug.Log("Fired!");
            };
        }

        void Update() {
            move = input.Player.Move.ReadValue<Vector2>();

            // Toggle sprite run if it is moving
            animator.SetBool("run", move != Vector2.zero);
            if (move.x < 0) {
                spriteRenderer.flipX = true;
            }
            if (move.x > 0) {
                spriteRenderer.flipX = false;
            }
        }

        private void FixedUpdate() {
            rb2d.velocity = move * MoveSpeed;
        }

        private void OnEnable() {
            input.Enable();
        }

        private void OnDisable() {
            input.Disable();
        }
    }
}
