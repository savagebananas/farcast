using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Every inventory slot has itemData and stackSize
*/

[System.Serializable]
public class InventorySlot
{
    [SerializeField] public InventoryItemData itemData; //Reference to item data
    [SerializeField] private int stackSize; //Amount of items in slot

    public InventoryItemData ItemData => itemData;
    public int StackSize => stackSize;

    public InventorySlot(InventoryItemData source, int amount) //Constructor to make an occupiced slot
    {
        itemData = source;
        stackSize = amount;
    }

    public InventorySlot() //Constructor to make a blank slot
    {
        ClearSlot();
    }

    public void ClearSlot() //method to clear backend inventory slot
    {
        itemData = null;
        stackSize = -1;
    }

    public void AssignItem(InventorySlot invSlot) //Assigns Item to Slot
    {
        if (itemData == invSlot.itemData) AddToStack(invSlot.stackSize); //Does slot contain the same item? Add to stack if so
        else //Overide slot with the inventory slot that is being passed in
        {
            itemData = invSlot.itemData;
            stackSize = 0;
            AddToStack(invSlot.stackSize);
        }
    }

    public void UpdateInventorySlot(InventoryItemData data, int amount) //Updates slot directly
    {
        itemData = data;
        stackSize = amount;
    }

    public bool RoomLeftInStack(int amountToAdd, out int amountRemaining) //Checks if there is enough room left in stack to add a certain amount of items
    {
        amountRemaining = ItemData.maxStackSize - stackSize;
        return EnoughRoomLeftInStack(amountToAdd); //returns if there is room left in stack to add X items
    }

    public bool EnoughRoomLeftInStack(int amountToAdd)
    {
        if (itemData == null || itemData != null && stackSize + amountToAdd <= itemData.maxStackSize) return true;
        else return false;
    }

    public void AddToStack(int amount)
    {
        stackSize += amount;
    }

    public void RemoveFromStack(int amount)
    {
        stackSize -= amount;
        if (stackSize <= 0)
        {
            Debug.Log("Clear slot");
            ClearSlot();
        }
    }

    public bool SplitStack(out InventorySlot splitStack)
    {
        if (stackSize <= 1)
        {
            splitStack = null;
            return false;
        }

        int halfStack = Mathf.RoundToInt(stackSize / 2);
        RemoveFromStack(halfStack);

        splitStack = new InventorySlot(ItemData, halfStack);
        return true;
    }
}
