using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using DG.Tweening;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
public class MenuManager : MonoBehaviour
{
    public static MenuManager instance;
    public enum MENUS
    {
        ITEMS,
        STATS,
        EQUIPO,
        EQUIPAMIENTO,
        MAPA,
        GUARDAR,
        OPCIONES,
        MAINMENU,

    }

    [HideInInspector] public MENUS menuState;

    public List<GameObject> menus = new List<GameObject>();
    [SerializeField] GameObject mainMenu;
    int index = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        switch (PlayerOverworld.instance.state)
        {
            case PlayerOverworld.State.Normal:
                HandleMenu();
                break;
            case PlayerOverworld.State.OnMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (!mainMenu.activeSelf)
                    {
                        OpenCloseMenu(false, menus[index]);
                    }
                    else
                    {
                        OpenCloseMenu(false, mainMenu);
                        PlayerOverworld.instance.SetStateNormal();
                        OverworldManager.GetInstance().ContinueOvermap();
                    }
                }
                break;
        }
    }

    public void SetIndex(int number)
    {
        index = number;
    }

    public IEnumerator<float> _EventSystemReAssign()
    {
        yield return Timing.WaitForOneFrame;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(menus[index]);

        yield break;
    }
    public void OpenCloseMenu(bool isOpen,GameObject menu)
    {
        menu.SetActive(isOpen);
    }

    public bool IsMainMenuOpen()
    {
        return mainMenu.activeSelf;
    }
    private void HandleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !IsMainMenuOpen())
        {
            OpenCloseMenu(true,mainMenu);
            PlayerOverworld.instance.state = PlayerOverworld.State.OnMenu;
            OverworldManager.GetInstance().StopOvermap();
        }
    }

    public void ExecuteMenuFunction()
    {
        switch(index)
        {
            case 0:     //ITEMS
                menuState = MENUS.ITEMS;
                OpenCloseMenu(false, mainMenu);

                break;
            case 1:     //STATS
                menuState = MENUS.STATS;
                OpenCloseMenu(false, mainMenu);
                break;
            case 2:     //EQUIPO
                menuState = MENUS.EQUIPO;
                OpenCloseMenu(false, mainMenu);
                break;
            case 3:     //EQUIPAMIENTO
                menuState = MENUS.EQUIPAMIENTO;
                OpenCloseMenu(false, mainMenu);
                break;
            case 4:     //MAPA
                menuState = MENUS.MAPA;
                OpenCloseMenu(false, mainMenu);
                break;
            case 5:     //GUARDAR
                menuState = MENUS.GUARDAR;
                OpenCloseMenu(false, mainMenu);
                break;
            case 6:     //OPCIONES
                menuState = MENUS.OPCIONES;
                OpenCloseMenu(false, mainMenu);
                break;
        }
    }

}
