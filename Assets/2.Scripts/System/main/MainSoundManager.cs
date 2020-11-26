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

    [SerializeField]
    private AudioClip _explode;
    [SerializeField]
    private AudioClip _elevatorRising;
    [SerializeField]
    private AudioClip _elevatorDoorOpen;
    [SerializeField]
    private AudioClip _slash;
    [SerializeField]
    private AudioClip _shootLazer;
    [SerializeField]
    private AudioClip _wolfAttack;

    private Dictionary<SoundFXType, AudioClip> _clips;

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
        _clips = new Dictionary<SoundFXType, AudioClip>()
        {
            { SoundFXType.Explode, _explode },
            { SoundFXType.ElevatorRising, _elevatorRising },
            { SoundFXType.ElevatorDoorOpen, _elevatorDoorOpen },
            { SoundFXType.Slash, _slash },
            { SoundFXType.ShootLazer, _shootLazer },
            { SoundFXType.WolfAttack, _wolfAttack }
        };
    }

    public void PlayFXSound(ref AudioSource audio, SoundFXType type)
    {
        audio.volume = GetCurrentVolume();
        audio.loop = false;
        audio.clip = _clips[type];
        audio.Play();
    }

    private float GetCurrentVolume()
    {
        return _isMute ? 0 : _fxVolume * _bgVolume * _masterVolume;
    }
}
