using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MEC;
using DG.Tweening;
using CodeMonkey.Utils;

public class BattleUI : MonoBehaviour
{
    public static BattleUI  instance;

    public enum BATTLEMENUS
    {
        None,
        Submenu,

    }
    [HideInInspector] public BATTLEMENUS battleMenus;

    [Header("Botones Principales")]
    public List<GameObject> buttons = new List<GameObject>();

    [Header("Submenus")]
    [SerializeField] private GameObject submenuAttack, submenuSpecial, suyaiSpells, submenuInventory;//, chillpilaSpells, aranaSpells, antaySpells;

    [Header("SubBotones")]
    [SerializeField] List<SubButtons> subButtonsArray;

    [System.Serializable]
    public class SubButtons
    {
        public List<GameObject> subButtons = new List<GameObject>();
    }

    [HideInInspector] public string command;
    [HideInInspector] public string spellName;
    [HideInInspector] public GameObject radialMenu;
    int index = 0;
    [HideInInspector] public GameObject lastMenuActivated;

    private void Awake()
    {
        instance = this;
        radialMenu = gameObject;
        battleMenus = BATTLEMENUS.None;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[0]);
    }

    private void Update()
    {
        if (battleMenus == BATTLEMENUS.Submenu)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (Battle.GetInstance().state == Battle.State.WaitingForPlayer)
                {
                    Debug.Log("Retrocedio 1 paso");
                    if (lastMenuActivated != null)
                    {
                        lastMenuActivated.SetActive(false);
                    }
                    EventSystem.current.SetSelectedGameObject(null);
                    EventSystem.current.SetSelectedGameObject(buttons[index]);
                }
            }
        }
    }

    private void OnEnable()
    {
        Timing.RunCoroutine(_EventSystemReAssign(buttons[0]));
        //pedroSpells.SetActive(false);
        //chillpilaSpells.SetActive(false);
        //aranaSpells.SetActive(false);
        //antaySpells.SetActive(false);
        //suyaiSpells.SetActive(false);
        //lastMenuActivated = null;
    }

    public void SetIndex(int number)
    {
        index = number;
    }
    public void SetString(string spellName)
    {
        this.spellName = spellName;
    }

    IEnumerator<float> _EventSystemReAssign(GameObject objeto)
    {
        yield return Timing.WaitForOneFrame;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(objeto);
        yield break;
    }

    public void OpenLastWindow()
    {
        if (lastMenuActivated != null)
        {
            Debug.Log(lastMenuActivated.name);
            lastMenuActivated.SetActive(true);
            radialMenu.SetActive(true);
            Timing.RunCoroutine(_EventSystemReAssign(subButtonsArray[index].subButtons[0]));
        }
        else
        {
            Timing.RunCoroutine(_EventSystemReAssign(buttons[index]));
            radialMenu.SetActive(true);
        }
    }

    public void Command(string command)     //Comando a ejecutar
    {
        this.command = command;

        Timing.RunCoroutine(_WaitOneFrameForCommand());
    }

    IEnumerator<float> _WaitOneFrameForCommand()    //Ejecucion del comando
    {
        yield return Timing.WaitForOneFrame;

            switch (command)
        {
            case "Submenu.Attack":
                submenuAttack.SetActive(true);
                Timing.RunCoroutine(_EventSystemReAssign(subButtonsArray[0].subButtons[0]));
                battleMenus = BATTLEMENUS.Submenu;
                lastMenuActivated = submenuAttack;
                Debug.Log("Submenu de ataque activado");
                break;
            case "Submenu.Special":
                battleMenus = BATTLEMENUS.Submenu;
                switch (Battle.GetInstance().activeCharacterBattle.GetCharacterType())
                {
                    case Character.Type.Suyai:
                        Debug.Log("Submenu de habilidades activado");
                        lastMenuActivated = submenuSpecial;
                        submenuSpecial.SetActive(true);
                        suyaiSpells.SetActive(true);
                        Timing.RunCoroutine(_EventSystemReAssign(subButtonsArray[index].subButtons[0]));
                        break;
                    case Character.Type.Pedro:
                        ExecuteSpecial();
                        //Timing.RunCoroutine(_EventSystemReAssign(subButtons[3]));
                        break;
                    case Character.Type.Chillpila:
                        ExecuteSpecial();
                        //Timing.RunCoroutine(_EventSystemReAssign(subButtons[3]));
                        break;
                    case Character.Type.Arana:
                        ExecuteSpecial();
                        //Timing.RunCoroutine(_EventSystemReAssign(subButtons[3]));
                        break;
                    case Character.Type.Antay:
                        ExecuteSpecial();
                        //Timing.RunCoroutine(_EventSystemReAssign(subButtons[3]));
                        break;
                }
                break;
            case "Attack":
                Battle.GetInstance().state = Battle.State.EnemySelection;
                Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetAliveTeamCharacterBattleList(false)[0]);
                CloseBattleMenus();
                break;
            case "Defense":
                CloseBattleMenus();
                Battle.GetInstance()._Defense();
                break;
            case "Special":
                ExecuteSpecial();
                break;
            case "Inventory":
                if (Inventory.instance.GetBattleItemList().Count > 0)
                {
                    lastMenuActivated = submenuInventory;
                    submenuInventory.SetActive(true);
                    Debug.Log("Inventario se abrio");
                }
                else
                {
                    SoundManager.PlaySound(SoundManager.Sound.Error);
                }
                break;
            case "Run":
                Debug.Log("Huiste del combate");
                radialMenu.SetActive(false);
                //DataKeeper.instance.SetEscapedFromBattle(true);
                FunctionTimer.Create(OverworldManager.LoadBackToOvermap, 1f);
                break;
        }
        yield break;
    }

    public void CloseBattleMenus()
    {
        if (lastMenuActivated != null)
        {
            lastMenuActivated.SetActive(false);
        }
        radialMenu.SetActive(false);
    }

    void ExecuteSpecial()
    { 
        if (Battle.GetInstance().activeCharacterBattle.TrySpendSpecial())
        {
            switch (Battle.GetInstance().activeCharacterBattle.GetCharacterType())
            {
                case Character.Type.Arana:
                    radialMenu.SetActive(false);
                    Battle.GetInstance().state = Battle.State.EnemySelection;
                    Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetAliveTeamCharacterBattleList(false)[0]);
                    command = "Special";
                    break;

                case Character.Type.Suyai:
                    switch (spellName)
                    {
                        case "Curar":
                            radialMenu.SetActive(false);
                            Battle.GetInstance().state = Battle.State.AllySelection;
                            Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetAliveTeamCharacterBattleList(true)[0]);
                            Debug.Log("Cura individual");
                            break;
                        case "CuraGrupal":
                            radialMenu.SetActive(false);
                            Battle.GetInstance()._Special();
                            Debug.Log("CuraGrupal");
                            break;
                        case "Revivir":
                            if (Battle.GetInstance().GetDeadTeamCharacterBattleList(true).Count > 0)
                            {
                                radialMenu.SetActive(false);
                                Battle.GetInstance().state = Battle.State.DeadAllySelection;
                                Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetDeadTeamCharacterBattleList(true)[0]);
                                Debug.Log("Revivir");
                            }
                            else
                            {
                                SoundManager.PlaySound(SoundManager.Sound.Error);
                                Debug.Log("No tienes aliados muertos!");
                            }
                            break;
                        case "Turnos":
                            radialMenu.SetActive(false);
                            Battle.GetInstance()._Special();
                            Debug.Log("Turnos");
                            break;
                    }
                    break;

                default:
                    if (Battle.GetInstance().activeCharacterBattle.TrySpendSpecial())
                    {
                        Battle.GetInstance()._Special();
                    }
                    break;
            }
        }
    }
}
