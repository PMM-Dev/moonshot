using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class BlockCollideDisappear : MonoBehaviour
    {
        [SerializeField]
        private float _destroyTime = 1.0f;

        private ParticleSystem _particle;
        private bool _isCollided;

        private void Awake()
        {
            _particle = transform.GetComponentInChildren<ParticleSystem>();
            _particle.Stop();
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player"))
            {
                if(!_isCollided)
                {
                    _isCollided = true;
                    _particle.Play();
                    StartCoroutine(BlockTime());
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

        private IEnumerator BlockTime()
        {
            yield return new WaitForSeconds(0.5f);
            _particle.Stop();
            yield return new WaitForSeconds(_destroyTime - 0.5f);
            _particle.Play();
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
            StartCoroutine(CoFadeOut(gameObject.GetComponent<SpriteRenderer>(), 0.5f));
            yield return new WaitForSeconds(0.5f);
            _particle.Stop();
        }
        IEnumerator CoFadeOut(SpriteRenderer sr, float fadeOutTime)
        {
            Color tempColor = sr.color;
            while (tempColor.a > 0f)
            {
                tempColor.a -= Time.deltaTime / fadeOutTime;
                sr.color = tempColor;

                if (tempColor.a <= 0f) tempColor.a = 0f;

                yield return null;
            }
            sr.color = tempColor;
        }
    }
}

