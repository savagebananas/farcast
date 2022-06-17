using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : Weapon
{
    public GameObject player;

    private Rigidbody2D playerRigidbody;
    private playerMovement playerMovement;
    [HideInInspector] public float playerToCursorAngle;
    [HideInInspector] public Vector2 playerToWeaponReachVector;

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
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<playerMovement>();
    }

    void Update()
    {
        lineFacingMouse();
    }    
    
    public override void Attack()
    {
        if (Time.time >= nextAttackAllowedTime && playerMovement.isDashing == false)
        {
            swordAttack();

            nextAttackAllowedTime = Time.time + attackCooldown; //reset cooldown to x seconds after attack
        }
    }

    void swordAttack()
    {
        createSwordSlash();
        swordWeaponAnimator.weaponAnimator.SetTrigger("Attack_1");

        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(weaponSlashPosition.transform.position, weaponReach, enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++)
        {
            enemiesToDamage[i].GetComponent<EnemyBase>().hurt(damage, knockbackPower, (Vector2)playerToWeaponReachVector.normalized); //calls damage function on every enemy within attack range
        }
    }
    void createSwordSlash()
    {
        //rotations
        weaponSlashPosition.transform.rotation = mainWeaponReference.transform.rotation;

        //create prefab
        GameObject clone = (GameObject)Instantiate(swordSlashAnimation, weaponSlashPosition.transform.position, weaponSlashPosition.transform.rotation);
    }

    void lineFacingMouse() //IMPORTANT!!! ALLOWS THE THE COVERTION OF POLAR COORDINATES TO BECOME RECTANGULAR
    {
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; playerToCursorVector.Normalize();
        playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x); //player to mouse angle in radians
        playerToWeaponReachVector = new Vector2(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle));

        weaponSlashPosition.transform.position = (Vector2)transform.position + playerToWeaponReachVector; //sets postion to the edge of line (pointing at mouse)
    }
}
