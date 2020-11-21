using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class BlockCollideDisappear : MonoBehaviour
    {
        [SerializeField]
        private float _DestroyTime = 1.0f;

        private void OnTriggerEnter(Collider col)
        {
            if (col.gameObject.CompareTag("Player"))                
            {
                Destroy(gameObject, _DestroyTime);
            }
        }
    }
}

