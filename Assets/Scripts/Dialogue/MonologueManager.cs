using UnityEngine;
using UnityEngine.SceneManagement;

public class MonologueManager : MonoBehaviour
{
    [SerializeField] private DialogueRoundSO dialogue;

    private void Start()
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
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
}
