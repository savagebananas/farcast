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
    public PlayerHotbar playerAttack;
    public State followState;
    private Rigidbody2D rb;
    private Renderer rend;
    public GameObject bloodParticles;

    [SerializeField] private float health;
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
        rend = enemyBase.GetComponent<Renderer>();

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
        enemyBase.health -= damage;
        
        if (enemyBase.health > 0)
        {
            knockback(knockbackPower, attackingColliderToEnemyVector);
            //animator.SetTrigger("hurt");
        }
        else 
        {
            Debug.Log("Enemy Dead");
            //animator.SetTrigger("dead");
            knockback(5f, attackingColliderToEnemyVector);

        }
    }

    void knockback(float power, Vector2 attackingColliderToEnemyVector)
    {
        rb.AddForce(attackingColliderToEnemyVector.normalized * knockbackDistance * power, ForceMode2D.Impulse);
        rend.material.color = new Color(255, 255, 255, 255);
        StartCoroutine(knockbackCo());
        StartCoroutine(Flash());
    }

    private IEnumerator knockbackCo()
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        // add particles
        var particles = Instantiate(bloodParticles, transform.position, Quaternion.identity);
        /*
        if (enemyBase.health <= 0)
        {
            Destroy(enemyBase.gameObject);
        }
        */

        stateMachineManager.setNewState(followState); //after knockback, attack player
    }

    private IEnumerator Flash()
    {
        yield return new WaitForSeconds(knockbackDuration * .75f);
        rend.material.color = Color.white;

    }                                                                
}
