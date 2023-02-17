using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace CraftsmanHero {
    /// <summary>
    /// 游戏实体所会掉落的游戏道具
    /// <para>掉落：当游戏实体被击杀 / 破坏， 随机掉落物品栏里的道具。包括保底掉落</para>
    /// <para>游戏货币：包括金币，蓝币等</para>
    /// </summary>
    public class EntityDeathDrop : MonoBehaviour {
        public int dropCoin = 5;
        public int dropExperience = 5;
        public List<EntityDropInfo> dropTable;

        Entity entity;
        
        void Awake() {
            entity = GetComponent<Entity>();
            entity.OnDead += DeathDrop;
        }

        public List<GameItemScriptableObject> GetDropItem() {
            List<GameItemScriptableObject> dropItems = new();

            foreach (var dropInfo in dropTable) {
                if (dropInfo.guaranteeDrop) {
                    // Guarantee drop 1
                    Debug.Log($"{gameObject.name} guarantee dropped {dropInfo.gameItem.ItemName}");
                    dropItems.Add(dropInfo.gameItem);

                    for (int i = 1; i <= dropInfo.guaranteeDropCount; i++) {
                        Debug.Log($"{gameObject.name} guarantee dropped {dropInfo.gameItem.ItemName}");
                        dropItems.Add(dropInfo.gameItem);
                    }
                }
                else {
                    // Drop with probability
                    for (int i = 0; i < dropInfo.maxDrop; i++) {
                        if (UnityEngine.Random.Range(0, 100) <= dropInfo.probability) {
                            Debug.Log($"{gameObject.name} dropped {dropInfo.gameItem.ItemName} with chance of {dropInfo.probability}%");
                            dropItems.Add(dropInfo.gameItem);
                        }
                    }
                }
            }
            return dropItems;
        }

        public void DeathDrop() {
            var itemToDrop = GetDropItem();

            foreach (var gameItem in itemToDrop) {
                Debug.Log($"{gameItem.name} spawned from {gameObject.name}");
                var item = Instantiate(GameManager.Instance.gameItemPrefab);
                item.transform.position = transform.position;
                var itemDisplay = item.GetComponent<GameItemDisplay>();
                itemDisplay.GameItemToDisplay = gameItem;
            }
        }

        [Serializable]
        public class EntityDropInfo {
            public GameItemScriptableObject gameItem;
            public float probability;
            public int maxDrop;
            public bool guaranteeDrop;
            public int guaranteeDropCount;
        }
    }
}
