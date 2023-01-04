using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestInventoryPanel;
    public GameObject chestInventoryUI;
    public GameObject backpackUI;

    private void Awake()
    {
        chestInventoryUI.SetActive(false);
        backpackUI.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
        InventoryHolder.OnDynamicInventoryDisplayDestroy += HideInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
        InventoryHolder.OnDynamicInventoryDisplayDestroy -= HideInventory;
    }

    void Update()
    {
        if (backpackUI.activeInHierarchy == false && Input.GetKeyDown(KeyCode.G)) //Opens Inventory UI
        {
            backpackUI.SetActive(true);
        }
        else if (backpackUI.activeInHierarchy == true && Input.GetKeyDown(KeyCode.G))//Closes Inventory UI
        {
            backpackUI.SetActive(false);
        }
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        chestInventoryUI.SetActive(true);
        backpackUI.SetActive(true);
        chestInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    void HideInventory(InventorySystem invToDisplay)
    {
        chestInventoryUI.SetActive(false);
        backpackUI.SetActive(false);
    }


}
