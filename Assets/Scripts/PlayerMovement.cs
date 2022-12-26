using UnityEngine;

namespace CraftsmanHero {
    public class PlayerMovement : MonoBehaviour {
        private Rigidbody2D rb2d;
        private Animator animator;
        private SpriteRenderer spriteRenderer;

        [Range(1f, 10f)]
        public float MoveSpeed = 7.5f;
        private Vector2 move;

        // TODO: 应该需要重构到 Attack 类中
        [Space]
        private GameObject _hand;
        public GameObject swordBullet;
        private bool isSecondHit;
        public Transform bulletParent;

        private void Awake() {
            rb2d = GetComponent<Rigidbody2D>();
            animator = GetComponentInChildren<Animator>();
            spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _hand = Helper.getChildGameObject(gameObject, "hand");

            GameManager.Instance.input.Player.Fire.performed += callback => {
                GameObject bullet = Instantiate(swordBullet, transform);
                bullet.transform.SetParent(bulletParent);
                bullet.GetComponent<SpriteRenderer>().flipY = isSecondHit;
                isSecondHit = !isSecondHit;
            };
        }

        void Update() {
            move = GameManager.Instance.input.Player.Move.ReadValue<Vector2>();

            // Toggle sprite run if it is moving
            animator.SetBool("run", move != Vector2.zero);
            if (move.x < 0) {
                spriteRenderer.flipX = true;
            }
            if (move.x > 0) {
                spriteRenderer.flipX = false;
            }

            // rotate hand
            Vector2 mouseScreenPosition = GameManager.Instance.input.Player.Look.ReadValue<Vector2>();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane
                ));
            float distance = Vector3.Distance(transform.position, mouseWorldPosition);

            Vector3 direction = (mouseWorldPosition - transform.position).normalized;

            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            _hand.transform.rotation = Quaternion.Euler(0, 0, angle);
        }

        private void FixedUpdate() {
            rb2d.velocity = move * MoveSpeed;
        }
    }
}
