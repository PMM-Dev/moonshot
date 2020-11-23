using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainGameManager : MonoBehaviour
{
    [SerializeField]
    ElevatorMovement _elevatorMovement;
    [SerializeField]
    CameraFx _cameraFx;

    void Start()
    {
        // Pause all game flow before pressing start button
        MainEventManager.Instance.PauseGamePlayEvent?.Invoke();

        // 
        MainEventManager.Instance.StartMainGameEvent += _elevatorMovement.RiseElevator;
        MainEventManager.Instance.StartMainGameEvent += _cameraFx.ShakeOfElevatorMovement;
    }
}
