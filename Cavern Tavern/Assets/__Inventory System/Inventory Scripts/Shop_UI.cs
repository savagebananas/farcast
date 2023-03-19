using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_UI : MonoBehaviour
{
    public Shop shop;
    public ShopSlot_UI[] itemUISlots;


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
        }

    }

    private void ClearAll()
    {
        for (int i = 0; i < itemUISlots.Length; i++)
        {
            itemUISlots[i].ClearSlot();
        }
    }
}
