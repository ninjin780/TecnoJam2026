using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public static ChangeScene instance;

    private void Awake()
    {
        instance = this;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetNextScene();
    }

    public void SetNextScene()
    {
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex + 1);
    }
}
