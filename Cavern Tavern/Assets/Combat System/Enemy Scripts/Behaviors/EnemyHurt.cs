using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

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

    public CinemachineImpulseSource impulse;

    public EnemyBase enemyBase;
    public PlayerHotbar playerAttack;
    public State followState;
    private Rigidbody2D rb;
    private Renderer rend;
    public GameObject bloodParticles;
    public GameObject deathParticles;

    public Vector2 attackingColliderToEnemyVector;
    public float damage;
    public float knockbackPower;
    private float knockbackDistance;
    private float knockbackDuration;

    [Header("To Update Distance From Player and Stop Enemy From Attacking Outside Of Range")]
    [Space(5)]
    public AttackPlayer attackState;

    public override void OnStart()
    {
        rb = enemyBase.GetComponent<Rigidbody2D>();
        knockbackDistance = enemyBase.knockbackDistance;
        knockbackDuration = enemyBase.knockbackDuration;
        rend = enemyBase.GetComponent<Renderer>();
        impulse = transform.GetComponent<CinemachineImpulseSource>();

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
        
        if (enemyBase.health > 0) knockback(knockbackPower, attackingColliderToEnemyVector);
        else knockback(2f * knockbackPower, attackingColliderToEnemyVector);
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
        if (enemyBase.health <= 0)
        {
            //Camera Shake
            impulse.GenerateImpulse(2f);
            //particles
            var particle = Instantiate(deathParticles, transform.position, Quaternion.identity);
            particle.transform.parent = enemyBase.transform;

            yield return new WaitForSeconds(knockbackDuration);
            
            
            rb.velocity = Vector2.zero; //stop velocity (knockback)
            particle.transform.parent = null; //Move death particles out of enemy object so it will not get destroyed early

            Destroy(enemyBase.gameObject); //Destroy Enemy Object if dead
        }
        else
        {
            //Camera Shake
            impulse.GenerateImpulse(1f);
            //particles
            var hurtParticles = Instantiate(bloodParticles, transform.position, Quaternion.identity);
            hurtParticles.transform.parent = enemyBase.transform;

            yield return new WaitForSeconds(knockbackDuration);
            
            rb.velocity = Vector2.zero; //stop velocity (knockback)
            stateMachineManager.setNewState(followState); //Enemy changes to attack state after hurt
        }
    }

    private IEnumerator Flash()
    {
        yield return new WaitForSeconds(knockbackDuration * .5f);
        rend.material.color = Color.white;
    }                  
}
