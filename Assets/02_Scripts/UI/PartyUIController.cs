using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;

public class PartyUIController : MonoBehaviour
{
    [SerializeField] MenuInteractionController interactionController;
    public MenuStateController menuStateController;
    public GameObject topMenu, midMenu, bottomMenu;

    //private
    Image pj;
    GameObject lastMenuPicked;
    Character topChar, midChar, bottomChar, pjEntra, suyai, pedro, chillpila, arana, antay;
    [HideInInspector] public Character pjActual;

    private void OnEnable()
    {
        UpdatePartyUI();
    }

    public void UpdatePartyUI() // Actualiza el UI de canvas para mostrar la party correcta en el orden correcto
    {
        foreach (Character character in GameData.characterList)
        {
            if (character.IsInPlayerTeam())
            {
                switch (character.lanePosition)
                {
                    case Character.LanePosition.Up:
                        pj = topMenu.transform.Find("Character").GetComponent<Image>();
                        topChar = character;
                        break;
                    case Character.LanePosition.Middle:
                        pj = midMenu.transform.Find("Character").GetComponent<Image>();
                        midChar = character;
                        break;
                    case Character.LanePosition.Down:
                        pj = bottomMenu.transform.Find("Character").GetComponent<Image>();
                        bottomChar = character;
                        break;
                }
                switch (character.type)
                {
                    case Character.Type.Suyai:
                        pj.sprite = GameAssets.i.splashSuyai;
                        break;
                    case Character.Type.Pedro:
                        pj.sprite = GameAssets.i.splashPedro;
                        break;
                    case Character.Type.Chillpila:
                        pj.sprite = GameAssets.i.splashChillpila;
                        break;
                    case Character.Type.Arana:
                        pj.sprite = GameAssets.i.splashArana;
                        break;
                    case Character.Type.Antay:
                        pj.sprite = GameAssets.i.splashAntay;
                        break;
                }
            }
            switch (character.type)
            {
                case Character.Type.Suyai:
                    suyai = character;
                    break;
                case Character.Type.Pedro:
                    pedro = character;
                    break;
                case Character.Type.Chillpila:
                    chillpila = character;
                    break;
                case Character.Type.Arana:
                    arana = character;
                    break;
                case Character.Type.Antay:
                    antay = character;
                    break;
            }
        }

    }

    public void SaveGameObjectButton(GameObject go)
    {
        lastMenuPicked = go;
        SaveGameObject();
    }
    public GameObject SaveGameObject()
    {
        return lastMenuPicked;
    }

    public void CharacterInLanePicked()
    {
        switch (lastMenuPicked.name)
        {
            case "Top":
                pjActual = topChar;
                break;
            case "Mid":
                pjActual = midChar;
                break;
            case "Bottom":
                pjActual = bottomChar;
                break;
        }
    }

    public void PopUpWindowSet(string comando)
    {
        switch (comando)
        {
            case "PartyOptions":
                interactionController.popUpWindowController.ActivatePartyWindow(this);
                break;
        }
    }
    public void SwitchCharacter(string charName)
    {
        switch (charName)
        {
            case "Suyai":
                pjEntra = suyai;
                WhichCharacterAndLanes(pjEntra, pjActual);
                return;
            case "Pedro":
                pjEntra = pedro;
                WhichCharacterAndLanes(pjEntra, pjActual);
                return;
            case "Chillpila":
                pjEntra = chillpila;
                WhichCharacterAndLanes(pjEntra, pjActual);
                return;
            case "Antay":
                pjEntra = antay;
                WhichCharacterAndLanes(pjEntra, pjActual);
                return;
            case "Arana":
                pjEntra = arana;
                WhichCharacterAndLanes(pjEntra, pjActual);
                return;
        }
    }

    private void WhichCharacterAndLanes(Character pjEntra, Character pjSale)
    {
        if (pjActual == suyai)
        {
            if (pjEntra.IsInPlayerTeam())
            {
                switch (SaveGameObject().name)
                {
                    case "Top":
                        pjSale.ChangeLane(pjEntra.lanePosition);
                        pjEntra.ChangeLane(Character.LanePosition.Up);
                        break;
                    case "Mid":
                        pjSale.ChangeLane(pjEntra.lanePosition);
                        pjEntra.ChangeLane(Character.LanePosition.Middle);
                        break;
                    case "Bottom":
                        pjSale.ChangeLane(pjEntra.lanePosition);
                        pjEntra.ChangeLane(Character.LanePosition.Down);
                        break;
                }
                Timing.RunCoroutine(interactionController._EventSystemReAssign(SaveGameObject()));
            }
            else
            {
                Debug.Log("NO PUEDES SACAR A SUYAI DEL TEAM");
                SoundManager.PlaySound(SoundManager.Sound.Error);
                Timing.RunCoroutine(interactionController._EventSystemReAssign(SaveGameObject()));
                //mensaje de q no puedes sacar a suyai del team
            }
        }
        else
        {
            if (pjEntra.IsInPlayerTeam())
            {
                switch (SaveGameObject().name)
                {
                    case "Top":
                        pjSale.ChangeLane(pjEntra.lanePosition);
                        pjEntra.ChangeLane(Character.LanePosition.Up);
                        break;
                    case "Mid":
                        pjSale.ChangeLane(pjEntra.lanePosition);
                        pjEntra.ChangeLane(Character.LanePosition.Middle);
                        break;
                    case "Bottom":
                        pjSale.ChangeLane(pjEntra.lanePosition);
                        pjEntra.ChangeLane(Character.LanePosition.Down);
                        break;
                }
            }
            else
            {
                pjEntra.AssignIsInPlayerTeam(true);
                switch (SaveGameObject().name)
                {
                    case "Top":
                        pjSale.AssignIsInPlayerTeam(false);
                        pjEntra.ChangeLane(Character.LanePosition.Up);
                        break;
                    case "Mid":
                        pjSale.AssignIsInPlayerTeam(false);
                        pjEntra.ChangeLane(Character.LanePosition.Middle);
                        break;
                    case "Bottom":
                        pjSale.AssignIsInPlayerTeam(false);
                        pjEntra.ChangeLane(Character.LanePosition.Down);
                        break;
                }

            }
            Timing.RunCoroutine(interactionController._EventSystemReAssign(SaveGameObject()));
        }
        UpdatePartyUI();
    }
}
