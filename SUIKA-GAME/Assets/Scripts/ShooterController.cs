using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    [SerializeField] private GameObject planet; // 게임 오브젝트 형태로 planet 변수 값 지정
    [SerializeField] private Transform spawnPoint; // 슈터의 스폰 포인트로 행성을 발사하기 위한 기준이 되는 좌표

    [SerializeField] private float shooterSpeed = 100f; // 슈터를 마우스로 이동했을 때, 속도 실수 값
    [SerializeField] private float shooterRange = 4.3f; // 2D 환경에서의 2차원 x, y 좌표 중 x 좌표에 한해 슈터의 이동 범위 제한을 위한 범위 실수 값

    [SerializeField] private float shootPower = 1500f; // 슈터로 쏘아올린 행성의 힘 크기 실수 값
    [SerializeField] private float shootDelay = 1.2f; // 슈팅 딜레이를 조절하는 실수 값

    [SerializeField] private GameObject[] optionInterface; // 옵션 게임 오브젝트를 배열로 저장한 게임 오브젝트 변수 값

    private int currentPlanetNumber; // 랜덤 값 1 ~ 3레벨 사이의 랜덤 행성의 인덱스 값을 가진 정수 변수를 지정하기 위한 값
    private float fixShooterSpeed = 10f; // 마우스 속도 보간값(마우스 속도는 0이 되면 슈터가 움직일 수 없기 때문에 설정한다)
    private bool isShootDelayed; // 슈팅 딜레이 여부를 결정하는 참 or 거짓인 불리언 값
    private bool isStated; // 재개 또는 일시정지 상태를 지정하는 상태 참 or 거짓인 불리언 값
    private GameObject stuff; // 게임 오브젝트를 지정한 변수 값

    private void Start() // 스타트 메소드
    {
        OnShooterInit(); // 슈터 생성 메소드
    }

    private void OnShooterInit() // 슈터 생성 메소드
    {
        stuff = new GameObject("Planet"); // 빈 오브젝트의 이름을 Planet이라 설정한 뒤 stuff 게임 오브젝트 변수에 저장한다
        shooterSpeed = fixShooterSpeed + shooterSpeed * PlayerPrefs.GetFloat("MouseSpeed");
        // 마우스 속도 보간으로 옵션에서 설정한 마우스 값 적용
    }

    private void Update() // 업데이트 메소드
    {
        OnMouseCursor(); // 마우스 커서를 입력받는 메소드
        OnMouseInput(); // 마우스 버튼을 입력받는 메소드
        OnKeyInput(); // 키보드 버튼을 입력받는 메소드
    }

    private void OnMouseCursor() // 마우스 커서 입력을 통해 슈터를 이동시키는 메소드
    {
        float _horizontal = Input.GetAxis("Mouse X"); // Mouse X => (만약 키보드 입력이면...) Horizontal, 마우스 커서 입력 실수 값
        float _speed = _horizontal * Time.deltaTime * shooterSpeed; // 슈터의 속도를 시간 단위로 움직이기 위한 값

        transform.Translate(new Vector3(_speed, 0, 0)); // 슈터가 좌우로만 이동하여야 하기 때문에 x값에만 스피드를 설정하고 y,z값은 고정되게 0으로 설정함

        if (transform.position.x > shooterRange) // 슈터의 x좌표값이 shooterRange보다 클 때
        {
            transform.position = new Vector3(shooterRange, transform.position.y, 0); // 슈터의 위치를 shooterRange좌표와 슈터의 y좌표 z = 0 으로 고정한다
        }
        if (transform.position.x < -shooterRange) // 슈터의 -x좌표값이 shooerRange보다 작을 때
        {
            transform.position = new Vector3(-shooterRange, transform.position.y, 0); // 슈터의 위치를 shooterRange좌표와 슈터의 y좌표 z = 0 으로 고정한다 
        }
    }

    private void OnMouseInput() // 마우스 좌,우버튼 입력받는 메소드
    {
        if (isShootDelayed == false && Input.GetKeyDown(KeyCode.Mouse0)) // 마우스 왼쪽 버튼을 눌렀을 때
        {
            OnSetPlanet(); // 행성 초기 설정 메소드를 호출한다
            StartCoroutine(OnShootDelay()); // 코루틴을 시작한다
        }
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

    private void OnSetPlanet() // 행성 초기 설정 메소드
    {
        GameObject _planet = Instantiate(planet, spawnPoint.position, spawnPoint.rotation);
        // 게임 오브젝트 _planet을 스폰포인트 위치값과 스폰포인트 회전값에 생성한 뒤 개체 참조한다
        _planet.transform.parent = stuff.transform; // 행성의 게임 오브젝트 위치를 stuff로 옮긴다
        PlanetController _planetController = _planet.GetComponent<PlanetController>();
        // _planet 게임 오브젝트를 _planetController 컴포넌트로 가져온다
        _planet.tag = "Untagged"; // 행성의 태그는 Untagged(태그없음)
        _planetController.OnPlanetInit(currentPlanetNumber);
        // 게임오브젝트인 행성은 행성프리펩 1~3번을 스폰포인트 좌표로 스폰포인트 축(회전값)으로 생성한다

        OnShoot(_planet); // 행성 발사 메소드
    }

    private void OnShoot(GameObject _planet) // 행성 발사 메소드
    {
        SoundManager.Play.PlayEffect("PlanetShoot"); // 사운드 이름으로 사운드 출력
        _planet.GetComponent<Rigidbody2D>().AddForce(_planet.transform.up * shootPower, ForceMode2D.Impulse);
        // 행성 게임 오브젝트에서 리지드바디2D 컴포넌트를 불러온 후 행성의 윗방향을 기준으로 shootPower 만큼의 힘을 가한다
        currentPlanetNumber = Random.Range(0, PlanetDatabase.planetIndex / 3);
        // 1~3번의 행성을 랜덤하게 생성하기 위해 0부터 프리펩의 길이 사이의 랜덤 값을 반환
        ShowPrevPlanetUI.OnSpriteChange(currentPlanetNumber); // 스프라이트 체인지 상태를 true로 변경
    }

    private IEnumerator OnShootDelay() // 슈팅 딜레이 코루틴 메소드
    {
        isShootDelayed = true; // 슈팅 딜레이 상태를 참 or 거짓으로 나타낸 불리언 값으로 참으로 저장한다

        yield return new WaitForSeconds(shootDelay); // shootDelay초 만큼 대기 시간을 갖는다

        isShootDelayed = false; // 슈팅 딜레이 상태를 참 or 거짓으로 나타낸 불리언 값으로 거짓으로 저장한다
    }

    private void OnResume(bool _isActive) // 재개 메소드
    {
        isStated = !_isActive; // 상태 값을 인자 값의 반대 값으로 설정한다

        Time.timeScale = 1f; // 재개 시키는 변수

        for (int i = 0; i < optionInterface.Length; i++) // 옵션 게임 오브젝트의 크기만큼 반복하는 반복문
        {
            optionInterface[i].SetActive(!_isActive); // 활성화 상태를 인자 값으로 설정한다
        }
    }

    private void OnPause(bool _isActive) // 일시정지 메소드
    {
        isStated = !_isActive; // 상태 값을 인자값의 반대 값으로 설정한다

        Time.timeScale = 0f; // 일시정지 시키는 변수

        for (int i = 0; i < optionInterface.Length; i++) // 옵션 게임 오브젝트의 크기만큼 반복하는 반복문
        {
            optionInterface[i].SetActive(!_isActive); // 활성화 상태를 인자 값으로 설정한다
        }
    }
}
