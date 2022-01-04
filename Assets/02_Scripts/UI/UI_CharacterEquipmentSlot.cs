using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_CharacterEquipmentSlot : MonoBehaviour 
{
    public event EventHandler<OnItemDroppedEventArgs> OnItemEquipped;
    [SerializeField] UI_Inventory ui_Inventory;

    public class OnItemDroppedEventArgs : EventArgs 
    {
        public Item item;
    }

    public void EquipItemInSlot() 
    {
        Item item = ui_Inventory.GetItemPicked();
        OnItemEquipped?.Invoke(this, new OnItemDroppedEventArgs { item = item });
    }

}
