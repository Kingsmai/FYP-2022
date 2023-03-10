using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace CraftsmanHero {
    public class HotBar : MonoBehaviour {
        public GameObject hotBarSlotPrefab;
        [FormerlySerializedAs("hotBarSlots")] public GameObject hotBarSlotsParents;
        public List<HotBarSlot> hotBarSlots;

        [SerializeField] int currentSelectedSlot = 0;

        void Awake() {
            for (var i = 0; i < 9; i++) {
                hotBarSlots.Add(Instantiate(hotBarSlotPrefab, hotBarSlotsParents.transform, false).GetComponent<HotBarSlot>());
            }

            hotBarSlots[currentSelectedSlot].Select();
        }

        public void OnSelectHotBar(InputValue value) {
            var v = value.Get<float>();

            if (v == 0) return;

            if (v < 120) {
                hotBarSlots[currentSelectedSlot].Deselect();
                currentSelectedSlot = (currentSelectedSlot + 1) % hotBarSlots.Count;
                hotBarSlots[currentSelectedSlot].Select();
            }
            else {
                hotBarSlots[currentSelectedSlot].Deselect();
                currentSelectedSlot = (currentSelectedSlot - 1 + hotBarSlots.Count) % hotBarSlots.Count;
                hotBarSlots[currentSelectedSlot].Select();
            }
            
            //TODO: Update player handheld
        }
    }
}
