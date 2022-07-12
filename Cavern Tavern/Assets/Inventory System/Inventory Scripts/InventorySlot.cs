using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class InventorySlot
{
    [SerializeField] private InventoryItemData itemData; //the item itself
    [SerializeField] private int stackSize; //amount of items in slot

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount) //make slot with item and amount
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() //make empty inventory slot (preset)
    {
        ClearSlot();
    }

    public void ClearSlot() //method to clear inventory slot
    {
        itemData = null;
        stackSize = -1;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) //amountRemaining = space left in the inventory slot
    {
        amountRemaining = ItemData.maxStackSize - stackSize;
        return RoomLeftInStack(amountToAdd); //returns if there is room left in stack to add X items
    }

    public bool RoomLeftInStack(int amountToAdd)
    {
        if (stackSize + amountToAdd <= itemData.maxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
    }
}
