using UnityEngine;

namespace Entities.CombatHit
{
    public class HitBox : MonoBehaviour, IHitDetector
    {
        [SerializeField] private BoxCollider hitCollider;
        [SerializeField] private LayerMask layerMask;

        private readonly float _thickness = 0.025f;
        
        private IHitResponder _hitResponder;
        public IHitResponder HitResponder { get => _hitResponder; set => _hitResponder = value; }
        
        public void CheckHit()
        {
            var size = hitCollider.size;
            var lossyScale = transform.lossyScale;
            var scaledSize = new Vector3(
                size.x * lossyScale.x,
                size.y * lossyScale.y,
                size.z * lossyScale.z
            );

            var distance = scaledSize.y - _thickness;
            var direction = transform.up;
            var center = transform.TransformPoint(hitCollider.center);
            var start = center - direction * (distance / 2);
            var halfExtents = new Vector3(scaledSize.x, _thickness, scaledSize.z) / 2;
            var orientation = transform.rotation;

            var results = new RaycastHit[3];
            Physics.BoxCastNonAlloc(start, halfExtents, direction, results, orientation, distance, layerMask);
            foreach (var hit in results)
            {
                if (hit.collider == null) return;
                var hurtBox = hit.collider.GetComponent<IHurtBox>();
                if (hurtBox is not { Active: true }) continue;
                var hitData = new HitData
                {
                    Damage = _hitResponder?.Damage ?? 0,
                    HitPoint = hit.point,
                    HitNormal = hit.normal,
                    HurtBox = hurtBox,
                    HitDetector = this
                };

                if (!hitData.Validate()) continue;
                hitData.HitDetector.HitResponder?.Response(hitData);
                hitData.HurtBox.HurtResponder?.Response(hitData);
            }
        }
    }
}
