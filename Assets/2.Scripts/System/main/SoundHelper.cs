using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SoundHelper : MonoBehaviour
{
    private AudioSource _audioSource;
    private MainSoundManager _soundManager;

    [Range(0f, 1000f)]
    [SerializeField]
    private float _minDistance = 10f;
    [Range(0f, 1000f)]
    [SerializeField]
    private float _maxDistance = 100f;

    private void Awake()
    {
        InitializeAudioSource();
    }

    private void InitializeAudioSource()
    {
        _audioSource = GetComponent<AudioSource>();
        _audioSource.minDistance = _minDistance;
        _audioSource.maxDistance = _maxDistance;
        AnimationCurve ac = _audioSource.GetCustomCurve(AudioSourceCurveType.SpatialBlend);
        Keyframe[] keys = new Keyframe[1];

        for (int i = 0; i < keys.Length; ++i)
        {
            keys[i].value = 1f;
        }

        ac.keys = keys;
        _audioSource.SetCustomCurve(AudioSourceCurveType.SpatialBlend, ac);
    }

    private void Start()
    {
        _soundManager = MainSoundManager.Instance;
    }

    public void PlaySound(MainSoundManager.SoundFXType type)
    {
        _soundManager.PlayFXSound(ref _audioSource, type);
    }
}
