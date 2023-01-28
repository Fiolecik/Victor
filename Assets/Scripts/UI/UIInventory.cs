using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum SlotType
{
    inventory,
    equipted
}

public class UIInventory : MonoBehaviour
{
    [SerializeField] Transform itemParrents;
    [SerializeField] UIDragableItem sketchItem;
    public Inventory inventory;

    [SerializeField] UIItemSlot[] inventorySlots;
    [SerializeField] UIItemSlot[] equiptedSlots;

    [SerializeField] List<UIDragableItem> inventoryItems = new List<UIDragableItem>();
    [SerializeField] List<UIDragableItem> equiptedItems = new List<UIDragableItem>();

    void init()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].id = i;
            inventorySlots[i].type = SlotType.inventory;
            inventorySlots[i].inventoryParrent = this;
        }

        for (int i = 0; i < equiptedSlots.Length; i++)
        {
            equiptedSlots[i].id = i;
            equiptedSlots[i].inventoryParrent = this;
        }
    }

    public void updateItems()
    {
        List<Item> items = inventory.getItems();
        /*if (items.Count > inventoryItems.Count)
        {
            for (int i = 0; i < items.Count - inventoryItems.Count; i++)
            {
                inventoryItems.Add(Instantiate(sketchItem.gameObject, itemParrents).GetComponent<UIDragableItem>());
            }
        }
        else if (items.Count < inventoryItems.Count)
        {
            for (int i = 0; i < inventoryItems.Count - items.Count; i++)
            {
                Destroy(inventoryItems[0].gameObject);
                inventoryItems.RemoveAt(0);
            }
        }*/

        for (int i = 0; i < items.Count; i++)
        {
            inventoryItems[i].setItem(items[i], this, SlotType.inventory, i);
            inventoryItems[i].transform.position = inventorySlots[i].transform.position;
            inventoryItems[i].gameObject.SetActive(true);
        }

        for(int i=items.Count; i<inventoryItems.Count; i++)
        {
            inventoryItems[i].gameObject.SetActive(false);
        }

        ItemSlot[] slots = inventory.getSlots();

        if (equiptedItems.Count == 0)
        {
            for (int i = 0; i < inventory.getSlots().Length; i++)
            {
                equiptedItems.Add(Instantiate(sketchItem, itemParrents).GetComponent<UIDragableItem>());
            }
        }

        for (int i = 0; i < slots.Length; i++)
        {
            if (slots[i].holdingItem != null)
            {
                equiptedItems[i].gameObject.SetActive(true);
                equiptedItems[i].setItem(slots[i].holdingItem, this, SlotType.equipted, i);
                equiptedItems[i].transform.position = equiptedSlots[i].transform.position;
            }
            else
            {
                equiptedItems[i].gameObject.SetActive(false);
            }
        }
    }


    public void open(Inventory inv)
    {
        inventory = inv;
        inventory.onInventoryChanged += updateItems;
        init();
        StartCoroutine(delay());
        
    }
    IEnumerator delay()
    {
        yield return new WaitForSeconds(0.2f);
        updateItems();
    }

    public void resetHighlights()
    {
        for (int i = 0; i < inventorySlots.Length; i++)
        {
            inventorySlots[i].setHighlightStatus(HighlighStatus.offline);
        }

        for (int i = 0; i < equiptedSlots.Length; i++)
        {
            equiptedSlots[i].setHighlightStatus(HighlighStatus.offline);
        }
    }

    public void setHighlighStatus(SlotType st, int id, HighlighStatus hs)
    {
        if (st == SlotType.equipted)
        {
            equiptedSlots[id].setHighlightStatus(hs);
        }
        else if (st == SlotType.inventory)
        {
            inventorySlots[id].setHighlightStatus(hs); ;
        }
    }
}
