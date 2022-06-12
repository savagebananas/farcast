using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class StateMachineManager : MonoBehaviour
{
    protected EnemyState State;

    public void Start()
    {
        if (State != null)
        {
            State.Start();
        }
    }
    public void Update()
    {
        if(State != null)
        {
            State.Update();
        }
    }
    public void SetState(EnemyState state)
    {
        if (state != null)
        {
            State = state;
            StartCoroutine(State.Start());
        }
    }
}
