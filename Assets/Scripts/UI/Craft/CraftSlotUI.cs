using System;
using Inventory;
using Items;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.Craft
{
    [RequireComponent(typeof(Image))]
    public class CraftSlotUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
    {
        [SerializeField] private Image craftIcon;
        private Image _bgImage;
        private Color _initBgColor;
        
        public CraftItem CraftItem { get; private set; }
        private CraftManagerUI _craftManagerUI;

        private void Awake()
        {
            _bgImage = GetComponent<Image>();
            _initBgColor = _bgImage.color;
        }

        private void OnDisable()
        {
            SetIsColored(false);
        }

        public ItemStack ResultStack => new ItemStack
        {
            item = CraftItem.result,
            amount = CraftItem.resultAmount
        };

        public void SetCraftManger(CraftManagerUI craftManagerUI)
        {
            _craftManagerUI = craftManagerUI;
        }

        public void SetCraft(CraftItem item)
        {
            if (item == null) throw new Exception("Null craft reference");
            CraftItem = item;
            craftIcon.sprite = item.result.ItemIcon;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left) return;
            _craftManagerUI.SelectCraftItem(this);
        }

        public void SetIsColored(bool selected)
        {
            _bgImage.color = selected ? Color.yellow : _initBgColor;
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            _craftManagerUI.ShowResultItemInfo(this);
        }
    }
}
