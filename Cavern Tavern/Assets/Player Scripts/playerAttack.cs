using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{
    public Rigidbody2D playerRigidbody;
    private playerMovement playerMovement;
    private float playerToCursorAngle;

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

        swordWeaponAnimator = swordWeaponReference.GetComponent<weaponAnimatorController>();
        spearWeaponAnimator = spearWeaponReference.GetComponent<weaponAnimatorController>();


    }

    void Update()
    {
        leftMouseButtonPressed();
        drawRadiusAroundPlayer();
    }

    void leftMouseButtonPressed()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack();
        }
    }


    //==============================================================================
    //  Attack Functions
    //==============================================================================

    void attack()
    {
        if (Time.time >= nextAttackAllowedTime && playerMovement.isDashing == false)
        {
            swordAttack();

            //trigger sword animator rotation
            swordWeaponAnimator.weaponAnimator.SetTrigger("Attack_1");
            
            //Instantiate slash position
            createSlash();

            //reset cooldown to x seconds after attack
            nextAttackAllowedTime = Time.time + attackCooldown;
        }
    }

    void swordAttack()
    {
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(weaponSlashPosition.transform.position, weaponReach, enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyBase>().hurt(damage, knockbackPower, (Vector2)transform.position); //calls damage function on every enemy within attack range
        }

    }

    void createSlash()
    {
        //rotations
        weaponSlashPosition.transform.rotation = mainWeaponReference.transform.rotation;

        //create prefab
        GameObject clone = (GameObject)Instantiate(swordSlashAnimation, weaponSlashPosition.transform.position, weaponSlashPosition.transform.rotation);
    }

    void drawRadiusAroundPlayer() //IMPORTANT!!! ALLOWS THE THE COVERTION OF POLAR COORDINATES TO BECOME RECTANGULAR
    {
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; playerToCursorVector.Normalize();
        playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x); //player to mouse angle in radians

        weaponSlashPosition.transform.position = (Vector2)transform.position + new Vector2(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle)); //sets postion to the edge of line (pointing at mouse)

        Debug.DrawRay(transform.position, new Vector3(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle)), Color.cyan);
    }

    void OnDrawGizmosSelected() //displays the area in unity of attack range
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere((Vector2)transform.position + new Vector2(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle)), weaponReach); //draws sword attack radius
    }

}
