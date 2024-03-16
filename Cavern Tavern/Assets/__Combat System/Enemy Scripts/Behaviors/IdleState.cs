using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    public EnemyBase enemyBase;
    public State followState;

    private float alertRange;

    private float enemyDistanceToPlayer;
    public override void OnStart()
    {
        alertRange = enemyBase.alertRange;
        animator.SetTrigger("isIdle");
    }

    public override void OnUpdate()
    {
        enemyDistanceToPlayer = enemyBase.enemyDistanceFromPlayer();

        if (enemyDistanceToPlayer <= alertRange) //player close to enemy
        {
            stateMachineManager.setNewState(followState);
        }
    }

    public override void OnLateUpdate()
    {

    }
}
