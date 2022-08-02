using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CircleCollider2D))]
public class ItemPickup : MonoBehaviour
{
    public float pickupRadius = 1f;
    public InventoryItemData itemData; //the actual item(s) to pickup

    private CircleCollider2D itemCollider;

    private void Awake()
    {
        itemCollider = GetComponent<CircleCollider2D>();
        itemCollider.isTrigger = true; //sets collider to trigger (no physics or collision)
        itemCollider.radius = pickupRadius;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        var inventory = collision.transform.Find("PlayerInventory").GetComponent<InventoryHolder>();
        if (!inventory) return;
        if (inventory.InventorySystem.AddToInventory(itemData, 1)) //if item was added to inventory
        {
            Destroy(this.gameObject); //destroy gameobject since item is picked up
        }
    }
}
