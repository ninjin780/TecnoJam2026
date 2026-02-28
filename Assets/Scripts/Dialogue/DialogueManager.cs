using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private DialogueUI dialogueUI;
    public bool IsDialogueInProgress { get; private set; } = false;

    public static event Action<bool> OnDialogueStateChange;
    public static event Action OnDialogueFinished;

    [SerializeField] private float typingSpeed = 0.03f;

    [Header("Sound Parameters")]
    [Range(1, 5)]
    [SerializeField] private int frequency = 2;

    [SerializeField] private float maxPitch = 1.05f;
    [SerializeField] private float minPitch = 0.95f;

    [SerializeField] private float maxVol = 1.0f;
    [SerializeField] private float minVol = 0.9f;

    [SerializeField] private AudioClip audioClip;
    private AudioSource audioSource;

    public bool StopAudioSource = false; // Hace que no se superponga el sonido

    private Queue<DialogueTurn> dialogueTurnQueue;
    private InputAction clickedInputAction;

    private void Awake()
    {
        clickedInputAction = InputSystem.actions.FindAction("Dialogue/ClickAnywhere");
        audioSource = gameObject.AddComponent<AudioSource>();

        Instance = this;
        dialogueUI.ChangeDialogueBoxState(false);
    }
    
    public void StartDialogue(DialogueRoundSO dialogueRound)
    {
        if (IsDialogueInProgress) return;

        dialogueTurnQueue = new Queue<DialogueTurn>(dialogueRound.dialogueTurnList);
        
        IsDialogueInProgress = true;
        OnDialogueStateChange?.Invoke(true);

        StartCoroutine(DialogueCoroutine());
    }

    public IEnumerator DialogueCoroutine()
    {
        dialogueUI.ChangeDialogueBoxState(true);

        while (dialogueTurnQueue.Count > 0)
        {
            DialogueTurn turn = dialogueTurnQueue.Dequeue();

            dialogueUI.SetCharacterInfo(turn.CharacterSO);
            dialogueUI.ClearDialogueTextArea();

            yield return StartCoroutine(TypeLine(turn));
            yield return new WaitUntil(() => clickedInputAction.WasPressedThisFrame());
            yield return null;
        }

        dialogueUI.ChangeDialogueBoxState(false);

        OnDialogueStateChange?.Invoke(false);
        OnDialogueFinished?.Invoke();

        IsDialogueInProgress = false;
    }

    public IEnumerator TypeLine(DialogueTurn turn)
    {
        var typingSecondsWait = new WaitForSeconds(typingSpeed);
        char[] dialogueLine = turn.DialogueLine.ToCharArray();
        int amountOfLetters = 0;

        foreach(char letter in dialogueLine)
        {
            dialogueUI.TypeInDialogueArea(letter);

            PlayLetterSound(amountOfLetters);
            amountOfLetters++;

            yield return typingSecondsWait;
        }
    }

    private void PlayLetterSound(int amountOfLetters)
    {
        if (amountOfLetters % frequency == 0)
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
