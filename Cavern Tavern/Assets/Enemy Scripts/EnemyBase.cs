using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    public GameObject player;

    public float health;
    public float speed;

    public Rigidbody2D rigidbody;

    public float alertRange;
    public float attackRange;

    public float roamingPointRange;
    private Vector2 nextRoamPosition;
    private float timeBetweenRoams;
    private float nextRoamTime;
    private float oldPositionX;
    private bool idle;
    private bool isWalking = false;

    public Animator animator;

    void Start()
    {
        rigidbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();

        oldPositionX = transform.position.x;
    }

    void Update()
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

        //Plays walking animation
        if (isWalking == true)
        {
            walkAnimation();
        }
        //Plays Idle Animation 
        if(isWalking == false)
        {
            idleAnimation();
        }
    }

    float enemydDistanceFromPlayer()
    {
        float differenceX = player.transform.position.x - transform.position.x;
        float differenceY = player.transform.position.y - transform.position.y;
        return Mathf.Sqrt(differenceX * differenceX + differenceY * differenceX); 
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
