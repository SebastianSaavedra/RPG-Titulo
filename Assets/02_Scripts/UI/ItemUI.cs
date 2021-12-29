using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using MEC;

public class ItemUI : MonoBehaviour,ISelectHandler
{
    RectTransform rectTransform;
    UI_Inventory ui_Inventory;
    [HideInInspector] public Item item;
    public GameObject marcoBattleItem;

    public float Height => rectTransform.rect.height;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        ui_Inventory = gameObject.GetComponentInParent<UI_Inventory>();
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
        ui_Inventory.SetupPopUpWindow(item);
    }

    public void OnSelect(BaseEventData eventData)
    {
        Timing.RunCoroutine(_WaitOneFrame());
        Debug.Log(gameObject.name);
        if (eventData!=null && gameObject != null)
        ui_Inventory.SetSelectedItem(gameObject);
    }

    IEnumerator<float> _WaitOneFrame()
    {
        yield return Timing.WaitForOneFrame;
    }
}
