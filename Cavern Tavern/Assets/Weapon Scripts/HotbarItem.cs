using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class HotbarItem : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Debug.Log("use item");
            UseItem();

        }
    }
    public abstract void UseItem();
}
