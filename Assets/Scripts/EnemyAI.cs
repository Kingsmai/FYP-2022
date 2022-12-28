using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class EnemyAI : MonoBehaviour {
        Animator _anim;
        SpriteRenderer _spriteRenderer;

        private void Awake() {
            _anim = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
        }

        public void GetDamage(int damageAmount, Vector3 position) {
            _spriteRenderer.flipX = (position.x > transform.position.x);
            _anim.SetTrigger("Hit");
        }
    }
}
