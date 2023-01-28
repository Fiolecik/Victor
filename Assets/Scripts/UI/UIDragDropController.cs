using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIDragDropController : MonoBehaviour
{
    public static UIDragDropController instance;
    private void Awake()
    {
        if (instance == null)
            instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    UIDragableItem draggingItem;

    public void startDrag(UIDragableItem udi)
    {
        draggingItem = udi;
    }

    public void stopDrag()
    {
        resetHighlights();
        if (draggingItem != null)
        {
            draggingItem.inventoryParrent.updateItems();
            draggingItem = null;
        }
    }

    public void dropIn(UIItemSlot uis)
    {
        draggingItem.resetCg();
        resetHighlights();
        if(uis.type == SlotType.inventory)
        {
            if(draggingItem.where == SlotType.equipted)
            {
                uis.inventoryParrent.inventory.moveToInventory(draggingItem.placeId);
            }
        }
        else if (uis.type == SlotType.equipted)
        {
            if(draggingItem.where== SlotType.inventory)
            {
                uis.inventoryParrent.inventory.moveToSlot(uis.id, draggingItem.holdingItem);
            }
        }

        draggingItem.inventoryParrent.updateItems();
        draggingItem = null;
    }

    public void enteredIn(UIItemSlot uis)
    {
        if (draggingItem == null)
            return;
        resetHighlights();
        setHighlightsByType();

        if(uis.type == SlotType.inventory)
        {
            if(draggingItem.where== SlotType.equipted)
            {
                if(uis.inventoryParrent.inventory.isPossibleAddToInventory())
                {
                    uis.inventoryParrent.setHighlighStatus(uis.type, uis.id, HighlighStatus.overSlotPossible);
                }
                else
                {
                    uis.inventoryParrent.setHighlighStatus(uis.type, uis.id, HighlighStatus.overSlotNotPossible);
                }
            }
            else
            {
                uis.inventoryParrent.setHighlighStatus(uis.type, uis.id, HighlighStatus.overSlotPossible);
            }
        }
        else if(uis.type == SlotType.equipted)
        {
            if(draggingItem.where==SlotType.inventory)
            {
                if(uis.inventoryParrent.inventory.isPossibleMoveToSlot(uis.id, draggingItem.holdingItem))
                {
                    uis.inventoryParrent.setHighlighStatus(uis.type, uis.id, HighlighStatus.overSlotPossible);
                }
                else
                {
                    uis.inventoryParrent.setHighlighStatus(uis.type, uis.id, HighlighStatus.overSlotNotPossible);
                }
            }
        }
    }

    void resetHighlights()
    {
        if (draggingItem != null)
            draggingItem.inventoryParrent.resetHighlights();
    }

    void setHighlightsByType()
    {
        if (draggingItem != null)
        {
            ItemSlot[] slots = draggingItem.inventoryParrent.inventory.getSlots();
            for (int i = 0; i < slots.Length; i++)
            {
                if (draggingItem.inventoryParrent.inventory.isPossibleMoveToSlot(i, draggingItem.holdingItem))
                {
                    draggingItem.inventoryParrent.setHighlighStatus(SlotType.equipted, i, HighlighStatus.recomended);
                }
                else
                {
                    draggingItem.inventoryParrent.setHighlighStatus(SlotType.equipted, i, HighlighStatus.locked);
                }
            }
        }
    }
}
