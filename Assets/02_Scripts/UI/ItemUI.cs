using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MEC;

public class ItemUI : MonoBehaviour,ISelectHandler
{
    RectTransform rectTransform;
    UI_Inventory ui_Inventory;
    //UI_Equippables ui_Equippables;
    [HideInInspector] public Item item;
    public GameObject marcoBattleItem;

    public float Height => rectTransform.rect.height;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        ui_Inventory = GameObject.Find("UI_Inventory").GetComponent<UI_Inventory>();
    }

    public GameObject GetBattleItemActiveImage()
    {
        return marcoBattleItem;
    }

    public Item GetItem()
    {
        return item;
    }

    public void UseItem()
    {
        if (item.GetItemSubtype(item) == Item.ItemSubType.Consumable)
        {
            ui_Inventory.SetupPopUpWindow(item);
        }
        else if (item.GetItemSubtype(item) == Item.ItemSubType.Equippable)
        {
            ui_Inventory.SelectCharacterToUseItem(item);
        }
    }

    public void OnSelect(BaseEventData eventData)
    {
        Timing.RunCoroutine(_WaitOneFrame());
        //Debug.Log(gameObject.name);

        if (eventData!=null && gameObject != null)
        {
            ui_Inventory.SetSelectedItem(gameObject);
        }
    }

    IEnumerator<float> _WaitOneFrame()
    {
        yield return Timing.WaitForOneFrame;
    }
}
