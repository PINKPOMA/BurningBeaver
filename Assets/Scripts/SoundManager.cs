using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource audioSource;

    [Header("AudioClips")]
    public AudioClip buttonClickSound;
    public AudioClip scoopOutWaterSound;
    public AudioClip banSound;
    public AudioClip dieSound;
    public AudioClip explosionSound;

    protected override void Awake()
    {
        base.Awake();
        audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip audioClip)
    {
        audioSource.PlayOneShot(audioClip);
    }
}
