using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ShowPrevPlanetUI : MonoBehaviour
{
    public static Image preview; // 이미지 스프라이트를 화면에 보여주기 위한 이미지 변수

    private Text blinkingText; // 유니티 엔진 UI를 참조해 클래스 Text를 호출하여 blinkingText로 지정

    private void Start() // 스타트 메소드
    {
        OnBlinkText(); // 블링크 텍스트 호출 메소드
    }

    public void OnBlinkText() // 블링크 텍스트 호출 메소드
    {
        preview = GetComponentInParent<Image>();
        blinkingText = GetComponent<Text>(); // 블링크 텍스트 컴포넌트 텍스트 지정
        StartCoroutine(BlinkText()); // 블링크 텍스트 코루틴 호출
    }

    public IEnumerator BlinkText() // 블링크 텍스트 코루틴 호출
    {
        while (true) // 무한 반복
        {
            blinkingText.text = "P      "; // 텍스트에 공백을 나타낸다
            yield return new WaitForSeconds(0.3f); //0.3초 뒤 재개한다

            blinkingText.text = "  R    "; // 텍스트에 공백을 나타낸다
            yield return new WaitForSeconds(0.3f); //0.3초 뒤 재개한다

            blinkingText.text = "    E  "; // 텍스트에 공백을 나타낸다
            yield return new WaitForSeconds(0.3f); //0.3초 뒤 재개한다

            blinkingText.text = "      V"; // 텍스트에 공백을 나타낸다
            yield return new WaitForSeconds(0.3f); //0.3초 뒤 재개한다

            blinkingText.text = "PREV"; // 텍스트 정렬
            yield return new WaitForSeconds(0.3f); //0.3초 뒤 재개한다
        }
    }

    public static void OnSpriteChange(int _index) // 스프라이트 이미지 변경을 위한 메소드
    {
        preview.sprite = PlanetDatabase.planetSprite[_index]; // _index 인자를 이용해서 PlanetDatabase에서 sprite를 찾는다
    }
}
