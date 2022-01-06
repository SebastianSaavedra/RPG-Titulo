using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;

public class MenuStateController : MonoBehaviour
{
    public enum MENUS
    {
        MainMenu,
        Inventario,
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
    [SerializeField] WindowCharacterStats windowCharacterStats;

    private void OnEnable()
    {
        switch (menus)
        {
            case MENUS.MainMenu:
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign());
                break;
            case MENUS.Inventario:
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(firstPick));
                break;
            case MENUS.Equipo:
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(partyController.topMenu));
                if (windowCharacterStats.gameObject.activeInHierarchy)
                {
                    windowCharacterStats.gameObject.SetActive(false);
                }
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
