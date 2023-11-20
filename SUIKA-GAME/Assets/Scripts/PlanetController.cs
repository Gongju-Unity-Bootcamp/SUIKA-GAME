using UnityEngine;

public class PlanetController : MonoBehaviour
{
    [HideInInspector] public int planetLevel; // 행성 레벨을 지정한 정수 변수 값
    [HideInInspector] public int planetScore; // 행성 점수를 지정한 정수 변수 값

    private string planetName; // 행성 이름을 문자열로 지정한 문자열 값
    private float planetColliderRadius; // 행성 CircleCollider2D의 범위를 지정한 실수 값
    private Vector3 planetScale; // 행성 transform.localScale(크기)를 지정하기 위한 Vector3 값
    private Sprite planetSprite; // 행성 이미지 스프라이트를 넣은 스프라이트 값
    [HideInInspector] public int planetIndex; // 플래닛 레벨을 인덱스화한 정수 값

    [HideInInspector] public bool isGrowed; // 행성이 커졌을 때를 판단하는 참 or 거짓인 불리언 값
    private bool isTagged; // 행성에 태그가 달렸는지를 판단하는 참 or 거짓인 불리언 값

    public void OnPlanetInit(int _index) // 플래닛 생성(기본 정보 설정) 함수
    {
        planetIndex = _index; // 행성 인덱스를 메소드 인자 _index로 설정

        planetLevel = PlanetDatabase.planetLevel[_index]; // 행성 레벨을 PlanetDatabase에서 가져오는 구간
        planetScore = PlanetDatabase.planetScore[_index]; // 행성 점수를 PlanetDatabase에서 가져오는 구간
        planetName = PlanetDatabase.planetName[_index]; // 행성 이름을 PlanetDatabase에서 가져오는 구간
        planetColliderRadius = PlanetDatabase.planetColliderRadius[_index];
        // 행성 CircleCollider2D의 범위를 PlanetDatabase에서 가져오는 구간
        planetScale = PlanetDatabase.planetScale[_index]; // 행성 크기를 PlanetDatabase에서 가져오는 구간
        planetSprite = PlanetDatabase.planetSprite[_index]; // 행성 이미지 스프라이트를 PlanetDatabase에서 가져오는 구간

        gameObject.name = planetName; // 이 스크립트가 붙은 게임 오브젝트의 이름을 가져온 행성 이름으로 지정한다
        GetComponent<CircleCollider2D>().radius = planetColliderRadius; 
        // 이 게임 오브젝트에 CircleCollider2D의 범위를 가져온 행성 범위로 지정한다
        transform.localScale = planetScale; // 행성 크기를 지정한다
        GetComponent<SpriteRenderer>().sprite = planetSprite;
        // 이 게임 오브젝트에 SpriteRenderer의 이미지 스프라이트를 가져온 행성 이미지 스프라이트로 지정한다
    }

    private void OnCollisionEnter2D(Collision2D _hit) // 콜라이더가 한 번 충돌했을 때
    {
        if (isGrowed == false && _hit.collider.CompareTag("Planet")) // 이 행성이 커진 상태가 아니고 충돌 게임 오브젝트의 태그가 Planet이라면
        {
            OnPlanetHitCheck(_hit);// 행성 충돌 체크 메소드 호출
        }
    }

    private void OnCollisionStay2D(Collision2D _hit) // 콜라이더가 계속 충돌 중일 때
    {
        if (!isTagged) // 태그 지정이 안되었다면
        {
            gameObject.tag = "Planet"; // 이 게임 오브젝트의 태그를 Planet으로 변경한다
            isTagged = true; // 태그 지정 여부를 알기 위해 불리언 값을 참으로 설정한다
        }
        if (isGrowed == false && _hit.collider.CompareTag("Planet")) // 이 행성이 커진 상태가 아니고 충돌 게임 오브젝트의 태그가 Planet이라면
        {
            OnPlanetHitCheck(_hit);// 행성 충돌 체크 메소드 호출
        }
    }

    private void OnPlanetHitCheck(Collision2D _hit) // 행성 충돌 체크 메소드
    {
        PlanetController _other = _hit.gameObject.GetComponent<PlanetController>(); // 행성 컨트롤러를 충돌 오브젝트에서 가져온다
        Vector3 _otherPosition = _other.transform.position; // 충돌 오브젝트의 위치값을 가져온다
        Quaternion _otherRotation = _other.transform.rotation; // 충돌 오브젝트의 회전값을 가져온다

        if (_other.isGrowed == false && _other.planetLevel == planetLevel)
            // _other 행성이 커진 상태가 아니고 두 행성의 레벨이 동일하다면
        {
            isGrowed = _other.isGrowed = true; // 충돌 오브젝트와 이 게임 오브젝트의 행성이 커진 상태를 참 불리언 값으로 변경한다
            ParticleManager.Show.ShowParticle("PlanetLevelUp", _otherPosition); // 파티클 이펙트 이름의 파티클을 생성
            _other.OnLevelUp(_otherPosition, _otherRotation); // 충돌 오브젝트의 레벨업(행성이 커지는 것) 메소드를 호출한다
            OnGrow(isGrowed); // 행성이 커질 때 메소드에 isGrowed 인자 값을 넘긴다
        }
    }

    public void OnGrow(bool _isGrowed) // 행성이 커질 때 메소드
    {
        if (_isGrowed) // 행성이 커졌다면
        {
            Destroy(gameObject); // 이 게임 오브젝트를 삭제한다
        }
    }

    public void OnLevelUp(Vector3 _position, Quaternion _rotation) // 레벨업(행성이 커지는 것) 메소드
    {
        transform.position = _position; // 이 게임 오브젝트의 위치값을 충돌 오브젝트의 위치값으로 한다
        transform.rotation = _rotation; // 이 게임 오브젝트의 회전값을 충돌 오브젝트의 회전값으로 한다
        isGrowed = false; // 이 행성이 커진 상태를 거짓 불리언 값으로 변경한다

        int _index = ++planetIndex; // 행성 인덱스를 다음 레벨의 인덱스로 바꾼다
        SoundManager.Play.PlayEffect("PlanetLevelUp"); // 사운드 이름으로 사운드 출력
        ScoreManager.OnSetScore(PlanetDatabase.planetScore[_index]); // 스코어에 해당 레벨의 행성 스코어가 가진 값을 더한다
        OnPlanetInit(_index);
        // 다음 인덱스로 넘긴 값을 다시 OnPlanetInit() 메소드를 통해 호출하여 이 게임 오브젝트의 정보값을 수정한다
    }
}