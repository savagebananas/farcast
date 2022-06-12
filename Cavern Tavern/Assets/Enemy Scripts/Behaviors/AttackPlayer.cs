using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : EnemyState
{
    public AttackPlayer(GameObject enemy,  GameObject player) : base(enemy, player) { }


    public float attackRange;
    private float speed;

    private EnemyState WaitAfterAttack; 

    public override IEnumerator Start()
    {
        speed = enemy.GetComponent<EnemyBase>().speed;
        WaitAfterAttack = enemy.GetComponent<EnemyBase>().WaitAfterAttack;

        yield break;
    }
    public override IEnumerator Update()
    {
        if (Vector2.Distance((Vector2)enemy.transform.position, (Vector2)player.transform.position) > attackRange) //outside of attackrange
        {
            followPlayer();
        }
        if (Vector2.Distance((Vector2)enemy.transform.position, (Vector2)player.transform.position) <= attackRange) //within attack range
        {
            //attack
            StateMachine.SetState(WaitAfterAttack);
        }
        

        yield break;
    }

    void followPlayer()
    {
        enemy.transform.position = Vector2.MoveTowards(enemy.transform.position, player.transform.position, speed * Time.deltaTime);
    }
}

