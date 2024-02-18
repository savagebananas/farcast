using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestInventoryPanel;
    public GameObject chestInventoryUI;
    public GameObject backpackUI;
    public GameObject shopUI;

    private void Awake()
    {
        chestInventoryUI.SetActive(false);
        backpackUI.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayChestInventory;
        InventoryHolder.OnDynamicInventoryDisplayDestroy += HideAllInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayChestInventory;
        InventoryHolder.OnDynamicInventoryDisplayDestroy -= HideAllInventory;
    }

    /// <summary>
    /// Displays Inventory UI
    /// </summary>
    public void DisplayBackpack()
    {
        backpackUI.SetActive(true);
    }

    /// <summary>
    /// Hides Inventory UI
    /// </summary>
    public void HideBackpack()
    {
        backpackUI.SetActive(false);
    }

    public void DisplayChestInventory(InventorySystem invToDisplay)
    {
        chestInventoryUI.SetActive(true);
        backpackUI.SetActive(true);
        chestInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    public void HideAllInventory(InventorySystem invToDisplay)
    {
        chestInventoryUI.SetActive(false);
        backpackUI.SetActive(false);
    }


}
