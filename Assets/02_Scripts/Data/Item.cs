using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public enum ItemType {
        MedicinalHerbs,
        Money,
        Relleno,

    }

    [HideInInspector] public  ItemType itemType;
    [HideInInspector] public int amount;
    private Vector3 position;
    private bool isDestroyed;

    public Item(ItemType itemType, int amount)
    {
        this.itemType = itemType;
        this.amount = amount;
        isDestroyed = false;
    }

    public Item(ItemType itemType, int amount, Vector3 position)
    {
        this.itemType = itemType;
        this.amount = amount;
        this.position = position;
        isDestroyed = false;
    }

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.MedicinalHerbs:
                return GameAssets.i.item_Herb;
            case ItemType.Relleno:
                return GameAssets.i.item_cualquiercosa;
        }
    }

    public string GetItemInfo()
    {
        switch (itemType)
        {
            default:
            case ItemType.MedicinalHerbs:
                return "Plantas que curan";
            case ItemType.Relleno:
                return "Items para testear el sistema del inventario";
        }
    }

    public ItemType GetItemType() {
        return itemType;
    }

    public int GetAmount() {
        return amount;
    }

    public void SetAmount(int value)
    {
        amount = value;
    }

    public Vector3 GetPosition() {
        return position;
    }

    public bool IsDestroyed() {
        return isDestroyed;
    }

    public void DestroySelf() {
        isDestroyed = true;
    }

}
