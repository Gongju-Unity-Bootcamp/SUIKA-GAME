using System.Collections;
using UnityEngine;

public class ShooterAnimationController : MonoBehaviour
{
    [SerializeField] private GameObject shooter; // 슈터 게임 오브젝트를 저장하는 변수 값
    [SerializeField] private float animationDelay = 2f; // 애니메이션 딜레이를 설정하는 실수 값
    [SerializeField] private float shooterInitDelay = 1.4f; // 슈터 생성 딜레이를 설정하는 실수 값

    private float fixShooterYValue = 0.09f; // 슈터 Y 좌표를 보간하는 실수 값
    private Vector3 value = Vector3.zero; // Vactor3.SmoothDamp의 보간 Vector3 값
    private bool isLanded; // 슈터가 착륙했는지 확인하는 참 or 거짓인 불리언 값

    private void Start() // 스타트 메소드
    {
        OnShooterAnimationInit(true); // 슈터 생성 메소드 인자 값으로는 활성화 여부를 확인하는 참 or 거짓인 불리언 값을 받는다
    }

    private void OnShooterAnimationInit(bool _isActived) // 슈터 생성 메소드 인자 값으로는 활성화 여부를 확인하는 참 or 거짓인 불리언 값을 받는다
    {
        shooter.SetActive(!_isActived); // 슈터 게임 오브젝트의 인자값에서 반대값으로 설정한다
        gameObject.SetActive(_isActived); // 슈터 에니메이션 게임 오브젝트의 인자값으로 설정한다
        SoundManager.Play.PlayEffect("ShooterTakeoff"); // 사운드 이름으로 사운드 출력
    }

    private void Update() // 업데이트 메소드
    {
        OnAnimationTranslate(); // 애니메이션 이동 메소드
    }

    private void OnAnimationTranslate() // 애니메이션 이동 메소드
    {
        if (isLanded == false && transform.position == shooter.transform.position)
            // 착륙 상태가 아니거나 슈터 에니메이션 게임 오브젝트의 위치가 슈터 게임오브젝트의 위치와 같다면
        {
            isLanded = true; // 착륙 상태를 참으로 설정한다
            StartCoroutine(OnShooterInitDelay(false)); // 슈터 생성 딜레이 코루틴의 인자 값을 거짓으로 하여 코루틴을 실행한다
        }

        if (transform.position.y <= shooter.transform.position.y + fixShooterYValue)
            // 슈터 애니메이션 게임 오브젝트 포지션 y 값이 슈터 게임 오브젝트 y값에 보간 값을 더한거 보다 작거나 같을 때
        {
            transform.position = shooter.transform.position; // 슈터 애니메이션 게임 오브젝트의 위치를 슈터 게임 오브젝트의 위치로 설정한다
            transform.rotation = Quaternion.Slerp(transform.rotation, shooter.transform.rotation, animationDelay * Time.deltaTime);
            // 슈터 애니메이션 게임 오브젝트의 회전 값을 Slerp 함수를 이용해 부드럽게 보간한다
        }
        else // 위와 다르면
        {
            transform.position = Vector3.SmoothDamp(gameObject.transform.position, shooter.transform.position, ref value, animationDelay);
            // 슈터 애니메이션의 위치 값을 보간 값으로 하여 SmoothDamp 함수를 이용해 부드럽게 이동시킨다
        }
    }

    private IEnumerator OnShooterInitDelay(bool _isActived)
        // 슈터 생성 딜레이 코루틴 메소드로 인자 값으로는 활성화 상태인 참 or 거짓 불리언 값을 받는다
    {
        SoundManager.Play.PlayEffect("ShooterLand"); // 사운드 이름으로 사운드 출력

        yield return new WaitForSeconds(shooterInitDelay / 2); // shooterInitDelay / 2 실수 값 만큼 초당 딜레이 시킨다

        SoundManager.Play.PlayEffect("GameStart"); // 사운드 이름으로 사운드 출력

        yield return new WaitForSeconds(shooterInitDelay / 2); // shooterInitDelay / 2 실수 값 만큼 초당 딜레이 시킨다

        shooter.SetActive(!_isActived); // 슈터 게임 오브젝트의 활성화 상태를 인자 값의 반대 값으로 설정한다
        gameObject.SetActive(_isActived); // 슈터 애니메이션 게임 오브젝트의 활성화 상태를 인자 값으로 설정한다
    }
}
