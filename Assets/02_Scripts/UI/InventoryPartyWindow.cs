using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

[System.Serializable]
public class PartyWindowInventory
{
    [SerializeField] string laneName;

    public TextMeshProUGUI vida, ataque, defensa, turnos, critChance;//,experiencia,nivel;
    public Image characterSplashArt;
    public GameObject helmet, armor, weapon;
}

public class InventoryPartyWindow : MonoBehaviour
{
    public event EventHandler OnPartyStatsChanged;
    public event EventHandler<OnItemDroppedEventArgs> OnItemDropped;
    public class OnItemDroppedEventArgs : EventArgs
    {
        public Item item;
    }


    [SerializeField] PartyWindowInventory[] partyWindowInventories;
    [SerializeField] UI_Inventory ui_Inventory;
    //[SerializeField] UI_Equippables ui_Equippables;
    //Private non ser
    Image _characterSplashArt;
    TextMeshProUGUI _vida, _ataque, _defensa, _turnos,_critChance;//,experiencia,nivel;

    [HideInInspector] public Character topCharacter, midCharacter, bottomCharacter;
    [HideInInspector] public GameObject firstPick;


    private UI_CharacterEquipmentSlot weaponSlot;
    private UI_CharacterEquipmentSlot helmetSlot;
    private UI_CharacterEquipmentSlot armorSlot;
    private CharacterEquipment characterEquipment;

    private void OnEnable()
    {
        characterEquipment = CharacterEquipment.instance;
        foreach (Character character in GameData.characterList)
        {
            if (character.IsInPlayerTeam())
            {
                switch (character.lanePosition)
                {
                    case Character.LanePosition.Up:
                        _characterSplashArt = partyWindowInventories[0].characterSplashArt;
                        _vida = partyWindowInventories[0].vida;
                        _ataque = partyWindowInventories[0].ataque;
                        _defensa = partyWindowInventories[0].defensa;
                        _turnos = partyWindowInventories[0].turnos;
                        _critChance = partyWindowInventories[0].critChance;
                        topCharacter = character;
                        break;
                    case Character.LanePosition.Middle:
                        _characterSplashArt = partyWindowInventories[1].characterSplashArt;
                        _vida = partyWindowInventories[1].vida;
                        _ataque = partyWindowInventories[1].ataque;
                        _defensa = partyWindowInventories[1].defensa;
                        _turnos = partyWindowInventories[1].turnos;
                        _critChance = partyWindowInventories[1].critChance;
                        midCharacter = character;
                        break;
                    case Character.LanePosition.Down:
                        _characterSplashArt = partyWindowInventories[2].characterSplashArt;
                        _vida = partyWindowInventories[2].vida;
                        _ataque = partyWindowInventories[2].ataque;
                        _defensa = partyWindowInventories[2].defensa;
                        _turnos = partyWindowInventories[2].turnos;
                        _critChance = partyWindowInventories[2].critChance;
                        bottomCharacter = character;
                        break;
                }
                switch (character.type)
                {
                    case Character.Type.Suyai:
                        _characterSplashArt.sprite = GameAssets.i.splashSuyai;
                        TextSetup(character);

                        switch (character.lanePosition)
                        {
                            case Character.LanePosition.Up:
                                UI_CharacterEquipmentFunc(0);
                                break;
                            case Character.LanePosition.Middle:
                                UI_CharacterEquipmentFunc(1);
                                break;
                            case Character.LanePosition.Down:
                                UI_CharacterEquipmentFunc(2);
                                break;
                        }
                        break;

                    case Character.Type.Pedro:
                        _characterSplashArt.sprite = GameAssets.i.splashPedro;
                        TextSetup(character);
                        break;
                    case Character.Type.Chillpila:
                        _characterSplashArt.sprite = GameAssets.i.splashChillpila;
                        TextSetup(character);
                        break;
                    case Character.Type.Arana:
                        _characterSplashArt.sprite = GameAssets.i.splashArana;
                        TextSetup(character);
                        break;
                    case Character.Type.Antay:
                        _characterSplashArt.sprite = GameAssets.i.splashAntay;
                        TextSetup(character);
                        break;
                }
            }
        }
    }

    private void UI_CharacterEquipmentFunc(int i)
    {
        partyWindowInventories[i].helmet.SetActive(true);
        partyWindowInventories[i].armor.SetActive(true);
        partyWindowInventories[i].weapon.SetActive(true);

        helmetSlot = partyWindowInventories[i].helmet.GetComponent<UI_CharacterEquipmentSlot>();

        armorSlot = partyWindowInventories[i].armor.GetComponent<UI_CharacterEquipmentSlot>();

        weaponSlot = partyWindowInventories[i].weapon.GetComponent<UI_CharacterEquipmentSlot>();

        helmetSlot.OnItemEquipped += HelmetSlot_OnItemEquipped;
        armorSlot.OnItemEquipped += ArmorSlot_OnItemEquipped;
        weaponSlot.OnItemEquipped += WeaponSlot_OnItemEquipped;
        characterEquipment.OnEquipmentChanged += CharacterEquipment_OnEquipmentChanged;
        //Debug.Log("Se subscribieron eventos");

        UpdateVisual();
        firstPick = partyWindowInventories[i].helmet;
    }

    public GameObject GetFirstPick()
    {
        return firstPick;
    }

    private void OnDisable()
    {
        for (int i = 0; i < partyWindowInventories.Length; i++)
        {
            partyWindowInventories[i].helmet.SetActive(false);
            partyWindowInventories[i].armor.SetActive(false);
            partyWindowInventories[i].weapon.SetActive(false);
        }

        weaponSlot.OnItemEquipped -= WeaponSlot_OnItemEquipped;
        helmetSlot.OnItemEquipped -= HelmetSlot_OnItemEquipped;
        armorSlot.OnItemEquipped -= ArmorSlot_OnItemEquipped;
        characterEquipment.OnEquipmentChanged -= CharacterEquipment_OnEquipmentChanged;
        //Debug.Log("Se desubscribieron los eventos");
    }

    public void UseItemOnCharacter(string name)
    {
        switch (ui_Inventory.GetItemPicked().itemType)
        {
            case Item.ItemType.MedicinalHerbs:
                switch (name)
                {
                    case "Top":
                        Debug.Log("Curo al " + topCharacter.name);
                        topCharacter.GetHealthSystem().Heal(15);
                        ui_Inventory.GetInventory().RemoveItem(ui_Inventory.GetItemPicked());
                        break;
                    case "Middle":
                        Debug.Log("Curo al " + midCharacter.name);
                        midCharacter.GetHealthSystem().Heal(15);
                        ui_Inventory.GetInventory().RemoveItem(ui_Inventory.GetItemPicked());
                        break;
                    case "Bottom":
                        Debug.Log("Curo al " + bottomCharacter.name);
                        bottomCharacter.GetHealthSystem().Heal(15);
                        ui_Inventory.GetInventory().RemoveItem(ui_Inventory.GetItemPicked());
                        break;
                }
                break;
        }
        if (ui_Inventory.itemListBtnsConsumables.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(ui_Inventory.itemListBtnsConsumables[0].gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(ui_Inventory.GetBtnConsumibles());
        }
    }

    public void TextSetup(Character character)
    {
        _vida.SetText("Puntos de Vida: {0} / {1} ", character.stats.health, character.stats.healthMax);
        _ataque.SetText("Ataque: {0}", character.stats.attack);
        _defensa.SetText("Defensa: {0}", character.stats.defense);
        _turnos.SetText("Turnos: {0} ", character.stats.turns);
        _critChance.SetText("Critico: %{0} ", character.stats.critChance);
        OnPartyStatsChanged?.Invoke(this, EventArgs.Empty);
    }

    public void RefreshTextStatsAfterEquippinItem(Character character)
    {
        switch (character.lanePosition)
        {
            case Character.LanePosition.Up:
                partyWindowInventories[0].vida.SetText("Puntos de Vida: {0} / {1} ", character.stats.health, character.stats.healthMax);
                partyWindowInventories[0].ataque.SetText("Ataque: {0}", character.stats.attack);
                partyWindowInventories[0].defensa.SetText("Defensa: {0}", character.stats.defense);
                partyWindowInventories[0].turnos.SetText("Turnos: {0} ", character.stats.turns);
                partyWindowInventories[0].critChance.SetText("Critico: %{0} ", character.stats.critChance);
                break;
            case Character.LanePosition.Middle:
                partyWindowInventories[1].vida.SetText("Puntos de Vida: {0} / {1} ", character.stats.health, character.stats.healthMax);
                partyWindowInventories[1].ataque.SetText("Ataque: {0}", character.stats.attack);
                partyWindowInventories[1].defensa.SetText("Defensa: {0}", character.stats.defense);
                partyWindowInventories[1].turnos.SetText("Turnos: {0} ", character.stats.turns);
                partyWindowInventories[1].critChance.SetText("Critico: %{0} ", character.stats.critChance);
                break;
            case Character.LanePosition.Down:
                partyWindowInventories[2].vida.SetText("Puntos de Vida: {0} / {1} ", character.stats.health, character.stats.healthMax);
                partyWindowInventories[2].ataque.SetText("Ataque: {0}", character.stats.attack);
                partyWindowInventories[2].defensa.SetText("Defensa: {0}", character.stats.defense);
                partyWindowInventories[2].turnos.SetText("Turnos: {0} ", character.stats.turns);
                partyWindowInventories[2].critChance.SetText("Critico: %{0} ", character.stats.critChance);
                break;
        }
    }

    private void ArmorSlot_OnItemEquipped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        // Item dropped in Armor slot
        characterEquipment.TryEquipItem(CharacterEquipment.EquipSlot.Armor, e.item);
    }

    private void HelmetSlot_OnItemEquipped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        // Item dropped in Helmet slot
        characterEquipment.TryEquipItem(CharacterEquipment.EquipSlot.Helmet, e.item);
    }

    private void WeaponSlot_OnItemEquipped(object sender, UI_CharacterEquipmentSlot.OnItemDroppedEventArgs e)
    {
        // Item dropped in weapon slot
        characterEquipment.TryEquipItem(CharacterEquipment.EquipSlot.Weapon, e.item);
    }

    private void CharacterEquipment_OnEquipmentChanged(object sender, EventArgs e)
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        Item weaponItem = characterEquipment.GetWeaponItem();
        if (weaponItem != null)
        {
            for (int i = 0; i < partyWindowInventories.Length; i++)
            {
                partyWindowInventories[i].weapon.GetComponent<Button>().image.sprite = GameAssets.i.item_Weapon1;
            }
            //Debug.Log("Se actualizo el arma");
        }
        else
        {
            for (int i = 0; i < partyWindowInventories.Length; i++)
            {
                partyWindowInventories[i].weapon.GetComponent<Button>().image.sprite = null;
            }
        }

        Item armorItem = characterEquipment.GetArmorItem();
        if (armorItem != null)
        {
            for (int i = 0; i < partyWindowInventories.Length; i++)
            {
                partyWindowInventories[i].armor.GetComponent<Button>().image.sprite = GameAssets.i.item_Armor1;
            }
            //Debug.Log("Se actualizo la armadura");
        }
        else
        {
            for (int i = 0; i < partyWindowInventories.Length; i++)
            {
                partyWindowInventories[i].armor.GetComponent<Button>().image.sprite = null;
            }
        }

        Item helmetItem = characterEquipment.GetHelmetItem();
        if (helmetItem != null)
        {
            for (int i = 0; i < partyWindowInventories.Length; i++)
            {
                partyWindowInventories[i].helmet.GetComponent<Button>().image.sprite = GameAssets.i.item_Helmet1;
            }
            //Debug.Log("Se actualizo el casco");
        }
        else
        {
            for (int i = 0; i < partyWindowInventories.Length; i++)
            {
                partyWindowInventories[i].helmet.GetComponent<Button>().image.sprite = null;
            }
        }

        if (ui_Inventory.itemListBtnsEquippables.Count > 0)
        {
            EventSystem.current.SetSelectedGameObject(ui_Inventory.itemListBtnsEquippables[0].gameObject);
        }
        else
        {
            EventSystem.current.SetSelectedGameObject(ui_Inventory.GetBtnEquippables());
        }
    }
}
