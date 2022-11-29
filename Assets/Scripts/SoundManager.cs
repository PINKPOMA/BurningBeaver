using UnityEngine;

public class SoundManager : Singleton<SoundManager>
{
    private AudioSource _audioSource;

    [Header("AudioClips")]
    public AudioClip buttonClickSound;
    public AudioClip scoopOutWaterSound;
    public AudioClip banSound;
    public AudioClip dieSound;
    public AudioClip explosionSound;

    protected override void Awake()
    {
        base.Awake();
        _audioSource = GetComponent<AudioSource>();
    }

    public void Play(AudioClip audioClip)
    {
        _audioSource.PlayOneShot(audioClip);
    }
}
