using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;
using Unity.VisualScripting;

public class InventoryMouseItemData : MonoBehaviour
{
    public GameObject itemDropPrefab;

    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot assignedInventorySlot;

    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    private void Update()
    {
        if (assignedInventorySlot.ItemData != null) //if mouse currently has item
        {
            transform.position = Input.mousePosition;

            // Drop Item
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                // Drop Item and assign values
                var itemDrop = Instantiate(itemDropPrefab);
                itemDrop.transform.position = PlayerBase.playerTransform.position;
                InventoryItemData itemData = assignedInventorySlot.ItemData;
                itemDrop.GetComponentInChildren<SpriteRenderer>().sprite = itemData.itemIcon;
                itemDrop.GetComponentInChildren<ItemDropData>().SetData(itemData, assignedInventorySlot.StackSize);
                
                // If mouse on right side of player, drop item to the right
                if (Camera.main.ScreenToWorldPoint(Input.mousePosition).x > PlayerBase.playerTransform.position.x)
                {
                    Vector2 pos = itemDrop.transform.position;
                    Vector2 newPos = new Vector2(itemDrop.transform.position.x + 50, itemDrop.transform.position.y);
                    itemDrop.transform.position = Vector2.Lerp(itemDrop.transform.position, newPos, 5 * Time.deltaTime);
                }
                else // Drop item to the left
                {
                    Vector2 pos = itemDrop.transform.position;
                    Vector2 newPos = new Vector2(itemDrop.transform.position.x - 50, itemDrop.transform.position.y);
                    itemDrop.transform.position = Vector2.Lerp(itemDrop.transform.position, newPos, 5 * Time.deltaTime);
                }

                ClearSlot();
            }
        }
    }

    /// <summary>
    /// Updates Mouse Inventory Slot UI
    /// </summary>
    public void UpdateMouseSlot(InventorySlot inventorySlot) 
    {
        assignedInventorySlot.AssignItem(inventorySlot);
        itemSprite.sprite = inventorySlot.ItemData.itemIcon;
        itemSprite.color = Color.white;
        if (inventorySlot.StackSize > 1)
        {
            itemCount.text = inventorySlot.StackSize.ToString();
        }

        itemSprite.transform.SetAsLastSibling();
        itemCount.transform.SetAsLastSibling();

    }

    public void ClearSlot()
    {
        assignedInventorySlot.ClearSlot();
        itemCount.text = "";
        itemSprite.color = Color.clear;
        itemSprite.sprite = null;
    }

    public static bool IsPointerOverUIObject()
    {
        PointerEventData eventDataCurrentPosition = new PointerEventData(EventSystem.current);
        eventDataCurrentPosition.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        EventSystem.current.RaycastAll(eventDataCurrentPosition, results);
        return results.Count > 0;
    }




}
