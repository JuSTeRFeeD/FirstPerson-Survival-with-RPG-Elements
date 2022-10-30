using Entities.CombatHit;
using UnityEngine;

namespace Entities.HittableObjects
{
    [RequireComponent(typeof(Health))]
    public class Barrel : MonoBehaviour, IHurtResponder
    {
        [SerializeField] private HurtBox hurtBox;
        private Health _health;

        private void Start()
        {
            _health = GetComponent<Health>();
            hurtBox.HurtResponder = this;
        }
        
        public bool CheckHit(HitData data)
        {
            return true;
        }

        public void Response(HitData data)
        {
            _health.TakeDamage(data.Damage);
        }
    }
}
