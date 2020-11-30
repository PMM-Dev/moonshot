using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField]
    VideoPlayer _video;

    [SerializeField]
    GameObject[] MainGames;


    private void Awake()
    {
        _video.loopPointReached += IntroEndEvent;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            ShowMainGame();
        }
    }

    private void IntroEndEvent(VideoPlayer video)
    {
        ShowMainGame();
    }

    private void ShowMainGame()
    {
        foreach (GameObject element in MainGames)
        {
            element.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
