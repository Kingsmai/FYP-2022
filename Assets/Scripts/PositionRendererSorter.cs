using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

namespace CraftsmanHero {
    public class PositionRendererSorter : MonoBehaviour {
        // Place this component on a game object that has
        // a renderer component, and drag its parent object
        // (if any, the game object that moves position)
        // to the ParentTransform variable.

        public Transform ParentTransform;
        private int _sortingOrderBase = 10;
        public int offset = -1;
        public bool refreshOnUpdate = false;
        private Renderer _renderer;

        private float _timer;
        private float _timerMax = .1f; // 100 milliseconds

        private void Awake() {
            _renderer = GetComponent<Renderer>();
        }

        private void LateUpdate() {
            _timer -= Time.deltaTime;
            if (_timer < 0) {
                // Improve performance, refresh every 100 ms
                _timer = _timerMax;
                if (ParentTransform != null) {
                    _renderer.sortingOrder = (int)(_sortingOrderBase - ParentTransform.position.y - offset);
                } else {
                    _renderer.sortingOrder = (int)(_sortingOrderBase - transform.position.y - offset);
                }
            }
            // Prevent performance loss when the object is a static object
            if (!refreshOnUpdate) {
                Destroy(this);
            }
        }
    }
}
