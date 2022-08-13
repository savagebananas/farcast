using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HotbarItem : MonoBehaviour
{
    public GameObject player;

    private void Start()
    {
        player = GameObject.Find("Player");
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
