using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    /*
    This script controls the base of the enemy, including all its values such as health, speed, and damage
    The StateMachineManager class (attached to a child gameobject of enemy) is being called to allow the enemy to transition to different states
    The only state being called into is the hurt state, which immediately does damage and knocksback no matter the state
    This script gets called by the other states to access the enemyDistanceFromPlayer method, which then allows it to transiton from either follow, idle, or wandering states
    */

    [Header("Enemy Stats")]
    [Space(5)]
    public float health;
    public float damage;
    public float speed;
    public float attackSpeed;
    public float knockbackPower;
    public float alertRange;
    public float attackRange;

    [HideInInspector] public GameObject player;

    [Header("State Machine")]
    [Space(5)]
    public StateMachineManager stateMachineManager;
    public EnemyHurt enemyHurtState;
    
    [Header("Knockback Received")]
    [Space(5)]
    public float knockbackDuration; 
    public float knockbackDistance;


    void Start()
    {
        player = GameObject.Find("Player");
    }

    public float enemydDistanceFromPlayer()
    {
        float differenceX = player.transform.position.x - transform.position.x;
        float differenceY = player.transform.position.y - transform.position.y;
        return Mathf.Sqrt(differenceX * differenceX + differenceY * differenceY); 
    }

    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderToEnemyVector, float effectMultipler)
    {
        enemyHurtState.damageTaken = damage;
        enemyHurtState.knockbackPower = knockbackPower;
        enemyHurtState.attackingColliderToEnemyVector = attackingColliderToEnemyVector;
        enemyHurtState.weaponMultiplier = effectMultipler;
        stateMachineManager.setNewState(enemyHurtState);
    }

    void OnDrawGizmosSelected() //draws alert range and attack range
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, alertRange);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
