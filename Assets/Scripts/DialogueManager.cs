using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [Header("Dialogue Param")]
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private int currentIndex;

    [SerializeField] private float typingSpeed = 0.05f;
    private bool dialogueFinished = false;

    InputAction click;

    [Header("Sound Param")]
    [Range(1, 5)]
    [SerializeField] private int frequency = 2;

    [SerializeField] private float maxPitch = 1.05f;
    [SerializeField] private float minPitch = 0.95f;

    [SerializeField] private float maxVol = 1.0f;
    [SerializeField] private float minVol = 0.9f;
    
    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;

    public bool StopAudioSource = false; // Hace que no se superponga el sonido

    private void Awake()
    {
        click = InputSystem.actions.FindAction("Dialogue/ClickAnywhere");
        audioSource = gameObject.AddComponent<AudioSource>();
    }

    private void Start()
    {
        StartDialogue();
    }

    private void Update()
    {
        if (click.WasPressedThisFrame())
        {
            if (!dialogueFinished && dialogueText.text == dialogueLines[currentIndex])
            {
                TypeNextLine();
            }
            else
            {
                StopAllCoroutines();
                if (currentIndex < dialogueLines.Length) dialogueText.text = dialogueLines[currentIndex];
            }

            if (dialogueFinished)
            {
                ChangeScene.SetNextScene();
            }
        }
    }

    private void StartDialogue()
    {
        currentIndex = 0;
        dialogueFinished = false;
        StartCoroutine(TypeLine());
    }

    private IEnumerator TypeLine()
    {
        dialogueText.text = string.Empty;
        int amountOfCharacters = 0;

        foreach (char character in dialogueLines[currentIndex])
        {
            dialogueText.text += character;

            PlayCharacterSound(amountOfCharacters);
            amountOfCharacters++;

            yield return new WaitForSeconds(typingSpeed);
        }
    }

    private void TypeNextLine()
    {
        currentIndex++;

        if (currentIndex < dialogueLines.Length)
        {
            StartCoroutine(TypeLine());
        }
        else if (currentIndex == dialogueLines.Length)
        {
            dialogueFinished = true;
        }
    }

    private void PlayCharacterSound(int amount)
    {
        if (amount % frequency == 0)
        {
            if (!StopAudioSource)
            {
                audioSource.Stop();
            }
            
            audioSource.pitch = Random.Range(minPitch, maxPitch);
            audioSource.volume = Random.Range(minVol, maxVol);
            audioSource.PlayOneShot(audioClip);
        }
    }
}
