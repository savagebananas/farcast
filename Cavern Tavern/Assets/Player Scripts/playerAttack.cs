using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Weapon currentWeapon;

    void Update()
    {
        leftMouseButtonPressed();
    }

    void leftMouseButtonPressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            currentWeapon.Attack();
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
