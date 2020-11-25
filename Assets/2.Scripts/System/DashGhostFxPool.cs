using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGhostFxPool : MonoBehaviour
{
    public static DashGhostFxPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    //
    // SINGLETON

    [SerializeField]
    private GameObject _ghostPrefab;
    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    private Vector3 _playerLocalscal;


    private void Start()
    {
        _playerLocalscal = MainGameManager.Instance.Player.transform.localScale;
    }

    public IEnumerator PlayGhostFx(bool isLeft)
    {
        while (true)
        {
            GetFromPool(isLeft);

            yield return new WaitForSeconds(0.02f);
        }
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var instanceToAdd = Instantiate(_ghostPrefab);
            instanceToAdd.transform.SetParent(transform);
            AddToPool(instanceToAdd);
        }
    }

    public void AddToPool(GameObject instance)
    {
        instance.SetActive(false);
        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool(bool isLeft)
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }
        var instance = availableObjects.Dequeue();
        if (isLeft) instance.transform.localScale = _playerLocalscal;
        else instance.transform.localScale = new Vector3(-1 * _playerLocalscal.x, _playerLocalscal.y, _playerLocalscal.z);

        instance.SetActive(true);
        return instance;
    }
}
