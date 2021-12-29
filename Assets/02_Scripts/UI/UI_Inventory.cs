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
    }

    private void OnEnable()
    {
        if (!firstTime)
        {
            firstTime = true;
            itemContainer.localPosition = new Vector2(itemContainer.localPosition.x, 0);
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
        foreach (GameObject item in itemListBtns)
        {
            Destroy(item);
        }
        itemListBtns.Clear();

        if (inventory.GetItemList() != null)
        {
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
        }
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
