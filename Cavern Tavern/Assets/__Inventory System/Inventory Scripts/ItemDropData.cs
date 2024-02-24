using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDropData : MonoBehaviour
{
    public InventoryItemData item;
    public int amount;

    public void SetData(InventoryItemData item, int amount)
    {
        this.item = item;
        this.amount = amount;
    }
}
