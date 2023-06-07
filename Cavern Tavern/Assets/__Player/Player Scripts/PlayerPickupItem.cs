using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPickupItem : MonoBehaviour
{
    private GameObject itemGettingPickedUp;

    [SerializeField] float pickupRadius;
    [SerializeField] InventoryHolder playerInventory;
    [SerializeField] InventoryHolder playerHotbar;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) PickupNearestItem();
        MoveItemTowardsPlayer();

        if (FindNearestItem() != null) Debug.DrawLine(transform.position, FindNearestItem().transform.position, Color.red);
    }

    void PickupNearestItem() //adds nearest item into inventory and destroys the physical one
    {
        if(itemGettingPickedUp = FindNearestItem())
        {
            var itemData = itemGettingPickedUp.GetComponentInChildren<ItemDropData>();
            if (playerHotbar.InventorySystem.AddToInventory(itemData.item, itemData.amount)){} //if hotbar has space, add to hotbar
            else playerInventory.InventorySystem.AddToInventory(itemData.item, itemData.amount); //else add to bag
            
            Destroy(itemData);
            StartCoroutine(DestroyItemVisuals());
        }
    }

    void MoveItemTowardsPlayer()
    {
        if (itemGettingPickedUp != null)
        {
            var itemTransform = itemGettingPickedUp.transform;
            itemTransform.position = Vector2.Lerp(itemGettingPickedUp.transform.position, transform.position, 0.05f);
            
            var itemScale = Mathf.Lerp(itemTransform.localScale.x, 0, 0.03f);
            itemTransform.localScale = new Vector2(itemScale, itemScale);
        }
    }

    IEnumerator DestroyItemVisuals()
    {
        //Debug.Log("destroy item");
        yield return new WaitForSeconds(0.1f);
        Destroy(itemGettingPickedUp);
    }

    GameObject FindNearestItem() //Returns the nearest item gameobject within pickup range
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, pickupRadius);
        var nearestDistance = float.MaxValue;
        GameObject nearestItem = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "ItemDrop" && colliders[i].gameObject != itemGettingPickedUp)
            {
                if (Vector2.Distance(transform.position, colliders[i].transform.position) < nearestDistance)
                {
                    nearestDistance = Vector2.Distance(transform.position, colliders[i].gameObject.transform.position);
                    nearestItem = colliders[i].gameObject;
                }
            }
        }
        return nearestItem; 
    }
}
