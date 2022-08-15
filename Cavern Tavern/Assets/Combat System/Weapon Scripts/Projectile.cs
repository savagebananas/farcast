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

    public float damage;
    public float knockbackPower;
    public float effectMultiplier;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<PlayerBase>().hurt
                (damage, knockbackPower, collision.gameObject.transform.position - transform.position);
        }
        if (collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<EnemyBase>().hurt
                (damage, knockbackPower, (collision.gameObject.transform.position - transform.position).normalized, effectMultiplier);
        }
        //add particles
        Destroy(gameObject);
    }
}
