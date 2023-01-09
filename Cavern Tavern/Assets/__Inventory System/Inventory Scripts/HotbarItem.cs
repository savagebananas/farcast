using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HotbarItem : MonoBehaviour
{
    [HideInInspector] public GameObject player;
    [HideInInspector] public GameObject weaponReference;
    [HideInInspector] public int hotbarIndex;
    private void Start()
    {
        player = GameObject.Find("Player");
        weaponReference = GameObject.Find("WeaponRotationReference");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
        }
    }
    public abstract void UseItem();
}
