using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CraftsmanHero {
    public class DebugController : MonoBehaviour {
        bool showConsole;

        public static DebugCommand HELLO_WORLD;
        public static DebugCommand ROSEBUD;
        public static DebugCommand<string> SET_GOLD;
        public static DebugCommand<string, string> GOLD;
        public static DebugCommand HELP;

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
            HELLO_WORLD = new DebugCommand("hello_world", "The first command of this game.", "hello_world", () => { Log("Hello, world"); });

            ROSEBUD = new DebugCommand("rosebud", "Adds 1000 gold.", "rosebud", () => { Log("Added 1000 gold."); });

            SET_GOLD = new DebugCommand<string>("set_gold", "Sets the amounts of gold.", "set_gold <gold_amount>", prop => {
                if (!int.TryParse(prop, out var goldAmount)) {
                    Log($"Unable to recognize gold amount [{prop}]");
                    return;
                }

                Log($"Set money to {prop}.");
            });

            GOLD = new DebugCommand<string, string>("gold", "Modify Gold", "gold <opt> <prop>", (opt, prop) => {
                if (!int.TryParse(prop, out var goldAmount)) {
                    Log($"Unable to recognize gold amount [{prop}]");
                    return;
                }

                switch (opt) {
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
                        Log($"Invalid option [{goldAmount}]");
                        break;
                }
            });

            HELP = new DebugCommand("help", "Show a list of commands", "help", () => {
                for (int i = 0; i < commandList.Count; i++) {
                    var command = commandList[i] as DebugCommandBase;
                    Log($"{command.CommandFormat} - {command.CommandDescription}", "");
                }
            });

            commandList = new List<object>() {
                HELLO_WORLD,
                ROSEBUD,
                SET_GOLD,
                GOLD,
                HELP
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
                        else if ((command = commandList[i] as DebugCommand<string>) != null) {
                            (command as DebugCommand<string>).Invoke(properties[1]);
                        }
                        else if ((command = commandList[i] as DebugCommand<string, string>) != null) {
                            (command as DebugCommand<string, string>).Invoke(properties[1], properties[2]);
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
