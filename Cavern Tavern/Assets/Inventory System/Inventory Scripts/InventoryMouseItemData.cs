using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventoryMouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;
    public InventorySlot assignedInventorySlot;

    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }

    public void UpdateMouseSlot(InventorySlot inventorySlot) //if has an item, follow mouse pointer
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

    private void Update()
    {
        if (assignedInventorySlot.ItemData != null) //if mouse currently has item
        {
            transform.position = Input.mousePosition;
            if (Input.GetMouseButtonDown(0) && !IsPointerOverUIObject())
            {
                ClearSlot();
            }
        }
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
