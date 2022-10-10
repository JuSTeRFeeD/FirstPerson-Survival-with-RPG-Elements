using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Math = UnityEngine.ProBuilder.Math;

namespace UI.ScrollWheel
{
    public class ScrollWheelUI : MonoBehaviour
    {
        [SerializeField] private GameObject wheelItePrefab;
        [SerializeField] private RectTransform wheelContent;
        [SerializeField] private float wheelRadius = 150;
        [SerializeField] private List<Sprite> itemSprites = new List<Sprite>();

        private void Start()
        {
            SetupItems(itemSprites);
        }

        public void SetupItems(List<Sprite> items)
        {
            var pos = wheelContent.position;
            itemSprites = items;
            var i = 0;
            foreach (var item in items)
            {
                var angle = i++ * Mathf.PI * 2f / items.Count;
                var x = pos.x + Mathf.Cos(angle) * wheelRadius;
                var y = pos.y + Mathf.Sin(angle) * wheelRadius ;
                
                var scrollWheelItem = 
                    Instantiate(wheelItePrefab, new Vector3(x, y, 0), Quaternion.identity, wheelContent)
                    .GetComponent<ScrollWheelItemUI>();
                scrollWheelItem.SetItem(i, item);
                scrollWheelItem.OnItemClick += ctx => Debug.Log($"Clicked on ${ctx}");
            }
        }
    }
}
