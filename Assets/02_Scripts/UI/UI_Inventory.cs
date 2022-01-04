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
    [SerializeField] private GameObject btn_Consumibles, btn_Equippables;

    [SerializeField] private RectTransform itemContainerConsumables, itemContainerEquippables;
    [SerializeField] Image upArrow, downArrow;
    //[SerializeField] GameObject equipables;
    [SerializeField] GameObject originalFirstPick;
    [SerializeField] InventoryPartyWindow inventoryPartyWindow;
    GameObject firstPick;

    private bool firstTime = false;

    [HideInInspector] public List<GameObject> itemListBtnsConsumables, itemListBtnsEquippables;

    private Navigation buttonsNav;

    private void Awake()
    {
        buttonsNav.mode = Navigation.Mode.Explicit;
        itemContainerConsumables.localPosition = new Vector2(itemContainerConsumables.localPosition.x, 0);
        itemContainerEquippables.localPosition = new Vector2(itemContainerEquippables.localPosition.x, 0);
    }

    private void OnEnable()
    {
        firstPick = originalFirstPick;
        if (!firstTime)
        {
            Debug.Log("Primera iniciacion del inventario consumible");
            firstTime = true;
            SetInventory();
        }
    }

    private void Start()
    {
        menuInventario.gameObject.SetActive(false);
        //equipables.SetActive(false);
    }

    public void SetInventory()
    {
        inventory = Inventory.instance;
        inventory.OnItemListChanged += Inventory_OnItemListChanged;
        RefrestInventory();
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

    public void SetFirstPickConsumable()
    {
        firstPick = originalFirstPick;
    }

    public void SetFirstPickEquippable()
    {
        firstPick = inventoryPartyWindow.GetFirstPick();
    }

    public void SetupPopUpWindow(Item item)
    {
        interactionController.popUpWindowController.UI_InventoryPopUp(this, item);
    }

    public GameObject GetBtnConsumibles()
    {
        return btn_Consumibles;
    }

    public GameObject GetBtnEquippables()
    {
        return btn_Equippables;
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
                float scrollPos = Mathf.Clamp(GetIndexConsumables() - 4, 0, GetIndexConsumables()) * selectedItem.GetComponent<ItemUI>().Height;
                if (itemContainerConsumables.gameObject.activeInHierarchy)
                {
                    itemContainerConsumables.localPosition = new Vector2(itemContainerConsumables.localPosition.x, scrollPos);
                }
                else if (itemContainerEquippables.gameObject.activeInHierarchy)
                {
                    itemContainerEquippables.localPosition = new Vector2(itemContainerEquippables.localPosition.x, scrollPos);
                }
            }


            if (itemContainerConsumables.gameObject.activeInHierarchy)
            {
                bool showUpArrow = GetIndexConsumables() > 4;
                upArrow.gameObject.SetActive(showUpArrow);

                bool showDownArrow = GetIndexConsumables() + 4 < itemListBtnsConsumables.Count;
                downArrow.gameObject.SetActive(showDownArrow);
            }
            else if (itemContainerEquippables.gameObject.activeInHierarchy)
            {
                bool showUpArrow = GetIndexEquippables() > 4;
                upArrow.gameObject.SetActive(showUpArrow);

                bool showDownArrow = GetIndexEquippables() + 4 < itemListBtnsEquippables.Count;
                downArrow.gameObject.SetActive(showDownArrow);
            }
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

    private int GetIndexConsumables()
    {
        return itemListBtnsConsumables.IndexOf(selectedItem);
    }

    private int GetIndexEquippables()
    {
        return itemListBtnsEquippables.IndexOf(selectedItem);
    }

    private void RefrestInventory()
    {
        Debug.Log("La cantidad de items en el inventario es: " + inventory.GetItemList().Count);

        if (inventory.GetItemList().Count != 0)
        {
            if (itemListBtnsConsumables.Count > 0)
            {
                foreach (GameObject item in itemListBtnsConsumables)
                {
                    Destroy(item);
                }
                itemListBtnsConsumables.Clear();
            }
            if (itemListBtnsEquippables.Count > 0)
            {
                foreach (GameObject item in itemListBtnsEquippables)
                {
                    Destroy(item);
                }
                itemListBtnsEquippables.Clear();
            }

            //////////////////////////////////////////////////// 
            Timing.RunCoroutine(_WaitOneFrame());
            //////////////////////////////////////////////////// 

            foreach (Item item in inventory.GetItemList())
            {
                if (item.GetItemSubtype(item) == Item.ItemSubType.Consumable)
                {
                    RectTransform itemTemplate = Instantiate(GameAssets.i.pf_ItemSlot, itemContainerConsumables.transform);
                    itemListBtnsConsumables.Add(itemTemplate.gameObject);
                    itemTemplate.GetComponent<ItemUI>().item = item;
                    Image icon = itemTemplate.Find("Icon").GetComponent<Image>();
                    SuperTextMesh texto = itemTemplate.Find("Texto").GetComponent<SuperTextMesh>();
                    icon.sprite = item.GetSprite();
                    texto.text = item.GetItemInfo();
                }
                else if (item.GetItemSubtype(item) == Item.ItemSubType.Equippable)
                {
                    RectTransform itemTemplate = Instantiate(GameAssets.i.pf_ItemSlot, itemContainerEquippables.transform);
                    itemListBtnsEquippables.Add(itemTemplate.gameObject);
                    itemTemplate.GetComponent<ItemUI>().item = item;
                    Image icon = itemTemplate.Find("Icon").GetComponent<Image>();
                    SuperTextMesh texto = itemTemplate.Find("Texto").GetComponent<SuperTextMesh>();
                    icon.sprite = item.GetSprite();
                    texto.text = item.GetItemInfo();
                }
            }
            Debug.Log("Se instanciaron los items");
            AssignBtnOrder();
        }
    }

    public void AssignBtnOrder()
    {
        for (int i = 0; i < itemListBtnsConsumables.Count; i++)
        {
            if (itemListBtnsConsumables[i] == itemListBtnsConsumables[0])
            {
                buttonsNav.selectOnUp = itemListBtnsConsumables[itemListBtnsConsumables.Count - 1].GetComponent<Selectable>();
            }
            else
            {
                buttonsNav.selectOnUp = itemListBtnsConsumables[(i - 1)].GetComponent<Selectable>();
            }

            if (itemListBtnsConsumables[i] == itemListBtnsConsumables[itemListBtnsConsumables.Count - 1])
            {
                buttonsNav.selectOnDown = itemListBtnsConsumables[0].GetComponent<Selectable>();
            }
            else
            {
                buttonsNav.selectOnDown = itemListBtnsConsumables[(i + 1)].GetComponent<Selectable>();
            }
            buttonsNav.selectOnRight = btn_Consumibles.GetComponent<Selectable>();
            itemListBtnsConsumables[i].GetComponent<Selectable>().navigation = buttonsNav;
        }

        for (int x = 0; x < itemListBtnsEquippables.Count; x++)
        {
            if (itemListBtnsEquippables[x] == itemListBtnsEquippables[0])
            {
                buttonsNav.selectOnUp = itemListBtnsEquippables[itemListBtnsEquippables.Count - 1].GetComponent<Selectable>();
            }
            else
            {
                buttonsNav.selectOnUp = itemListBtnsEquippables[(x - 1)].GetComponent<Selectable>();
            }

            if (itemListBtnsEquippables[x] == itemListBtnsEquippables[itemListBtnsEquippables.Count - 1])
            {
                buttonsNav.selectOnDown = itemListBtnsEquippables[0].GetComponent<Selectable>();
            }
            else
            {
                buttonsNav.selectOnDown = itemListBtnsEquippables[(x + 1)].GetComponent<Selectable>();
            }
            buttonsNav.selectOnRight = btn_Equippables.GetComponent<Selectable>();
            itemListBtnsEquippables[x].GetComponent<Selectable>().navigation = buttonsNav;
        }
    }

    public void NavigationAssignConsumables()
    {
        if (itemListBtnsConsumables.Count != 0)
        {
            buttonsNav.selectOnUp = btn_Consumibles.GetComponent<Selectable>().navigation.selectOnUp;
            buttonsNav.selectOnDown = btn_Consumibles.GetComponent<Selectable>().navigation.selectOnDown;
            buttonsNav.selectOnLeft = itemListBtnsConsumables[0].GetComponent<Selectable>();
            btn_Consumibles.GetComponent<Selectable>().navigation = buttonsNav;
            Timing.RunCoroutine(interactionController._EventSystemReAssign(itemListBtnsConsumables[0]));
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Error);
        }
    }

    public void NavigationAssignEquippables()
    {
        if (itemListBtnsEquippables.Count != 0)
        {
            buttonsNav.selectOnUp = btn_Equippables.GetComponent<Selectable>().navigation.selectOnUp;
            buttonsNav.selectOnDown = btn_Equippables.GetComponent<Selectable>().navigation.selectOnDown;
            buttonsNav.selectOnLeft = itemListBtnsEquippables[0].GetComponent<Selectable>();
            btn_Equippables.GetComponent<Selectable>().navigation = buttonsNav;
            Timing.RunCoroutine(interactionController._EventSystemReAssign(itemListBtnsEquippables[0]));
        }
        else
        {
            SoundManager.PlaySound(SoundManager.Sound.Error);
        }
    }

    IEnumerator<float> _WaitOneFrame()
    {
        yield return Timing.WaitForOneFrame;
    }
}
