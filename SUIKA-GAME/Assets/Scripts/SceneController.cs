using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnStartClick() // 스타트 버튼을 누렀을 때의 메소드
    {
        SceneManager.LoadScene("Ingame"); // 인게임 씬을 로드한다
    }

    public void OnOptionClick() // 옵션 버튼을 누렀을 때의 메소드
    {
        SceneManager.LoadScene("Option"); // 옵션 씬을 로드한다
    }

    public void OnQuitClick() // 종료 버튼을 누렀을 때의 메소드
    {
        Application.Quit(); // 응용 프로그램을 즉시 종료한다
    }
}
