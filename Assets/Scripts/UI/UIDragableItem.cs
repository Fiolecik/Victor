using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIDragableItem : MonoBehaviour, IEndDragHandler, IDragHandler, IBeginDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] Image image;
    [SerializeField] CanvasGroup cg;
    [HideInInspector] public SlotType where;
    [HideInInspector] public Item holdingItem;
    [HideInInspector] public UIInventory inventoryParrent;
    [HideInInspector] public int placeId;

    public void OnBeginDrag(PointerEventData eventData)
    {
        UIDragDropController.instance.startDrag(this);
        cg.blocksRaycasts = false;
    }

    public void OnDrag(PointerEventData eventData)
    {
        image.rectTransform.anchoredPosition += eventData.delta / UIController.instance.mainCanvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        UIDragDropController.instance.stopDrag();
        cg.blocksRaycasts = true;
    }

    public void setItem(Item hi, UIInventory ip, SlotType type, int placeId)
    {
        where = type;
        inventoryParrent = ip;
        holdingItem = hi;
        image.sprite = hi.sprite;
        this.placeId = placeId;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIController.instance.showDescription(holdingItem);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        UIController.instance.hideDescription();
    }

    public void resetCg()
    {
        cg.blocksRaycasts = true;
    }
}
