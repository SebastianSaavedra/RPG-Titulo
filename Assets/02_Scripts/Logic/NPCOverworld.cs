using System;
using UnityEngine;

public class NPCOverworld : MonoBehaviour
{
    public static NPCOverworld instance;

    private const float SPEED = 10f;

    private Character_Anims charAnim;
    private Animator anim;
    private SpriteRenderer sprite;
    private State state;
    private Vector3 targetMovePosition;
    private PlayerOverworld playerOverworld;
    private Character character;
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
                //Setear animator
                break;
            case Character.Type.WarriorNPC_1:
                sprite.sprite = GameAssets.i.warriorNPC;
                //Setear animator
                break;
            case Character.Type.WarriorNPC_2:
                sprite.sprite = GameAssets.i.warriorNPC;
                //Setear animator
                break;

            case Character.Type.Shop:
                sprite.sprite = GameAssets.i.npc_SHOP;
                break;
        }

        SetTargetMovePosition(GetPosition());

        OverworldManager.GetInstance().OnOvermapStopped += NPCOvermap_OnOvermapStopped;
    }

    private void NPCOvermap_OnOvermapStopped(object sender, EventArgs e)
    {
        //Idle Anim
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