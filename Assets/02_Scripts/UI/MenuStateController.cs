using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class MenuStateController : MonoBehaviour
{
    public enum MENUS
    {
        MainMenu,
        Consumibles,
        Equipo,
        Equipamiento,
        Mapa,
        Guardar,
        Opciones,

    }
    public MENUS menus;
    public MenuInteractionController interactionController;
    public GameObject firstPick;
    [SerializeField] PartyUIController partyController;

    private void OnEnable()
    {
        switch (menus)
        {
            case MENUS.MainMenu:
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign());
                break;
            case MENUS.Consumibles:
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(firstPick));
                break;
            case MENUS.Equipo:
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(partyController.topMenu));
                break;
            case MENUS.Equipamiento:
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign());
                break;
            //case MENUS.Mapa:
            //    Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign());
            //    break;
            //case MENUS.Guardar:
            //    Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign());
            //    break;
            //case MENUS.Opciones:
            //    Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(firstPick));
            //    break;
        }
    }   
}
