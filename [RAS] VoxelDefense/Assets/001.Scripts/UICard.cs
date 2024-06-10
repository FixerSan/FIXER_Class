using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UICard : UIBase, IBeginDragHandler,IDragHandler, IEndDragHandler
{
    public int defenderIndex;
    private Image card;
    private Vector2 firstPos;

    public override void Init()
    {
        card = GetComponent<Image>();  
    }

    public void Init(int _index)
    {
        Init();
        defenderIndex = _index;
    }
    public override void Redraw()
    {
        
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        firstPos = card.rectTransform.anchoredPosition;
        card.raycastTarget = false;
        GameManager.Instance.ingame.SelectCard();
    }

    public void OnDrag(PointerEventData eventData)
    {
        card.rectTransform.anchoredPosition += eventData.delta;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        card.rectTransform.anchoredPosition = firstPos;
        card.raycastTarget = true;
        GameManager.Instance.ingame.DropCard(this);
    }

}
