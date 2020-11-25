using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGhostFx : MonoBehaviour
{
    [SerializeField]
    private Transform _player;
    private SpriteRenderer _sr;

    private Color _color;

    [SerializeField]
    private float _activeTime = 0.45f;
    private float _timeActivated;
    private float _alpha;
    [SerializeField]
    private float _alphaSet = 0.8f;
    [SerializeField]
    private float _alphaMultiplier = 0.85f;

    private void OnEnable()
    {
        _sr = GetComponent<SpriteRenderer>();
        _player = MainGameManager.Instance.Player.transform;

        _alpha = _alphaSet;
        transform.position = _player.position;
        transform.rotation = _player.rotation;
        _timeActivated = Time.time;
    }

    private void Update()
    {
        _alpha *= _alphaMultiplier;
        _color = new Color(1f, 1f, 1f, _alpha);
        _sr.color = _color;

        if (Time.time >= (_timeActivated + _activeTime))
        {
            DashGhostFxPool.Instance.AddToPool(gameObject);
        }
    }
}
