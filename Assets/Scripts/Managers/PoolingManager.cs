using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Managers
{
    public class PoolingManager : MonoBehaviour
    {
        [Tooltip("Only for identifying")]
        [SerializeField] private string poolName;
        [SerializeField] private GameObject prefab;
        [Tooltip("How much prefabs spawn on start")]
        [SerializeField] private int count;
        
        private readonly List<GameObject> _items = new List<GameObject>();

        public GameObject Prefab => prefab;
        
        private void Start()
        {
            for (var i = 0; i < count; i++)
            {
                AddItem();
            }
        }

        private GameObject AddItem()
        {
            var item = Instantiate(prefab, transform);
            item.SetActive(false);
            _items.Add(item);
            return item;
        }

        public GameObject Take()
        {
            foreach (var t in _items.Where(t => t.activeSelf == false))
            {
                t.SetActive(true);
                return t;
            }

            var item = AddItem();
            item.SetActive(true);
            return item;
        }
        
        public T Take<T>() where T : Component 
        {
            foreach (var t in _items.Where(t => t.activeSelf == false))
            {
                t.SetActive(true);
                return t.GetComponent<T>();
            }

            var item = AddItem();
            item.SetActive(true);
            return item.GetComponent<T>();
        }

        public void Dispose(GameObject item)
        {
            foreach (var t in _items.Where(t => t == item))
            {
                t.SetActive(false);
            }
        }
    }
}
