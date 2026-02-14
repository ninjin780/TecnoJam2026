using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.InputSystem;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private GameObject DialoguePanel;
    [SerializeField] private TMP_Text dialogueText;

    [SerializeField, TextArea(4, 6)] private string[] dialogueLines;
    private int currentIndex;

    [SerializeField] private float typingSpeed = 0.05f;
    private bool dialogueFinished = false;

    InputAction click;

    private void Awake()
    {
        click = InputSystem.actions.FindAction("Dialogue/ClickAnywhere");
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
                dialogueText.text = dialogueLines[currentIndex];
            }

            if (dialogueFinished)
            {
                // ir a la siguiente escena
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

        foreach (char character in dialogueLines[currentIndex])
        {
            dialogueText.text += character;
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
}
