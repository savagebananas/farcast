using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float maxHealth = 100;
    public float health;
    private Rigidbody2D rb;
    public HealthBarUI healthBarUI;
    
    public float knockbackDistance;
    public float knockbackDuration;

    public bool isHurt = false;
    private bool isDead = false;

    public static PlayerBase instance;
    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }

        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        health = maxHealth;
    }

    void Update()
    {
        health = Mathf.Clamp(health, 0, maxHealth);
    }

    #region hurt/knockback
    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderToPlayerVector)
    {
        isHurt = true;
        health -= damage;
        healthBarUI.lerpTimer = 0f;

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

    public void RestoreHealth(float amount)
    {
        health += amount;
        healthBarUI.lerpTimer = 0f;
    }
}
