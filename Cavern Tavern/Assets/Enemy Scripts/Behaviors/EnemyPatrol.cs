using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : State
{
    public EnemyBase enemyBase;
    public State followState;
    public Transform enemyTransform;

    private float speed;
    private float alertRange;
    private float enemyDistanceToPlayer;

    [Header("Roaming Variables")]
    [Space(5)]
    public float roamingPointRange;
    private Vector2 nextRoamPosition;
    private float timeBetweenRoams;
    private bool isWalking;
    private bool isIdle;


    private float oldPositionX;

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
            WanderAround(); 
        }
        else if (enemyDistanceToPlayer <= alertRange) //enemy detects player
        {
            stateMachineManager.setNewState(followState);
        }
    }

    public override void OnLateUpdate()
    {
    }

    #region Wander Around
    void WanderAround()
    {
        timeBetweenRoams = Random.Range(4f, 8f);

        if ((Vector2)enemyBase.transform.position == nextRoamPosition)
        {
            isWalking = false;
        }

        if (!isWalking && !isIdle) //when stopped walking (arrived at position), start idling
        {
            StartCoroutine(roamingCooldown());
            SearchWalkPoint();
        }
        else //when finished being idle, start walking
        {
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
    }

    private void SearchWalkPoint()
    {
        isWalking = true;
        float randomX = Random.Range(-roamingPointRange, roamingPointRange);
        float randomY = Random.Range(-roamingPointRange, roamingPointRange);
        nextRoamPosition = new Vector2(enemyBase.transform.position.x + randomX, enemyBase.transform.position.y + randomY);
    }

    private IEnumerator roamingCooldown()
    {
        isIdle = true;
        yield return new WaitForSeconds(timeBetweenRoams);
        isIdle = false;
    }
    #endregion 
}
