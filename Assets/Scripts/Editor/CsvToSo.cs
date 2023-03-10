using System.IO;
using UnityEditor;
using UnityEngine;

namespace CraftsmanHero {
    public class CsvToSo {
        static readonly string gameItemCsvPath = "/Editor/csv/game_item.csv";

        static readonly string gameItemSoPath = "Assets/Resources/GameItems/";

        [MenuItem("Utilities/Generate Game Items")]
        public static void GenerateGameItems() {
            Debug.Log("Generating Items...");

            var allLines = File.ReadAllLines(Application.dataPath + gameItemCsvPath);
            var headers = allLines[0].Split(',');

            Debug.Log($"{gameItemCsvPath} contains these columns:");

            foreach (var header in headers) Debug.Log(header);

            for (var i = 1; i < allLines.Length; i++) {
                var fields = allLines[i].Split(',');

                if (fields.Length != headers.Length) {
                    Debug.Log($"{allLines[i]} does not have {headers.Length} values");
                    return;
                }

                var gameItem = ScriptableObject.CreateInstance<GameItemScriptableObject>();
                gameItem.itemId = fields[0];
                gameItem.itemName = fields[1];
                gameItem.itemIcon = Resources.Load<Sprite>($"Icons/{gameItem.itemId}");
                gameItem.itemPrice = int.Parse(fields[3]);
                gameItem.description = fields[4];
                gameItem.maxStackCount = fields[5].Equals("") ? 64 : int.Parse(fields[5]);

                AssetDatabase.CreateAsset(gameItem, gameItemSoPath + gameItem.itemId + ".asset");
            }

            AssetDatabase.SaveAssets();
        }
    }
}
