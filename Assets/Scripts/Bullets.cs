using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class Bullets : MonoBehaviour {
        private SpriteRenderer _spriteRenderer;

        private float fadeDuration = 1;
        private float _elapsedTime = 0;

        private void Awake() {
            _spriteRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update() {
            _elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, _elapsedTime / fadeDuration);
            _spriteRenderer.color = new Color(255, 255, 255, alpha);
            if (_elapsedTime >= fadeDuration) {
                Destroy(gameObject);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            if (collision.gameObject.CompareTag("Entity")) {
                EntityStrobe strobeHelper = collision.GetComponent<EntityStrobe>();
                strobeHelper.StrobeColor(1, Color.white);
            }
        }
    }
}
