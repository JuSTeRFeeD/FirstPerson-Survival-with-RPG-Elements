using Entities.CombatHit;
using Inventory;
using Managers;
using UnityEngine;
using CallbackContext = UnityEngine.InputSystem.InputAction.CallbackContext;

namespace Entities.Player.Combat
{
    [RequireComponent(typeof(Animator))]
    public class PlayerCombat : MonoBehaviour
    {
        [SerializeField] private Equipment playerEquipment;
        [Space] 
        [SerializeField] private HitResponder meleeHitResponder;
        [Space]
        [SerializeField] private Vector3 firstPointAttackRange;
        [SerializeField] private Vector3 secondPointAttackRange;
        
        private Animator _animator;
        
        private static readonly int TwoHandsBaseAttack1 = Animator.StringToHash("Two Hands Base Attack 1");
        private static readonly int TwoHandsBaseAttack2 = Animator.StringToHash("Two Hands Base Attack 2");
        private static readonly int TwoHandsBaseAttack3 = Animator.StringToHash("Two Hands Base Attack 3");

        private void Start()
        {
            _animator = GetComponent<Animator>();
            
            var combat = GameManager.GameControls.Combat;
            combat.LeftMouse.performed += HandleBaseAttack;
            // combat.LeftMouse.triggered // HOLDING

            meleeHitResponder.HitResponseEvent += HandleHitResponseEvent;
        }

        private void HandleBaseAttack(CallbackContext ctx)
        {
            // var left = playerEquipment.GetItemInSlot(EquipmentSlotType.LeftHand);
            // var right = playerEquipment.GetItemInSlot(EquipmentSlotType.RightHand);
            // TODO: check what item is used
            
            // Right click (усиленная авта будет выбираться из двух оружек - какую юзать)
            
            // TODO: set attack speed

            if (!_animator.GetBool(TwoHandsBaseAttack1))
            {
                _animator.SetBool(TwoHandsBaseAttack1, true);
            }
            else if (!_animator.GetBool(TwoHandsBaseAttack2))
            {
                _animator.SetBool(TwoHandsBaseAttack2, true);   
            }
            else if (!_animator.GetBool(TwoHandsBaseAttack3))
            {
                _animator.SetBool(TwoHandsBaseAttack3, true);   
            }
        }

        public void HandleMeleeAttack()
        {
            meleeHitResponder.DoAttack();
        }

        private void HandleHitResponseEvent(HitData data)
        {
            Debug.Log("Hit to " + data.HurtBox.Owner.name);
        }
    }
}
