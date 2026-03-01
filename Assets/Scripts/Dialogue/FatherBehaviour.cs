using UnityEngine;
using UnityEngine.SceneManagement;

public class FatherBehaviour : MonoBehaviour
{
    public bool HasPlayerTalkedToFather = false;

    private void OnEnable()
    {
        DialogueManager.OnDialogueFinished += EnableLeaving;
    }

    private void OnDisable()
    {
        DialogueManager.OnDialogueFinished -= EnableLeaving;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (HasPlayerTalkedToFather)
        {
            NextScene();
        }
    }

    private void EnableLeaving()
    {
        HasPlayerTalkedToFather = true;
    }

    private void NextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneTransition.Instance.FadeAndLoad(nextScene);
    }
}
