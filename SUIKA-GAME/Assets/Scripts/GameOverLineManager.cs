using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverLineManager : MonoBehaviour
{
    [SerializeField] private float endTime = 6f; // 끝나는 시간을 정하기 위한 실수 값
    [SerializeField] private float warnTime = 3f; // 경고 시간을 정하기 위한 실수 값
    [SerializeField] private float alertTime = 0.5f; // 알람 시간을 정하기 위한 실수 값

    private SpriteRenderer spriteRenderer; // 스프라이트 랜더러 컴포넌트를 가져오기 위한 변수 값
    private GameObject hitObject; // 충돌된 게임 오브젝트를 저장하기 위한 값
    private bool isChecked; // 체크 확인 여부를 저장한 불리언 값
    private float overTime = 0; // 경과 시간을 담을 실수 값


    private void Start() // 스타트 메소드
    {
        OnGetSpriteRenderer(); // 스프라이트 렌더러 호출 메소드
    }

    private void OnGetSpriteRenderer() // 스프라이트 렌더러 호출 메소드
    {
        spriteRenderer = GetComponent<SpriteRenderer>(); // 스프라이트 렌더러 변수 값에 스프라이트 렌더러 컴포넌트를 가져온다
    }
    
    private void Update() // 업데이트 메소드
    {
        OnTimeSchedule(isChecked, endTime, warnTime, alertTime); // 시간 딜레이를 정해 게임 오버 판정을 내리는 메소드
    }

    private void OnTriggerStay2D(Collider2D _hit) // 트리거 체크가 된 충돌체가 막 닿았을 때 실행되는 메소드
    {
        if (_hit.transform.CompareTag("Planet") && isChecked == false) // 충돌한 오브젝트의 태그가 Planet이고 체크가 아직 안되어있을 때
        {
            isChecked = true; // 체크 표시를 위한 불리언 값
            hitObject = _hit.gameObject; // 충돌 오브젝트를 hitObject 변수에 저장
        }
    }
    private void OnTriggerExit2D(Collider2D _hit) // 트리거 체크가 된 충돌체가 막 떨어졌을 때 실행되는 메소드
    {
        if (_hit.transform.CompareTag("Planet") && _hit.gameObject == hitObject) 
            // 충돌한 오브젝트의 태그가 Planet이고 충돌 오브젝트가 hitObject에 저장되어 있다면
        {
            isChecked = false; // 체크 표시를 위한 불리언 값
            hitObject = null; // hitObject 변수를 널러블로 변경
        }
    }

    private void OnTimeSchedule(bool _isStabled, float _endTime, float _warnTime, float _alertTime) // 시간 딜레이 함수
    {
        if (_isStabled == true) // _isStabled 인자가 true일 때
        {
            overTime += Time.deltaTime; // overTime에 Time.deltaTime 값을 계속 더해준다
            
            if (overTime > _endTime) // overTime이 끝나는 시간 보다 클 때
            {
                SoundManager.Play.StopSE("WarnAlert"); // 사운드 이름으로 사운드 중지
                SoundManager.Play.PlayEffect("GameOver"); // 사운드 이름으로 사운드 출력
                OnGameOver(); // 게임오버 메소드 호출
            }
            else if (overTime > _warnTime) // overTime이 경고 시간보다 클 때
            {
                spriteRenderer.color = new Color(1f, 0f, 0f, 1f); // 범위 표시선을 빨간색으로 지정
                SoundManager.Play.PlayEffect("WarnAlert");
            }
            else if (overTime > _alertTime) // overTime이 알람 시간보다 클 때
            {
                spriteRenderer.color = new Color(1f, 1f, 1f, 1f); // 범위 표시선을 흰색으로 변경하여 표시
            }
            else
            {
                spriteRenderer.color = new Color(0f, 0f, 0f, 0f); // 일반 상태에서는 alpha(투명도?)값을 바꿔 표시하지 않는다
            }
        }
        else
        {
            overTime = 0; // _isStable 인자가 false일 때 경과 시간을 0으로 초기화한다
            spriteRenderer.color = new Color(0f, 0f, 0f, 0f); // 일반 상태에서는 alpha(투명도?)값을 바꿔 표시하지 않는다
            SoundManager.Play.StopSE("WarnAlert");
        }
    }

    private void OnGameOver() // 게임오버 메소드
    {
        PlayerPrefs.SetInt("Score", ScoreManager.score); // 스코어 변수를 다음 씬으로 스코어 정수 값
        PlayerPrefs.SetString("IsScored", "true"); // 스코어 변수를 다음 씬으로 넘기기 위한 참 or 거짓 불리언 값
        SceneManager.LoadScene("GameOver"); // 게임오버 씬을 로드한다
    }
}
