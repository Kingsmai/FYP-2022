using System;
using UnityEngine;

namespace CraftsmanHero {
    public class Mobs : Entity {
        bool isFacingRight;

        public override void Attack() {
            throw new NotImplementedException();
        }

        public override void GetDamage(int damageAmount, Vector3 position) {
            if (position.x > transform.position.x && !isFacingRight) {
                transform.Rotate(0, 180f, 0);
                HealthEffectParent.transform.Rotate(0, 180f, 0);
                isFacingRight = true;
            }
            else if (position.x < transform.position.x && isFacingRight) {
                transform.Rotate(0, 180f, 0);
                HealthEffectParent.transform.Rotate(0, 180f, 0);
                isFacingRight = false;
            }

            base.GetDamage(damageAmount, position);
        }
    }
}
