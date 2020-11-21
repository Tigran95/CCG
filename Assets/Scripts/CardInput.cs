using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;
public class CardInput : MonoBehaviour, IPointerEnterHandler,IPointerExitHandler,IPointerDownHandler,IPointerUpHandler,IDragHandler 
{
    [SerializeField] private float _enterMovestep;
    private Vector2 _lastPos;
    private Vector2 _finalPos;
    private Sequence seq;

    private Card _card;


    private void Start()
    {
        _card = GetComponent<Card>();
    }
    public void OnPointerDown(PointerEventData eventData)
    {
        InputController.InputBlocked = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (DropPanel.Instance.IsCardInRT(transform.position))
        {
            DropToPanel();
        }
        else
        {
            ReturnCardsToListView();
        }
    }
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (InputController.InputBlocked) { return; }
        seq.Append(transform.DOMove(_finalPos, 0.2f));
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        if (InputController.InputBlocked) { return; }
        seq.Append(transform.DOMove(_lastPos, 0.2f));
    }
    public void OnDrag(PointerEventData eventData)
    {
        if (!_card.CanIDrag) { return; }
        transform.position = eventData.position;
    }
    public void UpdateInListPositions()
    {
        seq = DOTween.Sequence();
        _lastPos = transform.position;
        _finalPos = _lastPos + new Vector2(0, _enterMovestep);
    }

    private void DropToPanel()
    {
        InputController.InputBlocked = false;
        CardRemover.RemoveCardFromList(_card);
        Destroy(this);
    }
    private void ReturnCardsToListView()
    {
        seq = DOTween.Sequence();
        seq.Append(transform.DOMove(_lastPos, 0.3f)).SetEase(Ease.OutCirc);
        seq.OnComplete(() =>
        {
            InputController.InputBlocked = false;
        });
    }
}

