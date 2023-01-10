using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero
{
    public class FlyingBullets : Bullets
    {
        private Rigidbody2D rb2d;
        private float _flyingSpeed = 5f;

        private void Awake() {
            rb2d = GetComponent<Rigidbody2D>();
        }

        private void FixedUpdate() {
            //rb2d.velocity = Vector3.right * _flyingSpeed;
        }
    }
}
