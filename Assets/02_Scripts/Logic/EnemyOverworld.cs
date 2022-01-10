using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using Pathfinding;
using MEC;

public class EnemyOverworld : MonoBehaviour
{
    public static EnemyOverworld instance;

    private const float SPEED = 5.75f;

    private Character_Anims charAnim;
    private Animator anim;
    private SpriteRenderer sprite;
    [HideInInspector] public State state;
    private Character character;
    private PlayerOverworld playerOvermap;
    private Vector3 spawnPosition;
    private float roamDistanceMax;
    private Vector3 roamPosition;
    //private Vector3 targetMovePosition;
    private float waitTimer;
    private float timer = 2f;
    [SerializeField] private LayerMask wallLayerMask;

    [Header("Pathfinding")]
    [SerializeField] AIDestinationSetter aiDestinationSetter;
    [SerializeField] AIPath aiPath;
    [SerializeField] Transform target;
    //[SerializeField] float speed = 200f;
    //[SerializeField] float nextWaypointDistance = 3f;

    //Path path;
    //int currentWaypoint = 0;
    //bool reachedEndOfPath = false;
    //Seeker seeker;
    bool huntingPlayer;


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
        //seeker = gameObject.GetComponent<Seeker>();

        SetStateWaiting();
    }

    //private void UpdatePath()
    //{
    //    if(seeker.IsDone())
    //        seeker.StartPath(transform.position, target.position, OnPathComplete);
    //}

    //private void OnPathComplete(Path p)
    //{
    //    if (!p.error)
    //    {
    //        path = p;
    //        currentWaypoint = 0;
    //    }
    //}

    public void Setup(Character character, PlayerOverworld playerOvermap)
    {
        this.character = character;
        this.playerOvermap = playerOvermap;
        aiPath.maxSpeed = SPEED;

        GameObject targetGameobject = new GameObject("target");
        targetGameobject.transform.position = new Vector3(0,0,0);
        target = targetGameobject.transform;

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
            case Character.Type.Anchimallen:
                sprite.sprite = GameAssets.i.anchimallenOWSprite;
                break;
            case Character.Type.Guirivilo:
                sprite.sprite = GameAssets.i.guiriviloOWSprite;
                break;
            case Character.Type.Piuchen:
                sprite.sprite = GameAssets.i.piuchenOWSprite;
                break;
        }
        transform.localScale = Vector3.one * 0.75f;
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
        if (!OverworldManager.IsOvermapRunning())
        {
            return;
        }

        switch (state)
        {
            case State.Normal:
                FindTarget();

                if (!huntingPlayer)
                {
                    HandleRoaming();
                }
                else
                {
                    TryAttackPlayer();
                }
                HandleMovement();
                break;

            case State.Waiting:
                timer -= Time.deltaTime;
                if (timer <= 0)
                {
                    //Debug.Log(character.name + " Volvio a su estado activo");
                    state = State.Normal;
                }
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

    //private void Pathfind()
    //{
    //    if (path == null)
    //        return;

    //    if (currentWaypoint >= path.vectorPath.Count)
    //    {
    //        reachedEndOfPath = true;
    //        return;
    //    }
    //    else
    //    {
    //        reachedEndOfPath = false;
    //    }
    //}

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

    private void FindTarget()
    {
        float findTargetRange = 6.7f;

        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < findTargetRange)
        {
            // Player within find target range
            SetTargetMovePosition(playerOvermap.GetPosition());
            huntingPlayer = true;
            //Debug.Log("Hunting player: " + huntingPlayer);
        }
        else
        {
            huntingPlayer = false;
            //Debug.Log("Hunting player: " + huntingPlayer);
        }
    }

    private void TryAttackPlayer()
    {
        float attackRange = 1.25f;
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < attackRange)
        {
            // Player within attack/interact range
            HandleInteractionWithPlayer();
        }
    }

    private void HandleMovement()
    {
        //Pathfind();
        //Vector2 direction = (path.vectorPath[currentWaypoint] - transform.position).normalized;
        Vector2 direction = (aiPath.destination - transform.position).normalized;
        //Vector2 force = direction * speed * Time.deltaTime;

        bool isIdle = direction.x == 0 && direction.y == 0;
        if (isIdle)
        {
            //Debug.Log(character.type + " Quieto");
            //charAnim.PlayAnimIdle();
        }
        else
        {
            //charAnim.PlayAnimMoving(direction);
            //charAnim.PlayMoveAnim(moveDir); movimiento segun dirección IMPORTANTE
            //if (Vector2.Distance(transform.position,aiPath.destination) > .1f )
            //{
            //    aiPath.Move(direction * SPEED * Time.deltaTime);
            //}
        }

        //float distance = Vector2.Distance(transform.position, aiPath.destination);

        //if (distance < aiPath.pickNextWaypointDist)
        //{
        //    aiPath.++;
        //}
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetTargetMovePosition(Vector3 targetMovePosition)
    {
        target.position = targetMovePosition;
        aiDestinationSetter.target = target;

        //Debug.Log("La posicion del target es: " + target.position);
        //if (IsInvoking())
        //{
        //    CancelInvoke("UpdatePath");
        //}
        //UtilsClass.WaitOneFrame();
        //InvokeRepeating("UpdatePath", 0, .5f);
    }

    //private IEnumerator<float> _UpdatePathLoop()
    //{
    //    //float loopTimer = 0.5f;
    //    while (true)
    //    {
    //        Timing.WaitForSeconds(.5f);
    //        //UpdatePath();
    //    }
    //}


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
