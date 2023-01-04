using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-Continually sets the weapon reference position (which controls the position of most weapons and types) to be on the player 
-Rotates weapon based on cursor position and flips the sprite if facing left (similar to the player facing cursor)
IMPORTANT: RANGED WEAPONS WILL NOT BE FLIPPING
 */

public class setWeaponPosition : MonoBehaviour
{
    public GameObject player;
    private bool facingRight;
    private bool facingLeft;

    void Update()
    {
        setPosition();
        setRotation();
    }

    void setPosition()
    {
        transform.position = player.transform.position + new Vector3(0f, -0.75f, -1f);
    }

    void setRotation()
    {
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        playerToCursorVector.Normalize();
        float playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x) * Mathf.Rad2Deg;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        if (mousePos.x < transform.position.x)
        {
            facingRight = false;
            facingLeft = true;
        }
        if (mousePos.x > transform.position.x)
        {
            facingRight = true;
            facingLeft = false;
        }

        if (facingRight == true)
        {
            transform.localRotation = Quaternion.Euler(0, 0, playerToCursorAngle);
            Vector3 tmpScale = transform.localScale;
            tmpScale.y = 1;
            transform.localScale = tmpScale;
        }
        
        if (facingLeft == true)
        {
            transform.localRotation = Quaternion.Euler(0, 0, (playerToCursorAngle));
            
            Vector3 tmpScale = transform.localScale;
            tmpScale.y = -1;
            transform.localScale = tmpScale;
        }
    }

}
