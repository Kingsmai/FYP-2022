using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CraftsmanHero
{
    public class Weapons : MonoBehaviour
    {
        public GameObject bulletPrefab;
        public Transform firepoint;

        // Gun Only
        Animator anim;

        public float cooldown;
        [SerializeField] private float _cooldownTimer;

        private void Awake() {
            anim = GetComponentInChildren<Animator>();
        }

        public void Fire() {
            if (_cooldownTimer <= 0) {
                if (bulletPrefab != null) {
                    Instantiate(bulletPrefab, firepoint.position, firepoint.rotation);
                } else {
                    Debug.LogError("Bullet is NULL!");
                }
                // Gun Only
                anim.SetTrigger("fire");
                _cooldownTimer = cooldown;
            }
        }

        private void Update() {
            _cooldownTimer -= Time.deltaTime;
        }
    }
}
