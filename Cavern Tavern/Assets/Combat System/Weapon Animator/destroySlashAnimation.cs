using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class destroySlashAnimation : MonoBehaviour
{
    Animator attachedAnimator;

    private void Awake()
    {
        attachedAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        Destroy(gameObject, attachedAnimator.runtimeAnimatorController.animationClips[0].length);
    }

}
