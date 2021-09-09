using UnityEngine;
using CodeMonkey.MonoBehaviours;

public class BattleManager : MonoBehaviour
{

    [SerializeField] private CameraFollow cameraFollow;

    private void Awake()
    {
        new Battle();

        if (Battle.enemyEncounter == null)
        {
            Debug.Log("PELEA DEBUG");
            GameData.EnemyEncounter enemyEncounter = new GameData.EnemyEncounter
            {
                enemyBattleArray = new GameData.EnemyEncounter.EnemyBattle[] {
                    new GameData.EnemyEncounter.EnemyBattle { characterType = Character.Type.TESTENEMY, lanePosition = Battle.LanePosition.Middle },
                    new GameData.EnemyEncounter.EnemyBattle { characterType = Character.Type.TESTENEMY, lanePosition = Battle.LanePosition.Up },
                    new GameData.EnemyEncounter.EnemyBattle { characterType = Character.Type.TESTENEMY, lanePosition = Battle.LanePosition.Down },
                },
            };
            Battle.LoadEnemyEncounter(new Character(Character.Type.TESTENEMY), enemyEncounter);
        }
    }

    private void Start()
    {
        Battle.GetInstance().Start(cameraFollow);
    }

    private void Update()
    {
        Battle.GetInstance().Update();
    }

}
