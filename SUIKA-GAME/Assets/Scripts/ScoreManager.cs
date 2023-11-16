using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    public Text scoreText;
    public Text scoreSign;
    int score = 0;

    private void Start()
    {
        SetText();
    }

    private void Update()
    {
        //GetScore();
    }

    private void GetScore()
    {
        score += 10;
        SetText();
    }

    private void SetText()
    {
        scoreSign.text = score.ToString();
    }
}
