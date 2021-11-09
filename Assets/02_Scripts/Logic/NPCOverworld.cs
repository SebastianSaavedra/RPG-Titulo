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

    public void Setup(Character character, PlayerOverworld playerOvermap)
    {
        this.character = character;
        this.playerOverworld = playerOvermap;

        //animación de caminar

        switch (character.type)
        {
            //case Character.Type.Tank:
            //    material.mainTexture = GameAssets.i.t_Tank;
            //    playerBase.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dSwordShield_Idle, GameAssets.UnitAnimTypeEnum.dSwordShield_Walk, 1f, 1f);
            //    transform.localScale = Vector3.one * 1.2f;
            //    break;
            //case Character.Type.Healer:
            //    material.mainTexture = GameAssets.i.t_Healer;
            //    playerBase.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dDualDagger_Idle, GameAssets.UnitAnimTypeEnum.dDualDagger_Walk, 1f, 1f);
            //    transform.localScale = Vector3.one * 1.0f;
            //    break;
            //case Character.Type.PlayerDoppelganger:
            //    material.mainTexture = GameAssets.i.t_Player;
            //    playerBase.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dBareHands_Idle, GameAssets.UnitAnimTypeEnum.dBareHands_Walk, 1f, 1f);
            //    transform.localScale = Vector3.one * 1.0f;
            //    break;
            //case Character.Type.Shop:
            //    material.mainTexture = GameAssets.i.t_Vendor;
            //    break;

            //case Character.Type.Villager_1: material.mainTexture = GameAssets.i.t_Villager_1; break;
            //case Character.Type.Villager_2: material.mainTexture = GameAssets.i.t_Villager_2; break;
            //case Character.Type.Villager_3: material.mainTexture = GameAssets.i.t_Villager_3; break;
            //case Character.Type.Villager_4: material.mainTexture = GameAssets.i.t_Villager_4; break;
            //case Character.Type.Villager_5: material.mainTexture = GameAssets.i.t_Villager_5; break;

            //case Character.Type.Randy:
            //    material.mainTexture = GameAssets.i.t_Randy;
            //    playerBase.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dBareHands_Idle, GameAssets.UnitAnimTypeEnum.dBareHands_Walk, 1f, 1f);
            //    break;
            //case Character.Type.TavernAmbush:
            //    material.mainTexture = GameAssets.i.t_EnemyMinionRed;
            //    playerBase.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dMinion_Idle, GameAssets.UnitAnimTypeEnum.dMinion_Walk, 1f, 1f);
            //    break;
            //case Character.Type.TavernAmbush_2:
            //case Character.Type.TavernAmbush_3:
            //    material.mainTexture = GameAssets.i.t_EnemyMinionOrange;
            //    playerBase.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dMinion_Idle, GameAssets.UnitAnimTypeEnum.dMinion_Walk, 1f, 1f);
            //    break;
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
