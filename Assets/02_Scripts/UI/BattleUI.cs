using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MEC;
using DG.Tweening;

public class BattleUI : MonoBehaviour
{
    public static BattleUI  instance;

    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [HideInInspector] public string command;
    public GameObject radialMenu;
    int index = 0;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[0]);
    }

    //private void Update()
    //{
    //    switch(index)
    //    {
    //        default:
    //            break;
    //        case 0:
    //            // HACER QUE APAREZCA LA DESCRIPCIÓN AL MOVERSE A TRAVES DEL MENU
    //            break;
    //    }
    //}

    private void OnEnable()
    {
        Timing.RunCoroutine(_EventSystemReAssign());
    }

    public void SetIndex(int number)
    {
        index = number;
    }

    IEnumerator<float> _EventSystemReAssign()
    {
        yield return Timing.WaitForOneFrame;
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[index]);
        yield break;
    }

    public void Command(string command)
    {
        this.command = command;
        Timing.RunCoroutine(_WaitOneFrameForCommand());
    }

    IEnumerator<float> _WaitOneFrameForCommand()        //  A FUTURO PODRIAS INCLUIR PARAMETROS PARA DEFINIR QUE COMANDO SE ELIGIO
    {
        yield return Timing.WaitForOneFrame;
        if (command == "Special" && Battle.GetInstance().activeCharacterBattle.GetCharacterType() == Character.Type.Arana)
        {
            Battle.GetInstance().state = Battle.State.EnemySelection;
            Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetAliveTeamCharacterBattleList(false)[0]);
            radialMenu.SetActive(false);
        }
        else if (command == "Special"  && Battle.GetInstance().activeCharacterBattle.TrySpendSpecial())
        {
            Battle.GetInstance()._Special();
            radialMenu.SetActive(false);
        }
        if (command == "Attack")
        {
            Battle.GetInstance().state = Battle.State.EnemySelection;
            Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetAliveTeamCharacterBattleList(false)[0]);
            radialMenu.SetActive(false);
        }
        if (command == "Items")
        {
            Debug.Log("Menu de items");
        }
        if (command == "Run")
        {
            Debug.Log("Huiste del combate");
        }
        yield break;
    }

}
