using System;
using UnityEngine;

namespace CraftsmanHero {
    [CreateAssetMenu(fileName = "New Weapon", menuName = "Game Item/Weapon", order = 1)]
    public class WeaponsSO : GameItemSO {
        public GameObject weaponPrefab;

        public Vector2 weaponFirepoint;

        public GameObject bulletPrefab;

        [Header("�������")]
        public float cooldown;
        public float accuracy = 5f; // 0 Ϊ�׼

        [Header("�ӵ����")]
        public DamageRange damage;
        public float bulletSpeed = 20;
        public bool isStaticBullet;

        private void OnValidate() {
            if (damage.maximumDamage >= damage.baseDamage) {
                damage.isRandomDamage = true;
            } else if (damage.maximumDamage <= damage.baseDamage) {
                damage.isRandomDamage = false;
                damage.maximumDamage = damage.baseDamage;
            }

            if (isStaticBullet) {
                bulletSpeed = 0;
            }
        }

        public int GetDamage() {
            if (damage.isRandomDamage) {
                return UnityEngine.Random.Range(damage.baseDamage, damage.maximumDamage);
            } else {
                return damage.baseDamage;
            }
        }

        public float GetAccuracy() {
            if (accuracy != 0f) {
                return UnityEngine.Random.Range(-accuracy, accuracy);
            } else {
                return 0;
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
