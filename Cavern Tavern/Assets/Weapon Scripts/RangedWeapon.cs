using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : Weapon
{
    public playerAttack playerAttackScript;

    public float damage;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
    public override void Attack()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(playerAttackScript.playerToWeaponReachVector.normalized * fireForce, ForceMode2D.Impulse);
        bullet.GetComponent<Projectile>().damage = damage;
    }

}
