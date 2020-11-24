using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DashGhostFxPool : MonoBehaviour
{
    [SerializeField]
    private GameObject _ghostPrefab;

    private Queue<GameObject> availableObjects = new Queue<GameObject>();

    public static DashGhostFxPool Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
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
        if (isLeft) instance.transform.localScale = new Vector3(1, 1, 1);
        else instance.transform.localScale = new Vector3(-1, 1, 1);

        instance.SetActive(true);
        return instance;
    }
}
