using UnityEngine;
using UnityEngine.SceneManagement;

public class Buttons : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("Escena01Monologue");
    }

    public void ExitGame()
    {
        Application.Quit(); 
    }
}
