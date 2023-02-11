using UnityEngine;

public class InputManager : Singleton<InputManager> {
    public delegate void InputEventHandler();
    public event InputEventHandler OnCancelPressed;
    public event InputEventHandler OnInventoryPressed;

    public delegate void InputScrollEventHandler(bool isScrollingDown);

    public event InputScrollEventHandler OnScroll;

    public PlayerControl Input;

    float scroll;

    public float Scroll {
        get => scroll;
        set {
            scroll = value;
            var isScrollingDown = value < 0;
            OnScroll?.Invoke(isScrollingDown);
        }
    }

    protected override void Awake() {
        base.Awake();
        Input = new PlayerControl();

        Input.Player.Scroll.performed += context => { Scroll = context.ReadValue<float>(); };

        Input.UI.Cancel.performed += context => { OnCancelPressed?.Invoke(); };

        Input.UI.OpenInventory.performed += context => { OnInventoryPressed?.Invoke(); };
    }

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
