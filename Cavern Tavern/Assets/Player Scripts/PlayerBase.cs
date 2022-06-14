using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public StateMachineManager stateMachineManager;
    public State movementState;

    public float health = 100;

    public float moveSpeed;
    public float dashSpeed;
    public float dashLength;
    public float dashCooldown;

    public float knockbackDistance;
    public float knockbackDuration;

    public bool isHurt = false;
    private bool isDead = false;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }

    #region hurt functions
    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderToPlayerVector)
    {
        isHurt = true;

        health -= damage;

        if (health > 0)
        {
            //animator.SetTrigger("hurt");
            knockback(knockbackPower, attackingColliderToPlayerVector);
        }
        else
        {
            Debug.Log("Player Dead");
            isDead = true;
            //animator.SetTrigger("dead");
            knockback(5f, attackingColliderToPlayerVector);
        }
    }

    void knockback(float power, Vector2 attackingColliderToPlayerVector)
    {
        rb.AddForce(attackingColliderToPlayerVector.normalized * knockbackDistance * power, ForceMode2D.Impulse);
        StartCoroutine(knockbackCo());
    }

    private IEnumerator knockbackCo()
    {
        yield return new WaitForSeconds(knockbackDuration);
        rb.velocity = Vector2.zero;
        isHurt = false;
    }

    #endregion
}
