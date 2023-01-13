using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero {
    public class Helper : MonoBehaviour {
        public static GameObject getChildGameObject(GameObject fromGameObject, string withName) {
            Transform[] transforms = fromGameObject.GetComponentsInChildren<Transform>();
            foreach (Transform t in transforms) if (t.gameObject.name == withName) return t.gameObject;
            return null;
        }
    }
}
