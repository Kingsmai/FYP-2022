using UnityEngine;

namespace CraftsmanHero {
    public class SwordBullets : Bullets {
        float _elapsedTime;
        readonly float fadeDuration = 1;

        void Update() {
            _elapsedTime += Time.deltaTime;
            var alpha = Mathf.Lerp(1, 0, _elapsedTime / fadeDuration);
            _spriteRenderer.color = new Color(255, 255, 255, alpha);

            if (_elapsedTime >= fadeDuration) {
                Destroy(gameObject);
            }
        }
    }
}
