using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordWeapon : HotbarItem
{
    [Header("General Variables")]
    [Space(5)]
    public LayerMask enemyLayer;
    private PlayerMovement playerMovement;
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
    public GameObject pivotPoint;
    public GameObject swordSlashAnimation;

    private void Awake()
    {
        mainWeaponReference = GameObject.Find("WeaponRotationReference");
    }
    void Start()
    {
        player = GameObject.Find("Player");
        playerMovement = player.GetComponent<PlayerMovement>();
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
        CreateSlash();
        pivotPoint.GetComponent<Animator>().SetTrigger("Attack_1");

        //Hurts all enemies within attack range
        Collider2D[] enemiesToDamage = Physics2D.OverlapCircleAll(SlashPosition(), weaponReach, enemyLayer);
        foreach (Collider2D enemy in enemiesToDamage) enemy.GetComponent<EnemyBase>().hurt(damage, knockbackPower, (Vector2)playerToWeaponReachVector.normalized, 1);
    }

    #region Slash Position and Rotation
    Vector2 SlashPosition()
    {
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position; playerToCursorVector.Normalize();
        playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x); //player to mouse angle in radians
        playerToWeaponReachVector = new Vector2(weaponReach * Mathf.Cos(playerToCursorAngle), weaponReach * Mathf.Sin(playerToCursorAngle));

        return (Vector2)transform.position + playerToWeaponReachVector; //sets postion to the edge of line (pointing at mouse)
    }

    private bool facingLeft;
    private bool facingRight;

    void CreateSlash()
    {
        GameObject slash = (GameObject)Instantiate(swordSlashAnimation, SlashPosition(), Quaternion.Euler(0, 0, 0)); //create sword slash

        #region slash rotation
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
            slash.transform.localRotation = Quaternion.Euler(0, 0, playerToCursorAngle);
            Vector3 tmpScale = slash.transform.localScale;
            tmpScale.y = 1;
            slash.transform.localScale = tmpScale;
        }

        if (facingLeft == true)
        {
            slash.transform.localRotation = Quaternion.Euler(0, 0, (playerToCursorAngle));

            Vector3 tmpScale = slash.transform.localScale;
            tmpScale.y = -1;
            slash.transform.localScale = tmpScale;
        }
        #endregion
    }
    #endregion

    void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(SlashPosition(), weaponReach);
    }
}
