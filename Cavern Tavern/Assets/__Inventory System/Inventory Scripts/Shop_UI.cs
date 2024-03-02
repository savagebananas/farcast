using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop_UI : MonoBehaviour
{
    public ShopSlot_UI[] itemUISlots;
    private InventoryItemData selectedItemData;

    [Header("Events")]
    public GameEvent onBuyItem;

    [Header("Selected Item UI")]
    public Image selectedItemIconUI;
    public TextMeshProUGUI selectedItemNameUI;
    public TextMeshProUGUI selectedItemDescriptionUI;

    public void UpdateShopUI(Component sender, object data)
    {
        if(sender is Shop)
        {
            ClearAll();

            var shopItems = (InventoryItemData[]) data;

            for (int i = 0; i < itemUISlots.Length; i++)
            {
                bool slotHasItem = i < shopItems.Length;
                if (slotHasItem)
                {
                    itemUISlots[i].gameObject.SetActive(true);
                    itemUISlots[i].UpdateShopSlot(shopItems[i]);
                }
                else
                {
                    itemUISlots[i].gameObject.SetActive(false);
                }
            }

            DisplaySelectedItem(this, shopItems[0]);
        }
    }

    private void ClearAll()
    {
        //Clear shop items UI (Box 1)
        for (int i = 0; i < itemUISlots.Length; i++)
        {
            itemUISlots[i].ClearSlot();
        }

        //Clear select item UI (Box 2)
        selectedItemIconUI.sprite = null;
        selectedItemIconUI.color = new Color(255, 255, 255, 0);
        selectedItemNameUI.text = "";
        selectedItemDescriptionUI.text = "";
    }

    public void DisplaySelectedItem(Component sender, object data)
    {

        InventoryItemData itemData = (InventoryItemData) data;
        selectedItemIconUI.sprite = itemData.itemIcon;
        selectedItemIconUI.color = new Color(255, 255, 255, 255);
        selectedItemNameUI.text = itemData.displayName;
        selectedItemDescriptionUI.text = itemData.description;

        selectedItemData = itemData;
    }
    

    /// <summary>
    /// Raises event for PlayerBase to listen
    /// </summary>
    public void BuyCurrentItem()
    {
        onBuyItem.Raise(null, selectedItemData);
    }
}
