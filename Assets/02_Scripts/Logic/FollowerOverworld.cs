using System;
using UnityEngine;
using Pathfinding;

public class FollowerOverworld : MonoBehaviour
{
    public static FollowerOverworld instance;

    private float SPEED = 8f;

    private Character_Anims charAnim;
    private Animator anim;
    private SpriteRenderer sprite;
    private State state;
    //private Vector3 targetMovePosition;
    private PlayerOverworld playerOvermap;
    private Vector3 followOffset;
    private Character character;
    private HealthSystem healthSystem;

    //[SerializeField] private GameObject otherFollower;

    [Header("Pathfinding")]
    [SerializeField] AIDestinationSetter aiDestinationSetter;
    [SerializeField] AIPath aiPath;
    [SerializeField] Transform target;

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

        GameObject targetGameobject = new GameObject("target");
        targetGameobject.transform.position = new Vector3(0, 0, 0);
        target = targetGameobject.transform;
        aiDestinationSetter.target = target;

        aiPath.maxSpeed = SPEED;

        float randomEndReachedDistance = UnityEngine.Random.Range(1.75f,4f);
        aiPath.endReachedDistance = randomEndReachedDistance;
        if (character.IsInPlayerTeam())
        {
            switch (character.type)
            {
                case Character.Type.Pedro:
                    sprite.sprite = GameAssets.i.spriteOWPedro;
                    sprite.sortingOrder = 99;
                    anim.runtimeAnimatorController = GameAssets.i.pedroOVERWORLDANIM;
                    break;
                case Character.Type.Arana:
                    sprite.sprite = GameAssets.i.spriteOWArana;
                    sprite.sortingOrder = 98;
                    anim.runtimeAnimatorController = GameAssets.i.aranaOVERWORLDANIM;
                    break;
                case Character.Type.Chillpila:
                    sprite.sprite = GameAssets.i.spriteOWChillpila;
                    sprite.sortingOrder = 97;
                    anim.runtimeAnimatorController = GameAssets.i.chillpilaOVERWORLDANIM;
                    break;
                case Character.Type.Antay:
                    sprite.sprite = GameAssets.i.spriteOWAntay;
                    sprite.sortingOrder = 96;
                    anim.runtimeAnimatorController = GameAssets.i.antayOVERWORLDANIM;
                    break;

            }
        }
        transform.localScale = Vector3.one * 0.75f;

        healthSystem = new HealthSystem(character.stats.healthMax);
        healthSystem.SetHealthAmount(character.stats.health);
        character.SetHealthSystem(healthSystem);

        SetTargetMovePosition(playerOvermap.GetPosition());

        OverworldManager.GetInstance().OnOvermapStopped += FollowerOverworld_OnOvermapStopped;
    }

    private void OnDestroy()
    {
        OverworldManager.GetInstance().OnOvermapStopped -= FollowerOverworld_OnOvermapStopped;
    }

    private void FollowerOverworld_OnOvermapStopped(object sender, OverworldManager.OnOvermapStoppedEventsArgs e)
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

    public Vector3 GetOffset()
    {
        return followOffset;
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
        //float tooFarDistance = 2f;
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) > aiPath.endReachedDistance + 1f)
        {
            aiPath.maxSpeed = SPEED;
            SetTargetMovePosition(playerOvermap.GetPosition() - followOffset);
            //Debug.Log("Muy Lejos");
        }
        //float tooCloseDistance = 1f;
        if (Vector3.Distance(GetPosition(), playerOvermap.GetPosition()) < aiPath.endReachedDistance)
        {
            //Debug.Log("Muy cerca");
            aiPath.maxSpeed = 0;
            //SetTargetMovePosition(GetPosition());
        }
    }

    private void HandleMovement()
    {
        //float minMoveDistance = 1f;
        //Vector3 moveDir = new Vector3(0, 0);
        Vector2 direction = (target.position - transform.position).normalized;
        //if (Vector3.Distance(GetPosition(), target.position) > minMoveDistance)
        //{
        //    direction = (target.position - GetPosition()).normalized;
        //}

        bool isIdle = aiPath.maxSpeed == 0;
        if (isIdle)
        {
            //Debug.Log(character.type + " en Idle");
            charAnim.PlayAnimIdle();
        }
        else
        {
            charAnim.PlayAnimMoving(direction);

            //Debug.Log("La dirección de " + character.type + " es : " + direction);
            //if (Vector2.Distance(transform.position, target.position) < minMoveDistance)
            //{
            //    aiPath.Move(direction * SPEED * Time.deltaTime);
            //    charAnim.PlayAnimMoving(direction);
            //}
            //else
            //{
            //    Debug.Log("esta muy cerca");
            //}
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
        target.position = targetMovePosition;

        //Debug.Log("La posicion del target es: " + target.position);
    }

}
