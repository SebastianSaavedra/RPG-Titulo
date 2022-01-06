using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UI_Shop : MonoBehaviour 
{
    public static UI_Shop instance;

    private Action onHide;
    private GameData.ShopContents shopContents;

    [SerializeField] GameObject itemBtn1, itemBtn2, itemBtn3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;

        Color colorBtn1 = itemBtn1.transform.Find("Icon").GetComponent<Image>().color;
        colorBtn1.a = 1f;

        Hide();
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
        itemBtn1.transform.Find("Cantidad").GetComponent<TextMeshProUGUI>().text = shopContents.healingHerbs.ToString();
        if (shopContents.healingHerbs <= 0)
        {
            Color colorBtn1 = itemBtn1.transform.Find("Icon").GetComponent<Image>().color;
            colorBtn1.a = .33f;
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
        gameObject.SetActive(true);
        Refresh();
    }
}
