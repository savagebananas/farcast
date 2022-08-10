using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : HotbarItem
{
    [Header("General Variables")]
    [Space(5)]
    public GameObject player;
    public LayerMask enemyLayer;
    private Rigidbody2D playerRigidbody;
    private playerMovement playerMovement;
    [HideInInspector] public float playerToCursorAngle;
    [HideInInspector] public Vector2 playerToWeaponReachVector;

    public float damage;
    public float weaponReach;
    public float knockbackPower;
    public float attackCooldown;
    float nextAttackAllowedTime = 0;

    [Header("Attack Animation Variables")]
    [Space(5)]

    public GameObject mainWeaponReference;
    public GameObject swordWeaponReference;
    public GameObject swordSlashAnimation;

    void Start()
    {

        player = GameObject.Find("Player");
        playerRigidbody = player.GetComponent<Rigidbody2D>();
        playerMovement = player.GetComponent<playerMovement>();

        mainWeaponReference = GameObject.Find("WeaponRotationReference");
        swordWeaponReference = GameObject.Find("Sword Weapon Postion Reference");
    } 
    
    public override void UseItem()
    {
        if (Time.time >= nextAttackAllowedTime && playerMovement.isDashing == false)
        {
            swordAttack();

            nextAttackAllowedTime = Time.time + attackCooldown; //reset cooldown to x seconds after attack
        }
    }

    void swordAttack()
    {
        //animations
        GameObject clone = (GameObject)Instantiate(swordSlashAnimation, UpdateSlashPosition(), mainWeaponReference.transform.rotation); //create sword slash
        swordWeaponReference.GetComponent<Animator>().SetTrigger("Attack_1");

        //Hurts all enemies within attack range
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(UpdateSlashPosition(), weaponReach, enemyLayer);
        for (int i = 0; i < enemiesToDamage.Length; i++) {enemiesToDamage[i].GetComponent<EnemyBase>().hurt(damage, knockbackPower, (Vector2)playerToWeaponReachVector.normalized);}
    }

    Vector2 UpdateSlashPosition()
    {
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; playerToCursorVector.Normalize();
        playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x); //player to mouse angle in radians
        playerToWeaponReachVector = new Vector2(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle));

        return (Vector2)transform.position + playerToWeaponReachVector; //sets postion to the edge of line (pointing at mouse)
    }
}
