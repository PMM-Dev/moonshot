using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

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

    public float BGVolume
    {
        get { return _bgVolume; }
        set
        {
            _bgVolume = value;
            _audioSource.volume = GetCurrentBGVolume() * 0.6f;
        }
    }
    [Range(0f, 1f)]
    [SerializeField]
    private float _masterVolume = 1f;

    private bool _isMute;


    private void Awake()
    {
        Instance = this;

        GetSoundsFromResources();

    }

    private void Start()
    {
        if (MainEventManager.Instance != null)
            MainEventManager.Instance.StartMainGameEvent += PlayBGM;
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
        audio.PlayOneShot(_audioClips[fileName]);
    }

    public void PlayLoopSound(ref AudioSource audio, string fileName)
    {
        audio.loop = true;
        audio.clip = _audioClips[fileName];
        audio.Play();
    }

    public float GetCurrentFXVolume()
    {
        return _isMute ? 0 : _fxVolume * _masterVolume;
    }

    public float GetCurrentBGVolume()
    {
        return _isMute ? 0 : _bgVolume * _masterVolume;
    }

    public void PlayBGM()
    {
        _audioSource.loop = true;
        _audioSource.clip = _audioClips["MainTheme"];
        _audioSource.volume = GetCurrentBGVolume() * 0.6f;
        _audioSource.Play();
    }
}
