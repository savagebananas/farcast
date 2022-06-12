using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyState : MonoBehaviour
{
    protected StateMachineManager StateMachine;
    [SerializeField] public GameObject enemy;
    [SerializeField] public GameObject player;
    protected Animator animator;

    public EnemyState(GameObject Enemy, GameObject Player) 
    {
        enemy = Enemy;
        player = Player;
    }

    public virtual IEnumerator Start()
    {
        animator = enemy.GetComponent<Animator>();
        yield break;
    }
    public virtual IEnumerator Update()
    {
        yield break;
    }
}
