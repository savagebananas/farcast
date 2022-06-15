using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
-Continually sets the weapon reference position (which controls the position of all weapons and types) to be on the player 
-Rotates weapon based on cursor position and flips the sprite if facing left (similar to the player facing cursor)
 */

public class setWeaponPosition : MonoBehaviour
{
    public GameObject player;
    public GameObject weaponRef;

    void Update()
    {
        setPosition();
        setRotation();
    }

    void setPosition()
    {
        transform.position = player.transform.position + new Vector3(0f, -0.5f, -1f);
    }

    void setRotation()
    {
        float weaponRefRotationZ = weaponRef.transform.rotation.z * Mathf.Rad2Deg;

        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        playerToCursorVector.Normalize();
        float playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0f, 0f, playerToCursorAngle);

        
        if (playerToCursorAngle < -90 || playerToCursorAngle > 90) //if cursor is on the left
        {
            transform.localRotation = Quaternion.Euler(180, 0, -playerToCursorAngle);
        }
        
    }
}
