using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverController : MonoBehaviour
{
    [SerializeField] private GameObject[] shooter; // 슈터 게임 오브젝트를 받는 변수 값
    [SerializeField] private GameObject player1P; // 플레이어1 승리 텍스트 오브젝트를 받는 변수 값
    [SerializeField] private GameObject player2P; // 플레이어2 승리 텍스트 오브젝트를 받는 변수 값
    [SerializeField] private GameObject playerDraw; // 플레이어Draw 텍스트 오브젝트를 받는 변수 값
    [SerializeField] private GameObject player2PScore; // 플레이어2 스코어 오브젝트를 받는 변수 값
    [SerializeField] private bool isDrawChecked; // 체크 표시 활성화 여부에 따라 동작하는 메소드가 달라지는 변수
    [SerializeField] private bool isGameOverChecked; // 체크 표시 활성화 여부에 따라 동작하는 메소드가 달라지는 변수

    private void Start() // 스타트 메소드
    {
        OnGameOverControllerInit(isDrawChecked); // 게임 오버 컨트롤러 생성 메소드
    }

    private void OnGameOverControllerInit(bool _isChecked) // 게임 오버 컨트롤러 생성 메소드
    {
        if (_isChecked == false)
        {
            return;
        }

        player1P.SetActive(false); // player1P 텍스트의 활성화 상태를 거짓으로 한다
        player2P.SetActive(false); // player2P 텍스트의 활성화 상태를 거짓으로 한다
        playerDraw.SetActive(false); // playerDraw 텍스트의 활성화 상태를 거짓으로 한다

        if (PlayerPrefs.GetInt("Score2P") == 0)
        {
            player2PScore.SetActive(false);
            return;
        }

        if (PlayerPrefs.GetInt("Score") > PlayerPrefs.GetInt("Score2P")) // 1P의 점수가 2P의 점수보다 더 클 때
        {
            player1P.SetActive(true); //player1P 텍스트의 활성화 상태를 참으로 한다
        }
        else if (PlayerPrefs.GetInt("Score") < PlayerPrefs.GetInt("Score2P")) // 1P의 점수가 2P의 점수보다 더 작을 때
        {
            player2P.SetActive(true); //player2P 텍스트의 활성화 상태를 참으로 한다
        }
        else
        {
            playerDraw.SetActive(true); //playerDraw 텍스트의 활성화 상태를 참으로 한다
        }
    }

    private void Update() // 업데이트 메소드
    {
        OnCheckGameOver(isGameOverChecked); // 게임 오버를 확인하는 메소드
    }

    private void OnCheckGameOver(bool _isChecked) // 게임 오버를 확인하는 메소드
    {
        if (_isChecked == false)
        {
            return;
        }

        bool _isCleared = true; // 게임 오버를 확인하는 참 or 거짓인 불리언 값을 참으로 설정한다

        for (int i = 0; i < shooter.Length; ++i) // 슈터 게임 오버젝트의 개수 만큼 반복하는 반복문
        {
            _isCleared &= !shooter[i].activeSelf; // 게임 오버 불리언 값이 모두 참일 경우에 참으로 반환한다
        }

        if (_isCleared) // 게임 오버 불리언 값이 참일 경우
        {
            SceneManager.LoadScene("GameOver"); // 게임 오버 씬을 로드한다
        }
    }
}
