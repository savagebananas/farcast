using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weaponAnimatorController : MonoBehaviour
{
    public Animator weaponAnimator;
    private string currentState;
    public string weaponType;

    void Start()
    {
        weaponAnimator = GetComponent<Animator>();
    }

    public void changeAnimationState(string newState)
    {
        //stop the same animation from interrupting itself
        if (currentState == newState) return;

        //play the animation
        weaponAnimator.Play(newState);

        //reassign current state
        currentState = newState;
    }


}
