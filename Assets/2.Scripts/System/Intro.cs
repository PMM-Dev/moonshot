using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Video;

public class Intro : MonoBehaviour
{
    [SerializeField]
    VideoPlayer _video;
    [SerializeField]
    Text _skipGuideText;

    [SerializeField]
    GameObject[] MainGames;


    private void Awake()
    {
        _video.loopPointReached += IntroEndEvent;
    }

    private void Start()
    {
        StartCoroutine(HideGuide());
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
        _skipGuideText.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    public IEnumerator HideGuide()
    {
        yield return new WaitForSecondsRealtime(4f);

        _skipGuideText.gameObject.SetActive(false);
    }
}
