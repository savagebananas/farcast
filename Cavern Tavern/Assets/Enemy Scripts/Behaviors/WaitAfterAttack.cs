using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaitAfterAttack : EnemyState
{
    public WaitAfterAttack(GameObject enemy, GameObject player) : base(enemy, player) { }

    public override IEnumerator Start()
    {

        yield break;
    }
    public override IEnumerator Update()
    {

        yield break;
    }
}

