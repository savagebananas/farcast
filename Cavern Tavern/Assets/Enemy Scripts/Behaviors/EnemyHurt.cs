using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : State
{
    /*
    When state is set, script calls OnStart() method which sets all components and calls the hurt() function once
    Hurt() function 
    -subtracts damage enemy health
    -calls the knockback function based on knockback power and the Vector difference from the attackingCollider (arrow come from left, enemy knockback right)
    Knockback() function
    -Adds Force to certain vector direction and starts coroutine, which counts down a number of seconds before stopping the velocity (stopping knockback)
    -After knockback, sets state back to follow state
    */

    public EnemyBase enemyBase;
    public playerAttack playerAttack;
    public State followState;
    private Rigidbody2D rb;

    private float health;
    public float damage;

    public float knockbackPower;
    private float knockbackDistance;
    private float knockbackDuration;

    [Header("To Update Distance From Player and Stop Enemy From Attacking Outside Of Range")]
    [Space(5)]
    public AttackPlayer attackState;
    
    public Vector2 attackingColliderToEnemyVector;

    public override void OnStart()
    {
        rb = enemyBase.GetComponent<Rigidbody2D>();
        health = enemyBase.health;
        knockbackDistance = enemyBase.knockbackDistance;
        knockbackDuration = enemyBase.knockbackDuration;

        hurt(damage, knockbackPower, attackingColliderToEnemyVector);
    }

    public override void OnUpdate()
    {
        attackState.enemyToPlayerDistance = enemyBase.enemydDistanceFromPlayer();
    }
    public override void OnLateUpdate()
    {

    }

    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderToEnemyVector)
    {
        health -= damage;
        
        if (health > 0)
        {
            knockback(knockbackPower, attackingColliderToEnemyVector);
            //animator.SetTrigger("hurt");
        }
        else 
        {
            Debug.Log("Enemy Dead");
            //animator.SetTrigger("dead");
            knockback(5f, attackingColliderToEnemyVector);
            // add particles
        }
    }

    void knockback(float power, Vector2 attackingColliderToEnemyVector)
    {
        Debug.Log(attackingColliderToEnemyVector.normalized);
        rb.AddForce(attackingColliderToEnemyVector.normalized * knockbackDistance * power, ForceMode2D.Impulse);
        StartCoroutine(knockbackCo());
    }

    private IEnumerator knockbackCo()
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        stateMachineManager.setNewState(followState); //after knockback, attack player
    }
}
