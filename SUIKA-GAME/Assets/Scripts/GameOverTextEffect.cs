using System.Collections;
using UnityEngine.UI;
using UnityEngine;
using UnityEngine.EventSystems;

public class GameOverTextEffect : MonoBehaviour
{
    private Text flashingText; // 유니티엔진UI을 참조해 클래스 Text를 호출하여 flashingText로 지정함
    
    private void Start()
    {
        flashingText = GetComponent<Text>(); // flashingText는 텍스트 컴포넌트를 불러온다
        StartCoroutine(BlinkText()); // BlinkText메소드를 호출하여 초기화한다
    }

    public IEnumerator BlinkText()
    {
        while (true) // 무한히 반복하기 위해 while문 사용
        {
            flashingText.text = ""; // 텍스트에 공백을 나타낸다
            yield return new WaitForSeconds(0.3f); //0.3초 뒤 재게한다
            flashingText.text = "GAME OVER"; // 텍스트에 GAME OVER를 나타낸다
            yield return new WaitForSeconds(0.3f); // 0.3초 뒤 재게한다
        }
    }
}
