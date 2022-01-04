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
    UI_Inventory ui_Inventory;
    Item itemSelected;
    //List<GameObject> botones = new List<GameObject>();
    //Transform canvas;
    //public Transform window;

    [SerializeField] GameObject firstButton,statsWindow,btn2,setBattleItemBtn,giveItemBtn;

    private void Awake()
    {
        instance = this;
        firstButton.SetActive(true);
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
        firstButton.SetActive(true);
        btn2.SetActive(true);
        setBattleItemBtn.SetActive(false);
        giveItemBtn.SetActive(false);
        Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(firstButton));
    }

    public void UI_InventoryPopUp(UI_Inventory ui_Inventory, Item item)
    {
        this.ui_Inventory = ui_Inventory;
        itemSelected = item;
        PlayerOverworld.instance.state = PlayerOverworld.State.SubMenu;
        gameObject.SetActive(true);
        firstButton.SetActive(false);
        btn2.SetActive(false);
        setBattleItemBtn.SetActive(true);
        giveItemBtn.SetActive(true);
        Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(setBattleItemBtn));
    }

    public void Trigger(string accion)
    {
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

            case "ItemDeCombate":
                BattleItemLogic();
                break;

            case "Dar objeto":
                ui_Inventory.SelectCharacterToUseItem(itemSelected);
                break;
        }
    }

    void BattleItemLogic()
    {
        ItemUI lastItemInTheList;
        if (ui_Inventory.GetInventory().GetBattleItemList().Count < 4)
        {
            if (ui_Inventory.GetInventory().GetBattleItemList().Contains(ui_Inventory.GetSelectedItemGameObject().GetComponent<ItemUI>()))
            {
                SoundManager.PlaySound(SoundManager.Sound.Error);
            }
            else
            {
                ui_Inventory.GetInventory().GetBattleItemList().Add(ui_Inventory.GetSelectedItemGameObject().GetComponent<ItemUI>());
                ui_Inventory.GetSelectedItemGameObject().GetComponent<ItemUI>().GetBattleItemActiveImage().SetActive(true);
            }
        }
        else
        {
            lastItemInTheList = ui_Inventory.GetInventory().GetBattleItemList()[0];
            lastItemInTheList.GetBattleItemActiveImage().SetActive(false);
            ui_Inventory.GetInventory().GetBattleItemList().Remove(lastItemInTheList);

            ui_Inventory.GetSelectedItemGameObject().GetComponent<ItemUI>().GetBattleItemActiveImage().SetActive(true);
            ui_Inventory.GetInventory().GetBattleItemList().Add(ui_Inventory.GetSelectedItemGameObject().GetComponent<ItemUI>());

        }
        Timing.RunCoroutine(MenuInteractionController.instance._EventSystemReAssign(ui_Inventory.GetSelectedItemGameObject()));
        Debug.Log("La cantidad de items en la lista es: " + ui_Inventory.GetInventory().GetBattleItemList().Count);
    }
}
