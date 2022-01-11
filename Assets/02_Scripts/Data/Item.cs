using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item {

    public enum ItemType {
        MedicinalHerbs,
        EscamaMarina,

        Armor_1,
        Armor_2,
        Armor_3,
        Armor_4,
        Armor_5,

        Helmet_1,
        Helmet_2,
        Helmet_3,
        Helmet_4,
        Helmet_5,

        Weapon_1,
        Weapon_2,
        Weapon_3,
        Weapon_4,
        Weapon_5,
    }

    public enum ItemSubType
    {
        None,
        Consumable,
        Equippable
    }

    //public enum ItemCLASSSubType
    //{
    //    SuyaiEquipment,
    //    PedroEquipment,
    //    AranaEquipment,
    //    ChillpilaEquipment,
    //    AntayEquipment
    //}

    [HideInInspector] public ItemType itemType;
    [HideInInspector] public ItemSubType itemSubType;
    //[HideInInspector] public ItemCLASSSubType itemCLASSSubType;
    [HideInInspector] public int amount;
    private Vector3 position;
    private bool isDestroyed;

    public Item(ItemType itemType)
    {
        this.itemType = itemType;
        isDestroyed = false;
    }

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

    public ItemSubType GetItemSubtype(Item item)
    {
        switch (item.GetItemType())
        {
            default:

            //Consumibles
            case ItemType.MedicinalHerbs:
            case ItemType.EscamaMarina:
                return ItemSubType.Consumable;

            //Equipables
            case ItemType.Helmet_1:
            case ItemType.Helmet_2:
            case ItemType.Helmet_3:
            case ItemType.Helmet_4:
            case ItemType.Helmet_5:
            case ItemType.Armor_1:
            case ItemType.Armor_2:
            case ItemType.Armor_3:
            case ItemType.Armor_4:
            case ItemType.Armor_5:
            case ItemType.Weapon_1:
            case ItemType.Weapon_2:
            case ItemType.Weapon_3:
            case ItemType.Weapon_4:
            case ItemType.Weapon_5:
                return ItemSubType.Equippable;
        }
    }

    public CharacterEquipment.EquipSlot GetEquipSlot()
    {
        switch (itemType)
        {
            default:
            //case ItemType.ArmorNone:
            case ItemType.Armor_1:
            case ItemType.Armor_2:
            case ItemType.Armor_3:
            case ItemType.Armor_4:
            case ItemType.Armor_5:
                return CharacterEquipment.EquipSlot.Armor;
            //case ItemType.HelmetNone:
            case ItemType.Helmet_1:
            case ItemType.Helmet_2:
            case ItemType.Helmet_3:
            case ItemType.Helmet_4:
            case ItemType.Helmet_5:
                return CharacterEquipment.EquipSlot.Helmet;
            //case ItemType.SwordNone:
            case ItemType.Weapon_1:
            case ItemType.Weapon_2:
            case ItemType.Weapon_3:
            case ItemType.Weapon_4:
            case ItemType.Weapon_5:
                return CharacterEquipment.EquipSlot.Weapon;
        }
    }

    public Sprite GetSprite()
    {
        switch (itemType)
        {
            default:
            case ItemType.MedicinalHerbs:
                return GameAssets.i.item_Herb;
            case ItemType.Weapon_1:
                return GameAssets.i.item_Weapon1;
            case ItemType.Armor_1:
                return GameAssets.i.item_Armor1;
            case ItemType.Helmet_1:
                return GameAssets.i.item_Helmet1;
            case ItemType.EscamaMarina:
                return GameAssets.i.item_EscamaMarina;
        }
    }

    public int GetItemStats()
    {
        switch (itemType)
        {
            default:
            case ItemType.Weapon_1:
                return 1;
            case ItemType.Armor_1:
                return 1;
            case ItemType.Helmet_1:
                return 1;
        }
    }

    public string GetItemInfo()
    {
        switch (itemType)
        {
            default:
            case ItemType.MedicinalHerbs:
                return "Plantas que curan";
            case ItemType.EscamaMarina:
                return "Pertenece a CaiCai... ¿Tendra uso?";
            case ItemType.Weapon_1:
                return "Cultrun";
            case ItemType.Armor_1:
                return "Quetpám";
            case ItemType.Helmet_1:
                return "Trarilonco";
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
