//using System;
//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.UI;
//using MEC;
//using UnityEngine.EventSystems;

//public class UI_Equippables : MonoBehaviour
//{
//    //[SerializeField] private Inventory inventory;
//    //private GameObject selectedItem;
//    //private Item itemPicked;

//    //[SerializeField] private MenuInteractionController interactionController;
//    //[SerializeField] private GameObject btn_Equippables;

//    //[SerializeField] private RectTransform itemContainerEquippables;
//    //[SerializeField] Image upArrow, downArrow;
//    //private GameObject firstPick;

//    //private bool firstTime = false;

//    //[HideInInspector] public List<GameObject> itemListBtnsEquippables;

//    //private Navigation buttonsNav;

//    //private void Awake()
//    //{
//    //    buttonsNav.mode = Navigation.Mode.Explicit;
//    //    itemContainerEquippables.localPosition = new Vector2(itemContainerEquippables.localPosition.x, 0);
//    //}

//    //private void OnEnable()
//    //{

//    //    if (!firstTime)
//    //    {
//    //        Debug.Log("Primera iniciacion del inventario equipable");
//    //        firstTime = true;
//    //        SetInventory();
//    //    }
//    //}

//    //public void SetInventory()
//    //{
//    //    inventory.OnItemListChanged += Inventory_OnEquippableItemListChanged;
//    //    RefrestInventory();
//    //}

//    //private void Inventory_OnEquippableItemListChanged(object sender, EventArgs e)
//    //{
//    //    //Debug.Log("Los items equipables se han actualizado");
//    //    RefrestInventory();
//    //}

//    //private void OnDestroy()
//    //{
//    //    inventory.OnItemListChanged -= Inventory_OnEquippableItemListChanged;
//    //}

//    //public void SelectCharacterToUseItem(Item item)
//    //{
//    //    itemPicked = item;

//    //    EventSystem.current.SetSelectedGameObject(firstPick);
//    //}

//    //public void SetFirstPick(GameObject btn)
//    //{
//    //    firstPick = btn;
//    //}

//    //public GameObject GetBtnEquippables()
//    //{
//    //    return btn_Equippables;
//    //}

//    //public GameObject GetSelectedItemGameObject()
//    //{
//    //    return selectedItem;
//    //}

//    //public ItemUI GetSelectedItemGameObjectItemUI()
//    //{
//    //    return selectedItem.GetComponent<ItemUI>();
//    //}

//    //public Item GetItemPicked()
//    //{
//    //    return itemPicked;
//    //}

//    //public void RemoveItemUI(Item item)
//    //{
//    //    inventory.RemoveItem(item);
//    //}

//    //private void Update()
//    //{
//    //    if (selectedItem != null)
//    //    {
//    //        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
//    //        {
//    //            float scrollPos = Mathf.Clamp(GetIndexEquippables() - 4, 0, GetIndexEquippables()) * selectedItem.GetComponent<ItemUI>().Height;
//    //            itemContainerEquippables.localPosition = new Vector2(itemContainerEquippables.localPosition.x, scrollPos);
//    //        }

//    //        bool showUpArrow = GetIndexEquippables() > 4;
//    //        upArrow.gameObject.SetActive(showUpArrow);

//    //        bool showDownArrow = GetIndexEquippables() + 4 < itemListBtnsEquippables.Count;
//    //        downArrow.gameObject.SetActive(showDownArrow);
//    //    }
//    //}

//    //public void SetSelectedItem(GameObject gameObject)
//    //{
//    //    selectedItem = gameObject;
//    //}

//    //public Inventory GetInventory()
//    //{
//    //    return inventory;
//    //}

//    //private int GetIndexEquippables()
//    //{
//    //    return itemListBtnsEquippables.IndexOf(selectedItem);
//    //}

//    //private void RefrestInventory()
//    //{
//    //    if (inventory.GetItemList().Count != 0)
//    //    {
//    //        Debug.Log("Spawneando la lista en el UI");
//    //        foreach (GameObject item in itemListBtnsEquippables)
//    //        {
//    //            Destroy(item);
//    //        }
//    //        itemListBtnsEquippables.Clear();

//    //        //////////////////////////////////////////////////// 
//    //        Timing.RunCoroutine(_WaitOneFrame());
//    //        //////////////////////////////////////////////////// 

//    //        foreach (Item item in inventory.GetItemList())
//    //        {
//    //            if (item.GetItemSubtype(item) == Item.ItemSubType.Equippable)
//    //            {
//    //                RectTransform itemTemplate = Instantiate(GameAssets.i.pf_ItemSlot, itemContainerEquippables.transform);
//    //                itemListBtnsEquippables.Add(itemTemplate.gameObject);
//    //                itemTemplate.GetComponent<ItemUI>().item = item;
//    //                Image icon = itemTemplate.Find("Icon").GetComponent<Image>();
//    //                SuperTextMesh texto = itemTemplate.Find("Texto").GetComponent<SuperTextMesh>();
//    //                icon.sprite = item.GetSprite();
//    //                texto.text = item.GetItemInfo();
//    //            }
//    //        }
//    //        AssignBtnOrder();
//    //    }
//    //}

//    //IEnumerator<float> _WaitOneFrame()
//    //{
//    //    yield return Timing.WaitForOneFrame;
//    //}

//    //public void AssignBtnOrder()
//    //{
//    //    for (int i = 0; i < itemListBtnsEquippables.Count; i++)
//    //    {
//    //        if (itemListBtnsEquippables[i] == itemListBtnsEquippables[0])
//    //        {
//    //            buttonsNav.selectOnUp = itemListBtnsEquippables[itemListBtnsEquippables.Count - 1].GetComponent<Selectable>();
//    //        }
//    //        else
//    //        {
//    //            buttonsNav.selectOnUp = itemListBtnsEquippables[(i - 1)].GetComponent<Selectable>();
//    //        }

//    //        if (itemListBtnsEquippables[i] == itemListBtnsEquippables[itemListBtnsEquippables.Count - 1])
//    //        {
//    //            buttonsNav.selectOnDown = itemListBtnsEquippables[0].GetComponent<Selectable>();
//    //        }
//    //        else
//    //        {
//    //            buttonsNav.selectOnDown = itemListBtnsEquippables[(i + 1)].GetComponent<Selectable>();
//    //        }
//    //        buttonsNav.selectOnRight = btn_Equippables.GetComponent<Selectable>();
//    //        itemListBtnsEquippables[i].GetComponent<Selectable>().navigation = buttonsNav;
//    //    }
//    //}

//}
