using System;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerOverworld : MonoBehaviour
{
    public static PlayerOverworld instance;

    private const float SPEED = 10f;

    [SerializeField] private LayerMask wallLayerMask;
    private Character_Anims charAnim;
    [HideInInspector] public State state;
    private HealthSystem healthSystem;
    private Character character;

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
        inventoryPartyWindow = GameObject.Find("InventoryPartyWindow").GetComponent<InventoryPartyWindow>();
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

        OverworldManager.GetInstance().OnOvermapStopped += NPCOvermap_OnOvermapStopped;
    }

    private void NPCOvermap_OnOvermapStopped(object sender, EventArgs e)
    {
        // Idle anim
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

    //private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    //{
    //    RefreshHealthBar();
    //}

    //private void RefreshHealthBar()
    //{
    //    healthWorldBar.SetSize(healthSystem.GetHealthPercent());
    //    if (healthSystem.GetHealthPercent() >= 1f)
    //    {
    //        // Full health
    //        healthWorldBar.Hide();
    //    }
    //    else
    //    {
    //        healthWorldBar.Show();
    //    }
    //}

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
            NPCOverworld npcOverworld = OverworldManager.GetInstance().GetClosestNPC(GetPosition(), 1.5f);
            ItemOverworld itemOverworld = OverworldManager.GetInstance().GetClosestItem(GetPosition(), 3.9f);
            if (npcOverworld != null)
            {
                switch (npcOverworld.GetCharacter().type)
                {
                    //case Character.Type.QuestNpc_1:
                    //    Dialogues.QuestDialogue(npcOverworld.GetCharacter());
                    //    break;
                    case Character.Type.WarriorNPC_1:
                        if (npcOverworld.GetCharacter().quest.questGoal.CanComplete())
                        {
                            Dialogues.TestDialogue_3();
                        }
                        else
                        {
                            Dialogues.TestDialogue_1(npcOverworld.GetCharacter());
                        }
                        break;
                    case Character.Type.WarriorNPC_2:
                        Dialogues.TestDialogue_2(npcOverworld.GetCharacter());
                        break;
                    case Character.Type.Shop:
                        Dialogues.ShopDialogue(npcOverworld.GetCharacter());
                        break;
                        //case Character.Type.Shop:
                        //    Cutscenes.Play_Shop(npcOvermap.GetCharacter());
                        //    break;
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
                            ResourceManager.instance.AddHerbs(itemOverworld.GetItem().GetAmount());
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
            // Idle Anim
        }
        else
        {
            if (CanMoveTo(moveDir, SPEED * Time.deltaTime))
            {
                //charAnim.PlayMoveAnim(moveDir);  Animacion de moovimiento depende de la direccion
                transform.position += moveDir * SPEED * Time.deltaTime;
            }
            else
            {
                //charAnim.PlayMoveAnim(moveDir);  Animacion de moovimiento depende de la direccion
            }
        }
    }

    private bool IsOnTopOfWall()
    {
        RaycastHit2D raycastHit = Physics2D.CircleCast(GetPosition(), 1f, Vector2.zero, 0f, wallLayerMask);
        return raycastHit.collider != null;
    }

    private bool CanMoveTo(Vector3 dir, float distance)
    {
        if (IsOnTopOfWall()) return true;
        RaycastHit2D raycastHit = Physics2D.CircleCast(GetPosition(), 1f, dir, distance, wallLayerMask);
        return raycastHit.collider == null;
    }

    private void TryMoveTo(Vector3 dir, float distance)
    {
        RaycastHit2D raycastHit = Physics2D.CircleCast(GetPosition(), 1f, dir,distance, wallLayerMask);
        if (raycastHit.collider == null)
        {
            transform.position += dir * distance;
        }
        else
        {
            transform.position += dir * (raycastHit.distance - .1f);
        }
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

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }
}
