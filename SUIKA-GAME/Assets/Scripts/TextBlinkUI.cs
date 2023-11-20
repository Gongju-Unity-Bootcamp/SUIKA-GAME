using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextBlinkUI : MonoBehaviour
{
    [SerializeField] private bool isBlinked; // 블링크 타입 여부를 참 or 거짓인 불리언 값으로 
    [SerializeField] private float blinkDelay = 0.6f;

    private Text blinkText; // 유니티 엔진 UI를 참조해 클래스 Text를 호출하여 blinkingText로 지정
    private int count = 0;

    private void Start() // 스타트 메소드
    {
        OnBlinkTextInit(); // 블링크 텍스트 생성 메소드 호출
    }

    private void OnBlinkTextInit() // 블링크 텍스트 생성 메소드 호출
    {
        blinkText = GetComponent<Text>(); // 블링크 텍스트의 컴포넌트를 가져온다
        StartCoroutine(OnBlinkText(isBlinked, blinkText.text));
        // 블링트 타입 여부와 블링크 텍스트 문자열을 인자 값으로 전달한 블링크 텍스트 메소드를 호출한다
    }

    private IEnumerator OnBlinkText(bool _isBlinked, string _text) // 블링크 타입 여부와 블링크 텍스트 문자열을 인자 값으로 받는 블링크 텍스트 메소드
    {
        char[] _textAlphabet = blinkText.text.ToCharArray(); // 블링크 텍스트 문자열을 캐릭터 형 배열로 저장한다

        if (_isBlinked) // 블링크 타입 여부가 참이라면
        {
            while (true) // 무한 반복문 실행
            {
                bool _isChecked = false; // 체크 여부 표시를 거짓으로 설정한다
                string _blinkText = ""; // 블링크 텍스트 문자열을 가져온다
                blinkText.text = ""; // 블링크 텍스트를 빈 문자열로 만든다

                if (count == _textAlphabet.Length) // 카운트 정수 값이 텍스트 캐릭터 형 배열의 길이와 같다면
                {
                    count = 0; // 카운트 정수 값을 0으로 초기화 한다
                    blinkText.text = new string(_textAlphabet); // 블링크 텍스트의 텍스트를 생성자를 이용하여 문자열로 바꾼다

                    yield return new WaitForSeconds(blinkDelay); // blinkDelay 만큼 대기시간을 갖는다

                    continue; // while 반복문 기준으로 아래 구문들을 무시하고 계속한다
                }

                for (int i = 0; i < _textAlphabet.Length; ++i) // 텍스트 캐릭터 형 배열의 크기만큼 반복문을 반복한다
                {
                    if (_isChecked == false && _textAlphabet[count].Equals(_textAlphabet[i])) // 체크 여부 표시가 거짓이고 카운트 값이 i값과 동일하다면
                    {
                        _blinkText += _textAlphabet[i]; // 블링크 텍스트에 해당 문자를 더한다
                        _isChecked = true; // 체크 여부 표시를 참으로 바꾼다
                    }
                    else
                    {
                        _blinkText += "   "; // 해당하지 않는 문자 위치는 공백으로 대체한다
                    }
                }

                blinkText.text = _blinkText; // 블링크 텍스트의 텍스트를 원 상태로 복구한다

                yield return new WaitForSeconds(blinkDelay); // blinkDelay 만큼 대기시간을 갖는다

                ++count; // 카운트를 전위 연산자로 올린다
            }
        }
        else // 블링크 타입 여부가 거짓이라면
        {
            while (true) // 무한 반복문 실행
            {
                string _blinkText = blinkText.text; // 블링크 텍스트 문자열을 가져온다
                blinkText.text = ""; // 블링크 텍스트를 빈 문자열로 만든다

                yield return new WaitForSeconds(blinkDelay); // blinkDelay 만큼 대기시간을 갖는다

                blinkText.text = _blinkText; // 블링크 텍스트의 텍스트를 원 상태로 복구한다

                yield return new WaitForSeconds(blinkDelay); // blinkDelay 만큼 대기시간을 갖는다
            }
        }
    }
}
