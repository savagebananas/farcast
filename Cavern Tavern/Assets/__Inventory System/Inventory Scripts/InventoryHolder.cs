using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryHolder : MonoBehaviour
{
    [SerializeField] private int inventorySize;
    [SerializeField] protected InventorySystem inventorySystem;

    public InventorySystem InventorySystem => inventorySystem; //getter
    
    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayRequested;
    public static UnityAction<InventorySystem> OnDynamicInventoryDisplayDestroy;

    private void Awake()
    {
        inventorySystem = new InventorySystem(inventorySize);
    }
}
