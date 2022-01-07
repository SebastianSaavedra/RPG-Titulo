using UnityEngine;
using CodeMonkey.Utils;

public class EnemyOverworld : MonoBehaviour
{

    public static EnemyOverworld instance;

    private const float SPEED = 5.5f;

    private Character_Anims charAnim;
    private Animator anim;
    private SpriteRenderer sprite;
    [HideInInspector] public State state;
    private Vector3 targetMovePosition;
    private Character character;
    private PlayerOverworld playerOvermap;
    private Vector3 spawnPosition;
    private float roamDistanceMax;
    private Vector3 roamPosition;
    private float waitTimer;
    private float timer = 2f;
    [SerializeField] private LayerMask wallLayerMask;


    public enum State
    {
        Normal,
        Waiting,
        Busy,
    }

    private void Awake()
    {
        instance = this;
        charAnim = gameObject.GetComponent<Character_Anims>();
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        SetStateWaiting();
    }

    public void Setup(Character character, PlayerOverworld playerOvermap)
    {
        this.character = character;
        this.playerOvermap = playerOvermap;
        switch (character.type)
        {
            case Character.Type.TESTENEMY:
                sprite.sprite = GameAssets.i.testEnemySprite;
                break;
            case Character.Type.Fusilero:
                sprite.sprite = GameAssets.i.fusileroOWSprite;
                break;
            case Character.Type.Lancero:
                sprite.sprite = GameAssets.i.lanceroOWSprite;
                break;
        }
        spawnPosition = GetPosition();
        roamPosition = GetPosition();
        roamDistanceMax = 3f;

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
        if (state == State.Waiting)
        {
            //if (useUnscaledDeltaTime)
            //{
            //    timer -= Time.unscaledDeltaTime;
            //}
            timer -= Time.deltaTime;
            if (timer <= 0)
            {
                //Debug.Log(character.name + " Volvio a su estado activo");
                state = State.Normal;
            }
        }

        if (!OverworldManager.IsOvermapRunning())
        {
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

    public void SetStateNormal()
    {
        state = State.Normal;
    }

    public void SetStateWaiting()
    {
        state = State.Waiting;
    }

    public State GetState()
    {
        return state;
    }

    private void HandleRoaming()
    {
        float minRoamDistance = 1.5f;
        if (Vector3.Distance(GetPosition(), roamPosition) < minRoamDistance)
        {
            // Near roam position, wait
            waitTimer -= Time.deltaTime;
            if (waitTimer <= 0f)
            {
                // Find new roam position
                Vector3 roamDir = UtilsClass.GetRandomDir();
                float roamDistance = Random.Range(5f, roamDistanceMax);
                RaycastHit2D raycastHit = Physics2D.Raycast(GetPosition(), roamDir, roamDistance,wallLayerMask);
                if (raycastHit.collider != null)
                {
                    // Hit something
                    roamDistance = raycastHit.distance - 1f;
                    if (roamDistance <= 0f) roamDistance = 0f;
                }
                roamPosition = GetPosition() + roamDir * roamDistance;
                SetTargetMovePosition(roamPosition);
                waitTimer = Random.Range(1f, 5f);
            }
        }
    }

    private void HandleTargetMovePosition()
    {
        float findTargetRange = 6.7f;

        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < findTargetRange)
        {
            // Player within find target range
            SetTargetMovePosition(playerOvermap.GetPosition());
        }
        float attackRange = 1.25f;
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
                //        // Normal battle
                //        Battle.LoadEnemyEncounter(character, character.enemyEncounter);
                //        //}
                //        break;
                //    }
        }
    }

}
