using System;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;


public class PlayerOverworld : MonoBehaviour
{
    public static PlayerOverworld instance;

    private const float SPEED = 15f;

    [SerializeField] private LayerMask wallLayerMask;
    private Character_Anims charAnim;
    public State state;
    private HealthSystem healthSystem;
    //private World_Bar healthWorldBar;
    private Character character;


    public enum State
    {
        Normal,
        Busy,
        OnMenu,
        SubMenu,

    }

    private void Awake()
    {
        instance = this;
        charAnim = gameObject.GetComponent<Character_Anims>();
        SetStateNormal();

    }

    public void Setup(Character character)
    {
        this.character = character;
        transform.position = character.position;
        healthSystem = new HealthSystem(character.stats.healthMax);
        healthSystem.SetHealthAmount(character.stats.health);
        //healthWorldBar = new World_Bar(transform, new Vector3(0, 10), new Vector3(15, 2), Color.grey, Color.red, healthSystem.GetHealthPercent(), UnityEngine.Random.Range(10000, 11000), new World_Bar.Outline { color = Color.black, size = .6f });
        //healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        //RefreshHealthBar();

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

    private bool spawnedTankInteractKey;

    private void HandleInteract()
    {
        //if (((int)GameData.state) < ((int)GameData.State.DefeatedTank))
        //{
        //    if (!spawnedTankInteractKey)
        //    {
        //        Character npcCharacter = GameData.GetCharacter(Character.Type.Villager_1);
        //        if (Vector3.Distance(GetPosition(), npcCharacter.position) < 12f)
        //        {
        //            spawnedTankInteractKey = true;
        //            Instantiate(GameAssets.i.pfKey, npcCharacter.position + new Vector3(0, 15), Quaternion.identity);
        //        }
        //    }
        //}

        if (Input.GetKeyDown(KeyCode.Space))
        {
            NPCOverworld npcOverworld = OverworldManager.GetInstance().GetClosestNPC(GetPosition(), 1.5f);
            if (npcOverworld != null)
            {
                switch (npcOverworld.GetCharacter().type)
                {
                    case Character.Type.NPC_1:
                        Dialogues.QuestDialogue(npcOverworld.GetCharacter());
                        //if (!npcOverworld.GetCharacter().IsFirstTimeTalking())
                        //{
                        //    Debug.Log("ENTRO EN DIALOGO");
                        //    npcOverworld.GetCharacter().AssignFirstTimeTalking();
                        //}
                        //else
                        //{
                        //    Debug.Log("ENTRO EN DIALOGO RANDOM");
                        //    Dialogues.RandomNPCDialog(npcOverworld.GetCharacter());
                        //}
                        break;
                    //case Character.Type.Shop:
                    //    Cutscenes.Play_Shop(npcOvermap.GetCharacter());
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
        RaycastHit2D raycastHit = Physics2D.BoxCast(GetPosition(), new Vector2(5, 9), 0f, Vector2.zero, 0f, wallLayerMask);
        return raycastHit.collider != null;
    }

    private bool CanMoveTo(Vector3 dir, float distance)
    {
        if (IsOnTopOfWall()) return true;
        RaycastHit2D raycastHit = Physics2D.BoxCast(GetPosition(), new Vector2(5, 9), 0f, dir, distance, wallLayerMask);
        return raycastHit.collider == null;
    }

    private void TryMoveTo(Vector3 dir, float distance)
    {
        RaycastHit2D raycastHit = Physics2D.BoxCast(GetPosition(), new Vector2(5, 9), 0f, dir, distance, wallLayerMask);
        if (raycastHit.collider == null)
        {
            transform.position += dir * distance;
        }
        else
        {
            transform.position += dir * (raycastHit.distance - .1f);
        }
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

    //private void DamageFlash()
    //{
    //    materialTintColor = new Color(1, 0, 0, 1f);
    //    material.SetColor("_Tint", materialTintColor);
    //}

    //public void DamageKnockback(Vector3 knockbackDir, float knockbackDistance)
    //{
    //    transform.position += knockbackDir * knockbackDistance;
    //    DamageFlash();
    //}

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    public void SetPosition(Vector3 position)
    {
        transform.position = position;
    }

}
