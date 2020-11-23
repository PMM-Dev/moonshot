using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Player;

public class BackgroundController : MonoBehaviour
{
    private const float StageHalfLength = 34.1f;
    private MapMaking _mapMaking;
    private PlayerController _playerController;

    private Coroutine _mapChanger;

    [SerializeField]
    private List<SpriteRenderer> _backgroundSpriteRenderers;

    private int counter;

    private void Awake()
    {
        _mapMaking = FindObjectOfType<MapMaking>();
        _playerController = FindObjectOfType<PlayerController>();

        for (int i = 0; i < transform.childCount; i++)
        {
            _backgroundSpriteRenderers.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
    }

    private void Start()
    {
        counter = 0;

        if (_mapMaking != null && _playerController != null)
        {

            _mapChanger = StartCoroutine(CheckHeight());
        }
    }

    private IEnumerator CheckHeight()
    {
        while (true)
        {
            if (_playerController.transform.position.y > _mapMaking.WholeMapOrder[counter].transform.position.y)
            {
                StartCoroutine(ChangeBackground(counter));
                counter++;
            }
            yield return null;
        }
    }

    private IEnumerator ChangeBackground(int startIndex)
    {
        float time = 0f;

        Color originColor = _backgroundSpriteRenderers[startIndex].color;
        Color targetColor = new Color(originColor.r, originColor.g, originColor.b, 0f);

        while (time < 1f)
        {
            time += Time.deltaTime;

            _backgroundSpriteRenderers[startIndex].color = Color.Lerp(originColor, targetColor, time);

            yield return null;
        }
    }

}
