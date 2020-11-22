using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class BlockCollideDisappear : MonoBehaviour
    {
        [SerializeField]
        private float _destroyTime = 1.0f;

        bool _isCollided;

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if(!_isCollided)
                {
                    StartCoroutine(Timer());
                    _isCollided = true;
                }
            }
        }

        private IEnumerator Timer()
        {
            float time = 0f;
            while(time < _destroyTime)
            {
                time += Time.deltaTime;
                yield return null;
            }
            gameObject.SetActive(false);
        }
    }
}

