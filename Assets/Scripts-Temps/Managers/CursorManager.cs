using UnityEngine;

public class CursorManager : Singleton<CursorManager> {
    [SerializeField] private Texture2D normalCursor;
    [SerializeField] private Texture2D aimCursor;

    private void Start() {
        AimCursor();
    }

    public void AimCursor() {
        Vector2 cursorHotspot = new Vector2(aimCursor.width / 2, aimCursor.height / 2);
        Cursor.SetCursor(aimCursor, cursorHotspot, CursorMode.Auto);
    }

    public void NormalCursor() {
        Vector2 cursorHotspot = Vector2.zero;
        Cursor.SetCursor(aimCursor, cursorHotspot, CursorMode.Auto);
    }
}