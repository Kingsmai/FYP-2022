using UnityEngine;

namespace CraftsmanHero {
    public class Helper : MonoBehaviour {
        public static GameObject getChildGameObject(GameObject fromGameObject, string withName) {
            var transforms = fromGameObject.GetComponentsInChildren<Transform>();

            foreach (var t in transforms)
                if (t.gameObject.name == withName) {
                    return t.gameObject;
                }

            return null;
        }
    }
}
