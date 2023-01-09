using System.Collections;
using System.Collections.Generic;
using UnityEngine;


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

    public void Update()
    {
        
        if (!autoShoot)
        {
            if (Input.GetMouseButtonDown(0)) UseItem();
        }
        else
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
    }
}
