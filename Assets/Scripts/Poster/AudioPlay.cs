using UnityEngine;

public class AudioPlay : MonoBehaviour
{
    private AudioSource AudioSource;
    [SerializeField] private AudioClip audioClip;

    private void Start()
    {
        AudioSource = GetComponent<AudioSource>();
    }

    private void OnEnable()
    {
        PosterSlot.CorrectDrop += PlaySound;
    }

    private void OnDisable()
    {
        PosterSlot.CorrectDrop -= PlaySound;
    }

    private void PlaySound()
    {
        if (audioClip != null)
        {
            AudioSource.PlayOneShot(audioClip);
        }
    }
}
