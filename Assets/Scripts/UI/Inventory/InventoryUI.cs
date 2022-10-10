using UnityEngine;

namespace UI.Inventory
{
    public class InventoryUI : MonoBehaviour
    {
        [SerializeField] private Transform cellsContainer;
        private InventoryCellUI[] _cells;

        private void Start()
        {
            _cells = new InventoryCellUI[cellsContainer.childCount];
            var i = 0;
            foreach(Transform child in cellsContainer)
            {
                _cells[i++] = child.GetComponent<InventoryCellUI>();
            }
        }
    }
}
