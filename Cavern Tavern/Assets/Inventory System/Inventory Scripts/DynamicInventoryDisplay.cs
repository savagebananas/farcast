using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DynamicInventoryDisplay : InventoryDisplay
{
    protected override void Start()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += RefreshDynamicInventory;
        base.Start();
    }

    public void RefreshDynamicInventory(InventorySystem invToDisplay)
    {
        inventorySystem = invToDisplay;
    }

    public override void AssignedSlot(InventorySystem invToDisplay)
    {
        
    }
}
