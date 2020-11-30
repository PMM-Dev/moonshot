using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Player;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController Instance;

    private MapMaking _mapMaking;
    private Coroutine _mapChanger;
    private List<SpriteRenderer> _backgroundSpriteRenderers;
    public List<Vector2> _heights;
    private Transform _cameraTransform;

    private float _x;
    private float _y;
    private float _pastY;
    private Color _color;
    private int _heightIndex;

    public int HeightIndex
    {
        get { return _heightIndex; }
        set
        {
            if (_heightIndex != value)
            {
                _pastY = _y;

                StartCoroutine(ChangeBackground(_heightIndex));
                _heightIndex = value;
            }
        }
    }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        _mapMaking = FindObjectOfType<MapMaking>();
        _cameraTransform = Camera.main.transform.parent.transform;
    }

    private void Start()
    {
        _backgroundSpriteRenderers = new List<SpriteRenderer>();
        for (int i = 0; i < transform.childCount; i++)
        {
            _backgroundSpriteRenderers.Add(transform.GetChild(i).GetComponent<SpriteRenderer>());
        }
        _color = _backgroundSpriteRenderers[0].color;

        _heights = new List<Vector2>();
        _heights.Add(new Vector2(-4f, 200f));
        _heights.Add(new Vector2(200f, 480f));
        _heights.Add(new Vector2(480f, 900f));
    }

    public void Initialize()
    {
        if (_mapMaking != null && _cameraTransform != null)
        {
            _heightIndex = 0;
            for (int i = 0; i < transform.childCount; i++)
            {
                _backgroundSpriteRenderers[_heightIndex].color = _color;
            }
            _mapChanger = StartCoroutine(CheckHeight());
            StartCoroutine(ChangeHeight());
        }
    }

    private IEnumerator CheckHeight()
    {
        while (true)
        {
            if (_mapMaking != null && _cameraTransform != null)
            {
                for (int i = _heightIndex; i < 3; i++)
                {
                    if (_cameraTransform.position.y >= _heights[i].x && _cameraTransform.position.y < _heights[i].y)
                    {
                        HeightIndex = i;
                    }
                }
            }
            yield return null;
        }
    }

    private IEnumerator ChangeHeight()
    {
        while (true)
        {
            if (_mapMaking != null && _cameraTransform != null)
            {
                _x = Mathf.Clamp(_cameraTransform.position.x / 80f, -0.8f, 0.8f);
                _y = Mathf.Clamp(5f + (_heights[_heightIndex].y - _cameraTransform.position.y) / 120f, -50f, 50f);
                _backgroundSpriteRenderers[_heightIndex].gameObject.transform.localPosition = new Vector3(_x, _y, 0f);
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
