using UnityEngine;

public class DialogeOutOfBounds : MonoBehaviour
{
    [SerializeField] private DialogueRoundSO dialogue;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }
}
