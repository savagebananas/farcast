using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class InventoryMouseItemData : MonoBehaviour
{
    public Image itemSprite;
    public TextMeshProUGUI itemCount;

    private void Awake()
    {
        itemSprite.color = Color.clear;
        itemCount.text = "";
    }
}
