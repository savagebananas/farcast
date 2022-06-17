using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float knockbackPower;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 8)
        {
            collision.gameObject.GetComponent<PlayerBase>().hurt(damage, knockbackPower, collision.gameObject.transform.position - transform.position);
        }
        if (collision.gameObject.layer == 10)
        {
            collision.gameObject.GetComponent<EnemyBase>().hurt(damage, knockbackPower * 0.25f , collision.gameObject.transform.position - transform.position);
        }
        Destroy(gameObject);
    }
}
