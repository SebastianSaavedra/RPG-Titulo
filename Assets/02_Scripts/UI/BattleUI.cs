using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using MEC;

public class BattleUI : MonoBehaviour
{
    public static BattleUI  instance;

    [SerializeField] List<GameObject> buttons = new List<GameObject>();
    [HideInInspector] public string command;
    public GameObject radialMenu;

    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[0]);
    }

    private void Update()
    {
        switch(buttons.Count)
        {
            default:
                break;
            case 0:
                // HACER QUE APAREZCA LA DESCRIPCIÓN AL MOVERSE A TRAVES DEL MENU
                break;
        }
    }

    private void OnEnable()
    {
        Timing.RunCoroutine(_EventSystemReAssign());
    }

    IEnumerator<float> _EventSystemReAssign()
    {
        yield return Timing.WaitForOneFrame;
        Debug.Log("SI SE ACTIVA");
        EventSystem.current.SetSelectedGameObject(null);
        EventSystem.current.SetSelectedGameObject(buttons[0]);
        yield break;
    }

    public void Command(string command)
    {
        Timing.RunCoroutine(_WaitOneFrameForCommand());
        this.command = command;
    }

    IEnumerator<float> _WaitOneFrameForCommand()
    {
        yield return Timing.WaitForOneFrame;
        if (command == "Special" && Battle.GetInstance().activeCharacterBattle.GetCharacterType() != Character.Type.Player)
        {
            Battle.GetInstance()._Special();
        }
        else
        {
            Battle.GetInstance().state = Battle.State.EnemySelection;
            Battle.GetInstance().SetSelectedTargetCharacterBattle(Battle.GetInstance().GetAliveTeamCharacterBattleList(false)[0]);
        }
        radialMenu.SetActive(false);
        yield break;
    }

}
