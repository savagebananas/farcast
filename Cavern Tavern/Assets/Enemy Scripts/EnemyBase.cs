using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    /*--------------------*/
    /*References to Player*/
    /*--------------------*/
    public GameObject player;
    private Vector2 enemyToPlayerVector;
    private float enemyToPlayerAngle;

    /*----------------*/
    /*Enemy Components*/
    /*----------------*/
    public Animator animator;
    private Rigidbody2D enemyRb;

    /*------------*/
    /*Enemy Stats*/
    /*-----------*/
    public float health;
    public float speed;
    private float activeMoveSpeed;
    public float alertRange;
    public float attackRange;

    /*-----------------*/
    /*Roaming Variables*/
    /*-----------------*/
    public float roamingPointRange;
    private Vector2 nextRoamPosition;
    private float timeBetweenRoams;
    private float nextRoamTime;
    private float oldPositionX;
    private bool idle;
    private bool isWalking = false;

    /*----------------------*/
    /*Hurt and Dead Booleans*/
    /*----------------------*/
    private bool isHurt = false;
    private bool isDead = false;



    /*----------------*/
    /*Knockback Values*/
    /*----------------*/
    public float knockbackDuration; //aka knockback resistance
    public float knockbackDistance;
    private Vector2 knockbackDifference;

    void Start()
    {
        animator = GetComponent<Animator>();
        enemyRb = GetComponent<Rigidbody2D>();

        oldPositionX = transform.position.x;

        activeMoveSpeed = speed;
    }

    void Update()
    {
        Vector3 enemyToPlayerVector = player.transform.position - transform.position;
        enemyToPlayerVector.Normalize();
        enemyToPlayerAngle = Mathf.Atan2(enemyToPlayerVector.y, enemyToPlayerVector.x); //player to mouse angle in radians

        knockbackDifference = (Vector2)transform.position - (Vector2)(player.GetComponent<playerAttack>().weaponSlashPosition.transform.position) * (-0.25f);
        knockbackDifference = knockbackDifference.normalized * knockbackDistance;
    }

    void FixedUpdate()
    {
        if (!isHurt)
        {
            faceDirection();

            if (enemydDistanceFromPlayer() <= alertRange)
            {
                followPlayer();

            }
            else if (enemydDistanceFromPlayer() > alertRange)
            {
                wanderAround();
            }
        }

        //Plays walking animation
        if (isWalking == true)
        {
            walkAnimation();
        }
        //Plays Idle Animation 
        if (isWalking == false)
        {
            idleAnimation();
        }

    }

    float enemydDistanceFromPlayer()
    {
        float differenceX = player.transform.position.x - transform.position.x;
        float differenceY = player.transform.position.y - transform.position.y;
        return Mathf.Sqrt(differenceX * differenceX + differenceY * differenceY); 
    }

    //==============================================================================
    //  FACE DIRECTION 
    //==============================================================================
    void faceDirection()
    {
        if (transform.position.x > oldPositionX) // moving right
        {
            transform.rotation = Quaternion.identity;
        }
        if (transform.position.x < oldPositionX) //moving left
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
        }

        oldPositionX = transform.position.x;
    }

    //==============================================================================
    //  Enemy Behaviors
    //==============================================================================

    void followPlayer()
    {
        isWalking = true;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //Vector2 temp = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        //GetComponent<Rigidbody2D>().MovePosition(temp - (Vector2)transform.position);
    }

    void wanderAround()
    {
        timeBetweenRoams = Random.Range(4f, 8f);
        float randomX = Random.Range(-roamingPointRange, roamingPointRange);
        float randomY = Random.Range(-roamingPointRange, roamingPointRange);

        if ((Vector2)transform.position == nextRoamPosition)
        {
            isWalking = false;
        }

        if (!isWalking && !idle) //when stopped walking (arrived at position), start idling
        {
            StartCoroutine(roamingCooldown());
            SearchWalkPoint();
        }
        else //when idle is false, start walking
        {
            transform.position = Vector2.MoveTowards(transform.position, nextRoamPosition, speed * Time.deltaTime);
        }
    }


    private void SearchWalkPoint()
    {
        isWalking = true;
        float randomX = Random.Range(-roamingPointRange, roamingPointRange);
        float randomY = Random.Range(-roamingPointRange, roamingPointRange);
        nextRoamPosition = new Vector2(transform.position.x + randomX, transform.position.y + randomY);
    }

    private IEnumerator roamingCooldown()
    {
        idle = true;
        yield return new WaitForSeconds(timeBetweenRoams);
        idle = false;
    }

    //==============================================================================
    //  Enemy Hurt / Dead (Overrides other states)
    //==============================================================================

    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderPosition)
    {
        isHurt = true;
        health -= damage;
        if (health > 0)
        {
            knockback(1f);
            //animator.SetTrigger("hurt");
            //idleAnimation();
        }
        else
        {
            Debug.Log("Enemy Dead");
            isDead = true;
            //animator.SetTrigger("dead");
            knockback(5f);
        }
    }

    void knockback(float multiplier)
    {
        GetComponent<Rigidbody2D>().AddForce(player.GetComponent<playerAttack>().playerToWeaponReachVector.normalized * knockbackDistance * multiplier, ForceMode2D.Impulse);
        StartCoroutine(knockbackCo());
        StartCoroutine(hurtCooldown());
    }

    private IEnumerator knockbackCo()
    {

        yield return new WaitForSeconds(knockbackDuration);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private IEnumerator hurtCooldown()
    {
        yield return new WaitForSeconds(1);
        isHurt = false;
    }

    //==============================================================================
    //  Enemy Animation States
    //==============================================================================

    void walkAnimation()
    {
        animator.SetTrigger("isWalking");
    }

    void idleAnimation()
    {
        animator.SetTrigger("isIdle");
    }

    //==============================================================================
    //  Drawing Radius
    //==============================================================================

    void OnDrawGizmosSelected() //displays the area in of attack range
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);

    }
}
