using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Map;
using Player;

public class BackgroundController : MonoBehaviour
{
    public static BackgroundController Instance;

    private const float StageHalfLength = 34.1f;
    private MapMaking _mapMaking;
    private PlayerController _playerController;

    private Coroutine _mapChanger;

    private List<SpriteRenderer> _backgroundSpriteRenderers;

    public List<Vector2> _heights;

    float x;
    float y;
    float pastY;

    private Color _color;

    private int _heightIndex;

    public int HeightIndex
    {
        get { return _heightIndex; }
        set
        {
            if (_heightIndex != value)
            {
                pastY = y;

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
        if (MainPlayerManager.Instance == null)
        {
            _playerController = FindObjectOfType<PlayerController>();
        }
        else
        {
            _playerController = FindObjectOfType<PlayerController>();
            //_playerController = MainPlayerManager.Instance.Player.GetComponent<PlayerController>();
        }

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

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            Initialize();
        }
    }

    public void Initialize()
    {
        if (_mapMaking != null && _playerController != null)
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
            if (_mapMaking != null && _playerController != null)
            {
                for (int i = _heightIndex; i < 3; i++)
                {
                    if (_playerController.transform.position.y >= _heights[i].x && _playerController.transform.position.y < _heights[i].y)
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
            if (_mapMaking != null && _playerController != null)
            {
                x = Mathf.Clamp(_playerController.transform.position.x / 80f, -0.8f, 0.8f);
                y = Mathf.Clamp(5f + (_heights[_heightIndex].y - _playerController.transform.position.y) / 120f, -50f, 50f);
                _backgroundSpriteRenderers[_heightIndex].gameObject.transform.localPosition = new Vector3(x, y, 0f);
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
