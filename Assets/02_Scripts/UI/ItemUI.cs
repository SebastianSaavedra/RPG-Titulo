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

    public float Height => rectTransform.rect.height;

    private void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    private void Start()
    {
        ui_Inventory = gameObject.GetComponentInParent<UI_Inventory>();
    }

    public void UseItem()
    {
        ui_Inventory.SelectCharacterToUseItem(item);
    }

    public void OnSelect(BaseEventData eventData)
    {
        if(eventData!=null)
        Timing.RunCoroutine(_WaitOneFrame());
        ui_Inventory.SetSelectedItem(gameObject);
    }

    IEnumerator<float> _WaitOneFrame()
    {
        yield return Timing.WaitForOneFrame;
    }
}
