using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class SwordBullets : Bullets {
        private float fadeDuration = 1;
        private float _elapsedTime = 0;

        private void Update() {
            _elapsedTime += Time.deltaTime;
            float alpha = Mathf.Lerp(1, 0, _elapsedTime / fadeDuration);
            base._spriteRenderer.color = new Color(255, 255, 255, alpha);
            if (_elapsedTime >= fadeDuration) {
                Destroy(gameObject);
            }
        }
    }
}
