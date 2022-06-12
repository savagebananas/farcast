using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHurt : EnemyState
{
    public EnemyHurt(GameObject enemy, GameObject player) : base(enemy, player) { }

    private float health;
    private float damage;

    public float knockbackDuration;
    public float knockbackDistance;
    public float knockbackPower;
    public Vector2 attackingColliderToEnemyVector;

    private EnemyState AttackPlayer;

    public override IEnumerator Start()
    {
        health = enemy.GetComponent<EnemyBase>().health;
        knockbackPower = player.GetComponent<playerAttack>().knockbackPower;

        AttackPlayer = enemy.GetComponent<EnemyBase>().AttackState;

        yield break;
    }
    public override IEnumerator Update()
    {
        hurt(damage, knockbackPower, attackingColliderToEnemyVector);

        yield break;
    }

    public void hurt(float damage, float knockbackPower, Vector2 attackingColliderToEnemyVector)
    {
        if (health > 0)
        {
            //animator.SetTrigger("hurt");
            knockback(knockbackPower);
            StateMachine.SetState(AttackPlayer); //after knockback, transition to attacking state
        }
        else
        {
            Debug.Log("Enemy Dead");
            //animator.SetTrigger("dead");
            knockback(5f);
        }
    }

    #region knockback functions
    void knockback(float power)
    {
        GetComponent<Rigidbody2D>().AddForce(player.GetComponent<playerAttack>().playerToWeaponReachVector.normalized * knockbackDistance * power, ForceMode2D.Impulse);
        StartCoroutine(knockbackCo());
        StartCoroutine(hurtCooldown());
    }

    private IEnumerator knockbackCo()
    {

        yield return new WaitForSeconds(knockbackDuration);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    private IEnumerator hurtCooldown()
    {
        yield return new WaitForSeconds(1);
    }
    #endregion 
}
