using UnityEngine;
using UnityEngine.UIElements;
using static UnityEngine.Rendering.DebugUI;

public class Jumpscare : MonoBehaviour
{
    [SerializeField] private GameObject image;
    [SerializeField] private AudioClip scream;
    [SerializeField] private AudioClip bagKidnap1;
    [SerializeField] private AudioClip bagKidnap2;
    private AudioSource audioSource;

    private void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
    }
    private void OnEnable()
    {
        DialogueManager.OnDialogueFinished += ShowJumpscare;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueFinished -= ShowJumpscare;
    }

    private void ShowJumpscare()
    {
        image.SetActive(true);

        audioSource.PlayOneShot(bagKidnap1);
        audioSource.PlayOneShot(scream);
        audioSource.PlayOneShot(bagKidnap2);
    }
}
