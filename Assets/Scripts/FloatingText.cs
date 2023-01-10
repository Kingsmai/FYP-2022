using TMPro;
using UnityEngine;

namespace CraftsmanHero {
    public class FloatingText : MonoBehaviour {
        public Color textColor = new Color(255, 255, 255, 255);

        private enum DamageTextStatus {
            FadeIn,
            Hung,
            FadeOut
        }

        private TextMeshPro text;
        private int _currentDamage;

        // FontSize Related
        private const float INIT_FONT_SIZE = 4f;
        private const float HUNG_FONT_SIZE = 8f;
        private const float END_FONT_SIZE = 10f;

        // Font Position Related
        private Vector3 _startPosition;
        private Vector3 _hungPosition;
        private Vector3 _endPosition;
        private float _moveDistance = 1f;
        private Vector3 offset = Vector3.up;

        // Timer Related
        private const float STATUS_DURATION = .5f; // Duration for each status
        private float _timeElapsed = 0;

        private DamageTextStatus _status;

        private void Awake() {
            text = GetComponent<TextMeshPro>();
            _status = DamageTextStatus.FadeIn;
            text.text = _currentDamage.ToString();
            text.color = textColor;
        }

        private void Update() {
            _timeElapsed += Time.deltaTime;
            float t = _timeElapsed / STATUS_DURATION;
            _startPosition = transform.parent.parent.position + offset;
            _hungPosition = _startPosition + Vector3.up;
            _endPosition = _hungPosition + Vector3.up * 2;
            switch (_status) {
                case DamageTextStatus.FadeIn:
                    if (_timeElapsed < STATUS_DURATION) {
                        // 从中间往上升，字慢慢放大
                        text.fontSize = Mathf.Lerp(INIT_FONT_SIZE, HUNG_FONT_SIZE, t);
                        transform.position = Vector3.Lerp(_startPosition, _hungPosition, t);
                    } else {
                        text.fontSize = HUNG_FONT_SIZE;
                        _timeElapsed = 0;
                        _status = DamageTextStatus.Hung;
                    }
                    break;
                case DamageTextStatus.Hung:
                    if (_timeElapsed > STATUS_DURATION) {
                        _timeElapsed = 0;
                        _endPosition = _hungPosition + Vector3.up * _moveDistance;
                        _status = DamageTextStatus.FadeOut;
                    }
                    break;
                case DamageTextStatus.FadeOut:
                    if (_timeElapsed < STATUS_DURATION) {
                        // 往上升，字放大，淡出
                        float alpha = Mathf.Lerp(1, .5f, t);
                        text.fontSize = Mathf.Lerp(HUNG_FONT_SIZE, END_FONT_SIZE, t);
                        transform.position = Vector3.Lerp(_hungPosition, _endPosition, t);
                        textColor.a = alpha;
                        text.color = textColor;
                    } else {
                        Destroy(gameObject);
                    }
                    break;
                default:
                    break;
            }
        }

        public void UpdateText(int damage) {
            _currentDamage += damage;
            if (_status == DamageTextStatus.Hung) {
                _timeElapsed = 0;
            }
            text.text = _currentDamage.ToString();
        }
    }
}
