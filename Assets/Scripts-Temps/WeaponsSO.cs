using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
    public class WeaponsSO : ScriptableObject {
        public string weaponId = "new_weapon";
        public string weaponName_cn = "ÐÂÎäÆ÷";
        public GameObject weaponPrefab;
        public GameObject bulletPrefab;
    }
}
