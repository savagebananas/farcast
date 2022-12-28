using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedAttack : State
{
    private PlayerBase playerBase;

    [SerializeField] private EnemyBase enemyBase;
    [SerializeField] private GameObject projectilePrefab;
    [SerializeField] private float fireForce;
    [SerializeField] private float effectMultiplier;
    
    private float attackRange;
    private float damage;

    private float timeBtwShots;
    public float shotCooldown;

    public State followPlayer;

    public override void OnLateUpdate() { }

    public override void OnStart()
    {
        playerBase = GameObject.Find("Player").GetComponent<PlayerBase>();
        damage = enemyBase.damage;
        attackRange = enemyBase.attackRange;
    }

    public override void OnUpdate()
    {
        facePlayer();

        if (timeBtwShots <= 0)
        {
            ShootProjectile();
            timeBtwShots = shotCooldown;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (enemyBase.enemydDistanceFromPlayer() > attackRange)
        {
            stateMachineManager.setNewState(followPlayer);
        }
    }

    void ShootProjectile()
    {
        Vector2 enemyToPlayerVector = playerBase.transform.position - transform.position;
        enemyToPlayerVector.Normalize();
        float enemyToPlayerAngle = Mathf.Atan2(enemyToPlayerVector.y, enemyToPlayerVector.x) * Mathf.Rad2Deg;

        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, enemyToPlayerAngle - 45));
        Projectile projectileScript = projectile.GetComponent<Projectile>();


        projectile.GetComponent<Rigidbody2D>().AddForce(enemyToPlayerVector.normalized * fireForce, ForceMode2D.Impulse);
        projectileScript.damage = damage;
        projectileScript.damagePlayer = true;
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
