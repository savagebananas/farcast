using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : InventoryHolder, IInteractable
{
    public UnityAction<IInteractable> OnInteractionComplete { get; set; }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        Debug.Log("open shop");
        interactSuccessful = true;
    }

    public void EndInteraction(Interactor interactor, out bool interactSuccessful)
    {
        Debug.Log("close shop");
        interactSuccessful = true;
    }


}
