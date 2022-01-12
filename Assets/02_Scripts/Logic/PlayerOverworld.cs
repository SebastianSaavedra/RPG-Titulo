using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerOverworld : MonoBehaviour
{
    public static PlayerOverworld instance;

    private const float SPEED = 8f;

    [SerializeField] private LayerMask wallLayerMask;
    private Character_Anims charAnim;
    private Animator animator;
    [HideInInspector] public State state;
    private HealthSystem healthSystem;
    private Character character;

    [SerializeField] float radio;
    Vector3 dir;
    float distance;

    private InventoryPartyWindow inventoryPartyWindow;

    public event EventHandler OnEquipChanged;

    public enum State
    {
        Normal,
        Busy,

    }

    private void Awake()
    {
        instance = this;
        charAnim = gameObject.GetComponent<Character_Anims>();
        animator = gameObject.GetComponent<Animator>();
        inventoryPartyWindow = GameObject.Find("InventoryPartyWindow").GetComponent<InventoryPartyWindow>();
        transform.localScale = Vector3.one * 0.75f;
        SetStateNormal();
    }

    public void Setup(Character character)
    {
        this.character = character;
        transform.position = character.position;
        healthSystem = new HealthSystem(character.stats.healthMax);
        healthSystem.SetHealthAmount(character.stats.health);
        character.SetHealthSystem(healthSystem);

        //RefreshTexture();
        
        //Animación de caminar

        OverworldManager.GetInstance().OnOvermapStopped += PlayerOverworld_OnOverworldStopped;
    }

    private void OnDestroy()
    {
        OverworldManager.GetInstance().OnOvermapStopped -= PlayerOverworld_OnOverworldStopped;
    }

    private void PlayerOverworld_OnOverworldStopped(object sender, OverworldManager.OnOvermapStoppedEventsArgs e)
    {
        switch (e.index)
        {
            case 0:
                animator.speed = 0;
                break;
            case 1:
                animator.speed = SPEED * .08f;
                break;
        }
    }

    //public void RefreshTexture()        //CAMBIO DE EQUIPAMIENTO
    //{
    //    //if (character.equipamiento)
    //    //{
    //    //    cambio de animator
    //    //}
    //}

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
                HandleMovement();
                HandleInteract();
                break;
        }
    }

    public void SetStateNormal()
    {
        state = State.Normal;
    }

    private void HandleInteract()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            NPCOverworld npcOverworld = OverworldManager.GetInstance().GetClosestNPC(GetPosition(), 2f);
            ItemOverworld itemOverworld = OverworldManager.GetInstance().GetClosestItem(GetPosition(), 3.9f);
            if (npcOverworld != null)
            {
                switch (npcOverworld.GetCharacter().type)
                {
                    case Character.Type.ViejaMachi:

                        switch (GameData.state)
                        {
                            case GameData.State.Starting:
                                Dialogues.Play_ViejaMachiQuest(npcOverworld.GetCharacter());
                                break;
                            case GameData.State.AlreadyTalkedWithViejaMachi:
                                Dialogues.Play_ViejaMachiQuestPending(npcOverworld.GetCharacter());
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_MachiEndGame();
                                break;
                        }
                        break;

                    case Character.Type.TrenTren:
                        switch (GameData.state)
                        {
                            default:
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_TrenTrenEndGame();
                                break;
                        }
                        break;

                    case Character.Type.SoldadoMapuche_1:

                        switch (GameData.state)
                        {
                            case GameData.State.Starting:
                                Dialogues.Play_SoldadoAdvertenciaAntesDeHablarConMachi(npcOverworld.GetCharacter());
                                break;
                            case GameData.State.AlreadyTalkedWithViejaMachi:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_SoldadoBuenaSuerte(npcOverworld.GetCharacter());
                                }
                                else
                                {
                                    Dialogues.Play_SoldadoAdvertenciaBosque(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;

                    case Character.Type.SoldadoMapuche_2:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Soldado_2Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Soldado_2(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.SoldadoDesesperado:
                        if (GameData.state == GameData.State.Endgame)
                        {
                            Dialogues.Play_NPCEndGame();
                        }
                        else
                        {
                            Dialogues.Play_Soldado_Desesperado();
                        }
                        break;
                    case Character.Type.HombreMapuche_1:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Hombre01Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Hombre01(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.HombreMapuche_2:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Hombre02Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Hombre02(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.MujerMapuche_1:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Mujer01Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Mujer01(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.MujerMapuche_2:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Mujer02Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Mujer02(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.NinoMapuche_1:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Nino01Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Nino01(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.NinoMapuche_2:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Nino02Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Nino02(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.NinaMapuche_1:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Nina01Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Nina01(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.NinaMapuche_2:
                        switch (GameData.state)
                        {
                            default:
                                if (npcOverworld.GetAlreadyTalkedWithThisNPC())
                                {
                                    Dialogues.Play_Nina02Repeat(npcOverworld);
                                }
                                else
                                {
                                    Dialogues.Play_Nina02(npcOverworld);
                                }
                                break;
                            case GameData.State.Endgame:
                                Dialogues.Play_NPCEndGame();
                                break;
                        }
                        break;
                    case Character.Type.Shop:
                        Dialogues.ShopDialogue(npcOverworld.GetCharacter());
                        break;
                }
            }
            else if (itemOverworld != null)
            {
                switch (itemOverworld.GetItem().GetItemType())
                {
                    case Item.ItemType.MedicinalHerbs:
                        if (itemOverworld.GetItem().GetAmount() > 0)
                        {
                            //Debug.Log(itemOverworld.GetItem().GetItemType());
                            Inventory.instance.AddItem(itemOverworld.GetItem());
                            QuestManager.instance.QuestProgress();
                            itemOverworld.GetItem().SetAmount(0);
                            SoundManager.PlaySound(SoundManager.Sound.Coin);
                            Debug.Log("A la planta le quedan: " + itemOverworld.GetItem().GetAmount() + " hierbas");
                        }
                        break;

                        // Para testear el recoger armas y equiparlas
                    //case Item.ItemType.Weapon_1:
                    //    if (itemOverworld.GetItem().GetAmount() > 0)
                    //    {
                    //        //Debug.Log(itemOverworld.GetItem().GetItemType());
                    //        Inventory.instance.AddItem(itemOverworld.GetItem());
                    //        itemOverworld.GetItem().SetAmount(0);
                    //        SoundManager.PlaySound(SoundManager.Sound.Coin);
                    //    }
                    //    break;
                }
            }
        }
    }

    private void HandleMovement()
    {
        float moveX = 0f;
        float moveY = 0f;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
        {
            moveY = +1f;
        }
        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
        {
            moveY = -1f;
        }
        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
        {
            moveX = -1f;
        }
        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
        {
            moveX = +1f;
        }

        Vector3 moveDir = new Vector3(moveX, moveY).normalized;
        bool isIdle = moveX == 0 && moveY == 0;
        if (isIdle)
        {
            charAnim.PlayAnimIdle();
        }
        else
        {
            if (CanMoveTo(moveDir, SPEED * Time.deltaTime))
            {
                charAnim.PlayAnimMoving(moveDir);
                transform.position += moveDir * SPEED * Time.deltaTime;
            }
            else
            {
                charAnim.PlayAnimMoving(moveDir);
            }
            //charAnim.PlayAnimMoving(moveDir);
            //transform.position += moveDir * SPEED * Time.deltaTime;
        }
    }

    private bool IsOnTopOfWall()
    {
        RaycastHit2D raycastHit = Physics2D.CircleCast(new Vector2(GetPosition().x, (GetPosition().y - 1)), radio, Vector2.zero, wallLayerMask);
        //if (raycastHit.collider != null)
        //{
        //    Debug.Log("IsOnTopOfWall: " + raycastHit.collider.name);
        //}
        return raycastHit.collider != null;
    }

    private bool CanMoveTo(Vector3 dir, float distance)
    {
        if (IsOnTopOfWall()) return true;
        this.dir = dir;
        this.distance = distance;
        RaycastHit2D raycastHit = Physics2D.CircleCast(new Vector2(GetPosition().x, (GetPosition().y - 1)), radio, dir, distance, wallLayerMask);
        //if (raycastHit.collider != null)
        //{
        //    Debug.Log("CanMoveTo: " + raycastHit.collider.name);
        //}
        return raycastHit.collider == null;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;
        RaycastHit2D raycastHit = Physics2D.CircleCast(GetPosition(), radio, dir, distance, wallLayerMask);
        Gizmos.DrawWireSphere(new Vector2(GetPosition().x, (GetPosition().y - 1)) + (Vector2)dir,radio);

    }

    public void SetEquipment(Item item)
    {
        SetEquipment(item.itemType);
    }

    private void SetEquipment(Item.ItemType itemType)
    {
        switch (itemType)
        {
            case Item.ItemType.Armor_1:
            case Item.ItemType.Armor_2:
            case Item.ItemType.Armor_3:
            case Item.ItemType.Armor_4:
            case Item.ItemType.Armor_5:
                EquipArmor();
                break;

            case Item.ItemType.Helmet_1:
            case Item.ItemType.Helmet_2:
            case Item.ItemType.Helmet_3:
            case Item.ItemType.Helmet_4:
            case Item.ItemType.Helmet_5:
                EquipHelmet();
                break;

            case Item.ItemType.Weapon_1:
            case Item.ItemType.Weapon_2:
            case Item.ItemType.Weapon_3:
            case Item.ItemType.Weapon_4:
            case Item.ItemType.Weapon_5:
                EquipWeapon();
                break;
        }
        OnEquipChanged?.Invoke(this, EventArgs.Empty);
    }

    private void EquipArmor()
    {
        Debug.Log(character.type + " se acaba de equipar una armadura");
        character.stats.defense += 1;
        character.stats.health += 10;
        character.stats.healthMax = character.stats.health;
        inventoryPartyWindow.RefreshTextStatsAfterEquippinItem(character);
    }

    private void EquipHelmet()
    {
        Debug.Log(character.type + " se acaba de equipar un casco");
        character.stats.turns += 1;
        inventoryPartyWindow.RefreshTextStatsAfterEquippinItem(character);
    }

    private void EquipWeapon()
    {
        Debug.Log(character.type + " se acaba de equipar un arma");
        character.stats.attack += 2;
        character.stats.critChance += 5;
        inventoryPartyWindow.RefreshTextStatsAfterEquippinItem(character);
    }

    public int GetHealth()
    {
        return character.stats.health;
    }

    public void Heal(int healAmount)
    {
        healthSystem.Heal(healAmount);
        character.stats.health = healthSystem.GetHealthAmount();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public Transform GetTransform()
    {
        return transform;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
