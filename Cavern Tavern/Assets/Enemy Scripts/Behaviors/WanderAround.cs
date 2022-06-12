using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WanderAround : EnemyState
{
    public WanderAround(GameObject enemy, GameObject player) : base(enemy, player)
    {
    }

    private float speed;
    public float roamingPointRange;
    private Vector2 nextRoamPosition;
    private float timeBetweenRoams;
    private bool idle;
    private bool isWalking = false;
    public override IEnumerator Start()
    {
        speed = enemy.GetComponent<EnemyBase>().speed;
        
        yield break;
    }
    public override IEnumerator Update()
    {
        wanderAround();

        yield break;
    }

    void wanderAround()
    {
        timeBetweenRoams = Random.Range(4f, 8f);

        if ((Vector2)enemy.transform.position == nextRoamPosition)
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
            enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, nextRoamPosition, speed * Time.deltaTime);
        }
    }


    private void SearchWalkPoint()
    {
        isWalking = true;
        float randomX = Random.Range(-roamingPointRange, roamingPointRange);
        float randomY = Random.Range(-roamingPointRange, roamingPointRange);
        nextRoamPosition = new Vector2(enemy.transform.position.x + randomX, enemy.transform.position.y + randomY);
    }

    private IEnumerator roamingCooldown()
    {
        idle = true;
        yield return new WaitForSeconds(timeBetweenRoams);
        //yield return new WaitUntil(() => boolean)
        idle = false;
    }
}

