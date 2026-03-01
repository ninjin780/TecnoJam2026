using UnityEngine;

public class DialogeOutOfBounds : MonoBehaviour
{
    [SerializeField] private DialogueRoundSO dialogue;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void OnTriggerExit2D(Collider2D collision)
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
