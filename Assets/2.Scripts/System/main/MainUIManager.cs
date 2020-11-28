using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _startPanel;
    [SerializeField]
    private GameObject _mainPanel;
    [SerializeField]
    private GameObject _optionPanel;
    [SerializeField]
    private GameObject _gameoverPanel;
    [SerializeField]
    private GameObject _creditPanel;
    [SerializeField]
    private RectTransform _creditTransform;
    [SerializeField]
    private GameObject _thanksPanel;
    [SerializeField]
    private GameObject _endingPanel;

    [SerializeField]
    private float _creditTime;
    [SerializeField]
    private float _creditEndY;
    [SerializeField]
    private bool isEnding = false;

    [SerializeField]
    private MainGameManager _mainGameManager;

    private bool _isOpenOption = false;

    private void Start()
    {
        MainEventManager.Instance.ClearMainGameEvent += StartEnding;
        MainEventManager.Instance.ClearMainGameEvent.Invoke();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (_isOpenOption)
            {
                OnClickOptionResume();
                _isOpenOption = false;
            }
            else
            {
                OnClickOption();
                _isOpenOption = true;
            }
        }

        if (isEnding)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                _creditTime /= 2;
            }

            if (Input.GetKeyUp(KeyCode.Mouse0))
            {
                _creditTime *= 2;
            }

        }
    }

    public void OnClickStart()
    {
        _mainGameManager.GameStart();

        _startPanel.SetActive(false);
        _mainPanel.SetActive(true);
    }

    public void OnClickQuickRestart()
    {
        SceneManager.LoadScene("RestartedMain");
    }

    public void OnClickOption()
    {
        _optionPanel.SetActive(true);
        _isOpenOption = true;

        MainEventManager.Instance.PauseGamePlayEvent?.Invoke();
    }

    public void OnClickOptionResume()
    {
        _optionPanel.SetActive(false);
        _isOpenOption = false;

        MainEventManager.Instance.ResumeGamePlayEvent?.Invoke();
    }

    public void OnClickInitRestart()
    {
        SceneManager.LoadScene("Main");
    }

    public void OnClickQuite()
    {
        Application.Quit();
    }

    public void ShowGameoverUI()
    {
        _mainPanel.SetActive(false);
        _gameoverPanel.SetActive(true);
    }

    public void StartEnding()
    {
        isEnding = true;
        StartCoroutine(Ending());
    }

    public IEnumerator Ending()
    {
        _endingPanel.SetActive(true);

        _creditPanel.SetActive(true);
        yield return StartCoroutine(PlayCredit());
        _creditPanel.SetActive(false);

        _thanksPanel.SetActive(true);
    }

    public IEnumerator PlayCredit()
    {
        float originY = _creditTransform.anchoredPosition.y;
        float t = 0f;
        while (t <= 1f)
        {
            t += Time.deltaTime * (1 / _creditTime);
            float movedY = Mathf.Lerp(originY, _creditEndY, t);
            _creditTransform.anchoredPosition = new Vector2(0, movedY);
            yield return null;
        }
    }
}
