using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero
{
    public class Weapons : MonoBehaviour
    {
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
                    Instantiate(weaponData.bulletPrefab, firepoint.position, firepoint.rotation);
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
