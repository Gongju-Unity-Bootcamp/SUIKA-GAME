using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject[] shooter;

    private void Update()
    {
        OnCheckGameOver();
    }

    private void OnCheckGameOver()
    {
        bool _isCleared = true;

        for (int i = 0; i < shooter.Length; ++i)
        {
            _isCleared &= !shooter[i].activeSelf;
        }

        if (_isCleared)
        {
            SceneManager.LoadScene("GameOver");
        }
    }
}
