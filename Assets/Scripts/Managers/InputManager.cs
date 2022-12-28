using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class InputManager : Singleton<InputManager> {
        public PlayerControl Input;

        public Vector3 MouseDirection { get; private set; } = Vector3.zero;
        public float MouseAngle { get; private set; }

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
            Vector3 mouseScreenDirection = Input.Player.Look.ReadValue<Vector2>();
            MouseDirection = new Vector3(
               mouseScreenDirection.x - Screen.width / 2,
               mouseScreenDirection.y - Screen.height / 2,
               0).normalized;
            MouseAngle = Mathf.Atan2(MouseDirection.y, MouseDirection.x) * Mathf.Rad2Deg;
        }
    }
}
