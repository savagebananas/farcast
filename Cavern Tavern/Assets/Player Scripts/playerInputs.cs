using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputs : MonoBehaviour
{
    public float attackDelay;
    public weaponAnimatorController swordWeaponAnimator;
    public weaponAnimatorController spearWeaponAnimator;
    public GameObject swordWeaponReference;
    public GameObject spearWeaponReference;


    void Start()
    {
        swordWeaponAnimator = swordWeaponReference.GetComponent<weaponAnimatorController>();
        spearWeaponAnimator = spearWeaponReference.GetComponent<weaponAnimatorController>();
    }

    void Update()
    {
        leftMouseButtonPressed();
    }

    void leftMouseButtonPressed()
    {
        //trigger attack and interact functions
        if (Input.GetMouseButtonDown(0))
        {
            attack();
            //interact();
        }
    }

    void attack()
    {
        swordWeaponAnimator.weaponAnimator.SetTrigger("Attack_1");
    }

    IEnumerator waitForSeconds(float seconds)
    {
        yield return new WaitForSeconds(seconds);
    }
}
