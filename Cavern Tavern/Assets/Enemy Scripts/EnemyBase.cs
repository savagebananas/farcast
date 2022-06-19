using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [Header("Enemy Stats")]
    [Space(5)]
    public float health;
    public float damage;
    public float speed;
    public float attackSpeed;
    public float knockbackPower;
    public float alertRange;
    public float attackRange;

    [Header("State Machine")]
    [Space(5)]
    public StateMachineManager stateMachineManager;
    public EnemyHurt enemyHurtState;

    [Header("ReferencesToPlayer")]
    [Space(5)]
    public GameObject player;
    private Vector2 enemyToPlayerVector;
    private float enemyToPlayerAngle;

    [Header("Roaming Variables")]
    [Space(5)]
    public float roamingPointRange;
    
    [Header("Knockback Values (for when player hits enemy)")]
    [Space(5)]
    public float knockbackDuration; 
    public float knockbackDistance;


    void Start()
    {

    }

    public float enemydDistanceFromPlayer()
    {
        float differenceX = player.transform.position.x - transform.position.x;
        float differenceY = player.transform.position.y - transform.position.y;
        return Mathf.Sqrt(differenceX * differenceX + differenceY * differenceY); 
    }

    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderToEnemyVector)
    {
        enemyHurtState.damage = damage;
        enemyHurtState.knockbackPower = knockbackPower;
        enemyHurtState.attackingColliderToEnemyVector = attackingColliderToEnemyVector;
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
