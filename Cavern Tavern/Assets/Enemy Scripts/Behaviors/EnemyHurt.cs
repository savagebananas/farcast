using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : State
{
    public EnemyBase enemyBase;
    public playerAttack playerAttack;
    public State attackState;
    private Rigidbody2D rb;

    private float health;
    private float playerDamage;

    private float playerKnockbackPower;
    private float knockbackDistance;
    private float knockbackDuration;
    
    [HideInInspector] public Vector2 attackingColliderToEnemyVector;

    public override void OnStart()
    {
        rb = enemyBase.GetComponent<Rigidbody2D>();
        health = enemyBase.health;
        playerDamage = playerAttack.damage;
        playerKnockbackPower = playerAttack.knockbackPower;
        knockbackDistance = enemyBase.knockbackDistance;
        knockbackDuration = enemyBase.knockbackDuration;

        hurt(playerDamage, playerKnockbackPower, attackingColliderToEnemyVector);
    }

    public override void OnUpdate()
    {

    }
    public override void OnLateUpdate()
    {

    }

    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderToEnemyVector)
    {
        health -= damage;
        
        if (health > 0)
        {
            knockback(knockbackPower);
            //animator.SetTrigger("hurt");
        }
        else 
        {
            Debug.Log("Enemy Dead");
            //animator.SetTrigger("dead");
            knockback(5f);
            // add particles
        }
    }

    void knockback(float power)
    {
        rb.AddForce(playerAttack.playerToWeaponReachVector.normalized * knockbackDistance * power, ForceMode2D.Impulse);
        StartCoroutine(knockbackCo());
    }

    private IEnumerator knockbackCo()
    {

        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        stateMachineManager.setNewState(attackState); //after knockback, attack player
    }


}
