using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartHotKey : MonoBehaviour
{
    [SerializeField]
    private MainUIManager _mainUIManager;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            _mainUIManager.OnClickStart();
        }
    }
}
