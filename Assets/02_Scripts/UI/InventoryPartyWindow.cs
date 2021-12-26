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

    public TextMeshProUGUI vida, ataque, defensa, turnos;//,experiencia,nivel;
    public Image characterSplashArt;
}

public class InventoryPartyWindow : MonoBehaviour
{
    public event EventHandler OnPartyStatsChanged;

    [SerializeField] PartyWindowInventory[] partyWindowInventories;
    [SerializeField] UI_Inventory ui_Inventory;
    //Private non ser
    Image _characterSplashArt;
    TextMeshProUGUI _vida, _ataque, _defensa, _turnos;//,experiencia,nivel;

    [HideInInspector] public Character topCharacter, midCharacter, bottomCharacter;

    private void OnEnable()
    {
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
                        topCharacter = character;
                        break;
                    case Character.LanePosition.Middle:
                        _characterSplashArt = partyWindowInventories[1].characterSplashArt;
                        _vida = partyWindowInventories[1].vida;
                        _ataque = partyWindowInventories[1].ataque;
                        _defensa = partyWindowInventories[1].defensa;
                        _turnos = partyWindowInventories[1].turnos;
                        midCharacter = character;
                        break;
                    case Character.LanePosition.Down:
                        _characterSplashArt = partyWindowInventories[2].characterSplashArt;
                        _vida = partyWindowInventories[2].vida;
                        _ataque = partyWindowInventories[2].ataque;
                        _defensa = partyWindowInventories[2].defensa;
                        _turnos = partyWindowInventories[2].turnos;
                        bottomCharacter = character;
                        break;
                }
                switch (character.type)
                {
                    case Character.Type.Suyai:
                        _characterSplashArt.sprite = GameAssets.i.splashSuyai;
                        TextSetup(character);
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
        EventSystem.current.SetSelectedGameObject(ui_Inventory.itemListBtns[0]);
    }

    private void TextSetup(Character character)
    {
        _vida.SetText("Puntos de Vida: {0} / {1} ", character.stats.health, character.stats.healthMax);
        _ataque.SetText("Ataque: {0}", character.stats.attack);
        _defensa.SetText("Defensa: {0}", character.stats.defense);
        _turnos.SetText("Turnos: {0} ", character.stats.turns);
        OnPartyStatsChanged?.Invoke(this, EventArgs.Empty);
    }
}
