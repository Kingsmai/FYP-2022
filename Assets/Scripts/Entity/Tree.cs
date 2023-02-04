using UnityEngine;

namespace CraftsmanHero {
    public class Tree : Entity {
        [Header("特效")] public ParticleSystem LeavesParticle;

        public override void GetDamage(int damageAmount, Vector3 position) {
            base.GetDamage(damageAmount, position);
            LeavesParticle.Play();
        }
    }
}
