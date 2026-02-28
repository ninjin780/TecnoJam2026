using UnityEngine;
using UnityEngine.InputSystem;

public class DialogueTrigger : MonoBehaviour
{
    [SerializeField] private DialogueRoundSO dialogue;
    [SerializeField] private SpriteRenderer showInteractable;
    public bool IsPlayerInRange;

    private InputAction interaction;

    [ContextMenu("Trigger Dialogue")]

    private void Start()
    {
        interaction = InputSystem.actions.FindAction("Player/Interact");
    }

    private void Update()
    {
        if (interaction.WasPressedThisFrame() && IsPlayerInRange)
        {
            TriggerDialogue();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        IsPlayerInRange = true;
        showInteractable.enabled = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        IsPlayerInRange = false;
        showInteractable.enabled = false;
    }

    public void TriggerDialogue()
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
