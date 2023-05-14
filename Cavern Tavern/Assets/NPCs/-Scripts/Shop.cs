using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : DialogueTrigger, IInteractable
{
    public InventoryItemData[] shopItems;
    
    [Header("Events")]
    public GameEvent openShopUI;

    [Header("UI")]
    private GameObject canvas;
    private GameObject shopUI;
    public GameObject shopSlotPrefab;

    //Interactor
    Interactor intr;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        shopUI = ObjectFinder.FindObject(canvas, "Shop UI");
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        interactSuccessful = true;
        intr = interactor;
        intr.canExitInteraction = false; //doesn't allow player to trigger interaction repeatedly (while still in current interaction)
        TriggerDialogue();

    }

    public void EndInteraction(Interactor interactor, out bool interactSuccessful)
    {
        interactSuccessful = true;
        shopUI.SetActive(false);
    }

    public override void EndDialogueEvent() //When dialogue ends, open shop ui
    {
        shopUI.SetActive(true);
        openShopUI.Raise(this, shopItems);
        intr.canExitInteraction = true; //allows player to exit shop
    }
}
