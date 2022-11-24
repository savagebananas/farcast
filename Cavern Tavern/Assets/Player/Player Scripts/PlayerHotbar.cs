using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotbar : MonoBehaviour
{
    /*
    Equips specific item from hotbar based on number pressed (1-5)
    */

    public GameObject currentItem;
    public List<InventorySlot> hotbarSlots;

    //Item Position References
    [SerializeField] private GameObject swordWeaponReference;
    [SerializeField] private GameObject rangedWeaponReference;
    [SerializeField] private GameObject consumableReference;

    private int lastKeyPressed;

    private void Start()
    {
        hotbarSlots = GameObject.Find("PlayerHotbar").GetComponent<InventoryHolder>().InventorySystem.InventorySlots;

        swordWeaponReference = GameObject.Find("Sword Weapon Postion Reference");
        rangedWeaponReference = GameObject.Find("Ranged Weapon Position Reference");
        consumableReference = GameObject.Find("Consumable Position Reference");
    }

    private void Update()
    {
        HotbarInputController();
        foreach(InventorySlot slot in hotbarSlots) //if amount of item in a slot is 0, destroy currentItem and update UI
        {
            if(slot.StackSize <= 0 && slot.itemData != null)
            {
                slot.ClearSlot();
                Destroy(currentItem);
            }
        } 
    }

    void HotbarInputController()
    {
        if (Input.GetKeyDown("1"))
        {
            EquipItem(1);
            lastKeyPressed = 1;
        }
        if (Input.GetKeyDown("2"))
        {
            EquipItem(2);
            lastKeyPressed = 2;
        }
        if (Input.GetKeyDown("3"))
        {
            EquipItem(3);
            lastKeyPressed = 3;
        }
        if (Input.GetKeyDown("4"))
        {
            EquipItem(4);
            lastKeyPressed = 4;
        }
        if (Input.GetKeyDown("5"))
        {
            EquipItem(5);
            lastKeyPressed = 5;
        }
    }

    void EquipItem(int hotbarNumber)
    {
        int slotNum = hotbarNumber - 1;

        if (currentItem != null) //Item already in hand
        {
            Destroy(currentItem); //destroy previous item
            InstantiateItem(slotNum);
        }
        else
        {
            InstantiateItem(slotNum);
        }

    }

    void InstantiateItem(int slotNum)
    {
        InventoryItemData itemData = hotbarSlots[slotNum].itemData;

        if (itemData.itemType == "Sword")
        {
            currentItem = (GameObject)Instantiate(itemData.hotbarItem, swordWeaponReference.transform.position, Quaternion.Euler(0, 0, 0));
            currentItem.transform.parent = swordWeaponReference.transform;
            currentItem.transform.localScale = new Vector3(1, 1, 1);
            currentItem.GetComponent<HotbarItem>().hotbarIndex = slotNum;

        }
        else if (itemData.itemType == "Ranged")
        {
            currentItem = (GameObject)Instantiate(itemData.hotbarItem, rangedWeaponReference.transform.position, rangedWeaponReference.transform.rotation);
            currentItem.transform.parent = rangedWeaponReference.transform;
            currentItem.transform.localScale = new Vector3(1, 1, 1);
            currentItem.GetComponent<HotbarItem>().hotbarIndex = slotNum;
        }
        else if (itemData.itemType == "Consumable")
        {
            currentItem = (GameObject)Instantiate(itemData.hotbarItem, consumableReference.transform.position, Quaternion.Euler(0, 0, 0));
            currentItem.transform.parent = consumableReference.transform;
            currentItem.transform.localScale = new Vector3(1, 1, 1);
            currentItem.GetComponent<HotbarItem>().hotbarIndex = slotNum;
        }
    }
}
