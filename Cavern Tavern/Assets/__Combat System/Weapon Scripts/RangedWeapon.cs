using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class RangedWeapon : HotbarItem
{
    public GameObject bulletPrefab;
    public GameObject pivotPoint;
    public GameObject muzzle;
    private Vector2 muzzleToCursorVector;

    public float effectMultiplier;
    public float damage;
    public float bulletSpeed;

    //Automatic shooting
    public bool autoShoot;
    public float timePerShot;
    private float timer = 0;

    //Squash and stretch
    public Animator squashAnimator;

    //Screenshake
    public CinemachineImpulseSource impulse;
    [Range(0f, 1f)]public float screenshakeValue;

    public void Update()
    { 
        if (!autoShoot) //Semi Auto
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                if (Input.GetMouseButtonDown(0)) 
                { 
                    ShootShotgunProjectile(5, 20f, 10f);//UseItem();
                    timer = timePerShot;                
                }
            }
        }
        else //Full Auto
        {
            if (Input.GetMouseButton(0))
            {
                if (timer > 0)
                {
                    timer -= Time.deltaTime;
                }
                else
                {
                    UseItem();
                    timer = timePerShot;
                }
            }
        }

    }    
    public override void UseItem()
    {
        //Angles
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponReference.transform.position;
        playerToCursorVector.Normalize();
        float playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x) * Mathf.Rad2Deg;
        muzzleToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - muzzle.transform.position;
        
        //Instantiate
        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(0, 0, playerToCursorAngle));

        Projectile projectileScript = bullet.GetComponentInChildren<Projectile>();


        //bullet.GetComponentInChildren<Rigidbody2D>().AddForce(playerToCursorVector.normalized * fireForce, ForceMode2D.Impulse);
        
        
        projectileScript.direction = muzzleToCursorVector.normalized;
        projectileScript.speed = bulletSpeed;
        projectileScript.damage = damage;
        projectileScript.effectMultiplier = effectMultiplier;
        projectileScript.damageEnemy = true; 

        //Squash and stretch weapon
        if (squashAnimator != null) squashAnimator.SetTrigger("SquashAndStretch");

        //screenshake
        if (impulse != null) impulse.GenerateImpulse(screenshakeValue);
    }

    void ShootShotgunProjectile(float numberOfBullets, float angleOfSpread, float randomOffset)
    {
        //Angles
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponReference.transform.position;
        playerToCursorVector.Normalize();
        float playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x) * Mathf.Rad2Deg;
        muzzleToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - muzzle.transform.position;

        float angleIncrement = angleOfSpread / numberOfBullets; //how many degrees between each bullet
        float angleOfDegreeZero = playerToCursorAngle - (angleOfSpread / 2f); //so that bullets spawn from 0 to angleSpread

        for (int i = 0; i < numberOfBullets; i++)
        {
            //Instantiate projectile and set initial direction
            GameObject projectile = Instantiate(bulletPrefab, transform.position, Quaternion.Euler(0, 0, playerToCursorAngle));
            Projectile projectileScript = projectile.GetComponentInChildren<Projectile>();
            float randOffset = Random.Range(-randomOffset, randomOffset);
            projectileScript.direction = (Vector2)(Quaternion.Euler(0, 0, angleOfDegreeZero + angleIncrement * i + randOffset) * Vector2.right);

            //projectile stats
            projectileScript.speed = bulletSpeed;
            projectileScript.damage = damage;
            projectileScript.effectMultiplier = effectMultiplier;
            projectileScript.damageEnemy = true;
        }

        //Squash and stretch weapon
        if (squashAnimator != null) squashAnimator.SetTrigger("SquashAndStretch");

        //screenshake
        if (impulse != null) impulse.GenerateImpulse(screenshakeValue);
    }
}
