using UnityEngine;
using CodeMonkey.Utils;

public class EnemyOverworld : MonoBehaviour
{

    public static EnemyOverworld instance;

    private const float SPEED = 60f;

    private Character_Anims charAnim;
    private State state;
    private Vector3 targetMovePosition;
    private Character character;
    private PlayerOverworld playerOvermap;
    private Vector3 spawnPosition;
    private float roamDistanceMax;
    private Vector3 roamPosition;
    private float waitTimer;

    private enum State
    {
        Normal,
    }

    private void Awake()
    {
        instance = this;
        charAnim = gameObject.GetComponent<Character_Anims>();
        SetStateNormal();
    }

    public void Setup(Character character, PlayerOverworld playerOvermap)
    {
        this.character = character;
        this.playerOvermap = playerOvermap;
        //switch (character.type)
        //{
        //    default:
        //    case Character.Type.Enemy_MinionOrange:
        //        material.mainTexture = GameAssets.i.t_EnemyMinionOrange;
        //        charAnim.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dMinion_Idle, GameAssets.UnitAnimTypeEnum.dMinion_Walk, 1f, 1f);
        //        break;
        //    case Character.Type.Enemy_MinionRed:
        //        material.mainTexture = GameAssets.i.t_EnemyMinionRed;
        //        charAnim.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dMinion_Idle, GameAssets.UnitAnimTypeEnum.dMinion_Walk, 1f, 1f);
        //        break;
        //    case Character.Type.Enemy_Ogre:
        //        material.mainTexture = GameAssets.i.t_Ogre;
        //        charAnim.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dOgre_Idle, GameAssets.UnitAnimTypeEnum.dOgre_Walk, 1f, 1f);
        //        break;
        //    case Character.Type.Enemy_Zombie:
        //        material.mainTexture = GameAssets.i.t_Zombie;
        //        charAnim.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dZombie_Idle, GameAssets.UnitAnimTypeEnum.dZombie_Walk, 1f, 1.3f);
        //        break;
        //    case Character.Type.EvilMonster:
        //    case Character.Type.EvilMonster_2:
        //    case Character.Type.EvilMonster_3:
        //        material.mainTexture = GameAssets.i.t_EvilMonster;
        //        charAnim.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dBareHands_Idle, GameAssets.UnitAnimTypeEnum.dBareHands_Walk, 1f, 1f);
        //        transform.localScale = Vector3.one * 1.7f;
        //        break;
        //}
        spawnPosition = GetPosition();
        roamPosition = GetPosition();
        roamDistanceMax = 20f;

        if (character.type == Character.Type.NormalEnemy1 ||
            character.type == Character.Type.NormalEnemy2 ||
            character.type == Character.Type.NormalEnemy3)
        {
            roamDistanceMax = 5f;
        }

        SetTargetMovePosition(GetPosition());
    }

    public Character GetCharacter()
    {
        return character;
    }

    public void SaveCharacterPosition()
    {
        character.position = GetPosition();
    }

    private void Update()
    {
        if (!OverworldManager.IsOvermapRunning())
        {
            //Idle Anim
            return;
        }

        switch (state)
        {
            case State.Normal:
                HandleRoaming();
                HandleTargetMovePosition();
                HandleMovement();
                break;
        }
    }

    private void SetStateNormal()
    {
        state = State.Normal;
    }

    private void HandleRoaming()
    {
        float minRoamDistance = 5f;
        if (Vector3.Distance(GetPosition(), roamPosition) < minRoamDistance)
        {
            // Near roam position, wait
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                // Find new roam position
                Vector3 roamDir = UtilsClass.GetRandomDir();
                float roamDistance = UnityEngine.Random.Range(5f, roamDistanceMax);
                RaycastHit2D raycastHit = Physics2D.Raycast(GetPosition(), roamDir, roamDistance);
                if (raycastHit.collider != null)
                {
                    // Hit something
                    roamDistance = raycastHit.distance - 1f;
                    if (roamDistance <= 0f) roamDistance = 0f;
                }
                roamPosition = GetPosition() + roamDir * roamDistance;
                SetTargetMovePosition(roamPosition);
                waitTimer = UnityEngine.Random.Range(1f, 5f);
            }
        }
    }

    private void HandleTargetMovePosition()
    {
        float findTargetRange = 70f;
        if (character.type == Character.Type.NormalEnemy1 ||
            character.type == Character.Type.NormalEnemy2 ||
            character.type == Character.Type.NormalEnemy3)
        {
            findTargetRange = 60f;
        }
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < findTargetRange)
        {
            // Player within find target range
            SetTargetMovePosition(playerOvermap.GetPosition());
        }
        float attackRange = 10f;
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < attackRange)
        {
            // Player within attack/interact range
            HandleInteractionWithPlayer();
        }
    }

    private void HandleMovement()
    {
        float minMoveDistance = 5f;
        Vector3 moveDir = new Vector3(0, 0);
        if (Vector3.Distance(GetPosition(), targetMovePosition) > minMoveDistance)
        {
            moveDir = (targetMovePosition - GetPosition()).normalized;
        }

        bool isIdle = moveDir.x == 0 && moveDir.y == 0;
        if (isIdle)
        {
            // Idle Anim
        }
        else
        {
            //charAnim.PlayMoveAnim(moveDir); movimiento segun dirección IMPORTANTE
            transform.position += moveDir * SPEED * Time.deltaTime;
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetMovePosition(Vector3 targetMovePosition)
    {
        this.targetMovePosition = targetMovePosition;
    }


    private void HandleInteractionWithPlayer()
    {
        switch (character.type)
        {
            default:
                // Battle!
                Battle.LoadEnemyEncounter(character, character.enemyEncounter);
                break;
            case Character.Type.NormalEnemy1:
                {
                    //if (character.subType == Character.SubType.Enemy_HurtMeDaddy)
                    //{
                    //    // Special enemy
                    //    Cutscenes.Play_HurtMeDaddy(character);
                    //}
                    //else
                    //{
                    //}
                    // Normal battle
                    Battle.LoadEnemyEncounter(character, character.enemyEncounter);
                    break;
                }
            //case Character.Type.Enemy_MinionRed:
            //    {
            //        if (character.subType == Character.SubType.Enemy_HurtMeDaddy_2)
            //        {
            //            // Special enemy
            //            Cutscenes.Play_HurtMeDaddy_2(character);
            //        }
            //        else
            //        {
            //            // Normal battle
            //            BattleHandler.LoadEnemyEncounter(character, character.enemyEncounter);
            //        }
            //        break;
            //    }
            //case Character.Type.EvilMonster:
            //    {
            //        Cutscenes.Play_EvilMonster_1(character);
            //        break;
            //    }
            //case Character.Type.EvilMonster_2:
            //    {
            //        Cutscenes.Play_EvilMonster_2(character);
            //        break;
            //    }
            //case Character.Type.EvilMonster_3:
            //    {
            //        Cutscenes.Play_EvilMonster_3(character);
            //        break;
            //    }
        }
    }

}
