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

    public void SlotClicked(InventorySlot_UI clickedSlot)
    {
        Debug.Log("Slot Clicked");
    }
}
