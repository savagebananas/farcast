using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthConsumable : HotbarItem
{
    /*
    Any item that can be consumed for health 
    */

    public float healthAmount;
    private InventorySlot inventorySlot;
    public Animator referenceAnimator;
    [SerializeField] SpriteRenderer itemSprite;

    [SerializeField] float cooldownTime;
    float nextCooldownEndTime = 0f;

    public void Start()
    {
        player = GameObject.Find("Player");
        inventorySlot = player.GetComponent<PlayerHotbar>().equippedItemSlot;
        referenceAnimator = GameObject.Find("Consumable Position Reference").GetComponent<Animator>();
    }

    private void Update()
    {
        if (Time.time >= nextCooldownEndTime)
        {
            itemSprite.color = new Color(1f, 1f, 1f, 1f);

            if (Input.GetMouseButtonDown(0)) UseItem();
        }
    }

    public override void UseItem()
    {
            StartCoroutine(ConsumeItem());
    }

    IEnumerator ConsumeItem()
    {
        nextCooldownEndTime = Time.time + cooldownTime;
        referenceAnimator.SetTrigger("ConsumeItem");//play animation first
        yield return new WaitForSeconds(0.2f); //wait for animation to end
        player.GetComponent<PlayerBase>().RestoreHealth(healthAmount); //Add health amount to player health
        inventorySlot.RemoveFromStack(1); //Take one out of inventory slot
        itemSprite.color = new Color(1f, 1f, 1f, 0f); //Make sprite invisible until cooldown over
    }
}
