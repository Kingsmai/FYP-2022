using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CraftsmanHero {
    public class UIController : MonoBehaviour {
        public GameObject inventoryUI;


        public GameObject console;
        public bool consoleOpened;
        [HideInInspector] public TMP_InputField consoleInputField;
        [HideInInspector] public TextMeshProUGUI consoleOutputField;

        public Stack<GameObject> CurrentOpenedUIs;
        bool showInventory;

        void Awake() {
            CurrentOpenedUIs = new Stack<GameObject>();
            consoleInputField = console.GetComponentInChildren<TMP_InputField>();
            consoleOutputField = console.GetComponentsInChildren<TextMeshProUGUI>()[0];
        }

        public void OnUIEsc(InputValue value) {
            Debug.Log("Esc Pressed");

            if (CurrentOpenedUIs.Count == 0) {
                Debug.Log("No UI opened.");
                Debug.Log("Pausing the game");
                return;
            }

            var currentOpenedUI = CurrentOpenedUIs.Pop();

            if (currentOpenedUI.Equals(inventoryUI)) {
                showInventory = false;
            }

            currentOpenedUI.SetActive(false);
        }

        public void OnToggleChatBox(InputValue value) {
            consoleOpened = !consoleOpened;
            console.SetActive(consoleOpened);

            if (consoleOpened) {
                CurrentOpenedUIs.Push(console);
                consoleInputField.Select();
            }

            if (!consoleOpened && CurrentOpenedUIs.Peek().Equals(console)) {
                CurrentOpenedUIs.Pop();
            }
        }

        public void OnToggleInventory(InputValue value) {
            if (consoleOpened) {
                return;
            }

            Debug.Log("Toggled Inventory");
            showInventory = !showInventory;
            inventoryUI.SetActive(showInventory);

            if (showInventory) {
                CurrentOpenedUIs.Push(inventoryUI);
            }

            if (!showInventory && CurrentOpenedUIs.Peek().Equals(inventoryUI)) {
                CurrentOpenedUIs.Pop();
            }
        }

        public void ConsoleLog(string msg, string speaker = "SYSTEM") {
            consoleOutputField.text += "\n";

            if (speaker != "") {
                consoleOutputField.text += $"[{speaker}]: {msg}";
                Debug.Log(msg);
            }
            else if (speaker == "ERROR") {
                Debug.LogError(msg);
            }
            else {
                consoleOutputField.text += msg;
                Debug.Log(msg);
            }
        }
    }
}
