using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    [Header("------------Audio Source------------")]
    [SerializeField] AudioSource SFXSource;

    [Header("------------Audio Clip------------")]
    public AudioClip Hurt_01;
    public AudioClip Jump_01;
    public AudioClip Jump_02;
    public AudioClip Pickup_01;
    public AudioClip Punch_01;
    public AudioClip Kick_01;
    public AudioClip Purificator_01;
    public AudioClip WebShoot_01;
    public AudioClip DieEnemy_01;
    public AudioClip Boom;
    public AudioClip Hit;
    public AudioClip Heal_01;

    public void PlaySFX(AudioClip clip)
    {
        SFXSource.PlayOneShot(clip);
    }

}
