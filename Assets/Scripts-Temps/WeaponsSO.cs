using System;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Weapon", menuName = "ScriptableObjects/Weapon", order = 1)]
    public class WeaponsSO : ScriptableObject {
        public string weaponId = "new_weapon";
        public string weaponName_cn = "ÐÂÎäÆ÷";
        public GameObject weaponPrefab;

        public Vector2 weaponFirepoint;

        public GameObject bulletPrefab;

        public float cooldown;

        [Space]
        public DamageRange damage;

        private void OnValidate() {
            if (damage.maximumDamage >= damage.baseDamage) {
                damage.isRandomDamage = true;
            } else if (damage.maximumDamage <= damage.baseDamage) {
                damage.isRandomDamage = false;
                damage.maximumDamage = damage.baseDamage;
            }
        }

        public int GetDamage() {
            if (damage.isRandomDamage) {
                return UnityEngine.Random.Range(damage.baseDamage, damage.maximumDamage);
            } else {
                return damage.baseDamage;
            }
        }
    }

    [Serializable]
    public class DamageRange {
        public int baseDamage = 1;
        public bool isRandomDamage;
        public int maximumDamage = 1;
    }
}
