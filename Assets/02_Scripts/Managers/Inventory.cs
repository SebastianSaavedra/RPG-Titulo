using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    public static Inventory instance;

    public event EventHandler OnItemListChanged;

    List<Item> itemList = new List<Item>();
    private List<ItemUI> battleItemsList = new List<ItemUI>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
    }

    //private void Start()
    //{
    //    AddItem(new Item(Item.ItemType.MedicinalHerbs, 1));
    //}

    //public void SetAction(Action<Item> useItemAction)
    //{
    //    this.useItemAction = useItemAction;
    //}

    public void AddItem(Item item)
    {
        itemList.Add(item);
        OnItemListChanged?.Invoke(this,EventArgs.Empty);
    }
    
    public void RemoveItem(Item item)
    {
        itemList.Remove(item);
        OnItemListChanged?.Invoke(this, EventArgs.Empty);
    }

    public List<Item> GetItemList()
    {
        return itemList;
    }

    public List<ItemUI> GetBattleItemList()
    {
        return battleItemsList;
    }
}
