using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShopSlot_UI : MonoBehaviour
{
    [SerializeField] private Image itemSprite;
    [SerializeField] private TextMeshProUGUI itemName;
    [SerializeField] private TextMeshProUGUI goldAmt;

    private Button button;

    private void Awake()
    {
        ClearSlot();
        button = GetComponent<Button>();
    }

    public void UpdateShopSlot(InventoryItemData itemData)
    {
        itemSprite.sprite = itemData.itemIcon;
        itemName.text = itemData.displayName;
        goldAmt.text = itemData.goldValue.ToString();
    }

    public void ClearSlot() //clears slot visually and logically
    {
        itemSprite.sprite = null;
        itemName.text = "";
        goldAmt.text = "";
    }
}
