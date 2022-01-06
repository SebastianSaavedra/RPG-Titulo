using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using UnityEngine.EventSystems;

public class MenuInteractionController : MonoBehaviour
{
    public static MenuInteractionController instance;

    public enum MENUSTATE
    {
        Normal,
        OnMainMenu,
        OnInventoryMenu,
        OnPartyMenu,
        OnShopWindow,

    }

    [HideInInspector] public MENUSTATE state;

    public List<GameObject> botonesMenus = new List<GameObject>();
    public List<GameObject> subMenus = new List<GameObject>();
    public GameObject mainMenu;
    public PopUpWindowController popUpWindowController;
    public PartyUIController uiPartyController;
    [SerializeField] UI_Inventory ui_Inventory;
    [HideInInspector] public GameObject actualLaneOpened;
    int index = 0;
    GameObject lastMenuOpened, currentMenuOpened;
    bool isChoosingCharacter;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        switch (state)
        {
            case MENUSTATE.Normal:
                HandleMenu();
                break;
            case MENUSTATE.OnMainMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    OpenCloseMenu(false, mainMenu);
                    PlayerOverworld.instance.SetStateNormal();
                    OverworldManager.GetInstance().ContinueOvermap();
                    SetNormalState();
                    Debug.Log("Cerraste el menu principal");
                }
                break;
            case MENUSTATE.OnPartyMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (popUpWindowController.gameObject.activeInHierarchy)
                    {
                        Timing.RunCoroutine(_WaitOneFrameForWindowAction());

                        Timing.RunCoroutine(_EventSystemReAssign(uiPartyController.SaveGameObject()));
                    }
                    else if (isChoosingCharacter)
                    {
                        Timing.RunCoroutine(_EventSystemReAssign(uiPartyController.SaveGameObject()));
                        SetIsChoosingCharacterBool(false);
                    }
                    else
                    {
                        OpenCloseMenu(lastMenuOpened, subMenus[index]);
                        SetMainMenuState();
                        Debug.Log("Cerraste el menu de la party");
                    }
                }
                break;
            case MENUSTATE.OnInventoryMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (popUpWindowController.gameObject.activeInHierarchy)
                    {
                        Timing.RunCoroutine(_WaitOneFrameForWindowAction());

                        Timing.RunCoroutine(_EventSystemReAssign(ui_Inventory.GetSelectedItemGameObject()));
                    }
                    else if (isChoosingCharacter)
                    {
                        Timing.RunCoroutine(_EventSystemReAssign(ui_Inventory.GetSelectedItemGameObject()));
                        SetIsChoosingCharacterBool(false);
                    }
                    else
                    {
                        OpenCloseMenu(lastMenuOpened, subMenus[index]);
                        SetMainMenuState();
                        Debug.Log("Cerraste el inventario");
                    }
                }
                break;
            case MENUSTATE.OnShopWindow:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    UI_Shop.Hide_Static();
                    PlayerOverworld.instance.SetStateNormal();
                    OverworldManager.GetInstance().ContinueOvermap();
                    SetNormalState();
                    Debug.Log("Cerraste el menu de la tienda");
                }
                break;
        }
    }

    public void SetNormalState()
    {
        state = MENUSTATE.Normal;
    }
    public void SetMainMenuState()
    {
        state = MENUSTATE.OnMainMenu;
    }
    public void SetInventoryState()
    {
        state = MENUSTATE.OnInventoryMenu;
    }
    public void SetPartyState()
    {
        state = MENUSTATE.OnPartyMenu;
    }
    public void SetShopState()
    {
        state = MENUSTATE.OnShopWindow;
    }

    public bool GetIsChoosingCharacter()
    {
        return isChoosingCharacter;
    }

    public void SetIsChoosingCharacterBool(bool isChoosing)
    {
        isChoosingCharacter = isChoosing;
    }

    public IEnumerator<float> _WaitOneFrameForWindowAction()
    {
        yield return Timing.WaitForOneFrame;
        popUpWindowController.gameObject.SetActive(false);
    }

    public IEnumerator<float> _EventSystemReAssign()
    {
        yield return Timing.WaitForOneFrame;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(botonesMenus[index]);

        yield break;
    }
    public IEnumerator<float> _EventSystemReAssign(GameObject go)
    {
        yield return Timing.WaitForOneFrame;

        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(go);

        yield break;
    }
    public void OpenCloseMenu(bool isOpen, GameObject menu)
    {
        menu.SetActive(isOpen);
    }
    public void OpenCloseMenu(GameObject menuAbrir,GameObject menuCerrar)
    {
        menuCerrar.SetActive(false);
        menuAbrir.SetActive(true);
    }

    public bool IsMainMenuOpen()
    {
        return mainMenu.activeSelf;
    }

    private void HandleMenu()
    {
        if (Input.GetKeyDown(KeyCode.Return) && !IsMainMenuOpen() && OverworldManager.GetInstance().IsOvermapRunningNonStatic())
        {
            OpenCloseMenu(true,mainMenu);
            PlayerOverworld.instance.state = PlayerOverworld.State.Busy;
            SetMainMenuState();
            OverworldManager.GetInstance().StopOvermap();
        }
    }

    public void AssignIndexAndExecuteMenuFunction(int number)
    {
        index = number;
        if (lastMenuOpened == null)
        {
            lastMenuOpened = mainMenu;
        }
        currentMenuOpened = subMenus[index];
        OpenCloseMenu(currentMenuOpened, lastMenuOpened);
    }
}
