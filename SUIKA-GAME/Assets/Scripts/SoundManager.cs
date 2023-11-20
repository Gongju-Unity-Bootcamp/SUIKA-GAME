using UnityEngine;

[System.Serializable]
public class Sound 
{
    public string name; // 사운드 이름을 문자열로 인스펙터 창에서 저장
    public AudioClip clip; // 사운드 클립을 오디오 클립으로 인스펙터 창에서 저장
}

public class SoundManager : MonoBehaviour
{
    [HideInInspector] public static SoundManager Play; // 이 클래스 자체를 인스턴스화 시켜 다른 클래스에서도 호출 가능하게 한다

    public AudioSource[] effect; // 효과음 오디오 소스로 배열 변수 값이고 효과음은 여러 개가 존재한다
    public AudioSource background; // 배경음 오디오 소스 변수 값

    public string[] playSoundName; // 사운드 이름을 지정한 문자열 배열 변수 값

    public Sound[] effectSound; // 효과음 사운드 소스를 지정한 배열 값으로 이 클래스 상단의 name, clip을 변수로 가진다
    public Sound backgroundSound; // 배경음 사운드 소스를 지정한 값으로 이 클래스 상단의 name, clip을 변수로 가진다

    public static bool isSoundChecked; // 사운드 체크를 위한 참 or 거짓 불리언 값


    private void Awake() // 가장 최초에 한 번 실행되는 메소드
    {
        OnSoundManagerInit(); // 사운드 매니저 생성 메소드
    }

    private void OnSoundManagerInit() // 사운드 매니저 생성 메소드
    {
        if (Play == null) // 인스턴스화가 안되어있다면
        {
            Play = this; // 이 클래스를 인스턴스로 설정한다
            DontDestroyOnLoad(gameObject); // 게임 오브젝트를 다음 씬으로 이동하여도 파괴할 수 없게 설정한다
        }
        else // 위의 조건에 부합하지 않다면
        {
            Destroy(gameObject); // 게임 오브젝트를 파괴한다, 이는 다시 타이틀 화면으로 돌아갔을 경우 새로 생성되는 사운드 매니저를 방지하기 위함이다
        }

        if (isSoundChecked == false) // 사운드 체크를 위한 참 or 거짓 불리언 값이 거짓이라면
        {
            PlayEffect("PlanetOut"); // 사운드 이름으로 사운드 출력
            isSoundChecked = true; // 사운드 체크를 위한 참 or 거짓 불리언 값을 참으로 설정한다
        }
        
        playSoundName = new string[effectSound.Length]; // 실행 중인 사운드 이름을 effectSound 길이로 한다
        OnVolumeChange(); // 볼륨 크기를 변경하는 메소드
    }

    private void Start() // 스타트 메소드
    {
        PlayBackground("Background"); // 배경음 실행 메소드
    }

    private void Update() // 업데이트 메소드
    {
        OnVolumeChange(); // 볼륨 크기를 변경하는 메소드
    }

    private void OnVolumeChange() // 볼륨 크기를 변경하는 메소드
    {
        background.volume = PlayerPrefs.GetFloat("BackgroundVolume"); // 배경음 크기를 저장했던 값으로 설정한다

        for (int i = 0; i < effect.Length; ++i) // 효과음 크기를 저장한 배열의 크기만큼 반복문을 실행한다
        {
            effect[i].volume = PlayerPrefs.GetFloat("EffectVolume"); // 효과음 크기를 저장했던 값으로 설정한다
        }
    }

    public void PlayEffect(string _soundName) // 효과음 실행 메소드
    {
        if (_soundName == null) // 사운드 이름이 존재하지 않다면
        {
            return; // 아래 실행 명령어들을 무시하고 반환
        }

        for (int i = 0; i < effectSound.Length; ++i) // 효과음 크기를 저장한 배열의 크기만큼 반복문을 실행한다
        {
            if (_soundName.Equals(effectSound[i].name) == true) // 인자 값으로 받은 사운드 이름이 효과음 사운드 이름과 같다면
            {
                for (int j = 0; j < effect.Length; ++j) // 효과음 사운드 크기를 지정한 배열의 크기만큼 반복문을 실행한다
                {
                    if (effect[j].isPlaying == false) // 효과음이 실행중이지 않다면
                    {
                        playSoundName[j] = effectSound[i].name; // 실행 중인 사운드 이름을 효과음 사운드 이름으로 저장한다
                        effect[j].clip = effectSound[i].clip; // 사운드 클립을 효과음 사운드 클립으로 저장한다
                        effect[j].Play(); // 사운드 클립을 실행한다

                        return; // 아래 실행 명령어들을 무시하고 반환
                    }
                }

                return; // 아래 실행 명령어들을 무시하고 반환
            }
        }
    }

    public void StopEffect(string _soundName) // 효과음 중지 메소드
    {
        if (_soundName == null) // 사운드 이름이 존재하지 않다면
        {
            return; // 아래 실행 명령어들을 무시하고 반환
        }

        for (int i = 0; i < effect.Length; ++i) // 효과음 크기를 저장한 배열의 크기만큼 반복문을 실행한다
        {
            if (playSoundName[i] != null && playSoundName[i].Equals(_soundName) == true)
                // 실행중인 사운드 이름에 인자로 받아온 사운드 이름이 존재하면
            {
                effect[i].Stop(); // 효과음 중지 메소드를 호출한다
                effect[i].clip = null; // 효과음 클립을 널러블로 만들어 없앤다
                playSoundName[i] = null; // 실행중인 사운드 이름을 널러블로 만들어 없앤다
            }
        }
    }

    public void StopAllEffect() // 모든 효과음 중지 메소드
    {
        for (int i = 0; i < effect.Length; ++i) // 효과음 크기를 저장한 배열의 크기만큼 반복문을 실행한다
        {
            effect[i].Stop(); // 효과음 중지 메소드를 호출한다
            effect[i].clip = null; // 효과음 클립을 널러블로 만들어 없앤다
            playSoundName[i] = null; // 실행중인 사운드 이름을 널러블로 만들어 없앤다
        }
    }

    public void PlayBackground(string _soundName) // 배경음 실행 메소드
    {
        if (_soundName == null) // 사운드 이름이 존재하지 않다면
        {
            return; // 아래 실행 명령어들을 무시하고 반환
        }

        if (_soundName.Equals(backgroundSound.name) == true) // 인자 값으로 받아온 사운드 이름이 배경음 이름과 같으면
        {
            background.clip = backgroundSound.clip; // 배경음 사운드 클립을 배경음 클립으로 저장한다
            background.Play(); // 배경음 실행
        }
    }

    public void StopBackground(string _soundName) // 배경음 중지 메소드
    {
        if (_soundName == null) // 사운드 이름이 존재하지 않다면
        {
            return; // 아래 실행 명령어들을 무시하고 반환
        }

        if (background.isPlaying == true && _soundName.Equals(backgroundSound.name) == true)
            // 배경음이 실행 중이고 인자값으로 받아온 사운드 이름이 배경음 사운드 이름과 동일하면
        {
            background.Stop(); // 배경음 중지 메소드 호출
        }
    }
}
