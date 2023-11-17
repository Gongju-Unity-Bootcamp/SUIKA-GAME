using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;

public class CSVReader
{
    public static string SPLIT_RE = @",(?=(?:[^""]*""[^""]*"")*(?![^""]*""))"; // csv 파일 변환 문자를 자르기 위한 문자열 변수 값
    public static string LINE_SPLIT_RE = @"\r\n|\n\r|\n|\r"; // 줄바꿈 변환 문자를 자르기 위한 문자열 변수 값
    public static char[] TRIM_CHARS = { '\"' }; // 공백 제거를 위한 캐릭터형 배열 변수 값

    public static List<Dictionary<string, object>> Read(string file)
        // 딕셔너리를 이용하여 리스트를 읽어들이는 정적 메소드로 인자로는 csv 파일을 받는다
    {
        var list = new List<Dictionary<string, object>>(); // var(자료형 자동 지정) 변수 list를 생성자를 이용해서 딕셔너리 리스트로 생성한다
        TextAsset data = Resources.Load(file) as TextAsset; // 유니티 엔진의 텍스트 에셋을 데이터 식별자로 리소스를 받아온다

        var lines = Regex.Split(data.text, LINE_SPLIT_RE); // 줄바꿈 변환 문자를 통해 자르고 난 뒤 lines에 저장한다

        if (lines.Length <= 1) // 라일 길이가 하나 이하라면
        {
            return list; // 리스트 즉시 반환
        }

        var header = Regex.Split(lines[0], SPLIT_RE); // csv 파일 변환 문자를 통해 header를 구분지은 후 header에 저장한다

        for (var i = 1; i < lines.Length; ++i) // i를 인덱스로 초기값 설정한 뒤 라인 길이 만큼 반복한다
        {

            var values = Regex.Split(lines[i], SPLIT_RE); // csv 파일 변환 문자를 통해 values에 저장한다

            if (values.Length == 0 || values[0] == "") // values의 길이가 0 이거나 빈 문자열일 때
            {
                continue; // 이번 반복문의 아래 구문을 제외하고 다시 반복한다
            }

            var entry = new Dictionary<string, object>(); // 새로운 딕셔너리를 생성자를 통해 생성한 후에 entry에 생성한다

            for (var j = 0; j < header.Length && j < values.Length; ++j)
                // j를 인덱스 초기값으로 설정한 뒤 header와(&& / and 연산) values값 미만일 때까지 반복한다
            {
                string value = values[j]; // 문자열 value에 j번 인덱스의 문자열을 저장한다
                value = value.TrimStart(TRIM_CHARS).TrimEnd(TRIM_CHARS).Replace("\\", "");
                // value 문자열 앞과 뒤의 공백을 설정한 캐릭터형 배열로 없앤 뒤 없어지지 않은 문자를 리플레이스 메소드를 이용해 빈 문자열로 재배치한다
                object finalvalue = value; // 오브젝트 타입의 finalvalue에 지정한 value 값을 저장한다
                int n; // 정수형 값을 입력받은 뒤 반환하기 위한 변수 값
                float f; // 실수형 값을 입력받은 뒤 반환하기 위한 변수 값

                if (int.TryParse(value, out n)) // 정수형으로 형변환을 시도 가능할 때(value 값이 정수일 때)
                {
                    finalvalue = n; // finalvalue에 변환한 정수 값을 저장한다
                }
                else if (float.TryParse(value, out f)) // 실수형으로 형변환을 시도 가능할 때(value 값이 정수일 때)
                {
                    finalvalue = f; // finalvalue에 변환한 실수 값을 저장한다
                }

                entry[header[j]] = finalvalue; // 생성해두었던 딕셔너리 entry의 지정 j 인덱스에 형변환을 마친 finalvalue 값을 저장한다
            }

            list.Add(entry); // 엔트리 딕셔너리를 리스트에 추가한다 add() 메소드는 인덱스 0부터 차례로 입력한다
        }

        return list; // 반복문을 모두 마친 뒤 저장된 리스트를 반환한다
    }
}
