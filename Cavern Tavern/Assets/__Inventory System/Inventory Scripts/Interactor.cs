using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    private GameObject currentInteractable;

    public float interactionPointRadius = 1f;
    public bool isInteracting = false;

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
        if (FindNearestInteractable() != null)
        {
            if (Input.GetKeyDown(KeyCode.E) && isInteracting == false) //interacts if button pressed and player is not already interacting
            {
                StartInteraction(FindNearestInteractable().GetComponent<IInteractable>());
                isInteracting = true;
            }

            else if (Input.GetKeyDown(KeyCode.E) && isInteracting == true) //Exits the interaction using the same button
            {
                EndInteraction(FindNearestInteractable().GetComponent<IInteractable>());
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
