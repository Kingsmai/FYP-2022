using UnityEngine;

public class InputManager : Singleton<InputManager> {
    public PlayerControl Input;

    public Vector3 MouseDirection { get; } = Vector3.zero;

    protected override void Awake() {
        base.Awake();
        Input = new PlayerControl();
    }

    void Update() { }

    void OnEnable() {
        Input.Enable();
    }

    void OnDisable() {
        Input.Disable();
    }

    public float GetMouseAngle(Vector3 origin) {
        origin.z = Camera.main.transform.position.z;
        var mouseScreenPosition = Input.Player.Look.ReadValue<Vector2>();
        var mouseWorldPosition = Camera.main.ScreenToWorldPoint(mouseScreenPosition);
        var mouseDirection = (mouseWorldPosition - origin).normalized;
        var angle = Mathf.Atan2(mouseDirection.y, mouseDirection.x) * Mathf.Rad2Deg;
        return angle;
    }

    public Vector2 GetMovementDirection() {
        return Input.Player.Move.ReadValue<Vector2>();
    }
}
