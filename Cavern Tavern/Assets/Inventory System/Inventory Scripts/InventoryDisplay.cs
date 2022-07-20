using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public abstract class InventoryDisplay : MonoBehaviour
{
    [SerializeField] InventoryMouseItemData mouseInventoryItem;

    protected InventorySystem inventorySystem;
    protected Dictionary<InventorySlot_UI, InventorySlot> slotDictionary;
    public InventorySystem InventorySystem => inventorySystem;
    public Dictionary<InventorySlot_UI, InventorySlot> SlotDictionary => slotDictionary;

    protected virtual void Start()
    {

    }

    public abstract void AssignedSlot(InventorySystem invToDisplay);
    protected virtual void UpdateSlot(InventorySlot updatedSlot) //updates UI slot which corresponds to the backend slot
    {
        foreach (var slot in SlotDictionary)
        {
            if (slot.Value == updatedSlot) //slot.value is the backend slot
            {
                slot.Key.UpdateUISlot(updatedSlot); //slot.key is the frontend slot
            }
        }
    }

    public void SlotClicked(InventorySlot_UI clickedUISlot)
    {
        // If playing is holding shift key (any key works), split the stack

        // Clicked slot has an item + Mouse doesn't have item => pickup that item
        if (clickedUISlot.AssignedInventorySlot.ItemData != null && mouseInventoryItem.assignedInventorySlot.ItemData == null)
        {
            mouseInventoryItem.UpdateMouseSlot(clickedUISlot.AssignedInventorySlot);
            clickedUISlot.ClearSlot();
            return;
        }

        // Clicked slot doesn't have item + Mouse does have item => place item in empty slot

        if (clickedUISlot.AssignedInventorySlot.ItemData == null && mouseInventoryItem.assignedInventorySlot.ItemData != null)
        {
            clickedUISlot.AssignedInventorySlot.AssignItem(mouseInventoryItem.assignedInventorySlot);
            clickedUISlot.UpdateUISlot();
            mouseInventoryItem.ClearSlot();
        }

        // Both slots have item:

            // Both slots the same item => combine 

                //Is slot item amount + mouse item amount > Max stack size? If so, take from mouse

            //If different items, swap items
    }
}
