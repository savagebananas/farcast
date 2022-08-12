using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHotbar : MonoBehaviour
{
    public GameObject currentItem;
    public List<InventorySlot> hotbarSlots;

    [SerializeField] private GameObject swordWeaponReference;
    [SerializeField] private GameObject rangedWeaponReference;

    private int lastKeyPressed;

    private void Start()
    {
        hotbarSlots = GameObject.Find("PlayerHotbar").GetComponent<InventoryHolder>().InventorySystem.InventorySlots;

        swordWeaponReference = GameObject.Find("Sword Weapon Postion Reference");
        rangedWeaponReference = GameObject.Find("Ranged Weapon Position Reference");
    }

    private void Update()
    {
        SetEquippedItem();
    }

    void SetEquippedItem()
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
        if (hotbarSlots[slotNum].itemData.itemType == "Sword")
        {
            currentItem = (GameObject)Instantiate(hotbarSlots[slotNum].itemData.hotbarItem, swordWeaponReference.transform.position, Quaternion.Euler(0, 0, 0));
            currentItem.transform.parent = swordWeaponReference.transform;
            currentItem.transform.localScale = new Vector3(1, 1, 1);
            
        }
        else if (hotbarSlots[slotNum].itemData.itemType == "Ranged")
        {
            currentItem = (GameObject)Instantiate(hotbarSlots[slotNum].itemData.hotbarItem, rangedWeaponReference.transform.position, rangedWeaponReference.transform.rotation);
            currentItem.transform.parent = rangedWeaponReference.transform;
            currentItem.transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
