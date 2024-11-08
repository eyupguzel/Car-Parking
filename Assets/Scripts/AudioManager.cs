using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip diamondSound;
    [SerializeField] private AudioClip menuClickSound;
    [SerializeField] private AudioClip menuClickSound2;
    [SerializeField] private AudioClip menuClickSound3;
    
    public enum AudioType
    {
        CrashSound,
        DiamondSound,
        Click_1,
        Click_2,
        Click_3
    }
    public void SoundPlay(AudioType _audioType)
    {
        switch (_audioType)
        {
            case AudioType.CrashSound: audioSource.PlayOneShot(crashSound);
                break;
            case AudioType.DiamondSound: audioSource.PlayOneShot(diamondSound);
                break;
            case AudioType.Click_1: audioSource.PlayOneShot(menuClickSound);
                break;
            case AudioType.Click_2: audioSource.PlayOneShot(menuClickSound2);
                break;
            case AudioType.Click_3: audioSource.PlayOneShot(menuClickSound3);
                break;
        }
    }
}
