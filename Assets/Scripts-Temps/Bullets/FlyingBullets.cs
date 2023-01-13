using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero
{
    public class FlyingBullets : Bullets
    {
        private Rigidbody2D rb2d;
        public float Speed = 5f;

        private void Awake() {
            rb2d = GetComponent<Rigidbody2D>();
            rb2d.velocity = transform.right * Speed;
        }
    }
}
