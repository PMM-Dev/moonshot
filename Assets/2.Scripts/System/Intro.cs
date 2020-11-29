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
        _video.loopPointReached += ShowMainGame;
    }

    private void ShowMainGame(VideoPlayer video)
    {
        foreach (GameObject element in MainGames)
        {
            element.SetActive(true);
        }
        gameObject.SetActive(false);
    }
}
