using Inventory;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Inventory
{
    public class InventorySlotUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, 
        IBeginDragHandler, IEndDragHandler, IDropHandler, IDragHandler,
        IPointerClickHandler
    {
        private const PointerEventData.InputButton MouseBtnLeft = PointerEventData.InputButton.Left;
        private const PointerEventData.InputButton MouseBtnRight = PointerEventData.InputButton.Right;
        private const PointerEventData.InputButton MouseBtnMiddle = PointerEventData.InputButton.Middle;

        private static readonly Color HoverColor = Color.grey;

        // Container for ItemImage & amountText for drag&drop
        [SerializeField] private RectTransform slotContainer;
        
        [SerializeField] private Image itemBackgroundImage;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI amountText;

        public InventoryUI InventoryUI { get; private set; }
        public int SlotIndex { get; private set; }
        public bool HasItem { get; private set; }
        private Color _initBgColor;
        private Transform _dragParent;

        private void Awake()
        {
            itemBackgroundImage = GetComponent<Image>();
            _initBgColor = itemBackgroundImage.color;
            _dragParent = transform.parent.parent.parent ? transform.parent.parent.parent : transform.parent;
        }

        public void Init(int index, InventoryUI inventoryUi)
        {
            InventoryUI = inventoryUi;
            SlotIndex = index;
        }

        public void SetData(ItemStuck stuck)
        {
            if (stuck != null) 
            {
                UpdateInfo(
                    stuck.item != null 
                        ? stuck.item.ItemIcon 
                        : null,
                    stuck.amount);
            }
            else
            {
                UpdateInfo(null, 0);
            }
        }

        private void UpdateInfo(Sprite icon, int amount)
        {
            itemImage.enabled = icon != null;
            itemImage.sprite = icon;
            HasItem = amount > 0;

            // Not need to set amount for EquipmentItems
            if (amountText == null) return;
            
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
        
        #region Item Interaction Methods
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            itemBackgroundImage.color = HoverColor;
            if (!HasItem) return;
            InventoryUI.inventoryManager.ShowItemTooltip(InventoryUI.OpenedInventory.items[SlotIndex]);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemBackgroundImage.color = _initBgColor;
            InventoryUI.inventoryManager.ShowItemTooltip(null);
        }
        private void OnDisable()
        {
            InventoryUI.inventoryManager.ShowItemTooltip(null);
            itemBackgroundImage.color = _initBgColor;
        }

        // Drag&Drop items
        public void ResetSlot()
        {
            InventoryUI.inventoryManager.draggingSlot = null;
            slotContainer.SetParent(transform);
            slotContainer.anchoredPosition3D = Vector3.zero;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!HasItem || eventData.button != MouseBtnLeft) return;
            InventoryUI.inventoryManager.draggingSlot = this; 
            slotContainer.SetParent(_dragParent);
            slotContainer.position = eventData.position;
        }
        
        public void OnDrag(PointerEventData eventData)
        {
            if (!HasItem || eventData.button != MouseBtnLeft) return;
            slotContainer.position = eventData.position;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            if (!HasItem || eventData.button != MouseBtnLeft) return;
            ResetSlot();
        }

        public virtual void OnDrop(PointerEventData eventData)
        {
            if (eventData.button != MouseBtnLeft) return;
                
            var draggingSlot = InventoryUI.inventoryManager.draggingSlot;
            ResetSlot();
            if (!draggingSlot) return;
            draggingSlot.ResetSlot();

            var draggingInventoryUI = draggingSlot.InventoryUI;

            // Same Inventory Container
            if (draggingInventoryUI.GetInstanceID() == InventoryUI.GetInstanceID())
            {
                if (draggingSlot.SlotIndex == SlotIndex) return;
                
                draggingInventoryUI.OpenedInventory.SwapItems(
                    draggingSlot.SlotIndex,
                    SlotIndex);
                return;
            }
            
            // Other Inventory Container
            // todo:  MoveItemToContainer юзать при клике шифт поди надо! ТУТ ИСПОЛЬЗОВАТЬ ДРУГОЕ НИД-====-=-=-=-=-=
            draggingInventoryUI.OpenedInventory.SwapItemWithContainer(
                InventoryUI.OpenedInventory, 
                draggingSlot.SlotIndex,
                SlotIndex);
        }


        // Use item (Right Click)
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != MouseBtnRight || !HasItem) return;
            var openedInv = InventoryUI.OpenedInventory;
            InventoryUI.inventoryManager.UseItem(
                openedInv.items[SlotIndex], 
                SlotIndex,
                openedInv);
        }
        
        #endregion
    }
}
