using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CraftsmanHero {
    public class DebugController : MonoBehaviour {
        GameItemScriptableObject[] gameItems;

        bool showConsole;

        public List<object> commandList;

        public GameObject console;
        TMP_InputField inputField;
        TextMeshProUGUI outputField;

        public void OnToggleChatBox(InputValue value) {
            showConsole = !showConsole;
            console.SetActive(showConsole);

            if (showConsole) {
                inputField.Select();
            }
        }

        public void OnReturn(InputValue value) {
            if (showConsole) {
                HandleInput(inputField.text);
                inputField.text = "";
            }
        }

        void Awake() {
            // Initialize game items
            gameItems = Resources.LoadAll<GameItemScriptableObject>("GameItems/");

            commandList = new List<object>() {
                // Hello world
                new DebugCommand("hello_world", "The first command of this game.", "hello_world", () => { Log("Hello, world"); }),

                // Modify Player Gold
                new DebugCommand<string[]>("gold", "Modify Gold", "gold <opt> <prop>", (props) => {
                    var option = props[1];
                    var prop = props[2];

                    if (!int.TryParse(prop, out var goldAmount)) {
                        var errorMsg = $"Unable to recognize gold amount [{prop}] to [{option}]";
                        Log(errorMsg, "ERROR");
                        Debug.LogError(errorMsg);
                        return;
                    }

                    switch (option) {
                        case "set":
                            Log($"Set money to {goldAmount}");
                            GameManager.Instance.SetGold(goldAmount);
                            break;
                        case "add":
                            Log($"Add {goldAmount} money");
                            GameManager.Instance.AddGold(goldAmount);
                            break;
                        case "sub":
                            Log($"Decreased {goldAmount} money");
                            GameManager.Instance.SubGold(goldAmount);
                            break;
                        default:
                            var errorMsg = $"Invalid option [{option}]";
                            Log(errorMsg);
                            Debug.LogError(errorMsg);
                            break;
                    }
                }),

                // Command Help
                new DebugCommand("help", "Show a list of commands", "help", () => {
                    for (int i = 0; i < commandList.Count; i++) {
                        var command = commandList[i] as DebugCommandBase;
                        Log($"{command.CommandFormat} - {command.CommandDescription}", "");
                    }
                }),

                // 20230307 Give Player items
                new DebugCommand<string[]>("give", "Give player specific item", "give <item_id> [amount]", props => {
                    var itemId = props[1];
                    // give 1 item by default
                    var amount = props[2] == null ? "1" : props[2];
                    int amt;

                    if (!int.TryParse(amount, out amt)) {
                        var errorMsg = $"Invalid amount properties [{amount}], expected integer";
                        Log(errorMsg, "ERROR");
                        Debug.LogError(errorMsg);
                        return;
                    }

                    var hasFound = false;

                    foreach (var gameItem in gameItems) {
                        if (gameItem.itemId.Equals(itemId)) {
                            hasFound = true;
                            break;
                        }
                    }

                    if (hasFound) {
                        Log($"Given {amount}x {itemId} to player.");
                    }
                    else {
                        var errorMsg = $"Unable to find game item with itemId: {itemId}.";
                        Log(errorMsg, "ERROR");
                        Debug.LogError(errorMsg);
                    }
                }),
                
                // 20230308 Modify Player Stats
                new DebugCommand<string[]>("stats", "Modify player stats", "stats <stat> <option> <properties>", props => {
                    if (props.Length < 4) {
                        var errorMsg = $"Insufficient arguments: stats requires 3 arguments, but only {props.Length - 1} were provided";
                        Log(errorMsg, "ERROR");
                        Debug.LogError(errorMsg);
                    }
                    var stat = props[1];
                    var option = props[2];
                    var properties = props[3];
                })
            };

            inputField = console.GetComponentInChildren<TMP_InputField>();
            outputField = console.GetComponentsInChildren<TextMeshProUGUI>()[0];
        }

        void HandleInput(string input) {
            if (input.Trim().StartsWith('/')) {
                string[] properties = input.Split(' ');

                // Command
                for (int i = 0; i < commandList.Count; i++) {
                    DebugCommandBase commandBase = commandList[i] as DebugCommandBase;

                    if (input.Contains(commandBase.CommandId)) {
                        DebugCommandBase command;

                        if ((command = commandList[i] as DebugCommand) != null) {
                            (command as DebugCommand).Invoke();
                        }
                        else if ((command = commandList[i] as DebugCommand<string[]>) != null) {
                            (command as DebugCommand<string[]>).Invoke(properties);
                        }

                        Debug.Log(command);
                    }
                }
            }
            else {
                // Normal Text
                // TODO: Need to change player name to match the player name
                Log(input, "Player");
            }
        }

        void Log(string msg, string speaker = "SYSTEM") {
            outputField.text += "\n";

            if (speaker != "") {
                outputField.text += $"[{speaker}]: {msg}";
            }
            else {
                outputField.text += msg;
            }
        }
    }
}
