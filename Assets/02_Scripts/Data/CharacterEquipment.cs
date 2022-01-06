using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CharacterEquipment : MonoBehaviour 
{
    public static CharacterEquipment instance;
    public event EventHandler OnEquipmentChanged;

    public enum EquipSlot 
    {
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
        if (instance == null)
        {
            instance = this;
        }
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;

        //if (gameObject.GetComponent<PlayerOverworld>())
        //{
        //    player = gameObject.GetComponent<PlayerOverworld>();
        //}
        //else if (gameObject.GetComponent<FollowerOverworld>())
        //{
        //    follower = gameObject.GetComponent<FollowerOverworld>();
        //}
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (GameObject.Find("pfPlayer"))
        {
            player = GameObject.Find("pfPlayer").GetComponent<PlayerOverworld>();
        }
    }

    //private void Start()
    //{
    //    if (follower != null)
    //    {
    //        Debug.Log(follower.GetCharacter().name + " Character Equipment");
    //    }
    //}

    public Item GetWeaponItem() 
    {
        return weaponItem;
    }

    public Item GetHelmetItem() 
    {
        return helmetItem;
    }

    public Item GetArmorItem()
    {
        return armorItem;
    }

    private void SetWeaponItem(Item weaponItem)
    {
        if (this.weaponItem == null || this.weaponItem != weaponItem)
        {
            this.weaponItem = weaponItem;
            //if (player)
            //{
            player.SetEquipment(weaponItem);
            Debug.Log("Se equipo un arma");
            //}
            //else if (follower)
            //{
            //    follower.SetEquipment(weaponItem.itemType);
            //}
            OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Error);
        }
    }

    private void SetHelmetItem(Item helmetItem)
    {
        if (this.helmetItem == null || this.helmetItem != helmetItem)
        {
            this.helmetItem = helmetItem;
            //if (player)
            //{
            player.SetEquipment(helmetItem);
            Debug.Log("Se equipo un casco");
            //}
            //else if (follower)
            //{
            //    follower.SetEquipment(helmetItem.itemType);
            //}
            OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Error);
        }
    }

    private void SetArmorItem(Item armorItem)
    {
        if (this.armorItem == null || this.armorItem != armorItem)
        {
            this.armorItem = armorItem;
            //if (player)
            //{
            player.SetEquipment(armorItem);
            Debug.Log("Se equipo una armadura");
            //}
            //else if (follower)
            //{
            //    follower.SetEquipment(armorItem.itemType);
            //}
            OnEquipmentChanged?.Invoke(this, EventArgs.Empty);
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Error);
        }
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