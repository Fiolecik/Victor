using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct ItemSlot
{
    public Item holdingItem;
    public ItemType type;
}

public class Inventory : MonoBehaviour
{
    [SerializeField] int invCapacity;
    List<Item> items = new List<Item>();
    [SerializeField] ItemSlot[] slots;

    public delegate void inventoryChanged();
    public inventoryChanged onInventoryChanged;
    public inventoryChanged onGunChanged;

    public bool addItem(Item item)
    {
        if(isPossibleAddToInventory())
        {
            items.Add(Item.copy(item));
            invoke();
            return true;
        }
        return false;
    }

    public void removeItem(Item item)
    {
        items.Remove(item);
        invoke();
    }

    public List<Item> getItems()
    {
        return items;
    }

    public ItemSlot[] getSlots()
    {
        return slots;
    }

    public void moveToSlot(int slot, Item item)
    {
        if (isPossibleMoveToSlot(slot, item) && slot < slots.Length)
        {
            int id = items.FindIndex(it => it == item);
            if (id == -1)
                return;
            slots[slot].holdingItem = items[id];
            removeItem(items[id]);
            if (slots[slot].type == ItemType.weapon)
                if (onGunChanged != null)
                    onGunChanged.Invoke();
            invoke();
        }
    }

    public void moveToInventory(int slot)
    {
        if (isPossibleAddToInventory() && slot < slots.Length && slots[slot].holdingItem!=null)
        {
            addItem(slots[slot].holdingItem);
            slots[slot].holdingItem = null;
            if (slots[slot].type == ItemType.weapon)
                if (onGunChanged != null)
                    onGunChanged.Invoke();
            invoke();
        }
    }

    public bool isPossibleMoveToSlot(int slot, Item item)
    {
        return slot < slots.Length && slots[slot].type == item.type && slots[slot].holdingItem == null;
    }

    public bool isPossibleAddToInventory()
    {
        return items.Count + 1 < invCapacity;
    }

    public Statistics getStatistics()
    {
        Statistics all = new Statistics();
        for(int i=0; i<slots.Length; i++)
        {
            if(slots[i].holdingItem !=null)
            {
                all += slots[i].holdingItem.statistics;
            }
        }
        return all;
    }

    void invoke()
    {
        if (onInventoryChanged != null)
            onInventoryChanged.Invoke();
    }

    public ItemSlot[] getSlotsByType(ItemType itemType)
    {
        List<ItemSlot> slots = new List<ItemSlot>();
        for(int i=0; i<this.slots.Length; i++)
        {
            if(itemType == this.slots[i].type)
            {
                slots.Add(this.slots[i]);
            }
        }
        return slots.ToArray();
    }
}
