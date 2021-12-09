using UnityEngine;
using CodeMonkey.Utils;

public class EnemyOverworld : MonoBehaviour
{

    public static EnemyOverworld instance;

    private const float SPEED = 5.5f;

    private Character_Anims charAnim;
    private Animator anim;
    private SpriteRenderer sprite;
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
        Busy
    }

    private void Awake()
    {
        instance = this;
        charAnim = gameObject.GetComponent<Character_Anims>();
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        SetStateNormal();
    }

    public void Setup(Character character, PlayerOverworld playerOvermap)
    {
        this.character = character;
        this.playerOvermap = playerOvermap;
        switch (character.type)
        {
            default:
            case Character.Type.TESTENEMY:
                sprite.sprite = GameAssets.i.spriteEnemy;
                break;
        }
        spawnPosition = GetPosition();
        roamPosition = GetPosition();
        roamDistanceMax = 5f;

        //if (character.type == Character.Type.NormalEnemy1)  // || character.type == Character.Type.NormalEnemy2 || character.type == Character.Type.NormalEnemy3
        //{
        //    roamDistanceMax = 5f;
        //}

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
        float minRoamDistance = 2f;
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
        float findTargetRange = 7.5f;
        //if (character.type == Character.Type.NormalEnemy1) //  || character.type == Character.Type.NormalEnemy2 || character.type == Character.Type.NormalEnemy3
        //{
        //    findTargetRange = 60f;
        //}
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < findTargetRange)
        {
            // Player within find target range
            SetTargetMovePosition(playerOvermap.GetPosition());
        }
        float attackRange = 1f;
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < attackRange)
        {
            // Player within attack/interact range
            HandleInteractionWithPlayer();
        }
    }

    private void HandleMovement()
    {
        float minMoveDistance = 1f;
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
                state = State.Busy;
                playerOvermap.state = PlayerOverworld.State.Busy;
                SoundManager.PlaySound(SoundManager.Sound.BattleTransition);
                Battle.LoadEnemyEncounter(character, character.enemyEncounter);
                break;
            //case Character.Type.TESTENEMY:
            //    {
            //        //if (character.subType == Character.SubType.Enemy_HurtMeDaddy)
            //        //{
            //        //    // Special enemy
            //        //    Cutscenes.Play_HurtMeDaddy(character);
            //        //}
            //        //else
            //        //{
            //        //}
            //        // Normal battle
            //        Battle.LoadEnemyEncounter(character, character.enemyEncounter);
            //        break;
            //    }
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
