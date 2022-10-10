using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace UI.ScrollWheel
{
    public class ScrollWheelItemUI : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private Image itemImage;
        private int _id;

        public delegate void ItemClickEvent(int id);
        public ItemClickEvent OnItemClick;

        public void SetItem(int id, Sprite sprite)
        {
            _id = id;
            itemImage.sprite = sprite;
        }

        public void OnPointerClick(PointerEventData eventData)
        {
            OnItemClick?.Invoke(_id);
        }

        public void OnPointerEnter(PointerEventData eventData)
        {
            itemImage.rectTransform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            itemImage.rectTransform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
