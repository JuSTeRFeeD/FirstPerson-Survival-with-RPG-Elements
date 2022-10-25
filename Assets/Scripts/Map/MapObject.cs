using System;
using UnityEngine;
using UnityEngine.Serialization;

namespace Map
{
    [Serializable]
    public class MapObject
    {
        public GameObject prefab;
        [Range(0, 1)]
        public float chance;
        public int maxCount;
    }
}