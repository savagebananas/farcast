using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyAnimationObject : MonoBehaviour
{
    public Animator animator;

    void Update()
    {
        Destroy(gameObject, animator.runtimeAnimatorController.animationClips[0].length);
    }

}
