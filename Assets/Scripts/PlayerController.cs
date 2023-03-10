using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class PlayerController : MonoBehaviour {
        Rigidbody2D rb2d;
        Animator anim;

        float movementSpeed = 7.5f;

        public float MovementSpeed {
            get => movementSpeed;
            set { movementSpeed = value; }
        }

        void Awake() {
            rb2d = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        public void Move(Vector2 dir) {
            // Debug.Log($"Move player towards direction: {dir * movementSpeed}");
            rb2d.velocity = dir * movementSpeed;

            // Play animation
            anim.SetBool("running", rb2d.velocity != Vector2.zero);
        }

        public void Dead() {
            anim.SetTrigger("dead");
        }

        public void Revive() {
            anim.SetTrigger("revive");
        }
    }
}
