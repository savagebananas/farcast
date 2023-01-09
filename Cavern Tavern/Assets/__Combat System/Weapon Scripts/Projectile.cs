using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    /*
    Used only in the projectile
    Checks for collision with player or enemy, calls their hurt function and does a set damage
    Destroys itself after colliding with anything
    */
    public GameObject ProjectileObject;

    public float damage;
    public float knockbackPower;
    public float effectMultiplier;
    public bool damagePlayer = false;
    public bool damageEnemy = false;

    private void OnTriggerEnter2D(Collider2D other)
    {
        
        if (other.CompareTag("Player") && damagePlayer == true)
        {
            other.gameObject.GetComponent<PlayerBase>().hurt
                (damage, knockbackPower, other.gameObject.transform.position - transform.position);
            Destroy(ProjectileObject);
        }
        if (other.CompareTag("Enemy") && damageEnemy == true)
        {
            other.gameObject.GetComponent<EnemyBase>().hurt
                (damage, knockbackPower, (other.gameObject.transform.position - transform.position).normalized, effectMultiplier);
            Destroy(ProjectileObject);
        }

        if (other.CompareTag("Environment Colliders"))
        {
            Destroy(ProjectileObject);
        }
    }
}
