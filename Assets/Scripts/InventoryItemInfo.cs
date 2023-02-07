using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    [Serializable]
    public class InventoryItemInfo {
        public delegate void InventoryItemInfoEventHandler();

        public event InventoryItemInfoEventHandler OnItemAmountChanged;

        public GameItemScriptableObject gameItem;

        [SerializeField] int amount;

        public int Amount {
            get { return amount; }
            set {
                amount = value;
                OnItemAmountChanged?.Invoke();
            }
        }

        public bool randomDrop;
    }
}
