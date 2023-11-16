using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText; // 스코어 택스트 지정을 위한 텍스트 변수 값
    public static Text scoreValue; // 스코어 점수의 택스트 지정을 위한 택스트 변수 값
    public static int score; // 스코어 점수 저장 값

    private void Start()
    {
        OnScoreText();
    }

    private void OnScoreText()
    {
        score = PlayerPrefs.GetInt("Score");
        scoreValue = GetComponent<Text>();
        PlayerPrefs.SetString("IsScored", "true");
        OnSetScore(score);
    }

    public static void OnSetScore(int _score) // 스코어 점수 설정 메소드
    {
        if (PlayerPrefs.GetString("IsScored").Equals("true"))
        {
            score = _score;
            scoreValue.text = score.ToString();
            PlayerPrefs.SetString("IsScored", "false");
        }
        else if (PlayerPrefs.GetString("IsScored").Equals("false"))
        {
            score += _score; // 이 스크립트의 스코어 점수 저장 값을 _score 인자 값으로 더한다
            scoreValue.text = score.ToString();
            // 스코어 점수의 텍스트 컴포넌트를 가져와 그 텍스트를 스코어로 문자열 형변환 시킨다
        }
    }
}
