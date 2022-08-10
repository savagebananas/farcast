using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
This is a scriptable object that defines the items in the game
Can be inherited to have branched version of items, such as potions and equipment
*/

[CreateAssetMenu(menuName = "Inventory System/Inventory Item")]
public class InventoryItemData : ScriptableObject
{
    public int id;
    public string displayName;
    [TextArea(4, 4)]
    public string description;
    public string itemType;
    public Sprite itemIcon;
    public int maxStackSize;
    public int goldValue;

    public GameObject hotbarItem;
}
