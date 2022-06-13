using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : State
{
    public GameObject player;
    public EnemyBase enemyBase;
    private float speed;
    private float attackRange;
    private float alertRange;

    public State attackState;
    public State roamingState;

    public override void OnStart()
    {
        speed = enemyBase.speed;
        attackRange = enemyBase.attackRange;
        alertRange = enemyBase.alertRange;
    }

    public override void OnUpdate()
    {
        facePlayer();

        if (enemyBase.enemydDistanceFromPlayer() <= attackRange)
        {
            stateMachineManager.setNewState(attackState);
        }
        else if (enemyBase.enemydDistanceFromPlayer() <= alertRange)
        {
            enemyBase.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (enemyBase.enemydDistanceFromPlayer() > alertRange)
        {
            stateMachineManager.setNewState(roamingState);
        }
    }

    public override void OnLateUpdate()
    {

    }

    void facePlayer()
    {
        if (enemyBase.transform.position.x < player.transform.position.x)
        {
            enemyBase.transform.rotation = Quaternion.identity;
        }

        if (enemyBase.transform.position.x > player.transform.position.x)
        {
            enemyBase.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
