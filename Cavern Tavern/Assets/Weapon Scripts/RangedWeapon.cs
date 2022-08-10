using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedWeapon : HotbarItem
{
    public float damage;

    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireForce;

    private Vector2 weaponPointToCursorVector;

    void Update()
    {
        weaponPointToCursorVector = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
        }
    }    
    public override void UseItem()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        bullet.GetComponent<Rigidbody2D>().AddForce(weaponPointToCursorVector.normalized * fireForce, ForceMode2D.Impulse);
        bullet.GetComponent<Projectile>().damage = damage;
    }



}
