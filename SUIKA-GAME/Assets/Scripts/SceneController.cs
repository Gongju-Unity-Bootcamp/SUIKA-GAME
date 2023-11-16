using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnStartClick()
    {
        SceneManager.LoadScene("Ingame");
    }

    public void OnOptionClick()
    {
        SceneManager.LoadScene("Option");
    }

    public void OnQuitClick()
    {
        Application.Quit();
    }
}
