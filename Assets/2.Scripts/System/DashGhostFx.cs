using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGhostFx : MonoBehaviour
{
    private Transform _player;

    private SpriteRenderer _sr;
    private SpriteRenderer _playerSr;

    private Color _color;

    [SerializeField]
    private float _activeTime = 0.1f;
    private float _timeActivated;
    private float _alpha;
    [SerializeField]
    private float _alphaSet = 0.8f;
    [SerializeField]
    private float _alphaMultiplier = 0.85f;

    private void OnEnable()
    {
        _sr = GetComponent<SpriteRenderer>();
        _player = MainGameManager.Instance.
    }
}
