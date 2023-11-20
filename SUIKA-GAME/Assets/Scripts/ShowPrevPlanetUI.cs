using UnityEngine;
using UnityEngine.UI;

public class ShowPrevPlanetUI : MonoBehaviour
{
    public static Image preview; // 이미지 스프라이트를 화면에 보여주기 위한 이미지 변수

    private void Start() // 스타트 메소드
    {
        preview = GetComponentInParent<Image>(); // 부모 오브젝트의 이미지 스프라이트 컴포넌트를 가져온다
    }

    public static void OnSpriteChange(int _index) // 스프라이트 이미지 변경을 위한 메소드
    {
        preview.sprite = PlanetDatabase.planetSprite[_index]; // _index 인자를 이용해서 PlanetDatabase에서 sprite를 찾는다
    }
}
