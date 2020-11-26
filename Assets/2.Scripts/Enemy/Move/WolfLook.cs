using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfLook : MonoBehaviour
{

    protected GameObject _player;
    private Vector3 _targetDirction;
    private Vector3 _originScale;
    private Vector3 _reversedScale;

    private void Start()
    {
        _player = MainPlayerManager.Instance.Player;
        _originScale = this.transform.localScale;
        _reversedScale = this.transform.localScale;
        _reversedScale.x *= -1;
        StartCoroutine(FilpSize());
    }

    protected IEnumerator FilpSize()
    {
        while (true)
        {
            CalculationDistance(_player.transform.position);
            FlipSize();
            yield return null;
        }
    }

    public void CalculationDistance(Vector3 _targetPosition)
    {
        _targetDirction = (_targetPosition - this.gameObject.transform.position).normalized;
    }

    public void FlipSize()
    {
        if (_targetDirction.x > 0)
            this.transform.localScale = _reversedScale;
        else
            this.transform.localScale = _originScale;
    }
}
