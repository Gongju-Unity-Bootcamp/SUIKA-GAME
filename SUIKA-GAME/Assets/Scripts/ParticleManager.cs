using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Particle
{
    public string name; // 파티클 이름을 문자열로 인스펙터 창에서 저장
    public ParticleSystem effect; // 파티클 게임 오브젝트를 파티클로 인스펙터 창에서 저장
}

public class ParticleManager : MonoBehaviour
{
    [HideInInspector] public static ParticleManager Show; // 이 클래스 자체를 인스턴스화 시켜 다른 클래스에서도 호출 가능하게 한다

    public Particle[] particleEffect; // 파티클 게임 오브젝트를 지정한 값으로 이 클래스 상단의 name, clip을 변수로 가진다

    public string[] showParticleName; // 보여지고 있는 파티클 게임 오브젝트을 지정한 문자열 배열 변수 값
    public GameObject[] showParticleGameObject; // 보여지고 있는 파티클 게임 오브젝트를 지정한 게임 오브젝트 배열 변수 값

    private void Awake() // 가장 최초에 한 번 실행되는 메소드
    {
        OnParitcleManagerInit(); // 파티클 매니저 생성 메소드
    }

    private void OnParitcleManagerInit() // 파티클 매니저 생성 메소드
    {
        if (Show == null) // 인스턴스화가 안되어있다면
        {
            Show = this; // 이 클래스를 인스턴스로 설정한다
            DontDestroyOnLoad(gameObject); // 게임 오브젝트를 다음 씬으로 이동하여도 파괴할 수 없게 설정한다
        }
        else // 위의 조건에 부합하지 않다면
        {
            Destroy(gameObject); // 게임 오브젝트를 파괴한다, 이는 다시 타이틀 화면으로 돌아갔을 경우 새로 생성되는 사운드 매니저를 방지하기 위함이다
        }

        showParticleName = new string[particleEffect.Length]; // 파티클 이름의 크기를 particleEffect의 길이로 한다
        showParticleGameObject = new GameObject[particleEffect.Length]; // 파티클 게임 오브젝트의 크기를 particleEffect의 길이로 한다
    }

    public void ShowParticle(string _particleName, Vector3 _position) // 파티클 실행 메소드
    {
        if (_particleName == null) // 파티클 이름이 존재하지 않다면
        {
            return; // 아래 실행 명령어들을 무시하고 반환
        }

        for (int i = 0; i < particleEffect.Length; ++i) // 파티클 크기를 저장한 배열의 크기만큼 반복문을 실행한다
        {
            if (_particleName.Equals(particleEffect[i].name)) // 인자 값으로 받은 파티클 이름이 지정된 파티클 이름과 같다면
            {
                for (int j = 0; j < showParticleName.Length; ++j) // 효과음 사운드 크기를 지정한 배열의 크기만큼 반복문을 실행한다
                {
                    if (particleEffect[i].name.Equals(showParticleName[j]) == false) // 효과음이 실행중이지 않다면
                    {
                        GameObject _particle = Instantiate(particleEffect[i].effect.gameObject, _position, particleEffect[i].effect.transform.rotation);
                        showParticleName[j] = _particleName; // 실행 중인 사운드 이름을 효과음 사운드 이름으로 저장한다
                        showParticleGameObject[j] = _particle;

                        return; // 아래 실행 명령어들을 무시하고 반환
                    }
                }

                return; // 아래 실행 명령어들을 무시하고 반환
            }
        }
    }

    public void HideParticle(string _particleName) // 파티클 중지 메소드
    {
        if (_particleName == null) // 파티클 이름이 존재하지 않다면
        {
            return; // 아래 실행 명령어들을 무시하고 반환
        }

        for (int i = 0; i < particleEffect.Length; ++i) // 파티클 크기를 저장한 배열의 크기만큼 반복문을 실행한다
        {
            if (showParticleName[i] != null && showParticleName[i].Equals(_particleName) == true)
                // 실행중인 파티클 이름에 인자로 받아온 파티클 이름이 존재하면
            {
                showParticleName[i] = null;
                Destroy(showParticleGameObject[i]);
                showParticleGameObject[i] = null;

                return; // 아래 실행 명령어들을 무시하고 반환
            }
        }
    }
}
