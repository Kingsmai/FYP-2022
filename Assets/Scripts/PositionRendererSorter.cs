using UnityEngine;

namespace CraftsmanHero {
    public class PositionRendererSorter : MonoBehaviour {
        public SpriteRenderer skinRenderer;
        public SpriteRenderer weaponRenderer;

        private void Awake() {
            skinRenderer = Helper.getChildGameObject(gameObject, "body")?.GetComponent<SpriteRenderer>();
            weaponRenderer = Helper.getChildGameObject(gameObject, "weapon")?.GetComponent<SpriteRenderer>();
        }

        private void OnTriggerEnter2D(Collider2D collision) {
            Debug.Log(collision.gameObject.name);
        }
        //// Place this component on a game object that has
        //// a renderer component, and drag its parent object
        //// (if any, the game object that moves position)
        //// to the ParentTransform variable.

        //public int offset = 0;
        //public bool refreshOnUpdate = true;

        //private float _timer;
        //private float _timerMax = .1f; // 100 milliseconds

        //private void LateUpdate() {
        //    _timer -= Time.deltaTime;
        //    if (_timer < 0) {
        //        // Improve performance, refresh every 100 ms
        //        _timer = _timerMax;
        //        transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.y + offset);
        //    }
        //    // Prevent performance loss when the object is a static object
        //    if (!refreshOnUpdate) {
        //        Destroy(this);
        //    }
        //}
    }
}
