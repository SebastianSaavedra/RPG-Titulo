using System;
using UnityEngine;
using CodeMonkey.Utils;

public class FollowerOverworld : MonoBehaviour
{
    public static FollowerOverworld instance;

    private float speed = 9.5f;

    private Character_Anims charAnim;
    private Animator anim;
    private SpriteRenderer sprite;
    private State state;
    private Vector3 targetMovePosition;
    private PlayerOverworld playerOvermap;
    private Vector3 followOffset;
    private Character character;
    private HealthSystem healthSystem;

    //public event EventHandler OnEquipChanged;

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

    public void Setup(Character character, PlayerOverworld playerOvermap, Vector3 followOffset)
    {
        this.character = character;
        this.playerOvermap = playerOvermap;
        this.followOffset = followOffset;

        if (character.IsInPlayerTeam())
        {
            switch (character.type)
            {
                case Character.Type.Pedro:
                    sprite.sprite = GameAssets.i.spriteOWPedro;
                    transform.localScale = Vector3.one * .9f;
                    break;
                case Character.Type.Arana:
                    sprite.sprite = GameAssets.i.spriteOWArana;
                    transform.localScale = Vector3.one * .9f;
                    break;
                case Character.Type.Chillpila:
                    sprite.sprite = GameAssets.i.spriteOWChillpila;
                    transform.localScale = Vector3.one * .9f;
                    break;
                case Character.Type.Antay:
                    sprite.sprite = GameAssets.i.spriteOWAntay;
                    transform.localScale = Vector3.one * .9f;
                    break;

            }
        }

        healthSystem = new HealthSystem(character.stats.healthMax);
        healthSystem.SetHealthAmount(character.stats.health);
        character.SetHealthSystem(healthSystem);

        SetTargetMovePosition(playerOvermap.GetPosition() + followOffset);
    }

    public void SaveCharacterPosition()
    {
        character.position = GetPosition();
    }

    public Character GetCharacter()
    {
        return character;
    }

    private void Update()
    {
        if (!OverworldManager.IsOvermapRunning())
        {
            // Idle Anim
            return;
        }

        switch (character.type)
        {
            default:
                switch (state)
                {
                    case State.Normal:
                        HandleTargetMovePosition();
                        HandleMovement();
                        break;
                }
                break;
        }
    }

    private void SetStateNormal()
    {
        state = State.Normal;
    }

    //private void UseItem(Item item)
    //{
    //    switch (item.GetItemType())
    //    {
    //        case Item.ItemType.MedicinalHerbs:
    //            Heal(15);
    //            break;
    //    }
    //}

    private void HandleTargetMovePosition()
    {
        float tooFarDistance = .5f;
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) > tooFarDistance)
        {
            SetTargetMovePosition(playerOvermap.GetPosition() + followOffset);
        }
        float tooCloseDistance = .2f;
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < tooCloseDistance)
        {
            SetTargetMovePosition(GetPosition());
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
            transform.position += moveDir * speed * Time.deltaTime;
        }
    }

    //public void SetEquipment(Item.ItemType itemType)
    //{
    //    switch (itemType)
    //    {
    //        case Item.ItemType.Armor_1:
    //        case Item.ItemType.Armor_2:
    //        case Item.ItemType.Armor_3:
    //        case Item.ItemType.Armor_4:
    //        case Item.ItemType.Armor_5: 
    //            EquipArmor(); 
    //            break;

    //        case Item.ItemType.Helmet_1:
    //        case Item.ItemType.Helmet_2:
    //        case Item.ItemType.Helmet_3:
    //        case Item.ItemType.Helmet_4:
    //        case Item.ItemType.Helmet_5: 
    //            EquipHelmet(); 
    //            break;

    //        case Item.ItemType.Weapon_1:
    //        case Item.ItemType.Weapon_2:
    //        case Item.ItemType.Weapon_3:
    //        case Item.ItemType.Weapon_4:
    //        case Item.ItemType.Weapon_5: 
    //            EquipWeapon(); 
    //            break;
    //    }
    //    OnEquipChanged?.Invoke(this, EventArgs.Empty);
    //}

    //private void EquipArmor()
    //{
    //    throw new NotImplementedException();
    //}

    //private void EquipHelmet()
    //{
    //    throw new NotImplementedException();
    //}

    //private void EquipWeapon()
    //{
    //    throw new NotImplementedException();
    //}

    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
        character.stats.health = healthSystem.GetHealthAmount();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

    public void SetTargetMovePosition(Vector3 targetMovePosition)
    {
        this.targetMovePosition = targetMovePosition;
    }

}
