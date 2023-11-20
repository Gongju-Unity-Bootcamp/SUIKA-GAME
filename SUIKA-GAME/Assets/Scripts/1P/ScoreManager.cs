using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{
    [SerializeField] private Text scoreText; // 스코어 택스트 지정을 위한 텍스트 변수 값
    public static Text scoreValue; // 스코어 점수의 택스트 지정을 위한 택스트 변수 값
    public static int score; // 스코어 점수 저장 값

    private void Start() // 스타트 메소드
    {
        OnScoreText(); // 스코어 텍스트 제작 메소드
    }

    private void OnScoreText() // 스코어 텍스트 제작 메소드
    {
        score = PlayerPrefs.GetInt("Score"); // 스코어 정수 변수를 지정 변수에서 가져온다
        scoreValue = GetComponent<Text>(); // 스코어 텍스트 값을 변경하기 위해 Text 컴포넌트를 가져온다
        PlayerPrefs.SetString("IsScored", "true"); // 지정 변수의 스코어 설정 참 or 거짓 불리언 값을 true로 변경한다
        OnSetScore(score); // 스코어를 설정한다
    }

    public static void OnSetScore(int _score) // 스코어 점수 설정 메소드
    {
        if (PlayerPrefs.GetString("IsScored").Equals("true")) // 만약 지정 변수의 스코어 설정 참 or 거짓 불리언 값이 true 라면
        {
            score = _score; // 이 스크립트의 스코어 점수 지정 값을 _score 인자 값으로 설정한다
            scoreValue.text = score.ToString();
            // 스코어 점수의 텍스트 컴포넌트를 가져와 그 텍스트를 스코어로 문자열 형변환 시킨다
            PlayerPrefs.SetString("IsScored", "false");
            // 지정 변수의 스코어 설정 참 or 거짓 불리언 값을 false로 바꿔 스코어 변경을 알린다
        }
        else if (PlayerPrefs.GetString("IsScored").Equals("false")) // 만약 지정 변수의 스코어 설정 참 or 거짓 불리언 값이 false 라면
        {
            score += _score; // 이 스크립트의 스코어 점수 저장 값을 _score 인자 값으로 더한다
            scoreValue.text = score.ToString();
            // 스코어 점수의 텍스트 컴포넌트를 가져와 그 텍스트를 스코어로 문자열 형변환 시킨다
        }
    }
}
