using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public event EventHandler OnItemListChanged;

    List<Item> itemList = new List<Item>();
    List<ItemUI> battleItemsList = new List<ItemUI>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    private void Start()
    {
        //AddItem(new Item(Item.ItemType.MedicinalHerbs, 1));
        //AddItem(new Item(Item.ItemType.MedicinalHerbs, 1));
        //AddItem(new Item(Item.ItemType.MedicinalHerbs, 1));
        //AddItem(new Item(Item.ItemType.MedicinalHerbs, 1));
        //AddItem(new Item(Item.ItemType.MedicinalHerbs, 1));
        AddItem(new Item(Item.ItemType.Weapon_1));
        AddItem(new Item(Item.ItemType.Armor_1));
        AddItem(new Item(Item.ItemType.Helmet_1));
    }

    //private void Update()
    //{
    //    Debug.Log("La cantidad de items en el inventario es: " + GetItemList().Count);
    //}

    public void AddItem(Item item)
    {
        itemList.Add(item);
        if (item.GetItemType() == Item.ItemType.MedicinalHerbs)
        {
            ResourceManager.instance.AddHerbs(1);
        }
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }
    
    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
        if (item.GetItemType() == Item.ItemType.MedicinalHerbs)
        {
            ResourceManager.instance.ConsumeHerbs(1);
        }
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public void ResetBattleItemList()
    {
        battleItemsList.Clear();
    }

    public List<ItemUI> GetBattleItemList()
    {
        return battleItemsList;
    }
}
