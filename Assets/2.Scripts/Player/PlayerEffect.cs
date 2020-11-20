using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerEffect : MonoBehaviour
    {
        private Coroutine _shadowCoroutine;

        [SerializeField]
        private SpriteRenderer _playerSpriteRenderer;

        [SerializeField]
        private GameObject _shadowPrefab;

        public void RunSlashShadow()
        {
            _shadowCoroutine = StartCoroutine(CreateShadow());

        }

        public void StopSlashShadow()
        {
            StopCoroutine(_shadowCoroutine);
        }

        private IEnumerator CreateShadow()
        {
            float interval = 0.25f;
            float time = interval;

            while (true)
            {
                time += Time.deltaTime;
                if (time > interval)
                {
                    GameObject a = Instantiate(_shadowPrefab);
                    a.GetComponent<SpriteRenderer>().sprite = _playerSpriteRenderer.sprite;
                    time = 0f;
                }
                yield return null;
            }

        }
    }
}
