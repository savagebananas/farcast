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
            ShootSingleProjectile();
            //ShootShotgunProjectile(4,100);
            timeBtwShots = shotCooldown;
        }
        else
        {
            timeBtwShots -= Time.deltaTime;
        }

        if (enemyBase.enemyDistanceFromPlayer() > attackRange)
        {
            stateMachineManager.setNewState(followPlayer);
        }
    }

    void ShootSingleProjectile()
    {
        //Angles
        Vector2 enemyToPlayerVector = playerBase.transform.position - transform.position;
        enemyToPlayerVector.Normalize();
        float enemyToPlayerAngle = Mathf.Atan2(enemyToPlayerVector.y, enemyToPlayerVector.x) * Mathf.Rad2Deg;

        //Instantiate
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, enemyToPlayerAngle - 45));
        Projectile projectileScript = projectile.GetComponentInChildren<Projectile>();

        //Assign Values
        //projectile.GetComponent<Rigidbody2D>().AddForce(enemyToPlayerVector.normalized * fireForce, ForceMode2D.Impulse);
        projectileScript.direction = enemyToPlayerVector;
        projectileScript.speed = 15;
        projectileScript.damage = damage;
        projectileScript.damagePlayer = true;
    }

    void ShootShotgunProjectile(float numberOfBullets, float angleOfSpread)
    {
        //Direction Towards Player
        Vector2 enemyToPlayerVector = playerBase.transform.position - transform.position;
        enemyToPlayerVector.Normalize();
        float enemyToPlayerAngle = Mathf.Atan2(enemyToPlayerVector.y, enemyToPlayerVector.x) * Mathf.Rad2Deg;

        for (int i = 0; i < numberOfBullets; i++)
        {
            float angleIncrement = angleOfSpread / numberOfBullets; //how many degrees between each bullet
            float angleOfDegreeZero = enemyToPlayerAngle - (angleOfSpread / 2f); //so that bullets spawn from 0 to angleSpread

            //Instantiate projectile and set initial direction
            GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, enemyToPlayerAngle - 45));
            Projectile projectileScript = projectile.GetComponentInChildren<Projectile>();
            projectileScript.direction = (Vector2) (Quaternion.Euler(0, 0, angleOfDegreeZero + angleIncrement * i) * Vector2.right);

            //projectile stats
            projectileScript.damage = damage;
            projectileScript.damagePlayer = true;
            projectileScript.speed = 15;
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
