using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler
    {
        // Container for ItemImage & amountText for drag&drop
        [SerializeField] private RectTransform slotContainer;
        
        [SerializeField] private Image itemBackgroundImage;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI amountText;

        public InventoryUI InventoryUI { get; private set; }
        
        public int SlotIndex { get; private set; }
        private bool _hasItem;
        private Color _initBgColor;
        
        private Vector3 _initDragPos;
        

        private void Awake()
        {
            itemBackgroundImage = GetComponent<Image>();
            _initDragPos = slotContainer.localPosition;
        }

        private void Start()
        {
            _initBgColor = itemBackgroundImage.color;
            SetData(null, 0);
        }

        public void Init(int index, InventoryUI inventory)
        {
            InventoryUI = inventory;
            SlotIndex = index;
        }

        public void SetData(Sprite icon, int amount)
        {
            UpdateInfo(icon, amount);
        }
        
        public void SetData(InventoryStuck stuck)
        {
            if (stuck != null) 
            {
                UpdateInfo(
                    stuck.Item != null 
                        ? stuck.Item.ItemIcon 
                        : null,
                    stuck.Amount);
                
            }
            else
            {
                SetData(null, 0);
            }
        }

        private void UpdateInfo(Sprite icon, int amount)
        {
            itemImage.enabled = icon != null;
            itemImage.sprite = icon;
            _hasItem = amount > 0;
            if (amount > 1)
            {
                amountText.text = amount.ToString();
                amountText.enabled = true;
            }
            else
            {
                amountText.enabled = false;
            }
        }
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            itemBackgroundImage.color = Color.black;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemBackgroundImage.color = _initBgColor;
        }

        // Drag&Drop items
        public void ResetSlotPosition()
        {
            slotContainer.SetParent(transform);
            slotContainer.localPosition = _initDragPos;
            InventoryUI.inventoryManager.draggingSlot = null;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!_hasItem) return;
            InventoryUI.inventoryManager.draggingSlot = this; 
            slotContainer.SetParent(transform.parent);
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!_hasItem) return;
            ResetSlotPosition();
        }

        public void OnDrop(PointerEventData eventData)
        {
            var draggingSlot = InventoryUI.inventoryManager.draggingSlot;
            ResetSlotPosition();
            if (!draggingSlot) return;

            if (!eventData.hovered[0].TryGetComponent(out InventorySlotUI dropSlot)) return;
            if (draggingSlot.SlotIndex == dropSlot.SlotIndex) return;

            // Same Inventory Container
            if (draggingSlot.InventoryUI.GetInstanceID() == dropSlot.InventoryUI.GetInstanceID())
            {
                draggingSlot.InventoryUI.OpenedInventory.SwapItems(
                    draggingSlot.SlotIndex, 
                    dropSlot.SlotIndex);
                return;
            }
            
            // Other Inventory Container
            // todo:  MoveItemToContainer юзать при клике шифт поди надо! ТУТ ИСПОЛЬЗОВАТЬ ДРУГОЕ НИД-====-=-=-=-=-=
            draggingSlot.InventoryUI.OpenedInventory.MoveItemToContainer(
                dropSlot.InventoryUI.OpenedInventory, 
                draggingSlot.SlotIndex);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (!_hasItem) return;
            slotContainer.position = eventData.position;
        }
    }
}
