using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerInputs : MonoBehaviour
{
    public float attackDelay;

    public GameObject mainWeaponReference;

    public weaponAnimatorController swordWeaponAnimator;
    public weaponAnimatorController spearWeaponAnimator;
    public GameObject swordWeaponReference;
    public GameObject spearWeaponReference;

    public GameObject swordSlash;
    public Animator swordSlashAnimator;
    public Vector3 slashPositionVector = new Vector3(2, 0, 0);


    void Start()
    {
        swordSlashAnimator = swordSlash.GetComponent<Animator>();

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
        Quaternion slashRotation = mainWeaponReference.transform.rotation;
        swordWeaponAnimator.weaponAnimator.SetTrigger("Attack_1");
        Instantiate(swordSlash, transform.position + slashPositionVector, mainWeaponReference.transform.rotation);
        // Quaternion.Euler(slashRotation.x, slashRotation.y, slashRotation.z)
    }

}
