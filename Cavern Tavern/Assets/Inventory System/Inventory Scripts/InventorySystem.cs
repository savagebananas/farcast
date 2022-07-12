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

    public UnityAction<InventorySlot> OnInventorySlotChanged; //fires event when item is added to inventory

    public InventorySystem(int size)
    {
        inventorySlots = new List<InventorySlot>(size); //creates list with set size
        for (int i = 0; i < size; i++)
        {
            inventorySlots.Add(new InventorySlot()); //fills each index with an empty inventory slot (nothing is passed in)
        }
    }

    public bool AddToInventory(InventoryItemData itemToAdd, int amountToAdd)
    {
        //temp
        inventorySlots[0] = new InventorySlot(itemToAdd, amountToAdd);
        return true;
    }
}
