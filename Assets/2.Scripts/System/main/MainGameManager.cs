using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    void Start()
    {
        MainEventManager.Instance.PauseGamePlayEvent?.Invoke();
    }
}
