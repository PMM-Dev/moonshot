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
    private GameObject _endingPanel;

    [SerializeField]
    private MainGameManager _mainGameManager;

    private bool _isOpenOption = false;

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
    }

    public void OnClickStart()
    {
        _mainGameManager.GameStart();

        _startPanel.SetActive(false);
        _mainPanel.SetActive(true);
    }

    public void OnClickRestart()
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

    public void OnClickOptionRestart()
    {
        _optionPanel.SetActive(false);

        OnClickRestart();
    }

    public void ShowGameoverUI()
    {
        _mainPanel.SetActive(false);
        _gameoverPanel.SetActive(true);
    }

    public void ShowEndingPanel()
    {
        _endingPanel.SetActive(true);
    }
}
