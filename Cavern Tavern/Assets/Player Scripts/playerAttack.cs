using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerAttack : MonoBehaviour
{

    public Rigidbody2D playerRigidbody;

    private playerMovement playerMovement;


    public float attackCooldown;
    float nextAttackAllowedTime = 0;

    /*--------------------------*/
    /*Attack Animation Variables*/
    /*--------------------------*/

    public GameObject mainWeaponReference;
    public GameObject weaponSlashPosition;

    //General Weapon type references w/ animator attached to it
    public GameObject swordWeaponReference;
    public GameObject spearWeaponReference;

    //General Weapon Animators to trigger (ex: rotate weapon to look like sword swing)
    public weaponAnimatorController swordWeaponAnimator;
    public weaponAnimatorController spearWeaponAnimator;

    //Animations to instantiate
    public GameObject swordSlashAnimation;

    void Start()
    {
        playerMovement = gameObject.GetComponent<playerMovement>();

        swordWeaponAnimator = swordWeaponReference.GetComponent<weaponAnimatorController>();
        spearWeaponAnimator = spearWeaponReference.GetComponent<weaponAnimatorController>();

        //weapon slash position (can be changed based on weapon length)
        weaponSlashPosition.transform.position += new Vector3(2f, 0f);

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
        if (Time.time >= nextAttackAllowedTime && playerMovement.isDashing == false)
        {
            //trigger sword animator rotation
            swordWeaponAnimator.weaponAnimator.SetTrigger("Attack_1");
            
            //Instantiate slash position
            setSlashPosition();

            //reset cooldown to x seconds after attack
            nextAttackAllowedTime = Time.time + attackCooldown;
        }
    }

    void setSlashPosition()
    {
        weaponSlashPosition.transform.rotation = mainWeaponReference.transform.rotation;
        GameObject clone = (GameObject)Instantiate(swordSlashAnimation, weaponSlashPosition.transform.position, weaponSlashPosition.transform.rotation);
    }

}
