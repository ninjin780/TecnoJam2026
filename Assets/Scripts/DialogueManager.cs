using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance;

    [SerializeField] private DialogueUI dialogueUI;
    public bool IsDialogueInProgress { get; private set; } = false;
    public static event Action<bool> OnDialogueStateChange;

    [SerializeField] private float typingSpeed = 0.03f;

    private Queue<DialogueTurn> dialogueTurnQueue;
    private InputAction clickedInputAction;

    private void Awake()
    {
        clickedInputAction = InputSystem.actions.FindAction("Dialogue/ClickAnywhere");

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
        IsDialogueInProgress = false;
    }

    public IEnumerator TypeLine(DialogueTurn turn)
    {
        var typingSecondsWait = new WaitForSeconds(typingSpeed);
        char[] dialogueLine = turn.DialogueLine.ToCharArray();

        foreach(char letter in dialogueLine)
        {
            dialogueUI.TypeInDialogueArea(letter);
            yield return typingSecondsWait;
        }
    }
}
