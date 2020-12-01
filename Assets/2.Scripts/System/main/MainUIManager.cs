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
    private Toggle _fullscrenToggle;
    [SerializeField]
    private Dropdown _resolutionDropdown;
    private bool _isFullscreen;
    [SerializeField]
    private Toggle _cameraFXToggle;
    private bool _isCameraFXOn;
    private CameraFx _cameraFX;
    [SerializeField]
    private Slider _masterVolumeSlider;
    [SerializeField]
    private Slider _bgmVolumeSlider;
    [SerializeField]
    private Slider _fxVolumeSlider;
    [SerializeField]
    private Text _versionText;

    [SerializeField]
    private Button _quitButton;
    [SerializeField]
    private Button _resumeButton;
    [SerializeField]
    private Button _restartButton;
    [SerializeField]
    private Button _muteButton;

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
    GameObject DarkPanel;

    [SerializeField]
    private MainGameManager _mainGameManager;

    private bool _isOpenOption = false;

    private void Start()
    {
        _cameraFX = Camera.main.GetComponent<CameraFx>();

        MainEventManager.Instance.ClearMainGameEvent += StartEnding;

        _masterVolumeSlider.value = MainSoundManager.Instance.MasterVolume;
        _bgmVolumeSlider.value = MainSoundManager.Instance.BGVolume;
        _fxVolumeSlider.value = MainSoundManager.Instance.FXVolume;

        _isFullscreen = PlayerPrefs.GetInt("fullscreen", 1) == 1 ? true : false;
        _isCameraFXOn = PlayerPrefs.GetInt("CameraFXOn", 1) == 1 ? true : false;
        _cameraFX.IsCameraFXOn = _isCameraFXOn;
        if (_isFullscreen)
        {
            _fullscrenToggle.isOn = true;
        }
        else
        {
            _fullscrenToggle.isOn = false;
        }
        if (_isCameraFXOn)
        {
            _cameraFXToggle.isOn = true;
        }
        else
        {
            _cameraFXToggle.isOn = false;
        }

        InitFullscreen(_isFullscreen);
        _resolutionDropdown.value = SetResolution(PlayerPrefs.GetInt("resolution", 1280));

        Cursor.lockState = CursorLockMode.Confined;

        _fullscrenToggle.onValueChanged.AddListener(delegate { OnFullscreenTogleChanged(_fullscrenToggle); });
        _cameraFXToggle.onValueChanged.AddListener(delegate { OnCameraFXToggleChanged(_cameraFXToggle); });
        _resolutionDropdown.onValueChanged.AddListener(delegate { OnResolutionDropdownChanged(_resolutionDropdown); });
        _masterVolumeSlider.onValueChanged.AddListener(delegate { MainSoundManager.Instance.OnClickMasterVolume(_masterVolumeSlider.value); });
        _bgmVolumeSlider.onValueChanged.AddListener(delegate { MainSoundManager.Instance.OnClickBackgroundVolume(_bgmVolumeSlider.value); });
        _fxVolumeSlider.onValueChanged.AddListener(delegate { MainSoundManager.Instance.OnClickFXVolume(_fxVolumeSlider.value); });

        _quitButton.onClick.AddListener(delegate { OnClickQuite(); });
        _resumeButton.onClick.AddListener(delegate { OnClickOptionResume(); });
        _restartButton.onClick.AddListener(delegate { OnClickQuickRestart(); });
        _muteButton.onClick.AddListener(delegate { MainSoundManager.Instance.SwitchMute(); });

        _versionText.text = Application.version;
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            OnClickQuickRestart();
        }
    }

    public void OnClickStart()
    {
        _mainGameManager.GameStart();

        _startPanel.SetActive(false);
        _mainPanel.SetActive(true);

        StartCoroutine(FalsePanel());
    }

    public void OnClickQuickRestart()
    {
        SceneManager.LoadScene("RestartedMain");
    }

    public void OnClickOption()
    {
        _optionPanel.SetActive(true);
        _isOpenOption = true;
        Cursor.lockState = CursorLockMode.None;
        MainEventManager.Instance.PauseGamePlayEvent?.Invoke();
    }

    public void OnClickOptionResume()
    {
        _optionPanel.SetActive(false);
        _isOpenOption = false;
        Cursor.lockState = CursorLockMode.Confined;
        MainEventManager.Instance.ResumeGamePlayEvent?.Invoke();
    }

    private void InitFullscreen(bool isFullscreen)
    {
        if (isFullscreen)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
        }
    }

    private void OnFullscreenTogleChanged(Toggle change)
    {
        if (change.isOn)
        {
            Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            PlayerPrefs.SetInt("fullscreen", 1);
            _isFullscreen = true;
        }
        else
        {
            Screen.fullScreenMode = FullScreenMode.Windowed;
            PlayerPrefs.SetInt("fullscreen", 0);
            _isFullscreen = false;
        }
    }

    private void OnCameraFXToggleChanged(Toggle change)
    {
        if (change.isOn)
        {
            
            PlayerPrefs.SetInt("CameraFXOn", 1);
            _isCameraFXOn = true;
        }
        else
        {
            PlayerPrefs.SetInt("CameraFXOn", 0);
            _isCameraFXOn = false;
        }
        _cameraFX.IsCameraFXOn = _isCameraFXOn;
    }

    private void OnResolutionDropdownChanged(Dropdown change)
    {
        SetResolution(change.value);
    }

    private int SetResolution(int index)
    {
        switch (index)
        {
            case 3840:
            case 0:
                Screen.SetResolution(3840, 2160, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 3840);
                return 0;
            case 2880:
            case 1:
                Screen.SetResolution(2880, 1800, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 2880);
                return 1;
            case 2560:
            case 2:
                Screen.SetResolution(2560, 1440, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 2560);
                return 2;
            case 1920:
            case 3:
                Screen.SetResolution(1920, 1080, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 1920);
                return 3;
            case 1600:
            case 4:
                Screen.SetResolution(1600, 900, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 1600);
                return 4;
            case 1280:
            case 5:
                Screen.SetResolution(1280, 720, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 1280);
                return 5;
            case 1024:
            case 6:
                Screen.SetResolution(1024, 768, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 1024);
                return 6;
            case 800:
            case 7:
                Screen.SetResolution(800, 600, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 800);
                return 7;
            case 640:
            case 8:
                Screen.SetResolution(640, 480, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 640);
                return 8;
            case 480:
            case 9:
                Screen.SetResolution(480, 360, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 480);
                return 9;
            default:
                Screen.SetResolution(1280, 720, _isFullscreen);
                PlayerPrefs.SetInt("resolution", 1280);
                return 5;
        }
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

    private IEnumerator FalsePanel()
    {
        float progress = 0f;

        Color origin = new Color(0f, 0f, 0f, 1f);
        Color target = new Color(0f, 0f, 0f, 0f);

        SpriteRenderer sprRend = DarkPanel.GetComponent<SpriteRenderer>();
        while (progress < 1f)
        {
            progress += Time.deltaTime * 1f;
            sprRend.color = Color.Lerp(origin, target, progress);
            yield return null;
        }
        DarkPanel.SetActive(false);
        yield return null;
    }
}
