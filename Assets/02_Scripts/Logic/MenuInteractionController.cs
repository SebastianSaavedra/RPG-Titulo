using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using DG.Tweening;
using TMPro;
using System.Linq;
using UnityEngine.EventSystems;
public class MenuInteractionController : MonoBehaviour
{
    public static MenuInteractionController instance;

    public List<GameObject> botonesMenus = new List<GameObject>();
    public List<GameObject> subMenus = new List<GameObject>();
    public GameObject mainMenu;
    public PopUpWindowController popUpWindowController;
    public MenuStateController uiPartyController;
    public GameObject topMenu, midMenu, bottomMenu;
    int index = 0;
    GameObject lastMenuOpened, currentMenuOpened;

    private void Awake()
    {
        instance = this;
    }

    private void Update()
    {
        switch (PlayerOverworld.instance.state)
        {
            case PlayerOverworld.State.Normal:
                HandleMenu();
                break;
            case PlayerOverworld.State.OnMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    if (!mainMenu.activeSelf)
                    {
                        OpenCloseMenu(lastMenuOpened, subMenus[index]);
                    }
                    else
                    {
                        OpenCloseMenu(false, mainMenu);
                        PlayerOverworld.instance.SetStateNormal();
                        OverworldManager.GetInstance().ContinueOvermap();
                    }
                }
                break;
            case PlayerOverworld.State.SubMenu:
                if (Input.GetKeyDown(KeyCode.Escape))
                {
                    Timing.RunCoroutine(_WaitOneFrameForWindowAction());
                    Timing.RunCoroutine(_EventSystemReAssign(uiPartyController.SaveGameObject()));
                }
                break;
        }
    }

    public IEnumerator<float> _WaitOneFrameForWindowAction()
    {
        yield return Timing.WaitForOneFrame;
        popUpWindowController.gameObject.SetActive(false);
        PlayerOverworld.instance.state = PlayerOverworld.State.OnMenu;
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
        EventSystem.current.SetSelectedGameObject(go.gameObject);

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
        if (Input.GetKeyDown(KeyCode.Return) && !IsMainMenuOpen())
        {
            OpenCloseMenu(true,mainMenu);
            PlayerOverworld.instance.state = PlayerOverworld.State.OnMenu;
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
    public void ChangeTeam(bool IsInTeam)
    {
        foreach (Character character in GameData.characterList)
        {
            switch (character.type)
            {
                case Character.Type.Suyai:
                    character.AssignIsInPlayerTeam(IsInTeam);
                    break;
                case Character.Type.Pedro:
                    character.AssignIsInPlayerTeam(IsInTeam);
                    break;
                case Character.Type.Arana:
                    character.AssignIsInPlayerTeam(IsInTeam);
                    break;
                case Character.Type.Chillpila:
                    character.AssignIsInPlayerTeam(IsInTeam);
                    break;
                case Character.Type.Antay:
                    character.AssignIsInPlayerTeam(IsInTeam);
                    break;
            }
        }
    }
}
