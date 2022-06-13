using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float health = 100;
    public float knockbackDistance = 10;

    private bool isHurt = false;
    private bool isDead = false;

    private Rigidbody2D rb;

    private Vector2 enemyToPlayerVector;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    #region hurt functions
    public void hurt(float damage, float knockbackPower, Vector2 enemyPosition)
    {
        enemyToPlayerVector = (Vector2)transform.position - enemyPosition;

        isHurt = true;

        //SetState(PlayerHurtState); <--logic will me migrated to state

        health -= damage;

        if (health > 0)
        {
            //animator.SetTrigger("hurt");
            knockback(knockbackPower);
        }
        else
        {
            Debug.Log("Player Dead");
            isDead = true;
            //animator.SetTrigger("dead");
            knockback(5f);
        }
    }

    void knockback(float power)
    {
        rb.AddForce(enemyToPlayerVector.normalized * knockbackDistance * power, ForceMode2D.Impulse);
        StartCoroutine(knockbackCo());
        StartCoroutine(hurtCooldown());
    }

    private IEnumerator knockbackCo()
    {

        yield return new WaitForSeconds(0.5f);
        rb.velocity = Vector2.zero;
    }

    private IEnumerator hurtCooldown()
    {
        yield return new WaitForSeconds(1);
        isHurt = false;
    }
    #endregion
}
