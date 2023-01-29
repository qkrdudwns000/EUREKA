using UnityEngine;
using UnityEngine.EventSystems;

public class QuestPopupHandler : MonoBehaviour, IBeginDragHandler, IDragHandler
{
    private RectTransform _parentRect; // �˾�â ��ü�� rect��

    private Vector2 _moveOffset; // �巡�׽������� ���콺�����Ϳ� rect���̰Ÿ��� ��������.

    private Vector2 halfSize = Vector2.zero; //ȭ������γ����������ʰ��ϱ����Ѻ���.

    private void Awake()
    {
        _parentRect = transform.parent.GetComponent<RectTransform>();
        halfSize = _parentRect.sizeDelta * 0.5f;
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _moveOffset = (Vector2)_parentRect.position - eventData.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 pos = eventData.position + _moveOffset;
        pos.x = Mathf.Clamp(pos.x, halfSize.x, Screen.width - halfSize.x);
        pos.y = Mathf.Clamp(pos.y, halfSize.y, Screen.height - halfSize.y);
        _parentRect.position = pos;
    }
}
