using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void OnStartClick() // 스타트 버튼을 눌렀을 때의 메소드
    {
        PlayerPrefs.SetInt("Score", 0); // 스코어 키로 스코어 변수를 찾아 삭제한다
        SoundManager.Play.PlayEffect("MouseClick"); // 사운드 이름으로 사운드 출력
        SceneManager.LoadScene("Ingame"); // 인게임 씬을 로드한다
    }

    public void OnOptionClick() // 옵션 버튼을 눌렀을 때의 메소드
    {
        PlayerPrefs.SetString("SceneName", SceneManager.GetActiveScene().name);
        // 이전 씬의 이름을 지정한 값으로 옵션에서 나갈 때 되돌아가는 용도로 저장한다
        SoundManager.Play.PlayEffect("MouseClick"); // 사운드 이름으로 사운드 출력
        SceneManager.LoadScene("Option"); // 옵션 씬을 로드한다
    }

    public void OnQuitClick() // 종료 버튼을 눌렀을 때의 메소드
    {
        SoundManager.Play.PlayEffect("MouseClick"); // 사운드 이름으로 사운드 출력
        Application.Quit(); // 응용 프로그램을 즉시 종료한다
    }

    public void OnCancelClick() // 취소 버튼을 눌렀을 때의 메소드
    {
        SoundManager.Play.PlayEffect("MouseClick"); // 사운드 이름으로 사운드 출력
        SceneManager.LoadScene(PlayerPrefs.GetString("SceneName"));
        // 이전 씬의 이름을 지정한 값으로 옵션에서 나갈 때 되돌아가는 용도로 저장한다
    }
}
