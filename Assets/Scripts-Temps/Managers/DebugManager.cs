using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

namespace CraftsmanHero {
    public class DebugManager : Singleton<DebugManager> {
        public bool IS_DEBUG_MODE = false;

        [Space]

        private const int WINDOW_PADDING = 16;
        private const int BUTTON_PADDING = 8;
        private const int BUTTON_WIDTH = 128;
        private const int BUTTON_HEIGHT = 24;

        private List<DebugButton> debug_buttons;

        public TextMeshProUGUI OutputText;

        public int SkinId = 101;

        public static void Log(object msg) {
            Instance.OutputText.text = msg.ToString();
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
                GameManager.Instance.CurrentPlayer.GetComponent<Player>().ChangeSkin(SkinId);
            }));
        }

        private void OnGUI() {
            if (IS_DEBUG_MODE) {
                for (int i = 0; i < debug_buttons.Count; i++) {
                    DebugButton button = debug_buttons[i];
                    int posX = WINDOW_PADDING;
                    int posY = WINDOW_PADDING + BUTTON_PADDING * i + BUTTON_HEIGHT * i;
                    if (GUI.Button(new Rect(posX, posY, BUTTON_WIDTH, BUTTON_HEIGHT), button.ButtonText)) {
                        button.ClickEv();
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
