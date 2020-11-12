using UnityEngine;
using UnityEngine.UI;

public class LobbyUIManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _mainPanel;
    [SerializeField]
    private GameObject _optionPanel;


    public void OnPressPlay()
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene("main");
    }

    public void OnPressOption()
    {
        _mainPanel.SetActive(false);
        _optionPanel.SetActive(true);
    }

    public void OnPressExit()
    {
        Application.Quit();
    }

    public void OnPressBack()
    {
        _mainPanel.SetActive(true);
        _optionPanel.SetActive(false);
    }

    public void OnChangeWindow(Toggle toggle)
    {
        if (toggle.isOn) Screen.fullScreenMode = FullScreenMode.Windowed;
        else Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
    }

    public void OnChangeResolution(Dropdown dropdown)
    {
        bool isFullscreen = Screen.fullScreen;
        switch (dropdown.value)
        {
            case 0:
                Screen.SetResolution(1920, 1080, isFullscreen);
                break;
            case 1:
                Screen.SetResolution(1440, 810, isFullscreen);
                break;
            case 2:
                Screen.SetResolution(1280, 720, isFullscreen);
                break;
            case 3:
                Screen.SetResolution(640, 360, isFullscreen);
                break;
            default:
                break;
        }
    }
}