using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : State
{
    public EnemyBase enemyBase;
    public State followState;
    public Transform enemyTransform;

    public bool randomRoam;

    private float speed;
    private float alertRange;
    private float enemyDistanceToPlayer;

    [Header("Roaming Variables")]
    [Space(5)]
    public float roamingPointRange;
    public Transform[] patrolPoints;
    private Vector2 nextRoamPosition;
    private float timeBetweenRoams;
    int currentPointIndex;

    bool once = false;
    bool once2 = false;

    public override void OnStart()
    {
        speed = enemyBase.speed;
        alertRange = enemyBase.alertRange;

        SearchWalkPoint();
    }

    public override void OnUpdate()
    {
        enemyDistanceToPlayer = enemyBase.enemydDistanceFromPlayer();

        if (enemyDistanceToPlayer >= alertRange) //enemy doesn't detect player
        {
            if(randomRoam == true)
            {
                Roam();
            }
            if(randomRoam == false)
            {
                Patrol();
            }
            
        }
        else if (enemyDistanceToPlayer <= alertRange) //enemy detects player
        {
            stateMachineManager.setNewState(followState);
        }
    }

    public override void OnLateUpdate()
    {
    }

    #region Patrolling To Set Points
    void Patrol()
    {
        if (patrolPoints.Length > 0)
        {
            if ((Vector2)enemyBase.transform.position != (Vector2)patrolPoints[currentPointIndex].position)
            {
                moveTowardsPatrolPoint();
            }
            else
            {
                animator.SetTrigger("isIdle");
                if (once == false)
                {
                    once = true;
                    StartCoroutine(patrolCooldown());
                }
            }
        }
        else
        {
            Debug.Log("Cannot Find Any Patrol Points!");
        }

    }

    void moveTowardsPatrolPoint()
    {
        animator.SetTrigger("isWalking");
        enemyBase.transform.position = Vector2.MoveTowards(enemyBase.transform.position, patrolPoints[currentPointIndex].position, speed * Time.deltaTime);

        if (enemyBase.transform.position.x < patrolPoints[currentPointIndex].position.x) //moving right
        {
            enemyTransform.rotation = Quaternion.identity;
        }
        if (enemyBase.transform.position.x > patrolPoints[currentPointIndex].position.x) //moving left
        {
            enemyTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }



    private IEnumerator patrolCooldown()
    {
        timeBetweenRoams = Random.Range(3f, 6f);
        yield return new WaitForSeconds(timeBetweenRoams);
        if (currentPointIndex + 1 < patrolPoints.Length)
        {
            currentPointIndex++;
        }
        else
        {
            currentPointIndex = 0;
        }
        once = false; 
    }
    #endregion

    #region Roaming To Random Points
    void Roam()
    {
        if ((Vector2)enemyBase.transform.position != (Vector2)nextRoamPosition) //if not at next roam position
        {
            moveTowardsRoamPoint();
        }
        else
        {
            animator.SetTrigger("isIdle");
            if (once2 == false)
            {
                once2 = true;
                
                StartCoroutine(roamingCooldown());
            }
        }
    }

    void moveTowardsRoamPoint()
    {
        animator.SetTrigger("isWalking");
        enemyBase.transform.position = Vector2.MoveTowards(enemyBase.transform.position, nextRoamPosition, speed * Time.deltaTime);

        if (enemyBase.transform.position.x < nextRoamPosition.x) //moving right
        {
            enemyTransform.rotation = Quaternion.identity;
        }
        if (enemyBase.transform.position.x > nextRoamPosition.x) //moving left
        {
            enemyTransform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }

    private void SearchWalkPoint()
    {
        float randomX = Random.Range(-roamingPointRange, roamingPointRange);
        float randomY = Random.Range(-roamingPointRange, roamingPointRange);
        nextRoamPosition = new Vector2(enemyBase.transform.position.x + randomX, enemyBase.transform.position.y + randomY);
    }

    private IEnumerator roamingCooldown()
    {
        timeBetweenRoams = Random.Range(3f, 6f);
        yield return new WaitForSeconds(timeBetweenRoams);
        SearchWalkPoint();
        once2 = false;
    }
    #endregion
}
