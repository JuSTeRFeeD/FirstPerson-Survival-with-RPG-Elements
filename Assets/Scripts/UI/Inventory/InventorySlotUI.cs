using Inventory;
using Items;
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
        // Container for ItemImage & amountText for drag&drop
        [SerializeField] private RectTransform slotContainer;
        [SerializeField] private Image itemBackgroundImage;
        [SerializeField] private Image itemImage;
        [SerializeField] private TextMeshProUGUI amountText;
        
        private const PointerEventData.InputButton MouseBtnLeft = PointerEventData.InputButton.Left;
        private const PointerEventData.InputButton MouseBtnRight = PointerEventData.InputButton.Right;
        private const PointerEventData.InputButton MouseBtnMiddle = PointerEventData.InputButton.Middle;
        private static readonly Color HoverColor = Color.grey;

        private InventoryManagerUI _inventoryManager; 
        public IInventoryUI InventoryUI { get; private set; }
    
        public int SlotIndex { get; protected  set; }
        public bool HasItem { get; private set; }
        private Color _initBgColor;
        private Transform _dragParent;

        public delegate void OnMouseEnter(InventorySlotUI slot);
        public OnMouseEnter MouseEnterEvent;
        public delegate void OnMouseExit(InventorySlotUI slot);
        public OnMouseExit MouseExitEvent;
        public delegate void OnItemUse(InventorySlotUI slot);
        public OnItemUse ItemUseEvent;
        public delegate void OnItemDropToSlot(InventorySlotUI slot);
        public OnItemDropToSlot ItemDropToSlotEvent;
        
        private void Awake()
        {
            itemBackgroundImage = GetComponent<Image>();
            _initBgColor = itemBackgroundImage.color;
            _dragParent = transform.parent.parent.parent.parent;
        }

        public void SetData(ItemStack stuck)
        {
            if (stuck is { amount: > 0 }) 
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
        
        public void SetData(BaseItem item)
        {
            if (item != null) 
            {
                UpdateInfo(item.ItemIcon ,1);
            }
            else
            {
                UpdateInfo(null, 0);
            }
        }

        public void ClearData()
        {
            UpdateInfo(null, 0);
        }

        public void Init(int slotIndex, InventoryManagerUI inventoryManager, IInventoryUI inventoryUI)
        {
            InventoryUI = inventoryUI;
            _inventoryManager = inventoryManager;
            SlotIndex = slotIndex;
            ClearData();
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
            MouseEnterEvent?.Invoke(this);
            itemBackgroundImage.color = HoverColor;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            MouseExitEvent?.Invoke(this);
            itemBackgroundImage.color = _initBgColor;
        }
        private void OnDisable()
        {
            itemBackgroundImage.color = _initBgColor;
        }

        // Drag&Drop items
        public void ResetSlotDrag()
        {
            slotContainer.SetParent(transform);
            slotContainer.anchoredPosition3D = Vector3.zero;
        }
        
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!HasItem || eventData.button != MouseBtnLeft) return;
            _inventoryManager.draggingSlot = this; 
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
            ResetSlotDrag();
        }

        public void OnDrop(PointerEventData eventData)
        {
            if (eventData.button != MouseBtnLeft) return;
            ItemDropToSlotEvent?.Invoke(this);
            ResetSlotDrag();
        }


        // Use item (Right Click)
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != MouseBtnRight || !HasItem) return;
            ItemUseEvent?.Invoke(this);
        }
        
        #endregion
    }
}
