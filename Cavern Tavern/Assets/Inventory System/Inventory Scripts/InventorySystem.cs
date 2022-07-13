using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;

/*
 Creates an inventory with a set size for items to fill up in
-Uses a list of InventorySlots (class)
 */

[System.Serializable] 
public class InventorySystem
{
    [SerializeField] private List<InventorySlot> inventorySlots;
    private int inventorySize;

    public List<InventorySlot> InventorySlots => inventorySlots; //getter
    public int InventorySize => InventorySlots.Count; //getter

    public UnityAction<InventorySlot> OnInventorySlotChanged; //helps fire event

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size); //creates list with set size
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot()); //fills each index with an empty inventory slot (nothing is passed in)
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd) //Returns true if item is added
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlots)) //Check whether item exists in inventory
        {
            foreach (var slot in invSlots) //runs through list of slots with item and adds item to one with space
            {
                if (slot.RoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd); 
                    OnInventorySlotChanged?.Invoke(slot); 
                    return true;
                }
            }
 
        }

        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
            OnInventorySlotChanged?.Invoke(freeSlot);
            return true;
        }

        return false; //no space for item
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlots)
    {
        //Uses System.Linq: Checks all inventory slots, find all slots where item matches and places it in invSlot list
        invSlots = InventorySlots.Where(slot => slot.ItemData == itemToAdd).ToList();

        return invSlots == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(slot => slot.ItemData == null); //finds first slot in list (inventory) that is empty
        return freeSlot == null ? false : true;
    }
}
