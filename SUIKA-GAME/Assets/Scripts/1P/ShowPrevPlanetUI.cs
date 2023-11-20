using UnityEngine;
using UnityEngine.UI;

public class ShowPrevPlanetUI : MonoBehaviour
{
    public static Image preview; // �̹��� ��������Ʈ�� ȭ�鿡 �����ֱ� ���� �̹��� ����

    private void Start() // ��ŸƮ �޼ҵ�
    {
        preview = GetComponentInParent<Image>(); // �θ� ������Ʈ�� �̹��� ��������Ʈ ������Ʈ�� �����´�
    }

    public static void OnSpriteChange(int _index) // ��������Ʈ �̹��� ������ ���� �޼ҵ�
    {
        preview.sprite = PlanetDatabase.planetSprite[_index]; // _index ���ڸ� �̿��ؼ� PlanetDatabase���� sprite�� ã�´�
    }
}
