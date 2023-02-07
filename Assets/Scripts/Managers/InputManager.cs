using UnityEngine;

public class InputManager : Singleton<InputManager> {
    public PlayerControl Input;

    public delegate void InputEventHandler();
    public delegate void InputScrollEventHandler(bool isScrollingDown);

    public event InputScrollEventHandler OnScroll;
    public event InputEventHandler OnCancelPressed;

    float scroll;

    public float Scroll {
        get { return scroll; }
        set {
            scroll = value;
            bool isScrollingDown = value < 0;
            OnScroll?.Invoke(isScrollingDown);
        }
    }

    protected override void Awake() {
        base.Awake();
        Input = new PlayerControl();

        Input.Player.Scroll.performed += context => {
            Scroll = context.ReadValue<float>();
        };

        Input.UI.Cancel.performed += context => {
            OnCancelPressed?.Invoke();
        };
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
