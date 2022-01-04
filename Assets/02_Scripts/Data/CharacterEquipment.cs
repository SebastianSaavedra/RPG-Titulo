using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterEquipment : MonoBehaviour {

    public event EventHandler OnEquipmentChanged;

    public enum EquipSlot {
        Helmet,
        Armor,
        Weapon
    }

    [SerializeField] private PlayerOverworld player;
    //[SerializeField] private FollowerOverworld follower;

    private Item weaponItem;
    private Item helmetItem;
    private Item armorItem;

    private void Awake() 
    {
        if (gameObject.GetComponent<PlayerOverworld>())
        {
            player = gameObject.GetComponent<PlayerOverworld>();
        }
        //else if (gameObject.GetComponent<FollowerOverworld>())
        //{
        //    follower = gameObject.GetComponent<FollowerOverworld>();
        //}
    }

    //private void Start()
    //{
    //    if (follower != null)
    //    {
    //        Debug.Log(follower.GetCharacter().name + " Character Equipment");
    //    }
    //}

    public Item GetWeaponItem() {
        return weaponItem;
    }

    public Item GetHelmetItem() {
        return helmetItem;
    }

    public Item GetArmorItem() {
        return armorItem;
    }

    private void SetWeaponItem(Item weaponItem) {
        this.weaponItem = weaponItem;
        //if (player)
        //{
            player.SetEquipment(weaponItem.itemType);
            Debug.Log("Se equipo un arma");
        //}
        //else if (follower)
        //{
        //    follower.SetEquipment(weaponItem.itemType);
        //}
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetHelmetItem(Item helmetItem) {
        this.helmetItem = helmetItem;
        //if (player)
        //{
            player.SetEquipment(helmetItem.itemType);
            Debug.Log("Se equipo un casco");
        //}
        //else if (follower)
        //{
        //    follower.SetEquipment(helmetItem.itemType);
        //}
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetArmorItem(Item armorItem) {
        this.armorItem = armorItem;
        //if (player)
        //{
            player.SetEquipment(armorItem.itemType);
            Debug.Log("Se equipo una armadura");
        //}
        //else if (follower)
        //{
        //    follower.SetEquipment(armorItem.itemType);
        //}
        OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TryEquipItem(EquipSlot equipSlot, Item item) 
    {
        if (equipSlot == item.GetEquipSlot()) 
        {
            // Comprueba si el item encaja en la categoria
            switch (equipSlot) {
            default:
                case EquipSlot.Armor:
                    SetArmorItem(item);
                    break;
                case EquipSlot.Helmet:
                    SetHelmetItem(item);
                    break;
                case EquipSlot.Weapon:
                    SetWeaponItem(item);
                    break;
            }
        }
    }

}
