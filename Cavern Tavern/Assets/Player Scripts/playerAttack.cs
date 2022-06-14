using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    private PlayerBase playerBase; //temp
    private playerMovement playerMovement;
    public float playerToCursorAngle;
    public Vector2 playerToWeaponReachVector;

    public float damage;
    public float weaponReach;
    public float knockbackPower;
    public float attackCooldown;
    float nextAttackAllowedTime = 0;

    /*---------------------------*/
    /*Attacking Enemies Variables*/
    /*---------------------------*/

    public LayerMask enemyLayer;

    /*--------------------------*/
    /*Attack Animation Variables*/
    /*--------------------------*/

    public GameObject mainWeaponReference;
    public GameObject weaponSlashPosition;

    //General Weapon type references w/ animator attached to it
    public GameObject swordWeaponReference;
    public GameObject spearWeaponReference;

    //General Weapon Animators to trigger (ex: rotate weapon to look like sword swing)
    public weaponAnimatorController swordWeaponAnimator;
    public weaponAnimatorController spearWeaponAnimator;

    /*---------------------------------------*/
    /*Instantiating slash animation variables*/
    /*---------------------------------------*/
    public GameObject swordSlashAnimation;
    

    void Start()
    {
        playerMovement = gameObject.GetComponent<playerMovement>();
        playerBase = GetComponent<PlayerBase>(); //temp
        swordWeaponAnimator = swordWeaponReference.GetComponent<weaponAnimatorController>();
        spearWeaponAnimator = spearWeaponReference.GetComponent<weaponAnimatorController>();
    }

    void Update()
    {
        leftMouseButtonPressed();
        lineFacingMouse();
    }

    void leftMouseButtonPressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
    }

    void attack()
    {
        if (Time.time >= nextAttackAllowedTime && playerMovement.isDashing == false)
        {
            swordAttack();
            
            nextAttackAllowedTime = Time.time + attackCooldown; //reset cooldown to x seconds after attack
        }
    }

    #region Sword Attack
    void swordAttack()
    {
        createSwordSlash();
        swordWeaponAnimator.weaponAnimator.SetTrigger("Attack_1");

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(weaponSlashPosition.transform.position, weaponReach, enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyBase>().hurt(damage, knockbackPower, (Vector2)playerToWeaponReachVector.normalized); //calls damage function on every enemy within attack range
        }
        //playerBase.hurt(damage, knockbackPower, (Vector2)playerToWeaponReachVector.normalized); <--test to check player knockback (which is broken)
    }

    void createSwordSlash()
    {
        //rotations
        weaponSlashPosition.transform.rotation = mainWeaponReference.transform.rotation;

        //create prefab
        GameObject clone = (GameObject)Instantiate(swordSlashAnimation, weaponSlashPosition.transform.position, weaponSlashPosition.transform.rotation);
    }
    #endregion

    void lineFacingMouse() //IMPORTANT!!! ALLOWS THE THE COVERTION OF POLAR COORDINATES TO BECOME RECTANGULAR
    {
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; playerToCursorVector.Normalize();
        playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x); //player to mouse angle in radians
        playerToWeaponReachVector = new Vector2(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle));

        weaponSlashPosition.transform.position = (Vector2)transform.position + playerToWeaponReachVector; //sets postion to the edge of line (pointing at mouse)

        Debug.DrawRay(transform.position, playerToWeaponReachVector, Color.cyan);
        Debug.DrawRay(transform.position, playerToWeaponReachVector * (-0.5f), Color.blue);
    }

    void OnDrawGizmosSelected() //displays the area in unity of attack range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle)), weaponReach); //draws sword attack radius
    }

}
