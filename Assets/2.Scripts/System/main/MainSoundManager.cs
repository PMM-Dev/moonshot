using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainSoundManager : MonoBehaviour
{
    public static MainSoundManager Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
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

    public void PlayExplodeSound()
    {
        _audioSource.PlayOneShot(_explode);
    }

    public void PlayElevatorRisingSound()
    {
        _audioSource.PlayOneShot(_elevatorRising);
    }

    public void PlayElevatorDoorOpenSound()
    {
        _audioSource.PlayOneShot(_elevatorDoorOpen);
    }

    public void PlaySlashSound()
    {
        _audioSource.PlayOneShot(_slash);
    }

    public void PlayShootLazerSound()
    {
        _audioSource.PlayOneShot(_shootLazer);
    }

    public void PlayWolfAttackSound()
    {
        _audioSource.PlayOneShot(_wolfAttack);
    }
}
