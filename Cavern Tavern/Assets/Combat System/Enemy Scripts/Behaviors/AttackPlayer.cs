using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : State
{
    public PlayerBase playerBase;
    public EnemyBase enemyBase;
    public State followPlayer;

    private float attackDelay;

    private float damage;
    private float knockbackPower;
    private float attackRange;

    public float enemyToPlayerDistance;

    private bool playerIsHurt;

    public override void OnStart()
    {
        attackDelay = enemyBase.attackSpeed;
        damage = enemyBase.damage;
        attackRange = enemyBase.attackRange;
        knockbackPower = enemyBase.knockbackPower;

        StartCoroutine(AttackDelay());
    }

    public override void OnUpdate()
    {
        enemyToPlayerDistance = enemyBase.enemydDistanceFromPlayer();
        playerIsHurt = playerBase.isHurt;

        facePlayer();
    }

    public override void OnLateUpdate()
    {

    }

    private IEnumerator AttackDelay()
    {
        //animator.SetTrigger("attack");
        yield return new WaitForSeconds(attackDelay);

        if (enemyToPlayerDistance <= attackRange && !playerIsHurt) 
        {
            playerBase.hurt(damage, knockbackPower, playerBase.transform.position - enemyBase.transform.position);
            StartCoroutine(AttackDelay()); //calls the attack again after hitting player
        }

        else //not in attack range
        {
            stateMachineManager.setNewState(followPlayer);
        }
    }

    void facePlayer()
    {
        if (enemyBase.transform.position.x < playerBase.transform.position.x)
        {
            enemyBase.transform.rotation = Quaternion.identity;
        }
        if (enemyBase.transform.position.x > playerBase.transform.position.x)
        {
            enemyBase.transform.rotation = Quaternion.Euler(0, 180, 0);
        }
    }
}
