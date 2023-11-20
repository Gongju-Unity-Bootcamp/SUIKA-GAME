using UnityEngine;
using UnityEngine.EventSystems;

public class MouseUIController : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData _eventData) // ���콺 �����Ͱ� UI ������Ʈ�� Ŭ������ �� �޼ҵ�
    {
        // ���� �ۼ�
    }

    public void OnDrag(PointerEventData _eventData) // ���콺 �����Ͱ� UI ������Ʈ �ȿ��� �巡�� �Ǿ��� �� �޼ҵ�
    {
        // ���� �ۼ�
    }

    public void OnPointerEnter(PointerEventData _eventData) // ���콺 �����Ͱ� UI ������Ʈ�� ������ �� �޼ҵ�
    {
        SoundManager.Play.PlayEffect("MouseCursor"); // ���� �̸����� ���� ���
    }

    public void OnPointerExit(PointerEventData _eventData) // ���콺 �����Ͱ� UI ������Ʈ���� ������ �� �޼ҵ�
    {
        SoundManager.Play.StopEffect("MouseCursor"); // ���� �̸����� ���� ���
    }
}
