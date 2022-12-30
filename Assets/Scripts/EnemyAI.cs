using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class EnemyAI : MonoBehaviour {
        Animator _anim;
        SpriteRenderer _spriteRenderer;
        private Transform _damageEffectParent;
        DamageText _currentDamageText;

        private void Awake() {
            _anim = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _damageEffectParent = Helper.getChildGameObject(gameObject, "damage_effect").transform;
        }

        public void GetDamage(int damageAmount, Vector3 position) {
            _spriteRenderer.flipX = (position.x > transform.position.x);
            _anim.SetTrigger("Hit");
            if (_currentDamageText == null) {
                _currentDamageText = Instantiate(FontsManager.Instance.damageText, _damageEffectParent).GetComponent<DamageText>();
            }
            _currentDamageText.UpdateText(damageAmount);
        }
    }
}
