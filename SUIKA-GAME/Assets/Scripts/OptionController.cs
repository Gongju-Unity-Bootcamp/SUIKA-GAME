using UnityEngine;

public class OptionController : MonoBehaviour
{
    [SerializeField] private GameObject[] optionInterface; // 옵션 게임 오브젝트를 배열로 저장한 게임 오브젝트 변수 값

    private bool isStated; // 재개 또는 일시정지 상태를 지정하는 상태 참 or 거짓인 불리언 값

    private void Update() // 업데이트 메소드
    {
        OnKeyInput(); // 키보드 버튼을 입력받는 메소드
    }

    private void OnKeyInput()
    {
        if (Input.GetKeyDown(KeyCode.Escape)) // ESC 버튼을 눌렀을 때
        {
            if (isStated) // isStated의 불리언 값이 참이라면
            {
                OnResume(isStated); // 퍼즈의 인자 값으로 isStated의 반대값을 전달한다
                isStated = false; // isStated 불리언 값을 거짓으로 설정한다
            }
            else // 위와 반대라면
            {
                OnPause(isStated); // 퍼즈의 인자 값으로 isStated를 전달한다
                isStated = true; // isStated 불리언 값을 참으로 설정한다
            }
        }
    }

    private void OnResume(bool _isActive) // 재개 메소드
    {
        isStated = !_isActive; // 상태 값을 인자 값의 반대 값으로 설정한다

        Time.timeScale = 1f; // 재개 시키는 변수

        for (int i = 0; i < optionInterface.Length; ++i) // 옵션 게임 오브젝트의 크기만큼 반복하는 반복문
        {
            optionInterface[i].SetActive(!_isActive); // 활성화 상태를 인자 값으로 설정한다
        }
    }

    private void OnPause(bool _isActive) // 일시정지 메소드
    {
        isStated = !_isActive; // 상태 값을 인자값의 반대 값으로 설정한다

        Time.timeScale = 0f; // 일시정지 시키는 변수

        for (int i = 0; i < optionInterface.Length; ++i) // 옵션 게임 오브젝트의 크기만큼 반복하는 반복문
        {
            optionInterface[i].SetActive(!_isActive); // 활성화 상태를 인자 값으로 설정한다
        }
    }
}
