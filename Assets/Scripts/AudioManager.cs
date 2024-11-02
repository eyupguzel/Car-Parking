using UnityEngine;

public class AudioManager : GenericSingleton<AudioManager>
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip crashSound;
    [SerializeField] private AudioClip diaomndSound;

    public void CrashSound()
    {
        audioSource.PlayOneShot(crashSound);
    }

    public void CollactableDiamondSound()
    {
        audioSource.PlayOneShot(diaomndSound);
    }
}
