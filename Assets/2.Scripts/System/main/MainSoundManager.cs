using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class MainSoundManager : MonoBehaviour
{
    public static MainSoundManager Instance { get; private set; }

    public enum SoundFXType
    {
        Explode,
        ElevatorRising,
        ElevatorDoorOpen,
        Slash,
        ShootLazer,
        WolfAttack
    }

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
    [Range(0f, 1f)]
    [SerializeField]
    private float _masterVolume = 1f;

    private bool _isMute;


    private void Awake()
    {
        Instance = this;

        GetSoundsFromResources();

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
        audio.volume = GetCurrentVolume();
        audio.loop = false;
        audio.clip = _audioClips[fileName];
        audio.Play();
    }

    private float GetCurrentVolume()
    {
        return _isMute ? 0 : _fxVolume * _bgVolume * _masterVolume;
    }
}
