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
                        Log($"Unable to recognize gold amount [{prop}] to [{option}]", "ERROR");
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
                            Log($"Invalid option [{option}]", "ERROR");
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
                        Log($"Invalid amount properties [{amount}], expected integer", "ERROR");
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
                        Log($"Unable to find game item with itemId: {itemId}.", "ERROR");
                    }
                }),

                // 20230308 Modify Player Stats
                new DebugCommand<string[]>("stats", "Modify player stats", "stats <stat> <option> <properties>", props => {
                    if (props.Length < 4) {
                        Log($"Insufficient arguments: stats requires 3 arguments, but only {props.Length - 1} were provided", "ERROR");
                    }

                    var stat = props[1];
                    var option = props[2];
                    var properties = props[3];

                    switch (stat) {
                        case "health":
                            if (!int.TryParse(properties, out var healthAmount)) {
                                Log($"Invalid amount properties [{healthAmount}], expected integer", "ERROR");
                                return;
                            }

                            switch (option) {
                                case "max":
                                    GameManager.Instance.SetMaxHealth(healthAmount);
                                    Log($"Set player max health to {healthAmount}");
                                    break;
                                case "set":
                                    GameManager.Instance.SetHealth(healthAmount);
                                    Log($"Set player health to {healthAmount}");
                                    break;
                                case "regen":
                                    GameManager.Instance.RegenerateHealth(healthAmount);
                                    Log($"Regenerate {healthAmount} health");
                                    break;
                                case "damage":
                                    GameManager.Instance.TakeDamage(healthAmount);
                                    Log($"Take {healthAmount} damage");
                                    break;
                                default:
                                    Log($"Invalid option [{option}]");
                                    break;
                            }

                            break;
                        case "mana":
                            if (!int.TryParse(properties, out var manaAmount)) {
                                Log($"Invalid amount properties [{manaAmount}], expected integer", "ERROR");
                                return;
                            }

                            switch (option) {
                                case "max":
                                    GameManager.Instance.SetMaxMana(manaAmount);
                                    Log($"Set player max mana to {manaAmount}");
                                    break;
                                case "set":
                                    GameManager.Instance.SetMana(manaAmount);
                                    Log($"Set player mana to {manaAmount}");
                                    break;
                                case "regen":
                                    GameManager.Instance.RegenerateMana(manaAmount);
                                    Log($"Regenerate {manaAmount} mana");
                                    break;
                                case "damage":
                                    GameManager.Instance.SpendMana(manaAmount);
                                    Log($"Spend {manaAmount} mana");
                                    break;
                                default:
                                    Log($"Invalid option [{option}]");
                                    break;
                            }

                            break;
                        default:
                            Log($"Invalid stat [{stat}]");
                            break;
                    }
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
                Debug.Log(msg);
            }
            else if (speaker == "ERROR") {
                Debug.LogError(msg);
            }
            else {
                outputField.text += msg;
                Debug.Log(msg);
            }
        }
    }
}
