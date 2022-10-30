using UnityEngine;

namespace Entities.CombatHit
{
    public class HitData
    {
        public float Damage; // TODO: Change damage to specific class
        public Vector3 HitPoint;
        public Vector3 HitNormal;
        public IHurtBox HurtBox;
        public IHitDetector HitDetector;

        public bool Validate()
        {
            if (HurtBox == null) return false;
            if (!HurtBox.CheckHit(this)) return false;
            if (HurtBox.HurtResponder == null || !HurtBox.HurtResponder.CheckHit(this)) return false;
            return HitDetector.HitResponder == null || HitDetector.HitResponder.CheckHit(this);
        }
    }
    
    public interface IHitResponder 
    {
        int Damage { get; }
        public bool CheckHit(HitData data);
        public void Response(HitData data);
    }
    
    public interface IHitDetector 
    {
        public IHitResponder HitResponder { get; set; }
        public void CheckHit();
    }
    
    public interface IHurtResponder
    {
        public bool CheckHit(HitData data);
        /// <summary>
        /// Process hit to this object
        /// </summary>
        /// <param name="data"></param>
        public void Response(HitData data);
    }
    
    public interface IHurtBox 
    {
        public bool Active { get; }
        public GameObject Owner { get; }
        public Transform Transform { get; }
        public IHurtResponder HurtResponder { get; set; }
        /// <summary>
        ///  Someone hit this object
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public bool CheckHit(HitData data);
    }
}
