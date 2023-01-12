using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    public LayerMask interactionLayer;
    public float interactionPointRadius = 1f;
    public bool isInteracting = false;

    private void Update()
    {
        var colliders = Physics2D.OverlapCircleAll(this.transform.position, interactionPointRadius);
        
        if (Input.GetKeyDown(KeyCode.E) && isInteracting == false) //interacts if button pressed and player is not already interacting
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null) 
                {
                    isInteracting = true;
                    StartInteraction(interactable);
                } 
            }
        }

        else if (Input.GetKeyDown(KeyCode.E) && isInteracting == true) //Exits the interaction using the same button
        {
            for (int i = 0; i < colliders.Length; i++)
            {
                var interactable = colliders[i].GetComponent<IInteractable>();
                if (interactable != null)
                {
                    isInteracting = false;
                    EndInteraction(interactable);
                }
            }
        }
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(this.transform.position, interactionPointRadius);
    }
}
