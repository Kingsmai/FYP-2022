using UnityEngine;

namespace CraftsmanHero {
    public class PlayerMovement : MonoBehaviour {
        private PlayerControl input;
        private Rigidbody2D rb2d;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        [Range(1f, 10f)]
        public float MoveSpeed = 10f;
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

        private void OnEnable() {
            input.Enable();
        }

        private void OnDisable() {
            input.Disable();
        }

        // Update is called once per frame
        void Update() {
            move = input.Player.Move.ReadValue<Vector2>();
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
    }
}
