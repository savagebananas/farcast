using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;


public class RangedWeapon : HotbarItem
{
    public GameObject pivotPoint;
    public GameObject muzzle;
    private Vector2 muzzleToCursorVector;
    public GameObject bulletPrefab;
    public float fireForce;

    public float effectMultiplier;
    public float damage;

    public bool autoShoot;
    public float timePerShot;
    private float timer;

    //Squash and stretch
    public Animator squashAnimator;

    //Screenshake
    public CinemachineImpulseSource impulse;
    [Range(0f, 1f)]public float screenshakeValue;

    public void Update()
    {
        
        if (!autoShoot) //Semi Auto
        {
            if (Input.GetMouseButtonDown(0)) UseItem();
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
        //Firing Projectile
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - weaponReference.transform.position;
        playerToCursorVector.Normalize();
        float playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x) * Mathf.Rad2Deg;

        muzzleToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - muzzle.transform.position;

        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(0, 0, playerToCursorAngle - 45));
        Projectile projectileScript = bullet.GetComponentInChildren<Projectile>();


        bullet.GetComponentInChildren<Rigidbody2D>().AddForce(playerToCursorVector.normalized * fireForce, ForceMode2D.Impulse);
        projectileScript.damage = damage;
        projectileScript.effectMultiplier = effectMultiplier;
        projectileScript.damageEnemy = true;

        //Squash and stretch weapon
        if (squashAnimator != null) squashAnimator.SetTrigger("SquashAndStretch");

        //screenshake
        impulse.GenerateImpulse(screenshakeValue);
    }
}
