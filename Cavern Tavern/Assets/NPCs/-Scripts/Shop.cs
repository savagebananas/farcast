using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour, IInteractable
{
    public InventoryItemData[] shopItems;
    
    [Header("Events")]
    public GameEvent openShopUI;

    [Header("UI")]
    private GameObject canvas;
    private GameObject shopUI;
    public GameObject shopSlotPrefab;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        shopUI = ObjectFinder.FindObject(canvas, "Shop UI");
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        interactSuccessful = true;
        shopUI.SetActive(true);
        openShopUI.Raise(this, shopItems);

        //UpdateShopUI();
    }

    public void EndInteraction(Interactor interactor, out bool interactSuccessful)
    {
        interactSuccessful = true;
        shopUI.SetActive(false);
    }
}
