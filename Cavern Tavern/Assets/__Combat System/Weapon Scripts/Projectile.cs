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
    public Vector2 direction;
    public float speed;
    Rigidbody2D rbody;

    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
    }


    void Update()
    {
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rbody.rotation = angle;
    }

    void FixedUpdate()
    {
        Vector2 newPosition = rbody.position + direction * speed * Time.deltaTime;
        rbody.MovePosition(newPosition);
        
    }

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
