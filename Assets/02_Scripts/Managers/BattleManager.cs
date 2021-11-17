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
            Debug.Log("PELEA DEBUG, ESTA PELEA SOLO DEBERIA INICIARSE SI ES QUE NO SE HA ENCONTRADO INFORMACION RESPECTO AL ENEMYENCOUNTER");
            GameData.EnemyEncounter enemyEncounter = new GameData.EnemyEncounter
            {
                enemyBattleArray = new GameData.EnemyEncounter.EnemyBattle[] {
                    new GameData.EnemyEncounter.EnemyBattle { characterType = Character.Type.TESTENEMY, lanePosition = Battle.LanePosition.Middle },
                    new GameData.EnemyEncounter.EnemyBattle { characterType = Character.Type.TESTENEMY, lanePosition = Battle.LanePosition.Up },
                    new GameData.EnemyEncounter.EnemyBattle { characterType = Character.Type.TESTENEMY, lanePosition = Battle.LanePosition.Down },
                    //new GameData.EnemyEncounter.EnemyBattle { characterType = Character.Type.TESTENEMY, lanePosition = Battle.LanePosition.Top },
                    //new GameData.EnemyEncounter.EnemyBattle { characterType = Character.Type.TESTENEMY, lanePosition = Battle.LanePosition.Bottom },
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
