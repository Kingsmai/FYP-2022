using UnityEngine;

namespace CraftsmanHero {
    public class Weapons : MonoBehaviour {
        public WeaponsSO weaponData;

        Transform firepoint;
        Animator anim;


        [SerializeField] private float _cooldownTimer;

        private void Awake() {
            anim = GetComponentInChildren<Animator>();
            firepoint = new GameObject("firepoint").transform;
            firepoint.SetParent(transform, false);
            firepoint.localPosition = weaponData.weaponFirepoint;
        }

        public void Fire() {
            if (_cooldownTimer <= 0) {
                if (weaponData.bulletPrefab != null) {
                    Quaternion rotationOffset = Quaternion.Euler(0, 0, weaponData.GetAccuracy());
                    Bullets bullet = Instantiate(weaponData.bulletPrefab, firepoint.position, firepoint.rotation * rotationOffset).GetComponent<Bullets>();
                    bullet.Damage = weaponData.GetDamage();
                    if (!weaponData.isStaticBullet) {
                        FlyingBullets flyingBullets = (FlyingBullets)bullet;
                        flyingBullets.SetSpeed(weaponData.bulletSpeed);
                    }
                } else {
                    Debug.LogError("Bullet is NULL!");
                }
                // Gun Only
                anim.SetTrigger("fire");
                _cooldownTimer = weaponData.cooldown;
            }
        }

        private void Update() {
            _cooldownTimer -= Time.deltaTime;
        }
    }
}
