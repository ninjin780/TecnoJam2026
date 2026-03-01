using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionDialogue : MonoBehaviour
{
    [SerializeField] private DialogueRoundSO dialogue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void OnEnable()
    {
        DialogueManager.OnDialogueFinished += NextScene;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueFinished -= NextScene;
    }

    private void NextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneTransition.Instance.FadeAndLoad(nextScene);
    }
}
