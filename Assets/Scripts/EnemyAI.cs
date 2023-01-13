using UnityEngine;

namespace CraftsmanHero {
    public class EnemyAI : MonoBehaviour {
        Animator _anim;
        SpriteRenderer _spriteRenderer;
        private Transform _damageEffectParent;
        FloatingText _currentDamageText;

        private void Awake() {
            _anim = GetComponentInChildren<Animator>();
            _spriteRenderer = GetComponentInChildren<SpriteRenderer>();
            _damageEffectParent = new GameObject("damage").transform;
            _damageEffectParent.parent = transform;
        }

        public void GetDamage(int damageAmount, Vector3 position) {
            _spriteRenderer.flipX = (position.x > transform.position.x);
            _anim.SetTrigger("Hit");
            if (_currentDamageText == null) {
                _currentDamageText = Instantiate(TextsManager.Instance.damageText, _damageEffectParent).GetComponent<FloatingText>();
            }
            _currentDamageText.UpdateText(damageAmount);
        }
    }
}
