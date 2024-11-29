using System;
using Car;
using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioSource carAudioSource;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip diamondSound;
    [SerializeField] private AudioClip menuClickSound1;
    [SerializeField] private AudioClip menuClickSound2;
    [SerializeField] private AudioClip menuClickSound3;


    void Start()
    {
        GameManager.Instance.CrashHandler += CrashSound;
        CarController.collectDiamond += DiamondSound;
    }

    public void DiamondSound()
    {
        audioSource.PlayOneShot(diamondSound);

    }

    public void CrashSound()
    {
       audioSource.PlayOneShot(crashSound);

    }

    public void MenuClickSound1()
    {
        audioSource.PlayOneShot(menuClickSound1);

    }
    public void MenuClickSound2()
    {
        audioSource.PlayOneShot(menuClickSound2);
    }

    public void MenuClickSound3()
    {
       audioSource.PlayOneShot(menuClickSound3);
    }
    
    
}
