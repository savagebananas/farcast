using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Weapon currentWeapon;

    public Rigidbody2D playerRigidbody;
    private playerMovement playerMovement;


    void Start()
    {

    }

    void Update()
    {
        leftMouseButtonPressed();
        //lineFacingMouse();
    }

    void leftMouseButtonPressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Attack();
            //attack();
        }
    }

    /*
    void OnDrawGizmosSelected() //displays the area in unity of attack range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle)), weaponReach); //draws sword attack radius
    }
    */

}
