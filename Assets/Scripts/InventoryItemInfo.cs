using System;
using UnityEngine;

namespace CraftsmanHero {
    [Serializable]
    public class InventoryItemInfo {
        public delegate void InventoryItemInfoEventHandler();

        public GameItemScriptableObject gameItem;

        [SerializeField] int amount;

        public bool randomDrop;

        public int Amount {
            get => amount;
            set {
                amount = value;
                OnItemAmountChanged?.Invoke();
            }
        }

        public event InventoryItemInfoEventHandler OnItemAmountChanged;
    }
}
