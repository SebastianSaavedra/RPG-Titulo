using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[System.Serializable]
public class BattleUI_ItemSlot
{
    public GameObject itemUI;

    public void IsActive()
    {
        itemUI.SetActive(true);
    }

    public TextMeshProUGUI info;
    public Image icon;
    public Item item;
}

public class BattleUI_Inventory : MonoBehaviour
{
    [SerializeField] BattleUI_ItemSlot[] battleUI_ItemSlots;

    private void OnEnable()
    {
        List<ItemUI> items =  Inventory.instance.GetBattleItemList();
        if (items.Count != 0)
        {
            for (int i = 0; i < items.Count; i++)
            {
                switch (battleUI_ItemSlots[i].itemUI.name)
                {
                    case "Item1":
                        battleUI_ItemSlots[0].IsActive();
                        battleUI_ItemSlots[0].item = items[0].GetItem();
                        battleUI_ItemSlots[0].info.SetText(items[0].GetItem().GetItemInfo());
                        battleUI_ItemSlots[0].icon.sprite = items[0].GetItem().GetSprite();
                        break;

                    case "Item2":
                        battleUI_ItemSlots[1].IsActive();
                        battleUI_ItemSlots[1].item = items[1].GetItem();
                        battleUI_ItemSlots[1].info.SetText(items[1].GetItem().GetItemInfo());
                        battleUI_ItemSlots[1].icon.sprite = items[1].GetItem().GetSprite();
                        break;

                    case "Item3":
                        battleUI_ItemSlots[2].IsActive();
                        battleUI_ItemSlots[2].item = items[2].GetItem();
                        battleUI_ItemSlots[2].info.SetText(items[2].GetItem().GetItemInfo());
                        battleUI_ItemSlots[2].icon.sprite = items[2].GetItem().GetSprite();
                        break;

                    case "Item4":
                        battleUI_ItemSlots[3].IsActive();
                        battleUI_ItemSlots[3].item = items[3].GetItem();
                        battleUI_ItemSlots[3].info.SetText(items[3].GetItem().GetItemInfo());
                        battleUI_ItemSlots[3].icon.sprite = items[3].GetItem().GetSprite();
                        break;
                }
            }
        }
    }
}
