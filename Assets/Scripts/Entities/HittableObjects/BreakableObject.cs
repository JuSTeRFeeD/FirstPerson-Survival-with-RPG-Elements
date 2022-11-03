using System;
using System.Collections.Generic;
using EditorHelpers;
using Entities.CombatHit;
using Inventory;
using Items;
using Managers;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace Entities.HittableObjects
{
    [RequireComponent(typeof(Health))]
    public class BreakableObject : MonoBehaviour, IHurtResponder
    {
        [SerializeField] private HurtBox hurtBox;
        [Space]
        [SerializeField] private Drop[] drops = new Drop[8];
        private Health _health;

        private void Start()
        {
            _health = GetComponent<Health>();
            hurtBox.HurtResponder = this;

            _health.DeathEvent += HandleBreak;
        }

        private void HandleBreak(Health health)
        {
            if (drops.Length == 0)
            {
                Destroy(gameObject);
            };
            var dropStacks = new List<ItemStack>();
            foreach (var drop in drops)
            {
                if (drop.dropToInventoryOnHit) continue;
                if (!(Random.Range(0f, 100f) <= drop.dropChance)) continue;
                if (drop.onDropMaxAmount == 0)
                {
                    throw new Exception("Item drop upper rate is zero");
                }
                dropStacks.Add(new ItemStack
                {
                    item = drop.item,
                    amount = Random.Range(drop.onDropMinAmount, drop.onDropMaxAmount)
                });
            }

            if (dropStacks.Count != 0)
            {
                GameManager.DropManager.CreateDropBag(transform.position, dropStacks.ToArray());
            }
            Destroy(gameObject);
        }
        
        public bool CheckHit(HitData data)
        {
            return true;
        }

        public void Response(HitData data)
        {
            _health.TakeDamage(data);
        }
    }

    [Serializable]
    public class Drop
    {
        public BaseItem item;
        
        [Space]
        [Header("Drop on hit")]
        public bool dropToInventoryOnHit;
        [Tooltip("The total amount of the resource that will drop")]
        [Min(0)]
        // TODO: not working attribute ShowIf
        [ShowIf(ActionOnConditionFail.DisableAttribute, ConditionOperator.And, nameof(dropToInventoryOnHit))]
        public int totalAmountOnHit = 0;
        
        [Space]
        [Header("Chance drop on destroy")]
        [FormerlySerializedAs("chance")]
        [Range(0f, 100f)] public float dropChance;
        [Min(0)] public int onDropMinAmount;
        [Min(0)] public int onDropMaxAmount;
    }
}
