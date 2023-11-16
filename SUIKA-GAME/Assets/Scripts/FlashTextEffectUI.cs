using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class FlashTextEffectUI : MonoBehaviour
{
    private Text flashingText; // 유니티 엔진UI를 참조해 클래스 Text를 호출하여 flashingText로 지정
    
    private void Start() // 스타트 메소드
    {
        OnFlashText(); // 플래시 텍스트 호출 메소드
    }

    private void OnFlashText() // 플래시 텍스트 호출 메소드
    {
        flashingText = GetComponent<Text>(); // flashingText는 텍스트 컴포넌트를 불러온다
        StartCoroutine(FlashText()); // FlashText 메소드를 호출하여 초기화한다
    }

    public IEnumerator FlashText() // 텍스트 깜빡임 효과를 부여하기 위한 코루틴 메소드
    {
        while (true) // 무한히 반복하기 위해 while문 사용
        {
            flashingText.text = ""; // 텍스트에 공백을 나타낸다

            yield return new WaitForSeconds(0.3f); //0.3초 뒤 재개한다

            flashingText.text = "GAME OVER"; // 텍스트에 GAME OVER를 나타낸다

            yield return new WaitForSeconds(0.3f); // 0.3초 뒤 재개한다
        }
    }
}
