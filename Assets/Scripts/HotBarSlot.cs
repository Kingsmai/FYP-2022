using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CraftsmanHero
{
    public class HotBarSlot : MonoBehaviour {
        public GameObject selectIndicator;
        public Image gameItemIcon;

        void Awake() {
            selectIndicator.SetActive(false);
            gameItemIcon.gameObject.SetActive(false);
        }

        public void Select() {
            selectIndicator.SetActive(true);
        }

        public void Deselect() {
            selectIndicator.SetActive(false);
        }
    }
}
