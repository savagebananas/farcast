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

    public void Update()
    {
        muzzleToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        
        if (Input.GetMouseButtonDown(0)) UseItem();
    }    
    public override void UseItem()
    {
        Vector2 playerToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - player.transform.position;
        playerToCursorVector.Normalize();
        float playerToCursorAngle = Mathf.Atan2(playerToCursorVector.y, playerToCursorVector.x) * Mathf.Rad2Deg;

        GameObject bullet = Instantiate(bulletPrefab, muzzle.transform.position, Quaternion.Euler(0, 0, playerToCursorAngle - 45));
        Projectile projectileScript = bullet.GetComponent<Projectile>();

        bullet.GetComponent<Rigidbody2D>().AddForce(muzzleToCursorVector.normalized * fireForce, ForceMode2D.Impulse);
        projectileScript.damage = damage;
        projectileScript.effectMultiplier = effectMultiplier;
        projectileScript.damageEnemy = true;
    }
}
