using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        public Vector2 border;
        public float camHeight = 20f;

        private void LateUpdate() {
            transform.position = new Vector3(target.position.x, target.position.y, target.position.z - camHeight);
        }
    }
}
