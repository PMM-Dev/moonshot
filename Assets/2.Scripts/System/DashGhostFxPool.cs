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

    private Vector3 _playerLocalScale;


    private void Start()
    {
        // _playerLocalScale = MainPlayerManager.Instance.Player.transform.localScale;
        _playerLocalScale = new Vector3(1f, 1f, 1f);
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
        instance.transform.parent = this.transform;
        availableObjects.Enqueue(instance);
    }

    public GameObject GetFromPool(bool isLeft)
    {
        if (availableObjects.Count == 0)
        {
            GrowPool();
        }
        var instance = availableObjects.Dequeue();
        if (isLeft) instance.transform.localScale = _playerLocalScale;
        else instance.transform.localScale = new Vector3(-1 * _playerLocalScale.x, _playerLocalScale.y, _playerLocalScale.z);

        instance.SetActive(true);
        instance.transform.parent = null;
        return instance;
    }
}
