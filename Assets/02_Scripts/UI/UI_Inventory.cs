using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using MEC;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour
{
    private Inventory inventory;
    private GameObject selectedItem;
    private Item itemPicked;

    [SerializeField] private MenuInteractionController interactionController;
    [SerializeField] private MenuStateController menuInventario;
    [SerializeField] private GameObject btn_Consumibles;

    [SerializeField] private RectTransform itemContainer;
    [SerializeField] Image upArrow, downArrow;
    [SerializeField] InventoryPartyWindow inventoryPartyWindow;
    [SerializeField] GameObject firstPick;

    private bool firstTime = false;

    [HideInInspector] public List<GameObject> itemListBtns;

    private Navigation customNav;

    private void Awake()
    {
        customNav.mode = Navigation.Mode.Explicit;
        itemContainer.localPosition = new Vector2(itemContainer.localPosition.x, 0);
    }

    private void OnEnable()
    {
        if (!firstTime)
        {
            firstTime = true;
            SetInventory();
        }
    }

    public void SetInventory()
    {
        inventory = Inventory.instance;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefrestInventory();
        if (!inventoryPartyWindow.gameObject.activeInHierarchy)
        {
            inventoryPartyWindow.gameObject.SetActive(true);
        }
        if (menuInventario.gameObject.activeInHierarchy)
        {
            menuInventario.gameObject.SetActive(false);
        }
    }

    private void Inventory_OnItemListChanged(object sender, System.EventArgs e)
    {
        RefrestInventory();
    }

    private void OnDestroy()
    {
        inventory.OnItemListChanged -= Inventory_OnItemListChanged;
    }

    public void SelectCharacterToUseItem(Item item)
    {
        itemPicked = item;

        EventSystem.current.SetSelectedGameObject(firstPick);

    }
    public void SetupPopUpWindow(Item item)
    {
        interactionController.popUpWindowController.UI_InventoryPopUp(this, item);
    }

    public GameObject GetBtnConsumibles()
    {
        return btn_Consumibles;
    }

    public GameObject GetSelectedItemGameObject()
    {
        return selectedItem;
    }

    public ItemUI GetSelectedItemGameObjectItemUI()
    {
        return selectedItem.GetComponent<ItemUI>();
    }

    public Item GetItemPicked()
    {
        return itemPicked;
    }

    public void RemoveItemUI(Item item)
    {
        inventory.RemoveItem(item);
    }

    private void Update()
    {
        if (selectedItem != null)
        {
            if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            {
                float scrollPos = Mathf.Clamp(GetIndex() - 4, 0, GetIndex()) * selectedItem.GetComponent<ItemUI>().Height;
                itemContainer.localPosition = new Vector2(itemContainer.localPosition.x, scrollPos);
            }

            bool showUpArrow = GetIndex() > 4;
            upArrow.gameObject.SetActive(showUpArrow);

            bool showDownArrow = GetIndex() + 4 < itemListBtns.Count;
            downArrow.gameObject.SetActive(showDownArrow);
        }
    }

    public void SetSelectedItem(GameObject gameObject)
    {
        selectedItem = gameObject;
    }

    public Inventory GetInventory()
    {
        return inventory;
    }

    private int GetIndex()
    {
        return itemListBtns.IndexOf(selectedItem);
    }

    private void RefrestInventory()
    {
        if (inventory.GetItemList() != null)
        {
            foreach (GameObject item in itemListBtns)
            {
                Destroy(item);
            }
            itemListBtns.Clear();

            foreach (Item item in inventory.GetItemList())
            {
                RectTransform itemTemplate = Instantiate(GameAssets.i.pf_ItemSlot, itemContainer.transform);
                itemListBtns.Add(itemTemplate.gameObject);
                itemTemplate.GetComponent<ItemUI>().item = item;
                Image icon = itemTemplate.Find("Icon").GetComponent<Image>();
                SuperTextMesh texto = itemTemplate.Find("Texto").GetComponent<SuperTextMesh>();
                icon.sprite = item.GetSprite();
                texto.text = item.GetItemInfo();
            }
            AssignBtnOrder();

            //////////////////////////////////////////////////// 
            Timing.RunCoroutine(_WaitOneFrame());
            //////////////////////////////////////////////////// 
            ///
            //Debug.Log("El tama�o de la lista es: " + inventory.GetBattleItemList().Count);

            //if (inventory.GetBattleItemList().Count > 0)
            //{
            //    for (int i = 0; i < Inventory.instance.GetBattleItemList().Count; i++)
            //    {
            //        Debug.Log("Antes del switch");
            //        switch (i)
            //        {
            //            default:
            //                Debug.Log("Esto no tuvo que haber ocurrido" + i);
            //                break;

            //            case 0:
            //                inventory.item1 = Inventory.instance.GetBattleItemList()[0];
            //                inventory.AddObjectAsReference(i, GetSelectedItemGameObjectItemUI());
            //                inventory.item1.GetBattleItemActiveImage().SetActive(true);
            //                break;
            //            case 1:
            //                inventory.item2 = Inventory.instance.GetBattleItemList()[1];
            //                inventory.AddObjectAsReference(i, GetSelectedItemGameObjectItemUI());
            //                inventory.item2.GetBattleItemActiveImage().SetActive(true);
            //                break;
            //            case 2:
            //                Inventory.instance.item3 = Inventory.instance.GetBattleItemList()[2];
            //                Inventory.instance.AddObjectAsReference(i, GetSelectedItemGameObjectItemUI());
            //                inventory.item3.GetBattleItemActiveImage().SetActive(true);
            //                break;
            //            case 3:
            //                Inventory.instance.item4 = Inventory.instance.GetBattleItemList()[3];
            //                Inventory.instance.AddObjectAsReference(i, GetSelectedItemGameObjectItemUI());
            //                inventory.item4.GetBattleItemActiveImage().SetActive(true);
            //                break;
            //        }
            //        Debug.Log("Antes del switch");
            //        Timing.RunCoroutine(_WaitOneFrame());
            //    }
            //}
        }
    }

    IEnumerator<float> _WaitOneFrame()
    {
        yield return Timing.WaitForOneFrame;
    }

    public void AssignBtnOrder()
    {
        for (int i = 0; i < itemListBtns.Count; i++)
        {
            if (itemListBtns[i] == itemListBtns[0])
            {
                customNav.selectOnUp = itemListBtns[itemListBtns.Count - 1].GetComponent<Selectable>();
            }
            else
            {
                customNav.selectOnUp = itemListBtns[(i - 1)].GetComponent<Selectable>();
            }

            if (itemListBtns[i] == itemListBtns[itemListBtns.Count - 1])
            {
                customNav.selectOnDown = itemListBtns[0].GetComponent<Selectable>();
            }
            else
            {
                customNav.selectOnDown = itemListBtns[(i + 1)].GetComponent<Selectable>();
            }
            customNav.selectOnRight = btn_Consumibles.GetComponent<Selectable>();
            itemListBtns[i].GetComponent<Selectable>().navigation = customNav;
        }
    }

    public void NavigationAssign()
    {
        if (itemListBtns.Count != 0)
        {
            customNav.selectOnUp = btn_Consumibles.GetComponent<Selectable>().navigation.selectOnUp;
            customNav.selectOnDown = btn_Consumibles.GetComponent<Selectable>().navigation.selectOnDown;
            customNav.selectOnLeft = itemListBtns[0].GetComponent<Selectable>();
            btn_Consumibles.GetComponent<Selectable>().navigation = customNav;
            Timing.RunCoroutine(interactionController._EventSystemReAssign(itemListBtns[0]));
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Error);
        }
    }
}
