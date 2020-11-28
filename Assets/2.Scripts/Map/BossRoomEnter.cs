using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Map
{
    public class BossRoomEnter : MonoBehaviour
    {
        [SerializeField]
        private AnimationCurve _curve;
        [SerializeField]
        private List<GameObject> _bossBlock = new List<GameObject>();
        [SerializeField]
        private Transform _bossBlockTarget;
        [SerializeField]
        private Transform _bossPosition;
        [SerializeField]
        private GameObject _mapManager;

        private bool _isBoard;

        private Vector3 _bossBlockStartPoint;
        private Vector3 _bossBlockEndPoint;

        private void Awake()
        {
            _bossBlockStartPoint = transform.position;
            _bossBlockEndPoint = _bossBlockTarget.position;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.CompareTag("Player") && !_isBoard)
            {
                MainGameManager.Instance.SpawnBoss(_bossPosition);
                StartCoroutine(BossCutScene());
                _isBoard = true;
            }
        }

        private IEnumerator MoveSlow(Vector3 start, Vector3 end, float speed, GameObject game)
        {
            float progress = 0f;
            while (progress < 1f)
            {
                progress += Time.deltaTime * speed;
                game.transform.position = Vector3.Lerp(start, end, _curve.Evaluate(progress));
                _mapManager.GetComponent<MapMaking>().WholeMapOrder[21].transform.GetChild(0).GetComponent<MakePlayerDie>().CanMakePlayerDie = true;
                _mapManager.GetComponent<MapMaking>().WholeMapOrder[21].transform.GetChild(1).gameObject.SetActive(false);
                yield return null;
            }
            StartCoroutine(ChangeOffset(new Vector3(0f,3.5f,-10f)));
        }

        private IEnumerator BossCutScene()
        {          
            MainSoundManager.Instance.StopBGM();
            yield return new WaitForSeconds(2f);         
            StartCoroutine(MoveSlow(_bossBlockStartPoint, _bossBlockEndPoint, 0.5f, this.gameObject));
            yield return new WaitForSeconds(0.7f);
            foreach (GameObject block in _bossBlock)
            {
                block.SetActive(true);
            }
            foreach (GameObject block in _bossBlock)
            {
                Vector3 startpoint = block.transform.position - new Vector3(0, 3f, 0);
                Vector3 endpoint = block.transform.position;
                StartCoroutine(MoveSlow(startpoint, endpoint, 1f, block));
                StartCoroutine(ChangeColor(block));
            }
            yield return new WaitForSeconds(3f);
            Enemy.PatternController.Instance.Appear();
            MainSoundManager.Instance.PlayBossBGM();
            yield return null;
        }

        private IEnumerator ChangeColor(GameObject game)
        {
            float progress = 0f;
            SpriteRenderer color = game.GetComponent<SpriteRenderer>();
            while (progress < 1f)
            {
                progress += Time.deltaTime * 1f;
                color.color = Color.Lerp(new Color(1f, 1f, 1f, 0f), new Color(1f, 1f, 1f, 1f), _curve.Evaluate(progress));
                yield return null;
            }
        }

        private IEnumerator ChangeOffset(Vector3 target)
        {
            float progress = 0f;
            SmoothTargetFollowing smo = Camera.main.transform.parent.GetComponent<SmoothTargetFollowing>();
            Vector3 origin = smo.Offset;
            while (progress < 1f)
            {
                progress += Time.deltaTime * 1f;
                smo.Offset = Vector3.Lerp(origin, target, _curve.Evaluate(progress));
                yield return null;
            }
        }
    }

}