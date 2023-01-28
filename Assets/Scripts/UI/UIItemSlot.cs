using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum HighlighStatus
{
    overSlotPossible,
    overSlotNotPossible,
    recomended,
    locked,
    offline
}

public class UIItemSlot : MonoBehaviour, IDropHandler, IPointerEnterHandler
{
    [SerializeField] Image status;
    [SerializeField] Color overSlotPossible;
    [SerializeField] Color overSlotNotPossible;
    [SerializeField] Color recomended;
    [SerializeField] Color locked;
    public SlotType type;
    public int id;
    public UIInventory inventoryParrent;

    public void OnDrop(PointerEventData eventData)
    {
        UIDragDropController.instance.dropIn(this);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        UIDragDropController.instance.enteredIn(this);
    }

    public void setHighlightStatus(HighlighStatus status)
    {
        this.status.gameObject.SetActive(true);
        switch (status)
        {
            case HighlighStatus.overSlotPossible:
                this.status.color = overSlotPossible;
                break;
            case HighlighStatus.overSlotNotPossible:
                this.status.color = overSlotNotPossible;
                break;
            case HighlighStatus.recomended:
                this.status.color = recomended;
                break;
            case HighlighStatus.locked:
                this.status.color = locked;
                break;
            case HighlighStatus.offline:
                this.status.gameObject.SetActive(false);
                break;
            default:
                break;
        }
    }
}
