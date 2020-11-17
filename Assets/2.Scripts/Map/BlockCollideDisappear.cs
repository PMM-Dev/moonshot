using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockCollideDisappear : MonoBehaviour
{
    [SerializeField]
    private float DestroyTime = 1.0f;

    private void OnTriggerEnter(Collider col)
    {
         Destroy(gameObject, DestroyTime);
    }
}
