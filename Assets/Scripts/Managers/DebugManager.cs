using System.Collections.Generic;
using CraftsmanHero;
using TMPro;
using UnityEngine;

public enum LogType {
    DEFAULT,
    INFO,
    WARN,
    ERROR,
    SUCCESS
}

public class DebugManager : Singleton<DebugManager> {
    [Space] const int WINDOW_PADDING = 16;

    const int BUTTON_PADDING = 8;
    const int BUTTON_WIDTH = 128;
    const int BUTTON_HEIGHT = 24;
    public bool IS_DEBUG_MODE;

    public TextMeshProUGUI OutputText;

    Player currentPlayer;

    List<DebugButton> debug_buttons;

    string SkinId = "";

    void Start() {
        currentPlayer = GameManager.Instance.CurrentPlayer;

        debug_buttons = new List<DebugButton>();
        debug_buttons.Add(new DebugButton("Get Damage", () => { currentPlayer.GetDamage(1, Vector3.zero); }));
        debug_buttons.Add(new DebugButton("Regeneration", () => { currentPlayer.HealthRecover(2); }));
        debug_buttons.Add(new DebugButton("Change Skin", () => { }));
        debug_buttons.Add(new DebugButton("Next Skin", () => { currentPlayer.NextSkin(); }));
    }

    void OnGUI() {
        if (IS_DEBUG_MODE) {
            // Create Buttons
            for (var i = 0; i < debug_buttons.Count; i++) {
                var button = debug_buttons[i];
                var posX = WINDOW_PADDING;
                var posY = WINDOW_PADDING + BUTTON_PADDING * i + BUTTON_HEIGHT * i;

                if (GUI.Button(new Rect(posX, posY, BUTTON_WIDTH, BUTTON_HEIGHT), button.ButtonText)) {
                    button.ClickEv();
                }

                if (button.ButtonText == "Change Skin") {
                    // Create Skin id Textbox
                    posX += BUTTON_WIDTH + BUTTON_PADDING;
                    SkinId = GUI.TextField(new Rect(posX, posY, BUTTON_WIDTH, BUTTON_HEIGHT), SkinId);
                }
            }
        }
    }

    public static void Log(object msg, LogType type = LogType.DEFAULT) {
        var logText = "";

        switch (type) {
            case LogType.DEFAULT:
            case LogType.INFO:
                logText += "INFO: ";
                break;
            case LogType.WARN:
                logText += "WARNING: ";
                break;
            case LogType.ERROR:
                logText += "ERROR: ";
                break;
            case LogType.SUCCESS:
                logText += "SUCCESS: ";
                break;
        }

        logText += msg.ToString();
        Instance.OutputText.text = logText;
    }

    struct DebugButton {
        public readonly string ButtonText;

        public delegate void OnClickHandler();

        public event OnClickHandler OnClick;

        public DebugButton(string text, OnClickHandler clickEv) {
            ButtonText = text;
            OnClick = clickEv;
        }

        public void ClickEv() {
            OnClick?.Invoke();
        }
    }
}
