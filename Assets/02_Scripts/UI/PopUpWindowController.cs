using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using MEC;

public class PopUpWindowController : MonoBehaviour // Le asigne monobehaviour porque decidi hardcodear el texto de la ventana
{
    public static PopUpWindowController instance;

    PartyUIController partyUIController;
    //List<GameObject> botones = new List<GameObject>();
    //Transform canvas;
    //public Transform window;

    [SerializeField] GameObject firstButton,statsWindow;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    //public void Setup(string pregunta, string boton)
    //{
    //    PlayerOverworld.instance.state = PlayerOverworld.State.SubMenu;
    //    gameObject.SetActive(true);
    //    this.pregunta.text = pregunta;
    //    this.pfBoton.transform.Find("Texto").GetComponent<TextMeshProUGUI>().text = boton;
    //    Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(this.pfBoton.gameObject));
    //}
    //public void Setup(string pregunta, string boton, MenuStateController menuStateController)
    //{
    //    PlayerOverworld.instance.state = PlayerOverworld.State.SubMenu;
    //    gameObject.SetActive(true);
    //    this.pregunta.text = pregunta;
    //    this.pfBoton.transform.Find("Texto").GetComponent<TextMeshProUGUI>().text = boton;
    //    this.menuStateController = menuStateController;
    //    Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(this.pfBoton.gameObject));
    //}
    //public void Setup(string pregunta, string[] opciones, MenuStateController menuStateController)
    //{
    //    PlayerOverworld.instance.state = PlayerOverworld.State.SubMenu;
    //    gameObject.SetActive(true);
    //    this.pregunta.text = pregunta;

    //    for (int i = 0; i <= opciones.Length; i++)
    //    {
    //        Instantiate(pfBoton, gameObject.transform);
    //    }
    //    //pfBoton.transform.Find("Texto").GetComponent<TextMeshProUGUI>().text = pfBoton;

    //    this.menuStateController = menuStateController;
    //    //Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(pfBoton));
    //}

    //public void Setup(string[] opciones)
    //{
    //    PlayerOverworld.instance.state = PlayerOverworld.State.SubMenu;

    //    //for (int i = 0; i < opciones.Length; i++)
    //    //{
    //    //    GameObject boton = UnityEngine.Object.Instantiate(GameAssets.i.pfPopUpWindowButton.gameObject,window.transform);

    //    //    botones.Add(boton);
    //    //}

    //    botones[0].transform.Find("Texto").GetComponent<TextMeshProUGUI>().text = "Ver Stats";
    //    botones[1].transform.Find("Texto").GetComponent<TextMeshProUGUI>().text = "Cambiar Compañero";

    //    Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(botones[0].gameObject));
    //}

    public void ActivatePartyWindow(PartyUIController partyUIController)
    {
        this.partyUIController = partyUIController;
        PlayerOverworld.instance.state = PlayerOverworld.State.SubMenu;
        gameObject.SetActive(true);
        Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(firstButton));
    }

    public void Trigger(string accion)
    {
        //switch(menuStateController.menus)
        //{
        //    case MenuStateController.MENUS.Equipo:
        //        Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(menuStateController.firstPick));
        //        break;
        //}

        switch (accion)
        {
            case "Stats":
                if (statsWindow.activeInHierarchy)
                {
                    statsWindow.SetActive(false);
                }
                statsWindow.SetActive(true);
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(partyUIController.SaveGameObject()));
                break;
            case "Cambiar":
                Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(partyUIController.menuStateController.firstPick));
                break;
        }
    }
}
