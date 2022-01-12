using System;
using UnityEngine;

public class NPCOverworld : MonoBehaviour
{
    public static NPCOverworld instance;

    private const float SPEED = 8f;

    private Character_Anims charAnim;
    private Animator anim;
    private SpriteRenderer sprite;
    private State state;
    private Vector3 targetMovePosition;
    private PlayerOverworld playerOverworld;
    private Character character;
    [SerializeField] private bool alreadyTalkedWithNPC;
    //public bool overrideOverworldRunning;


    private enum State
    {
        Normal,
    }

    private void Awake()
    {
        instance = this;
        charAnim = gameObject.GetComponent<Character_Anims>();
        anim = gameObject.GetComponent<Animator>();
        sprite = gameObject.GetComponent<SpriteRenderer>();
        SetStateNormal();
    }

    public void Setup(Character character, PlayerOverworld playerOverworld)
    {
        this.character = character;
        this.playerOverworld = playerOverworld;

        //animación de caminar

        switch (character.type)
        {
            case Character.Type.QuestNpc_1:
                sprite.sprite = GameAssets.i.questNpc_1;
                transform.localScale = Vector3.one * 0.75f;
                //Setear animator
                break;
            case Character.Type.SoldadoMapuche_1:
                sprite.sprite = GameAssets.i.warriorNPC;
                transform.localScale = Vector3.one * 0.85f;
                //Setear animator
                break;
            case Character.Type.SoldadoMapuche_2:
                sprite.sprite = GameAssets.i.warriorNPC;
                transform.localScale = Vector3.one * 0.85f;
                //Setear animator
                break;
            case Character.Type.SoldadoDesesperado:
                sprite.sprite = GameAssets.i.warriorNPC;
                transform.localScale = Vector3.one * 0.85f;
                //Setear animator
                break;
            case Character.Type.ViejaMachi:
                sprite.sprite = GameAssets.i.npc_ViejaMachi;
                transform.localScale = Vector3.one * 0.75f;
                break;
            case Character.Type.HombreMapuche_1:
            case Character.Type.HombreMapuche_2:
                sprite.sprite = GameAssets.i.npc_HombreMapuche;
                transform.localScale = Vector3.one * 0.85f;
                break;
            case Character.Type.MujerMapuche_1:
            case Character.Type.MujerMapuche_2:
                sprite.sprite = GameAssets.i.npc_MujerMapuche;
                transform.localScale = Vector3.one * 0.75f;
                break;
            case Character.Type.NinoMapuche_1:
            case Character.Type.NinoMapuche_2:
                sprite.sprite = GameAssets.i.npc_NinoMapuche;
                transform.localScale = Vector3.one * 0.85f;
                break;
            case Character.Type.NinaMapuche_1:
            case Character.Type.NinaMapuche_2:
                sprite.sprite = GameAssets.i.npc_NinaMapuche;
                transform.localScale = Vector3.one * 0.85f;
                break;

            //case Character.Type.TrenTren:
            //    sprite.sprite = GameAssets.i.trenTrenSprite;
            //    break;

            //case Character.Type.CaiCai:
            //    sprite.sprite = GameAssets.i.trenTrenSprite;
            //    break;

            case Character.Type.Shop:
                sprite.sprite = GameAssets.i.npc_SHOP;
                transform.localScale = Vector3.one * 0.75f;
                break;
        }

        SetTargetMovePosition(GetPosition());

        OverworldManager.GetInstance().OnOvermapStopped += NPCOverworld_OnOverworldStopped;
    }

    private void OnDestroy()
    {
        OverworldManager.GetInstance().OnOvermapStopped -= NPCOverworld_OnOverworldStopped;
    }

    private void NPCOverworld_OnOverworldStopped(object sender, OverworldManager.OnOvermapStoppedEventsArgs e)
    {
        switch (e.index)
        {
            case 0:
                anim.speed = 0;
                break;
            case 1:
                anim.speed = SPEED * .08f;
                break;
        }
    }

    public void SetTalkForTheFirstTime()
    {
        alreadyTalkedWithNPC = true;
    }

    public bool GetAlreadyTalkedWithThisNPC()
    {
        return alreadyTalkedWithNPC;
    }

    public void SaveCharacterPosition()
    {
        character.position = GetPosition();
    }

    public Character GetCharacter()
    {
        return character;
    }

    //public void SetLastMoveDir(Vector3 lastMoveDir)
    //{
    //    charAnim.SetLastMoveDir(lastMoveDir);
    //}

    private void Update()
    {
        if (!OverworldManager.IsOvermapRunning()) //  && !overrideOverworldRunning
        {
            return;
        }

        switch (state)
        {
            case State.Normal:
                HandleMovement();
                break;
        }
    }

    private void SetStateNormal()
    {
        state = State.Normal;
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
            //Idle Anim
        }
        else
        {
            //playerBase.PlayMoveAnim(moveDir); movimiento segun dirección IMPORTANTE
            transform.position += moveDir * SPEED * Time.deltaTime;
            charAnim.PlayAnimMoving(moveDir);
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

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}