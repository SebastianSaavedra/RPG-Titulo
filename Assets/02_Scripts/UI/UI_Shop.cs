using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MEC;
using TMPro;
using System.Collections.Generic;

[Serializable]
public class ItemsBtns
{
    public GameObject itemBtn;
    public TextMeshProUGUI CantidadTxt;
    public Image icon;
    public TextMeshProUGUI info;
}

public class UI_Shop : MonoBehaviour 
{
    public static UI_Shop instance;

    private Action onHide;
    private GameData.ShopContents shopContents;

    [SerializeField] ItemsBtns[] itemsBtns;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;


        Hide();
    }

    private void Start()
    {
        if (shopContents.healingHerbs > 0)
        {
            itemsBtns[0].icon.color = new Color(1, 1, 1, 1);
            itemsBtns[0].CantidadTxt.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
            itemsBtns[0].info.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, 1);
        }
        else
        {
            itemsBtns[0].icon.color = new Color(1, 1, 1, .3f);
            itemsBtns[0].CantidadTxt.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, .3f);
            itemsBtns[0].info.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, .3f);
        }
    }

    public void Buy_HealingHerbs() 
    {
        if (shopContents.healingHerbs > 0 && ResourceManager.instance.GetMoneyAmount() >= 5) 
        {
            shopContents.healingHerbs--;
            ResourceManager.instance.PayMoney(5);
            Inventory.instance.AddItem(new Item(Item.ItemType.MedicinalHerbs, 1));
            Refresh();
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Error);
        }
    }

    public void Buy_Item2()
    {
        Debug.Log("Compraste otro item");
    }

    public void Buy_Item3()
    {
        Debug.Log("Compraste otro item");
    }

    private void Refresh()
    {
        Timing.RunCoroutine(_WaitOneFrame());

        itemsBtns[0].CantidadTxt.SetText(shopContents.healingHerbs.ToString());

        if (shopContents.healingHerbs <= 0)
        {
            itemsBtns[0].icon.color = new Color(1, 1, 1, .3f);
            itemsBtns[0].CantidadTxt.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, .3f);
            itemsBtns[0].info.color = new Color(0.1960784f, 0.1960784f, 0.1960784f, .3f);
        }
    }

    public static void Hide_Static() => instance.Hide();

    private void Hide() 
    {
        gameObject.SetActive(false);
        if (onHide != null) 
        {
            onHide();
        }
    }

    public static void Show_Static(GameData.ShopContents shopContents, Action onHide) => instance.Show(shopContents, onHide);

    private void Show(GameData.ShopContents shopContents, Action onHide) 
    {
        this.shopContents = shopContents;
        this.onHide = onHide;
        MenuInteractionController.instance.SetShopState();
        gameObject.SetActive(true);
        Refresh();
        EventSystem.current.SetSelectedGameObject(null);
        Timing.RunCoroutine(_WaitOneFrame());
        EventSystem.current.SetSelectedGameObject(itemsBtns[0].itemBtn);
    }

    public IEnumerator<float> _WaitOneFrame()
    {
        yield return Timing.WaitForOneFrame;
    }
}
