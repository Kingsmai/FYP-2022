using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace CraftsmanHero {
    public enum LogType {
        DEFAULT,
        INFO,
        WARN,
        ERROR,
        SUCCESS
    }

    public class DebugManager : Singleton<DebugManager> {
        public bool IS_DEBUG_MODE = false;

        [Space]

        private const int WINDOW_PADDING = 16;
        private const int BUTTON_PADDING = 8;
        private const int BUTTON_WIDTH = 128;
        private const int BUTTON_HEIGHT = 24;

        private List<DebugButton> debug_buttons;

        public TextMeshProUGUI OutputText;

        private string SkinId = "";

        public static void Log(object msg, LogType type = LogType.DEFAULT) {
            string logText = "";
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
                default:
                    break;
            }
            logText += msg.ToString();
            Instance.OutputText.text = logText;
        }

        private void Start() {
            debug_buttons = new List<DebugButton>();
            debug_buttons.Add(new DebugButton("Get Damage", () => {
                GameManager.Instance.CurrentPlayer.GetComponent<Player>().GetDamage(5);
            }));
            debug_buttons.Add(new DebugButton("Regeneration", () => {
                GameManager.Instance.CurrentPlayer.GetComponent<Player>().HealthRecover(10);
            }));
            debug_buttons.Add(new DebugButton("Change Skin", () => {
                int skinId;
                if (int.TryParse(SkinId, out skinId)) {
                    GameManager.Instance.CurrentPlayer.GetComponent<Player>().ChangeSkin(skinId);
                } else {
                    Log("Skin id parse failed!", LogType.ERROR);
                }
            }));
        }

        private void OnGUI() {
            if (IS_DEBUG_MODE) {
                // Create Buttons
                for (int i = 0; i < debug_buttons.Count; i++) {
                    DebugButton button = debug_buttons[i];
                    int posX = WINDOW_PADDING;
                    int posY = WINDOW_PADDING + BUTTON_PADDING * i + BUTTON_HEIGHT * i;
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

        private struct DebugButton {
            public string ButtonText;
            public delegate void OnClickHandler();
            public event OnClickHandler OnClick;

            public DebugButton(string text, OnClickHandler clickEv) {
                this.ButtonText = text;
                this.OnClick = clickEv;
            }

            public void ClickEv() {
                OnClick?.Invoke();
            }
        }
    }
}
