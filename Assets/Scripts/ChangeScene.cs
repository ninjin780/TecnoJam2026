using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{   
    private void OnCollisionEnter2D(Collision2D collision)
    {
        SetNextScene();
    }

    public void SetNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        SceneTransition.Instance.FadeAndLoad(nextScene);
    }
}
