using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUIController : MonoBehaviour
{
    public DynamicInventoryDisplay chestInventoryPanel;
    public GameObject chestInventoryUI;
    public GameObject backpackUi;

    private void Awake()
    {
        chestInventoryUI.SetActive(false);
        backpackUi.SetActive(false);
    }

    private void OnEnable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested += DisplayInventory;
    }

    private void OnDisable()
    {
        InventoryHolder.OnDynamicInventoryDisplayRequested -= DisplayInventory;
    }

    void Update()
    {
        if (chestInventoryPanel.gameObject.activeInHierarchy && Input.GetKeyDown(KeyCode.G)) chestInventoryPanel.gameObject.SetActive(false);
    }

    void DisplayInventory(InventorySystem invToDisplay)
    {
        chestInventoryUI.SetActive(true);
        backpackUi.SetActive(true);
        chestInventoryPanel.RefreshDynamicInventory(invToDisplay);
    }

    public void HideInventory()
    {
        chestInventoryUI.SetActive(false);
        backpackUi.SetActive(false);
    }


}
