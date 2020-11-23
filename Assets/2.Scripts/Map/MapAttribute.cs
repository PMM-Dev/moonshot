using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public enum MapEndType { Left, Middle, Right };
    public class MapAttribute : MonoBehaviour
    {

        [SerializeField]
        private MapEndType _mapEnd;

        public MapEndType MapEnd
        {
            get
            {
                return _mapEnd;
            }
        }

    }
}

