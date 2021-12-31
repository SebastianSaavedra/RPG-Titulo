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
    //[HideInInspector] public ItemUI item1, item2, item3, item4;

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

    public void ResetBattleItemList()
    {
        battleItemsList.Clear();
    }

    //public void AddObjectAsReference(int x, ItemUI itemUI)
    //{
    //    switch (x)
    //    {
    //        case 0:
    //            GameObject item1 = itemUI.gameObject;
    //            this.item1 = item1.GetComponent<ItemUI>();
    //            break;
    //        case 1:
    //            GameObject item2 = itemUI.gameObject;
    //            this.item2 = item2.GetComponent<ItemUI>();
    //            break;
    //        case 2:
    //            GameObject item3 = itemUI.gameObject;
    //            this.item3 = item3.GetComponent<ItemUI>();
    //            break;
    //        case 3:
    //            GameObject item4 = itemUI.gameObject;
    //            this.item4 = item4.GetComponent<ItemUI>();
    //            break;
    //    }
    //}

    public List<ItemUI> GetBattleItemList()
    {
        return battleItemsList;
    }
}
