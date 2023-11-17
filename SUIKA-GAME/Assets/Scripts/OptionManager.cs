using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class OptionManager : MonoBehaviour
{
    public Slider backgroundSlider; // 배경음 슬라이더를 저장한 값
    public Slider effectSlider; // 효과음 슬라이더를 저장한 값
    public Slider mouseSpeedSlider; // 마우스 속도 슬라이더를 저장한 값

    private float backgroundVolume; // 배경음 볼륨 크기를 저장한 값
    private float effectVolume; // 효과음 볼륨 크기를 저장한 값
    private float mouseSpeed; // 마우스 속도를 저장한 값

    private void Start()
    {
        OnUpdateOption(); // 설정 업데이트 값 호출 메소드
    }

    public void OnUpdateOption() // 설정 업데이트 값 호출 메소드
    {
        backgroundVolume = PlayerPrefs.GetFloat("BackgroundVolume"); // 배경음 볼륨 크기를 저장한 값에서 가져온다
        effectVolume = PlayerPrefs.GetFloat("EffectVolume"); // 효과음 볼륨 크기를 저장한 값에서 가져온다
        mouseSpeed = PlayerPrefs.GetFloat("MouseSpeed"); // 마우스 속도를 저장한 값에서 가져온다

        backgroundSlider.value = backgroundVolume; // 배경음 슬라이더의 값을 저장했던 값으로 변경하여 UI에 표시한다
        effectSlider.value = effectVolume; // 효과음 슬라이더의 값을 저장했던 값으로 변경하여 UI에 표시한다
        mouseSpeedSlider.value = mouseSpeed; // 마우스 속도 슬라이더의 값을 저장했던 값으로 변경하여 UI에 표시한다

        backgroundSlider.onValueChanged.AddListener(delegate { OnSaveOption(); }); // 배경음 슬라이더의 값을 변경할 때마다 함수를 호출한다
        effectSlider.onValueChanged.AddListener(delegate { OnSaveOption(); }); // 효과음 슬라이더의 값을 변경할 때마다 함수를 호출한다
        mouseSpeedSlider.onValueChanged.AddListener(delegate { OnSaveOption(); }); // 마우스 속도 슬라이더의 값을 변경할 때마다 함수를 호출한다
    }

    public void OnSaveOption() // 설정 값을 저장하는 메소드
    {
        backgroundVolume = backgroundSlider.value; // 배경음 크기를 배경음 슬라이더 값에서 가져온다
        effectVolume = effectSlider.value; // 효과음 크기를 효과 슬라이더 값에서 가져온다
        mouseSpeed = mouseSpeedSlider.value; // 마우스 속도를 마우스 속도 슬라이더 값에서 가져온다

        PlayerPrefs.SetFloat("BackgroundVolume", backgroundVolume); // 배경음 크기를 지정한 값으로 저장한다
        PlayerPrefs.SetFloat("EffectVolume", effectVolume); // 효과음 크기를 지정한 값으로 저장한다
        PlayerPrefs.SetFloat("MouseSpeed", mouseSpeed); // 마우스 속도를 지정한 값으로 저장한다
    }
    public void OnSaveClick()
    {
        PlayerPrefs.SetInt("Score", 0); // 스코어 키로 스코어 변수를 찾아 삭제한다
        OnSaveOption(); // 설정 값을 저장하는 메소드 호출
        SoundManager.Play.PlayEffect("MouseClick"); // 사운드 이름으로 사운드 출력
        SceneManager.LoadScene(PlayerPrefs.GetString("SceneName")); // 이전 씬을 저장한 값에서 호출하여 불러온다
    }

    public void OnCheckSceneChange()
    {
        SoundManager.Play.PlayEffect("SceneChange"); // 사운드 이름으로 사운드 출력
    }
}
