using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace CraftsmanHero {
    public class PlayerEvent : MonoBehaviour {
        public void OnMove(InputValue value) {
            var direction = value.Get<Vector2>();
            GameManager.Instance.player.Move(direction);
        }
    }
}
