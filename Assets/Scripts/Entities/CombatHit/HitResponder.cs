using UnityEngine;

namespace Entities.CombatHit
{
    public class HitResponder : MonoBehaviour, IHitResponder
    {
        [SerializeField] private HitBox hitBox;
        public int Damage { get; } = 1;

        public delegate void OnHitResponse(HitData data);
        public OnHitResponse HitResponseEvent;

        private void Start()
        {
            hitBox.HitResponder = this;
        }

        public void DoAttack()
        {
            hitBox.CheckHit();
        }
        
        public bool CheckHit(HitData data)
        {
            return true;
        }

        public void Response(HitData data)
        {
            HitResponseEvent?.Invoke(data);
        }
    }
}
