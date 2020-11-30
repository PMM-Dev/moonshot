using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;
using UnityEngine.UI;

public class MainSoundManager : MonoBehaviour
{
    public static MainSoundManager Instance { get; private set; }
    //
    // SINGLETON

    [SerializeField]
    private AudioSource _audioSource;

    // Resources - Sound - FX
    private AudioClip[] _clipFiles;
    private Dictionary<string, AudioClip> _audioClips;

    [Range(0f, 1f)]
    [SerializeField]
    private float _fxVolume = 1f;
    [Range(0f, 1f)]
    [SerializeField]
    private float _bgVolume = 1f;
    [Range(0f, 0.4f)]
    private float _correctionBackGroundValue = 0.2f;

    [SerializeField]
    private Image _muteIcon;

    public float BGVolume
    {
        get { return _bgVolume; }
        set
        {
            _bgVolume = value;
            _audioSource.volume = GetCurrentBGVolume();
        }
    }
    [Range(0f, 1f)]
    [SerializeField]
    private float _masterVolume = 1f;

    private int _isMute;


    private void Awake()
    {
        Instance = this;

        GetSoundsFromResources();

        if (PlayerPrefs.HasKey("isMute"))
            _isMute = PlayerPrefs.GetInt("isMute");
        else
        {
            _isMute = 0;
            PlayerPrefs.SetInt("isMute", _isMute);
        }
    }

    private void Start()
    {
        _muteIcon.color = _isMute == 1 ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0.5f);
        if (MainEventManager.Instance != null)
        {
            MainEventManager.Instance.StartMainGameEvent += PlayBGM;
            MainEventManager.Instance.ClearMainGameEvent += PlayEndingBGM;
        }

    }

    private void GetSoundsFromResources()
    {
        _clipFiles = Resources.LoadAll<AudioClip>("Sound/FX");

        _audioClips = new Dictionary<string, AudioClip>();
        for (int i = 0; i < _clipFiles.Length; i++)
        {
            _audioClips.Add(_clipFiles[i].name, _clipFiles[i]);
        }
    }

    public void PlayFXSound(ref AudioSource audio, string fileName)
    {
        audio.volume = GetCurrentFXVolume();
        audio.PlayOneShot(_audioClips[fileName]);
    }

    public void PlayLoopSound(ref AudioSource audio, string fileName)
    {
        audio.loop = true;
        audio.volume = GetCurrentFXVolume();
        audio.clip = _audioClips[fileName];
        audio.Play();
    }

    public float GetCurrentFXVolume()
    {
        return _isMute == 1 ? 0 : _fxVolume * _masterVolume;
    }

    public float GetCurrentBGVolume()
    {
        return _isMute == 1 ? 0 : _bgVolume * _masterVolume * _correctionBackGroundValue;
    }

    public void StopBGM()
    {
        StartCoroutine(FadeOut());
    }

    public IEnumerator FadeOut()
    {
        float originVolume = _audioSource.volume;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f;
            _audioSource.volume = Mathf.Lerp(originVolume, 0f, t);

            yield return null;
        }

        _audioSource.Stop();
    }

    public IEnumerator PlayEndingFade()
    {
        StopBGM();
        yield return new WaitForSeconds(3f);

        _audioSource.loop = true;
        _audioSource.clip = _audioClips["Ending"];

        float targetVolume = GetCurrentBGVolume();

        _audioSource.Play();

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * 0.5f;
            _audioSource.volume = Mathf.Lerp(0f, targetVolume, t);

            yield return null;
        }

   
    }

    public void PlayBGM()
    {
        _audioSource.loop = true;
        _audioSource.clip = _audioClips["MainTheme"];
        _audioSource.volume = GetCurrentBGVolume() ;
        _audioSource.Play();
    }

    public void PlayBossBGM()
    {
        _audioSource.loop = true;
        _audioSource.clip = _audioClips["backOfMoon"];
        _audioSource.volume = GetCurrentBGVolume() ;
        _audioSource.Play();
    }

    public void PlayEndingBGM()
    {
        StartCoroutine(PlayEndingFade());
    }

    public void SwitchMute()
    {
        _isMute = _isMute == 1 ? 0 : 1;
        PlayerPrefs.SetInt("isMute", _isMute);
        _muteIcon.color = _isMute == 1 ? new Color(1f, 1f, 1f, 1f) : new Color(1f, 1f, 1f, 0.5f);
        _audioSource.volume = GetCurrentBGVolume() ;
    }
}
