using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HotbarItem : MonoBehaviour
{
    public GameObject player;
    public int hotbarIndex;
    private void Start()
    {
        player = GameObject.Find("Player");
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            UseItem();
            //Debug.Log("use item");
        }
    }
    public abstract void UseItem();
}
