using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Shop : MonoBehaviour, IInteractable
{
    public InventoryItemData[] availableItems;

    public GameObject canvas;
    [SerializeField] private GameObject shopUI;
    [SerializeField] private GameObject itemBackground;
    public GameObject shopSlotPrefab;

    public UnityAction<IInteractable> OnInteractionComplete { get; set; }


    private void Start()
    {
        canvas = GameObject.Find("Canvas");
        shopUI = ObjectFinder.FindObject(canvas, "Shop UI");
        itemBackground = ObjectFinder.FindObject(canvas, "Shop Item Background");
    }

    public void Interact(Interactor interactor, out bool interactSuccessful)
    {
        interactSuccessful = true;

        shopUI.SetActive(true);
        UpdateShopUI();
    }

    public void EndInteraction(Interactor interactor, out bool interactSuccessful)
    {
        interactSuccessful = true;

        shopUI.SetActive(false);
    }

    void UpdateShopUI()
    {
        foreach (Transform t in itemBackground.transform) 
        {
            GameObject.Destroy(t.gameObject);
        }

        for (int i = 0; i < availableItems.Length; i++)
        {
            var slot = Instantiate(shopSlotPrefab, new Vector2(0, 0), Quaternion.identity);
            slot.transform.parent = itemBackground.transform;

            ShopSlot_UI shopSlotUI = slot.GetComponent<ShopSlot_UI>();
            shopSlotUI.UpdateShopSlot(availableItems[i]);
        }
    }


}
