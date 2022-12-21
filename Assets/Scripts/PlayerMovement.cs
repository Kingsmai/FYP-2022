using UnityEngine;

namespace CraftsmanHero {
    public class PlayerMovement : MonoBehaviour {
        private PlayerControl input;

        private void Awake() {
            input = new PlayerControl();

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
            Vector2 move = input.Player.Move.ReadValue<Vector2>();
            if (move != Vector2.zero) {
                Debug.Log(move);
            }
        }
    }
}
