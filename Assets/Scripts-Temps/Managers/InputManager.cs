using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

namespace CraftsmanHero {
    public class InputManager : Singleton<InputManager> {
        public PlayerControl Input;

        public Vector3 MouseDirection { get; private set; } = Vector3.zero;

        protected override void Awake() {
            base.Awake();
            Input = new PlayerControl();
        }

        private void Update() {
        }

        private void OnEnable() {
            Input.Enable();
        }

        private void OnDisable() {
            Input.Disable();
        }

        public float GetMouseAngle(Vector3 origin) {
            origin.z = Camera.main.transform.position.z;
            Vector2 mouseScreenPosition = Input.Player.Look.ReadValue<Vector2>();
            Vector3 mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
            Vector3 mouseDirection = (mouseWorldPosition - origin).normalized;
            float angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
            return angle;
        }

        public Vector2 GetMovementDirection() {
            return Input.Player.Move.ReadValue<Vector2>();
        }
    }
}
