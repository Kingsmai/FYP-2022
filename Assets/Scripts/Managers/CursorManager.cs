using UnityEngine;

public class CursorManager : Singleton<CursorManager> {
    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D aimCursor;
    [SerializeField] Texture2D selectCursor;

    void Start() {
        AimCursor();
    }

    public void AimCursor() {
        var cursorHotspot = new Vector2(aimCursor.width / 2, aimCursor.height / 2);
        Cursor.SetCursor(aimCursor, cursorHotspot, CursorMode.Auto);
    }

    public void NormalCursor() {
        var cursorHotspot = Vector2.zero;
        Cursor.SetCursor(aimCursor, cursorHotspot, CursorMode.Auto);
    }

    public void SelectCursor() {
        var cursorHotspot = Vector2.zero;
        Cursor.SetCursor(selectCursor, cursorHotspot, CursorMode.Auto);
    }
}
