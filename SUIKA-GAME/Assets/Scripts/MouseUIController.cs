using UnityEngine;
using UnityEngine.EventSystems;

public class MouseUIController : MonoBehaviour, IPointerClickHandler, IDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public void OnPointerClick(PointerEventData _eventData) // 마우스 포인터가 UI 오브젝트를 클릭했을 때 메소드
    {
        // 구문 작성
    }

    public void OnDrag(PointerEventData _eventData) // 마우스 포인터가 UI 오브젝트 안에서 드래그 되었을 때 메소드
    {
        // 구문 작성
    }

    public void OnPointerEnter(PointerEventData _eventData) // 마우스 포인터가 UI 오브젝트에 들어왔을 때 메소드
    {
        SoundManager.Play.PlayEffect("MouseCursor"); // 사운드 이름으로 사운드 출력
    }

    public void OnPointerExit(PointerEventData _eventData) // 마우스 포인터가 UI 오브젝트에서 나갔을 때 메소드
    {
        SoundManager.Play.StopSE("MouseCursor"); // 사운드 이름으로 사운드 출력
    }
}
