using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class BattleManager : MonoBehaviour
{
    private static BattleManager instance;
    public static BattleManager GetInstance()
    {
        return instance;
    }


    [SerializeField] Transform pAlly,pEnemy;
    [SerializeField] List<CharacterBattle> characters = new List<CharacterBattle>();

    CharacterBattle playerCharBattle, enemyCharBattle,activeCharacterBattle;

    private State state;
    private enum State
    {
        WaitingForPlayer,
        Busy
    }

    private void Awake()
    {
        instance = this;
    }
    private void Start()
    {
        playerCharBattle = SpawnCharacter(true);
        SpawnCharacter(true);
        SpawnCharacter(true);
        enemyCharBattle = SpawnCharacter(false);
        SpawnCharacter(false);
        SpawnCharacter(false);

        SetActiveCharacterBattle(playerCharBattle);
        CharacterListFill();
        state = State.WaitingForPlayer;
    }

    void CharacterListFill()
    {
        foreach (CharacterBattle obj in FindObjectsOfType(typeof(CharacterBattle)))
        {
            characters.Add(obj);
        }
    }

    private void Update()
    {
        if (state == State.WaitingForPlayer)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                state = State.Busy;
                playerCharBattle.Attack(enemyCharBattle, () =>
                {
                    ChooseNextActiveCharacter();
                });
            }
        }
    }

    private CharacterBattle SpawnCharacter(bool isPlayerTeam)
    {
        Vector3 position;
        Transform characterTransform;
        if (isPlayerTeam)
        {
            position = new Vector3(-5, 0);
            characterTransform = Instantiate(pAlly, position, Quaternion.identity);
        }
        else
        {
            position = new Vector3(5, 0);
            characterTransform = Instantiate(pEnemy, position, Quaternion.identity);
        }
        CharacterBattle charBattle = characterTransform.GetComponent<CharacterBattle>();

        charBattle.Setup(isPlayerTeam);

        return charBattle;
    }

    private void SetActiveCharacterBattle(CharacterBattle characterBattle)
    {
        if (activeCharacterBattle != null)
        {
            activeCharacterBattle.HideSelectionCircle();
        }
        activeCharacterBattle = characterBattle;
        activeCharacterBattle.ShowSelectionCircle();
    }

    private void ChooseNextActiveCharacter()
    {
        if (TestBattleOver())
        {
            return;
        }
        if (activeCharacterBattle == playerCharBattle)
        {
            SetActiveCharacterBattle(enemyCharBattle);
            state = State.Busy;

            enemyCharBattle.Attack(playerCharBattle, () =>
            {
                ChooseNextActiveCharacter();
            });
        }
        else
        {
            SetActiveCharacterBattle(playerCharBattle);
            state = State.WaitingForPlayer;
        }
    }

    private bool TestBattleOver()
    {
        if (playerCharBattle.IsDead())
        {

            return true;
        }
        if (enemyCharBattle.IsDead())
        {

            return true;
        }

        return false;
    }
}
