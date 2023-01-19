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
        else if (enemyBase.enemydDistanceFromPlayer() <= alertRange) //Enemy is in follow range, continue moving towards player
        {
            animator.SetTrigger("isWalking");
            enemyBase.transform.position = Vector2.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
        }
        else if (enemyBase.enemydDistanceFromPlayer() > alertRange) //Enemy is too far away, go back to roam/idle state
        {
            stateMachineManager.setNewState(roamingState);
        }
    }

    public override void OnLateUpdate() { }

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
