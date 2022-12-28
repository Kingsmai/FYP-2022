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

            InputManager.Instance.Input.Player.Fire.performed += callback => {
                Debug.Log(InputManager.Instance.MouseDirection);
                Vector3 bulletSpawnPosition = _hand.transform.position + new Vector3(
                    InputManager.Instance.MouseDirection.x, InputManager.Instance.MouseDirection.y, 0);
                Quaternion bulletSpawnRotation = Quaternion.Euler(0, 0, InputManager.Instance.MouseAngle);
                GameObject bullet = Instantiate(swordBullet, bulletSpawnPosition, bulletSpawnRotation);
                bullet.transform.SetParent(bulletParent);
                bullet.GetComponent<SpriteRenderer>().flipY = isSecondHit;
                isSecondHit = !isSecondHit;
            };

            // Register player to GameManager
            GameManager.Instance.SetCurrentPlayer(gameObject);
        }

        void Update() {
            move = InputManager.Instance.Input.Player.Move.ReadValue<Vector2>();

            // Toggle sprite run if it is moving
            animator.SetBool("run", move != Vector2.zero);

            spriteRenderer.flipX = (InputManager.Instance.MouseAngle > 90 || InputManager.Instance.MouseAngle < -90);

            // rotate hand
            _hand.transform.rotation = Quaternion.Euler(0, 0, InputManager.Instance.MouseAngle);
        }

        private void FixedUpdate() {
            rb2d.velocity = move * MoveSpeed;
        }
    }
}
