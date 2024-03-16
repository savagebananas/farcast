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
    public InventorySlot equippedItemSlot;

    //Item Position References
    [SerializeField] private GameObject swordWeaponReference;
    [SerializeField] private GameObject rangedWeaponReference;
    [SerializeField] private GameObject consumableReference;

    public int lastKeyPressed;
    private int equipedItemNumber;

    private void Start()
    {
        hotbarSlots = GameObject.Find("PlayerHotbar").GetComponent<InventoryHolder>().InventorySystem.InventorySlots;

        swordWeaponReference = GameObject.Find("Sword Weapon Postion Reference");
        rangedWeaponReference = GameObject.Find("Ranged Weapon Position Reference");
        consumableReference = GameObject.Find("Consumable Position Reference");
    }

    private void Update()
    {   
        if(TimeManager.isPaused == false) HotbarInputController();

        for(int i = 0; i < hotbarSlots.Count; i++) //updates equiped item number
        { 
            if (i == lastKeyPressed - 1)
            {
                equipedItemNumber = i;
            }
        }

        if (hotbarSlots[equipedItemNumber].itemData == null || hotbarSlots[equipedItemNumber].StackSize <= 0)
        {
            hotbarSlots[equipedItemNumber].ClearSlot();
            Destroy(currentItem);
        }
    }



    void HotbarInputController()
    {
        if (Input.GetKeyDown("1") && lastKeyPressed != 1)
        {
            EquipItem(1);
            lastKeyPressed = 1;
        }
        if (Input.GetKeyDown("2") && lastKeyPressed != 2)
        {
            EquipItem(2);
            lastKeyPressed = 2;
        }
        if (Input.GetKeyDown("3") && lastKeyPressed != 3)
        {
            EquipItem(3);
            lastKeyPressed = 3;
        }
        if (Input.GetKeyDown("4") && lastKeyPressed != 4)
        {
            EquipItem(4);
            lastKeyPressed = 4;
        }
        if (Input.GetKeyDown("5") && lastKeyPressed != 5)
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
            equippedItemSlot = hotbarSlots[slotNum];
            InstantiateItem(slotNum);
        }
        else
        {
            InstantiateItem(slotNum);
            equippedItemSlot = hotbarSlots[slotNum];
        }

    }

    void InstantiateItem(int slotNum)
    {
        InventoryItemData itemData = hotbarSlots[slotNum].itemData;

        if (itemData.itemType == "Sword")
        {
            currentItem = (GameObject)Instantiate(itemData.hotbarItem, swordWeaponReference.transform.position, swordWeaponReference.transform.rotation);
            currentItem.transform.parent = swordWeaponReference.transform;
            currentItem.transform.localScale = new Vector3(1, 1, 1);
            //currentItem.GetComponentInChildren<HotbarItem>().hotbarIndex = slotNum;

        }
        else if (itemData.itemType == "Ranged")
        {
            currentItem = (GameObject)Instantiate(itemData.hotbarItem, rangedWeaponReference.transform.position, rangedWeaponReference.transform.rotation);
            currentItem.transform.parent = rangedWeaponReference.transform;
            currentItem.transform.localScale = new Vector3(1, 1, 1);
            //currentItem.GetComponentInChildren<HotbarItem>().hotbarIndex = slotNum;
        }
        else if (itemData.itemType == "Consumable")
        {
            currentItem = (GameObject)Instantiate(itemData.hotbarItem, consumableReference.transform.position, Quaternion.Euler(0, 0, 0));
            currentItem.transform.parent = consumableReference.transform;
            currentItem.transform.localScale = new Vector3(1, 1, 1);
            //currentItem.GetComponent<HotbarItem>().hotbarIndex = slotNum;
        }
    }
}
