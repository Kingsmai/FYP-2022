using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero
{
    public class InputManager : Singleton<InputManager>
    {
        public PlayerControl Input;
        public float Angle { get; private set; }

        protected override void Awake() {
            base.Awake();
            Input = new PlayerControl();
        }

        private void Update() {
            SetMouseAngle();
        }

        private void OnEnable() {
            Input.Enable();
        }

        private void OnDisable() {
            Input.Disable();
        }

        public void SetMouseAngle() {
            Vector2 mouseScreenPosition = Input.Player.Look.ReadValue<Vector2>();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(new Vector3(
                mouseScreenPosition.x, mouseScreenPosition.y, Camera.main.nearClipPlane));
            Vector3 direction = (mouseWorldPosition - GameManager.Instance.CurrentPlayer.transform.position).normalized;
            Angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        }
    }
}
