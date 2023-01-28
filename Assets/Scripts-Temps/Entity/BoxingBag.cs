using UnityEngine;

namespace CraftsmanHero {
    public class BoxingBag : Mobs {
        Animator boxingBagAnim;

        protected override void Awake() {
            base.Awake();
            boxingBagAnim = GetComponentInChildren<Animator>();
        }

        public override void GetDamage(int damageAmount, Vector3 position) {
            base.GetDamage(damageAmount, position);
            boxingBagAnim.SetTrigger("hit");
        }
    }
}
