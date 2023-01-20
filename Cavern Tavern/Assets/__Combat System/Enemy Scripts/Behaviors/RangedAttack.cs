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
            ShootShotgunProjectile(4,100);
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
        //Angles
        Vector2 enemyToPlayerVector = playerBase.transform.position - transform.position;
        enemyToPlayerVector.Normalize();
        float enemyToPlayerAngle = Mathf.Atan2(enemyToPlayerVector.y, enemyToPlayerVector.x) * Mathf.Rad2Deg;

        //Instantiate
        GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, enemyToPlayerAngle - 45));
        Projectile projectileScript = projectile.GetComponent<Projectile>();

        //Assign Values
        projectile.GetComponent<Rigidbody2D>().AddForce(enemyToPlayerVector.normalized * fireForce, ForceMode2D.Impulse);
        projectileScript.damage = damage;
        projectileScript.damagePlayer = true;
    }

    void ShootShotgunProjectile(float numberOfBullets, float angleOfSpread)
    {
        //This adjust the buller count to actually fire the right amount of bullets
        numberOfBullets--;

        //Get vector towards player
         Vector2 enemyToPlayerVector = playerBase.transform.position - transform.position;
        enemyToPlayerVector.Normalize();
        float enemyToPlayerAngle = Mathf.Atan2(enemyToPlayerVector.y, enemyToPlayerVector.x) * Mathf.Rad2Deg;

        for(float i = -angleOfSpread/2; i <= angleOfSpread/2;i+= angleOfSpread/numberOfBullets){
                //Create projectile with a vector towards the player and all the other stats
                 GameObject projectile = Instantiate(projectilePrefab, transform.position, Quaternion.Euler(0, 0, enemyToPlayerAngle - 45));
                 Projectile projectileScript = projectile.GetComponent<Projectile>();
                 projectileScript.damage = damage;
                 projectileScript.damagePlayer = true;
                 projectileScript.speed = 8; //This should later on be made into a public variable that can be used in a modular way

                 //Adjust the projectiles vector so it fits the spread 
                 Vector2 direction = enemyToPlayerVector;
                 projectileScript.direction = (Vector2)(Quaternion.Euler(direction.x,direction.y,direction.x+i) * direction);
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
