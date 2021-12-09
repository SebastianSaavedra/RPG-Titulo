using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using MEC;

public class PopUpWindowController : MonoBehaviour
{
    public static PopUpWindowController instance;

    [SerializeField] TextMeshProUGUI pregunta;
    [SerializeField] GameObject boton;
    MenuStateController menuStateController;

    private void Awake()
    {
        instance = this;
        gameObject.SetActive(false);
    }

    public void Setup(string pregunta, string boton)
    {
        PlayerOverworld.instance.state = PlayerOverworld.State.SubMenu;
        gameObject.SetActive(true);
        this.pregunta.text = pregunta;
        this.boton.transform.Find("Texto").GetComponent<TextMeshProUGUI>().text = boton;
        //menuStateController = menuController;
        Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(this.boton));
    }

    //public void Trigger()
    //{
    //    switch(menuStateController.menus)
    //    {
    //        case MenuStateController.MENUS.Equipo:
    //            Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(menuStateController.transform.Find("FirstPick").gameObject));
    //            break;
    //    }
    //}
}
