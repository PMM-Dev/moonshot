using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundHelper : MonoBehaviour
{
    private AudioSource _audioSource;
    private AudioSource _loopAudioSource;
    private MainSoundManager _soundManager;

    [Range(0f, 1000f)]
    [SerializeField]
    private float _minDistance = 10f;
    [Range(0f, 1000f)]
    [SerializeField]
    private float _maxDistance = 100f;
    [Range(0f, 1f)]
    [SerializeField]
    private float _audioVolume = 1f;

    private void Awake()
    {
        Initialize();
    }

    private void Initialize()
    {
        _audioSource = gameObject.AddComponent<AudioSource>();
        _loopAudioSource = gameObject.AddComponent<AudioSource>();

        InitializeAudioSource(_audioSource);
        InitializeAudioSource(_loopAudioSource);
    }

    private void InitializeAudioSource(AudioSource audioSource)
    {
        audioSource.minDistance = _minDistance;
        audioSource.maxDistance = _maxDistance;
        AnimationCurve ac = audioSource.GetCustomCurve(AudioSourceCurveType.SpatialBlend);
        Keyframe[] keys = new Keyframe[1];

        for (int i = 0; i < keys.Length; ++i)
        {
            keys[i].value = 1f;
        }

        ac.keys = keys;
        audioSource.SetCustomCurve(AudioSourceCurveType.SpatialBlend, ac);

        audioSource.loop = false;
    }

    private void Start()
    {
        _soundManager = MainSoundManager.Instance;
        _loopAudioSource.loop = true;
    }

    public void PlaySound(bool isLoop, string clipName)
    {
        _audioSource.volume = _audioVolume * _soundManager.GetCurrentFXVolume();

        PlaySoundByType(isLoop, clipName);
    }

    public void PlaySound(bool isLoop, string clipName, float customVolume)
    {
        _audioSource.volume = customVolume * _soundManager.GetCurrentFXVolume();

        PlaySoundByType(isLoop, clipName);

        _audioSource.volume = _audioVolume;
    }

    public void PlaySound(bool isLoop, string clipName, float customVolume, float tempMinDistance, float tempMaxDistance)
    {
        _audioSource.volume = customVolume * _soundManager.GetCurrentFXVolume();
        _audioSource.minDistance = tempMinDistance;
        _audioSource.maxDistance = tempMaxDistance;

        PlaySoundByType(isLoop, clipName);

        _audioSource.minDistance = _minDistance;
        _audioSource.maxDistance = _maxDistance;
        _audioSource.volume = _audioVolume;
    }

    private void PlaySoundByType(bool isLoop, string clipName)
    {
        if (!isLoop)
            _soundManager.PlayFXSound(ref _audioSource, clipName);
        else
            _soundManager.PlayLoopSound(ref _loopAudioSource, clipName);
    }
}
