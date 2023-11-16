using System.Collections.Generic;
using UnityEngine;

public class PlanetDatabase : MonoBehaviour
{
    public static int[] planetLevel { get; private set; } 
    // 행성 레벨을 정수 배열로 담은 변수값 지정, 이 때 이 스크립트에서만 변수 조작이 가능하고 다른 스크립트에서는 변수 값을 가져오는 것만 가능하다
    public static int[] planetScore { get; private set; }
    // 위와 마찬가지이지만 행성 점수를 정수 배열로 담는다
    public static string[] planetName { get; private set; }
    // 위와 마찬가지이지만 행성 이름을 문자열 변수로 담는다
    public static float[] planetColliderRadius { get; private set; }
    // 위와 마찬가지이지만 행성 CircleCollider2D의 범위 값을 실수 배열로 담는다
    public static Vector3[] planetScale { get; private set; }
    // 위와 마찬가지이지만 행성 크기 값을 Vector3 배열로 담는다
    public static Sprite[] planetSprite { get; private set; }
    // 위와 마찬가지이지만 행성 이미지 스프라이트를 스프라이트 배열로 담는다
    public static int planetIndex { get; private set; }
    // 위와 마찬가지이지만 행성의 총 개수를 정수 변수 값에 담는다

    private void Start() // 스타트 메소드
    {
        OnLoadDataSheet(); // 행성의 모든 정보를 담은 데이터 시트지를 로드하는 메소드
    }

    private void OnLoadDataSheet() // 행성의 모든 정보를 담은 데이터 시트지를 로드하는 메소드
    {
        List<Dictionary<string, object>> data = CSVReader.Read("DataSheets/PlanetInfoSheet");
        // 리스트 안의 딕셔너리에 인덱스 값과 header (ex) "Level", "Name"같은 헤더를 의미)를 담아 엑셀 CSV 파일을 담는다
        planetIndex = data.Count; // 행성의 총 개수를 정수 변수 값에 담는다

        planetLevel = new int[data.Count]; // 행성 레벨에 대한 배열 범위(크기) 지정
        planetScore = new int[data.Count]; // 행성 점수에 대한 배열 범위(크기) 지정
        planetName = new string[data.Count]; // 행성 이름에 대한 배열 범위(크기) 지정
        planetColliderRadius = new float[data.Count]; // 행성 CircleCollider2D에 대한 배열 범위(크기) 지정
        planetScale = new Vector3[data.Count]; // 행성 크기에 대한 배열 범위(크기) 지정
        planetSprite = new Sprite[data.Count]; // 행성 이미지 스프라이트에 대한 배열 범위(크기) 지정

        for (var i = 0; i < data.Count; ++i) // 행성의 총 개수 만큼의 반복문
        {
            planetLevel[i] = int.Parse(data[i]["Level"].ToString()); // object를 문자열로 형변환한 뒤 정수로 형변환
            planetScore[i] = int.Parse(data[i]["Score"].ToString()); // object를 문자열로 형변환한 뒤 정수로 형변환
            planetName[i] = (string)data[i]["Name"].ToString();
            // 문자열 형변환 후에 문자열 배열 변수에 담는다
            planetColliderRadius[i] = float.Parse(data[i]["ColliderRadius"].ToString());
            // 문자열 형변환 후에 실수형 형변환 후 변수에 담는다
            planetScale[i] = new Vector3(float.Parse(data[i]["ScaleX"].ToString()), float.Parse(data[i]["ScaleX"].ToString()), 1.0f);
            // 문자열 형변환 후에 실수형 형변환 후 각각의 x, y, z 좌표에 담는다
            planetSprite[i] = Resources.Load<Sprite>((string)data[i]["ImageResource"].ToString());
            // 이미지 스프라이트로 Assets/Resources 경로에 있는 이미지 스프라이트 경로를 찾아 형변환 후에 변수에 담는다
        }
    }
}
