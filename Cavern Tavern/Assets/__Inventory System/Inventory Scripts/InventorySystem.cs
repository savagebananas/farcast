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

    public List<InventorySlot> InventorySlots => inventorySlots; 
    public int InventorySize => InventorySlots.Count; 

    public UnityAction<InventorySlot> OnInventorySlotChanged; 

    public InventorySystem(int size) //Constructor
    {
        inventorySlots = new List<InventorySlot>(size); 
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot()); //fills each index with an empty inventory slot
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd) //Returns true if item is added
    {
        if (ContainsItem(itemToAdd, out List<InventorySlot> invSlots)) //Check whether item exists in inventory
        {
            foreach (var slot in invSlots) //runs through list of slots with item and adds item to one with space
            {
                if (slot.EnoughRoomLeftInStack(amountToAdd))
                {
                    slot.AddToStack(amountToAdd); 
                    OnInventorySlotChanged?.Invoke(slot); 
                    return true;
                }
            }
 
        }

        if (HasFreeSlot(out InventorySlot freeSlot))
        {
            if (freeSlot.EnoughRoomLeftInStack(amountToAdd))
            {
                freeSlot.UpdateInventorySlot(itemToAdd, amountToAdd);
                OnInventorySlotChanged?.Invoke(freeSlot);
                return true;
            }
        }

        return false; //no space for item
    }

    public bool ContainsItem(InventoryItemData itemToAdd, out List<InventorySlot> invSlots) //Checks a inventory slots for the same item as the one passed
    {
        invSlots = InventorySlots.Where(slot => slot.ItemData == itemToAdd).ToList(); //Get a list of all the slots with the item

        return invSlots == null ? false : true;
    }

    public bool HasFreeSlot(out InventorySlot freeSlot)
    {
        freeSlot = InventorySlots.FirstOrDefault(slot => slot.ItemData == null); //Gets first free slot
        return freeSlot == null ? false : true;
    }
}
