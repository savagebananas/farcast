using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
Sets order of sprite for a top down pixel art game
Objects on a lower y-value will be in front (higher order in layer)
Items that the player is holding will have the same order number as player but +1
*/

public class SetOrderInLayer : MonoBehaviour
{
    public Renderer renderer;
    public float offset;
    private GameObject referenceObject;
    public bool attachedToPlayer;

    private void Start()
    {
        if (attachedToPlayer)
        {
            referenceObject = GameObject.Find("Player Sprite");
        }
        if(renderer == null) //temporary fix to add particle renderers
        {
            renderer = gameObject.GetComponent<ParticleSystem>().GetComponent<Renderer>();
        }    
    }

    private void LateUpdate()
    {
        if (referenceObject != null)
        {
            renderer.sortingOrder = referenceObject.GetComponent<SpriteRenderer>().sortingOrder + 10;
        }
        else
        {
            renderer.sortingOrder = -(int)((transform.position.y + offset) * 100);
        }
        
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Gizmos.DrawSphere(new Vector3(transform.position.x, transform.position.y + offset, 0), 0.1f);
    }
}
