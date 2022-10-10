using UnityEngine;

namespace Weapons
{
    public class BaseWeapon : MonoBehaviour
    {
        [Tooltip("In Seconds")]
        public float reloadTime = 1.5f;
        
        public delegate void AttackEvent();
        public AttackEvent OnShoot;
    }
}
