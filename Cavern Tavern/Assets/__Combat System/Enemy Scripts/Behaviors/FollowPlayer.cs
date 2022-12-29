using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayer : State
{
    private GameObject player;
    public EnemyBase enemyBase;
    private float speed;
    private float attackRange;
    private float alertRange;

    public State attackState;
    public State roamingState;

    public override void OnStart()
    {
        player = GameObject.Find("Player");

        speed = enemyBase.speed;
        attackRange = enemyBase.attackRange;
        alertRange = enemyBase.alertRange;
    }

    public override void OnUpdate()
    {
        facePlayer();

        if (enemyBase.enemydDistanceFromPlayer() <= attackRange) //Enemy is in attack range, then attack player
        {
            stateMachineManager.setNewState(attackState);
        }
        else if (enemyBase.enemydDistanceFromPlayer() <= alertRange)
        {
            animator.SetTrigger("isWalking");
            enemyBase.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (enemyBase.enemydDistanceFromPlayer() > alertRange)
        {
            stateMachineManager.setNewState(roamingState);
        }
    }

    public override void OnLateUpdate() { }

    Vector3 randomPositionNearPlayer()
    {
        float x = player.transform.position.x + Random.Range(-15,15);
        float y = player.transform.position.y + Random.Range(-15,15);
        return new Vector3(x, y, 0);
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
