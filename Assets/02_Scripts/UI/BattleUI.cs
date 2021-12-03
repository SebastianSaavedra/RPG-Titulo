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

    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [HideInInspector] public string command;
    [HideInInspector] public GameObject radialMenu;
    int index = 0;

    private void Awake()
    {
        instance = this;
        radialMenu = gameObject;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[0]);
    }

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

    IEnumerator<float> _WaitOneFrameForCommand()
    {
        yield return Timing.WaitForOneFrame;

        switch(command)
        {
            case "Attack":
                Battle.GetInstance().state = Battle.State.EnemySelection;
                Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetAliveTeamCharacterBattleList(false)[0]);
                radialMenu.SetActive(false);
                break;
            case "Special":
                switch (Battle.GetInstance().activeCharacterBattle.GetCharacterType())
                {
                    case Character.Type.Arana:
                        if (Battle.GetInstance().activeCharacterBattle.TrySpendSpecial())
                        {
                            Battle.GetInstance().state = Battle.State.EnemySelection;
                            Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetAliveTeamCharacterBattleList(false)[0]);
                            radialMenu.SetActive(false);
                        }
                        break;
                    //case Character.Type.Suyai:
                    //    break;
                    default:
                        if (Battle.GetInstance().activeCharacterBattle.TrySpendSpecial())
                        {
                            Battle.GetInstance()._Special();
                            radialMenu.SetActive(false);
                        }
                        break;
                }
                break;
            case "Items":
                Debug.Log("Menu de items");
                break;
            case "Run":
                Debug.Log("Huiste del combate");
                FunctionTimer.Create(OverworldManager.LoadBackToOvermap, .7f);
                break;
        }
        yield break;
    }

}
