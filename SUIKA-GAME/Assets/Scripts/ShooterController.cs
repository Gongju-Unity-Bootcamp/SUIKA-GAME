using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class ShooterController : MonoBehaviour
{
    public NextPlanetManager nextPlanetManager; // NextPlanetManager에 있는 isSpriteChanged 불리언 변수에 접근하기 위해 사용
    public GameObject[] planetPrefab; // 1레벨에서 3레벨 사이의 랜덤 행성을 생성하여 발사하기 위한 프리팹
    public int currentNumber; // 랜덤 값 1 ~ 3레벨 사이의 랜덤 행성의 인덱스 값을 가진 정수 변수

    [SerializeField] private Transform spawnPoint; // 슈터의 스폰 포인트로 행성을 발사하기 위한 기준이 되는 좌표

    [SerializeField] private float shooterSpeed = 30f; // 슈터를 마우스로 이동했을 때, 속도 실수 값
    [SerializeField] private float shootPower = 500f; // 슈터로 쏘아올린 행성의 힘 크기 실수 값

    private float shooterRange = 4.3f; // 2D 환경에서의 2차원 x, y 좌표 중 x 좌표에 한해 슈터의 이동 범위 제한을 위한 범위 실수 값

    private void Start()
    {

    }

    private void Update()
    {
        OnMouseCursor(); // 마우스 커서를 입력받는 메소드
        OnMouseInput(); // 마우스 버튼을 입력받는 메소드
    }

    private void OnMouseCursor() // 마우스 커서 입력을 통해 슈터를 이동시키는 메소드
    {
        float _horizontal = Input.GetAxis("Mouse X"); // Mouse X => (만약 키보드 입력이면...) Horizontal, 마우스 커서 입력 실수 값
        float _speed = _horizontal * Time.deltaTime * shooterSpeed; // 슈터의 속도를 시간 단위로 움직이기 위한 값

        transform.Translate(new Vector3(_speed, 0, 0)); // 슈터가 좌우로만 이동하여야 하기 때문에 x값에만 스피드를 설정하고 y,z값은 고정되게 0으로 설정함

        if (transform.position.x > shooterRange) // 슈터의 x좌표값이 shooterRange보다 클때
        {
            transform.position = new Vector3(shooterRange, transform.position.y, 0); // 슈터의 위치를 shooterRange좌표와 슈터의 y좌표 z = 0 으로 고정한다
        }
        if (transform.position.x < -shooterRange) // 슈터의 -x좌표값이 shooerRange보다 작을때
        {
            transform.position = new Vector3(-shooterRange, transform.position.y, 0); // 슈터의 위치를 shooterRange좌표와 슈터의 y좌표 z = 0 으로 고정한다 
        }
    }

    private void OnMouseInput() // 마우스 좌,우버튼 입력받는 메소드
    {
        if (Input.GetKeyDown(KeyCode.Mouse0)) // 마우스 왼쪽 버튼을 눌렀을 때
        {
            OnShoot(); // 발사하는 메소드를 호출한다
        }
    }

    private void OnShoot() // 발사하는 메소드
    {
        GameObject _planet = Instantiate(planetPrefab[currentNumber], spawnPoint.position, spawnPoint.rotation);
        // 게임오브젝트인 행성은 행성프리펩 1~3번을 스폰포인트 좌표로 스폰포인트 축(회전값)으로 생성한다

        _planet.tag = "Untagged"; // 행성의 태그는 Untagged(태그없음)
        _planet.GetComponent<Rigidbody2D>().AddForce(_planet.transform.up * shootPower);
        // 행성 게임 오브젝트에서 리지드바디2D 컴포넌트를 불러온 후 행성의 윗방향을 기준으로 shootPower 만큼의 힘을 가한다

        nextPlanetManager.isSpriteChanged = true; 
        currentNumber = UnityEngine.Random.Range(0, planetPrefab.Length); //1~3번의 행성을 랜덤하게 생성하기 위해 0부터 프리펩의 길이 사이의 랜덤 값을 반환
    }
}
