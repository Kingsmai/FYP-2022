using UnityEngine;

namespace CraftsmanHero {
    public class Weapons : MonoBehaviour {
        public WeaponsScriptableObject weaponData;


        [SerializeField] float _cooldownTimer;
        Animator anim;

        Transform firepoint;

        void Awake() {
            anim = GetComponentInChildren<Animator>();
            firepoint = new GameObject("firepoint").transform;
            firepoint.SetParent(transform, false);
            firepoint.localPosition = weaponData.weaponFirePoint;
        }

        void Update() {
            _cooldownTimer -= Time.deltaTime;
        }

        public void Fire() {
            if (_cooldownTimer <= 0) {
                if (weaponData.bulletPrefab != null) {
                    var rotationOffset = Quaternion.Euler(0, 0, weaponData.GetAccuracy());
                    var bullet =
                        Instantiate(weaponData.bulletPrefab, firepoint.position, firepoint.rotation * rotationOffset)
                            .GetComponent<Bullets>();
                    bullet.Damage = weaponData.GetDamage();

                    if (!weaponData.isStaticBullet) {
                        var flyingBullets = (FlyingBullets)bullet;
                        flyingBullets.SetSpeed(weaponData.bulletSpeed);
                    }
                }
                else {
                    Debug.LogError("Bullet is NULL!");
                }

                // Gun Only
                anim.SetTrigger("fire");
                _cooldownTimer = weaponData.cooldown;
            }
        }
    }
}
