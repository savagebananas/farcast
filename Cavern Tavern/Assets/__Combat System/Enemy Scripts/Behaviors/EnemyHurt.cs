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

    

    public EnemyBase enemyBase;
    private Rigidbody2D rb;
    public Renderer rend;
    public State followState;
    public AttackPlayer attackState;

    //Visuals
    public GameObject bloodParticles;
    public GameObject deathParticles;
    public GameObject smokeParticles;
    public Animator anchorAnimator;
    public CinemachineImpulseSource impulse;
    public float weaponMultiplier;
    public float enemySizeScale;

    //Knockback
    public Vector2 attackingColliderToEnemyVector;
    public float damage;
    public float knockbackPower;
    private float knockbackDistance;
    private float knockbackDuration;
    

    public override void OnStart()
    {
        rb = enemyBase.GetComponent<Rigidbody2D>();
        knockbackDistance = enemyBase.knockbackDistance;
        knockbackDuration = enemyBase.knockbackDuration;

        hurt(damage, knockbackPower, attackingColliderToEnemyVector);
    }

    public override void OnUpdate()
    {
        if(attackState != null) attackState.enemyToPlayerDistance = enemyBase.enemydDistanceFromPlayer(); //To Update Distance From Player and Stop Enemy From Attacking Outside Of Range
    }
    public override void OnLateUpdate(){}

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
        if (enemyBase.health <= 0) //enemy dead
        {
            //Camera Shake
            impulse.GenerateImpulse(0.5f);
            //animation
            anchorAnimator.SetTrigger("SquashAndStretch");
            //particles
            var particle = Instantiate(deathParticles, transform.position, Quaternion.identity);
            particle.transform.localScale = new Vector2(particle.transform.localScale.x * enemySizeScale, particle.transform.localScale.y * enemySizeScale);
            particle.transform.parent = enemyBase.transform;

            yield return new WaitForSeconds(knockbackDuration * weaponMultiplier * enemySizeScale);
            
            rb.velocity = Vector2.zero; //stop velocity (knockback)

            anchorAnimator.SetTrigger("Idle");

            particle.transform.parent = null; //Move blood particles out of enemy object so it will not get destroyed early
            
            //smoke death particles
            var smoke = Instantiate(smokeParticles, transform.position, Quaternion.identity);
            smoke.transform.localScale = new Vector2(smoke.transform.localScale.x * weaponMultiplier * enemySizeScale, smoke.transform.localScale.y * weaponMultiplier * enemySizeScale);

            Destroy(enemyBase.gameObject); //Destroy Enemy Object if dead
        }
        else //enemy hurt
        {
            //animation
            anchorAnimator.SetTrigger("SquashAndStretch");
            //particles
            var particle = Instantiate(bloodParticles, transform.position, Quaternion.identity);
            particle.transform.localScale = new Vector2(particle.transform.localScale.x * enemySizeScale, particle.transform.localScale.y * enemySizeScale);
            particle.transform.parent = enemyBase.transform;

            yield return new WaitForSeconds(knockbackDuration * weaponMultiplier * enemySizeScale);
            
            rb.velocity = Vector2.zero; //stop velocity (knockback)

            anchorAnimator.SetTrigger("Idle");

            stateMachineManager.setNewState(followState); //Enemy changes to attack state after hurt
        }
    }

    private IEnumerator Flash()
    {
        yield return new WaitForSeconds(knockbackDuration * weaponMultiplier * enemySizeScale);
        rend.material.color = Color.white;
    }           
    
}
