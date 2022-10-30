using UnityEngine;

namespace Entities.CombatHit
{
    public class HurtBox : MonoBehaviour, IHurtBox
    {
        [SerializeField] private bool isActive = true;
        [SerializeField] private GameObject owner;

        public bool Active => isActive;
        public GameObject Owner => owner;
        public Transform Transform => transform;
        public IHurtResponder HurtResponder { get; set; }

        public bool CheckHit(HitData data)
        {
            if (HurtResponder == null) Debug.LogError("no responder");
            return true;
        }
    }
}