using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : HotbarItem
{
    public float healthAmount;
    private InventorySlot inventorySlot;
    public Animator referenceAnimator;

    public void Start()
    {
        player = GameObject.Find("Player");
        inventorySlot = player.GetComponent<PlayerHotbar>().hotbarSlots[hotbarIndex];
        referenceAnimator = GameObject.Find("Consumable Position Reference").GetComponent<Animator>();
    }
    public override void UseItem()
    {
        player.GetComponent<PlayerBase>().RestoreHealth(healthAmount);
        referenceAnimator.SetTrigger("ConsumeItem");
        StartCoroutine(RemoveFromStack());
    }

    IEnumerator RemoveFromStack()
    {
        yield return new WaitForSeconds(0.2f);
        inventorySlot.RemoveFromStack(1);
    }
}
