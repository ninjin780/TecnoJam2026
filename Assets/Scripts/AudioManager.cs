using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip carCrash;
    public AudioClip motorSound;

    private AudioSource audioSource;

    private void Awake()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    public void PlayCarCrashSound()
    {
        audioSource.PlayOneShot(carCrash);
    }

    public void PlayCarMotorSound()
    {
        audioSource.PlayOneShot(motorSound);
    }
}
