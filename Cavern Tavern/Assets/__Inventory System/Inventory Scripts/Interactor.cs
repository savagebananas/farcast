using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private GameObject currentInteractable;
    public float interactionPointRadius = 1f;
    [SerializeField] private bool isInteracting = false;
    [SerializeField] private bool openedInventory = false;
    public bool canExitInteraction;

    private InventoryUIController inventoryUIController;
    private PlayerMovement playerMovement;



    private void Start()
    {
        inventoryUIController = GameObject.FindAnyObjectByType<Canvas>().GetComponentInChildren<InventoryUIController>();
        playerMovement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        InteractionInput();
    }

    void StartInteraction(IInteractable interactable)
    {
        interactable.Interact(this, out bool interactSuccessful);
        isInteracting = true;
    }

    void EndInteraction(IInteractable interactable)
    {
        interactable.EndInteraction(this, out bool interactSuccessful);
        isInteracting = false;
    }

    void InteractionInput()
    {
        // Open Inventory
        if (Input.GetKeyDown(KeyCode.G) && !isInteracting && !openedInventory)
        {
            inventoryUIController.DisplayBackpack();
            playerMovement.DisableMovement();
            isInteracting = true;
            openedInventory = true;
        }

        // Close Inventory
        else if (Input.GetKeyDown(KeyCode.G) && isInteracting && openedInventory && canExitInteraction)
        {
            inventoryUIController.HideBackpack();
            playerMovement.EnableMovement();
            isInteracting = false;
            openedInventory = false;
        }

        if (FindNearestInteractable() != null)
        {
            // interacts if button pressed and player is not already interacting
            if (Input.GetKeyDown(KeyCode.E) && isInteracting == false) 
            {
                StartInteraction(FindNearestInteractable().GetComponent<IInteractable>());
                playerMovement.DisableMovement();
                isInteracting = true;
            }

            // exits interaction using the same button (unless inventory is open, close with KeyCode.G)
            else if (Input.GetKeyDown(KeyCode.E) && isInteracting && !openedInventory && canExitInteraction) 
            {
                EndInteraction(FindNearestInteractable().GetComponent<IInteractable>());
                playerMovement.EnableMovement();
                isInteracting = false;
            }
        }
    }

    GameObject FindNearestInteractable() //Returns the nearest interactable (excluding items)
    {
        var colliders = Physics2D.OverlapCircleAll(transform.position, interactionPointRadius);
        var nearestDistance = float.MaxValue;
        GameObject nearestInteractable = null;

        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i].gameObject.tag == "Interactable" || colliders[i].gameObject.layer == 7)
            {
                if (Vector2.Distance(transform.position, colliders[i].transform.position) < nearestDistance)
                {
                    nearestDistance = Vector2.Distance(transform.position, colliders[i].gameObject.transform.position);
                    nearestInteractable = colliders[i].gameObject;
                }
            }
        }
        return nearestInteractable;
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, interactionPointRadius);
    }
}
