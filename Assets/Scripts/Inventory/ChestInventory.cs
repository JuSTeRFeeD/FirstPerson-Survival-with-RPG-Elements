using UnityEngine;

namespace Inventory
{
    [RequireComponent(typeof(BoxCollider))]
    public class ChestInventory : InventoryContainer
    {
        public delegate void OnCollideEnterPlayer(ChestInventory bag);
        public OnCollideEnterPlayer CollideEnterPlayerEvent;
        
        public delegate void OnCollideExitPlayer(ChestInventory bag);
        public OnCollideExitPlayer CollideExitPlayerEvent;

        private void OnTriggerEnter(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            CollideEnterPlayerEvent?.Invoke(this);
        }

        private void OnTriggerExit(Collider other)
        {
            if (!other.CompareTag("Player")) return;
            CollideExitPlayerEvent?.Invoke(this);
        }
    }
}