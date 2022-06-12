using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : StateMachineManager
{
    [Header("ReferencesToPlayer")]
    [Space(5)]

    public GameObject player;
    private Vector2 enemyToPlayerVector;
    private float enemyToPlayerAngle;

    [Header("Enemy State Classes")]
    [Space(5)]

    public EnemyState WanderState;
    public EnemyState AttackState;
    public EnemyState EnemyHurtState;
    public EnemyState WaitAfterAttack;

    [Header("Enemy Components")]
    [Space(5)]

    [HideInInspector]  public Animator animator;
    private Rigidbody2D enemyRb;

    [Header("Enemy Stats")]
    [Space(5)]

    public float health;
    public float speed;
    private float activeMoveSpeed;
    public float alertRange;
    public float attackRange;

    [Header("Roaming Variables")]
    [Space(5)]

    public float roamingPointRange;
    private Vector2 nextRoamPosition;
    private float timeBetweenRoams;
    
    private bool idle;
    private bool isWalking = false;

    [Header("Hurt and Dead Booleans")]
    [Space(5)]
    private bool isHurt = false;
    private bool isDead = false;

    [Header("Knockback Values")]
    [Space(5)]
    public float knockbackDuration; 
    public float knockbackDistance;
    private Vector2 knockbackDifference;

    //for calculating where to face
    private float oldPositionX;

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

    #region Face Direction Of Movement
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

    #endregion

    #region FollowPlayer

    void followPlayer()
    {
        isWalking = true;
        transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
    }
    #endregion

    #region WanderAround
    void wanderAround()
    {
        timeBetweenRoams = Random.Range(4f, 8f);

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
        //yield return new WaitUntil(() => boolean)
        idle = false;
    }

    #endregion

    #region Enemy Hurt

    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderToEnemyVector)
    {
        SetState(EnemyHurtState);
        isHurt = true;
        health -= damage;
        if (health > 0)
        {
            knockback(knockbackPower);
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

    void knockback(float power)
    {
        GetComponent<Rigidbody2D>().AddForce(player.GetComponent<playerAttack>().playerToWeaponReachVector.normalized * knockbackDistance * power, ForceMode2D.Impulse);
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

    #endregion

    #region Enemy Animation Triggers

    void walkAnimation()
    {
        animator.SetTrigger("isWalking");
    }

    void idleAnimation()
    {
        animator.SetTrigger("isIdle");
    }

    #endregion

    #region Debug Circle

    void OnDrawGizmosSelected() //displays the area in of attack range
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
    #endregion
}
