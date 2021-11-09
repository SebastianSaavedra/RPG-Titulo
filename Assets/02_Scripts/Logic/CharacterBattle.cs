using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using CodeMonkey.Utils;
public class CharacterBattle : MonoBehaviour
{
    public static CharacterBattle instance;

    private const float SPEED = 50f;

    private Character_Anims playerAnims;
    private State state;
    private Material material;
    private Animator anim;
    private Color materialTintColor;
    private Color baseTint;
    private bool isPlayerTeam;
    private Vector3 startingPosition;
    private Vector3 slideToPosition;
    private Transform selectionCircleTransform;
    private Battle.LanePosition lanePosition;
    private Character.Type characterType;
    [HideInInspector] public Character.Stats stats;
    private Action onAttackHit;
    private Action onAttackComplete;
    private Action onSlideComplete;
    private World_Bar healthWorldBar;
    private HealthSystem healthSystem;
    [HideInInspector] public StatusSystem statusSystem;
    private bool canSpecial;
    private bool hasStatus;

    Sprite sprite;

    Vector3 healthWorldBarLocalPosition = new Vector3(0, 13.25f);

    private enum State
    {
        Idle,
        SlideToTargetAndAttack,
        AttackingTarget,
        SlidingBack,
        SlideToPosition,
        Busy,
    }

    private void Awake()
    {
        instance = this;
        playerAnims = gameObject.GetComponent<Character_Anims>();
        anim = gameObject.GetComponent<Animator>();
        material = transform.GetComponent<SpriteRenderer>().material;
        materialTintColor = new Color(1, 0, 0, 0);
        selectionCircleTransform = transform.Find("SelectionCircle");
        HideSelectionCircle();
        SetStateIdle();
    }

    private void Start()
    {
        baseTint = GetComponent<SpriteRenderer>().material.color;
    }

    public void Setup(Character.Type characterType, Battle.LanePosition lanePosition, Vector3 startingPosition, bool isPlayerTeam, Character.Stats stats)
    {
        this.characterType = characterType;
        this.lanePosition = lanePosition;
        this.startingPosition = startingPosition;
        this.isPlayerTeam = isPlayerTeam;
        this.stats = stats;

        switch (characterType)
        {
            case Character.Type.Suyai:
                material.color = new Color(.75f, 1f, 0f);
                anim.runtimeAnimatorController = GameAssets.i.allyANIM;
                transform.localScale = Vector3.one * .75f;

                //if (GameData.GetCharacter(Character.Type.Player).hasFtnDewArmor)
                //{
                //    Texture2D newSpritesheetTexture = new Texture2D(material.mainTexture.width, material.mainTexture.height, TextureFormat.ARGB32, true);
                //    newSpritesheetTexture.SetPixels((material.mainTexture as Texture2D).GetPixels());
                //    Color[] ftnDewArmorPixels = GameAssets.i.t_FtnDewArmor.GetPixels(0, 0, 512, 128);
                //    newSpritesheetTexture.SetPixels(0, 256, 512, 128, ftnDewArmorPixels);
                //    newSpritesheetTexture.Apply();
                //    material.mainTexture = newSpritesheetTexture;
                //}
                break;
            case Character.Type.Antay:
                material.color = new Color(.7f, .4f, .2f);
                anim.runtimeAnimatorController = GameAssets.i.allyANIM;
                transform.localScale = Vector3.one * .75f;
                break;
            case Character.Type.Pedro:
                material.color = new Color(1f, 1f, 0f);
                anim.runtimeAnimatorController = GameAssets.i.allyANIM;
                transform.localScale = Vector3.one * .75f;
                break;
            case Character.Type.Chillpila:
                material.color = new Color(.55f, 0f, 1f);
                anim.runtimeAnimatorController = GameAssets.i.allyANIM;
                transform.localScale = Vector3.one * .75f;
                break;
            case Character.Type.Arana:
                material.color = new Color(.25f, 0.25f, 0.25f);
                anim.runtimeAnimatorController = GameAssets.i.allyANIM;
                transform.localScale = Vector3.one * .75f;
                break;

            /////////////////////////
            //ENEMIGOS
            /////////////////////////
            
            case Character.Type.TESTENEMY:
                anim.runtimeAnimatorController = GameAssets.i.enemyANIM;
                break;
        }
        //transform.Find("Body").GetComponent<MeshRenderer>().material = material;

        if (isPlayerTeam)
        {
            TurnSystem.instance.SetTurnCount(this.stats.turns);
        }
        switch (characterType)
        {
            case Character.Type.Pedro:
                SpecialAbilitiesCostSystem.instance.SetMoneyAmount(100);
                break;
            case Character.Type.Suyai:
                SpecialAbilitiesCostSystem.instance.SetHerbsAmount(10);
                break;
            case Character.Type.Arana:
                SpecialAbilitiesCostSystem.instance.SetTattoosAmount(5);
                break;
            case Character.Type.Antay:
                SpecialAbilitiesCostSystem.instance.SetHitsAmount(5);
                break;
            case Character.Type.Chillpila:
                SpecialAbilitiesCostSystem.instance.SetSoulsAmount(50);
                break;
        }
        healthSystem = new HealthSystem(stats.healthMax);
        healthSystem.SetHealthAmount(stats.health);
        statusSystem = new StatusSystem();
        //
        healthWorldBar = new World_Bar(transform, healthWorldBarLocalPosition, new Vector3(12 * (stats.healthMax / 100f), 1.6f), Color.grey, Color.red, healthSystem.GetHealthPercent(), UnityEngine.Random.Range(1000, 2000), new World_Bar.Outline { color = Color.black, size = .6f });
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        Timing.RunCoroutine(_WaitUntilAnimComplete("Base Layer.TEST_START"));
    }

    IEnumerator<float> _WaitUntilAnimComplete(string name)
    {
        anim.Play(name);
        yield return Timing.WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
        PlayIdleAnim();
        yield break;
    }

    private void HealthSystem_OnDead(object sender, EventArgs e)
    {
        healthWorldBar.Hide();
    }

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        healthWorldBar.SetSize(healthSystem.GetHealthPercent());
    }

    public void Damage(CharacterBattle attacker, int damageAmount)
    {
        //Vector3 bloodDir = (GetPosition() - attacker.GetPosition()).normalized;
        //Blood_Handler.SpawnBlood(GetPosition(), bloodDir);

        //SoundManager.PlaySound(SoundManager.Sound.CharacterDamaged);
        DamagePopups.Create(GetPosition(), damageAmount, false);
        healthSystem.Damage(damageAmount);
        DamageFlash();

        //ANIMACION DE SER HITEADO
        //playerAnims.GetUnitAnimation().PlayAnimForced(hitUnitAnimType, GetTargetDir(), 1f, (UnitAnim unitAnim) => 
        //{
        //    PlayIdleAnim();
        //}, null, null);
        //ANIMACION DE SER HITEADO

        if (IsDead())
        {
            if (!IsPlayerTeam())
            {
                // Enemy
                if (characterType != Character.Type.Jefe1) // && characterType != Character.Type.Jefe2 && characterType != Character.Type.Jefe3
                {
                    // Don't spawn Flying Body for Evil Monster
                    Debug.Log("Se murio");
                    //FlyingBody.Create(GameAssets.i.pfEnemyFlyingBody, GetPosition(), bloodDir);
                    //SoundManager.PlaySound(SoundManager.Sound.CharacterDead);
                }
                gameObject.SetActive(false);
            }
            else
            {
                // Player Team
                //SoundManager.PlaySound(SoundManager.Sound.OooohNooo);
            }
            //playerAnims.GetUnitAnimation().PlayAnimForced(UnitAnim.GetUnitAnim("LyingUp"), 1f, null);
            healthWorldBar.Hide();
            transform.localScale = new Vector3(-1, 1, 1);
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        else
        {
            // Knockback
            /*
            transform.position += bloodDir * 5f;
            if (hitUnitAnim != null) {
                state = State.Busy;
                enemyBase.PlayHitAnimation(bloodDir * (Vector2.one * -1f), SetStateNormal);
            }
            */
        }
    }

    public void Heal(int healAmount)
    {
        materialTintColor = new Color(0, 1, 0, 1f);
        Timing.RunCoroutine(_TintColor(materialTintColor));
        healthSystem.Heal(healAmount);
    }

    public void Buff(int number)
    {
        switch (number)
        {
            default:
                break;
            case 0:
                stats.attack += 5;
                break;
            case 1:
                Debug.Log("Experiencia al final del combate aumentada!");
                break;
            case 2:
                Debug.Log("Probabilidad de golpe critico aumentada!");
                break;
        }
    }

    public void Debuff(int number)
    {
        switch (number)
        {
            default:
                break;
            case 0:
                {
                    stats.attack -= 6;
                    //sprite = GameAssets.i.dmgDebuff.GetComponent<Sprite>();
                    Debug.Log("El daño total del enemigo es: " + stats.attack);
                    break;
                }
            case 1:
                {
                    stats.damageChance = 33;
                    //sprite = GameAssets.i.blindDebuff.GetComponent<Sprite>();
                    Debug.Log("La probabilidad de atacar del enemigo es: " + stats.damageChance);
                    break;
                }
            case 2:
                {
                    healthSystem.SetHealthAmount(healthSystem.GetHealthAmount() / 2);
                    //sprite = GameAssets.i.healthDebuff.GetComponent<Sprite>();
                    Debug.Log("La vida actual del enemigo es: " + healthSystem.GetHealthAmount());
                    break;
                }
        }
        statusSystem.SetStatusTimer(3);
    }

    public void StatusIcons(Sprite sprite)
    {

    }

    public bool IsDead()
    {
        return healthSystem.IsDead();
    }

    public int GetHealthAmount() => healthSystem.GetHealthAmount();

    public void PlayIdleAnim()
    {
        playerAnims.PlayAnimIdle();
        //playerBase.GetUnitAnimation().PlayAnimForced(UnitAnim.GetUnitAnim("LyingUp"), 1f, null);
    }

    private Vector3 GetTargetDir()
    {
        return new Vector3(isPlayerTeam ? 1 : -1, 0);
    }

    public bool IsPlayerTeam()
    {
        return isPlayerTeam;
    }

    public Battle.LanePosition GetLanePosition()
    {
        return lanePosition;
    }

    public Character.Type GetCharacterType()
    {
        return characterType;
    }

    public int GetAttack()
    {
        return stats.attack;
    }

    //public int GetSpecial()
    //{
    //    return stats.special;
    //}

    public bool TrySpendSpecial()
    {
        switch (characterType)
        {
            case Character.Type.Pedro:
                if (SpecialAbilitiesCostSystem.instance.GetMoneyAmount() >= SpecialAbilitiesCostSystem.instance.GetMaxMoneyAmount() / 4)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
            case Character.Type.Suyai:
                if (SpecialAbilitiesCostSystem.instance.GetHerbsAmount() >= SpecialAbilitiesCostSystem.instance.GetMaxHerbsAmount() / 5)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
            case Character.Type.Antay:
                if (SpecialAbilitiesCostSystem.instance.GetHitsAmount() >= SpecialAbilitiesCostSystem.instance.GetMaxHitsAmount() / 5)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
            case Character.Type.Arana:
                if (SpecialAbilitiesCostSystem.instance.GetTattoosAmount() >= SpecialAbilitiesCostSystem.instance.GetMaxTattoosAmount() / 5)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
            case Character.Type.Chillpila:
                if (SpecialAbilitiesCostSystem.instance.GetSoulsAmount() >= SpecialAbilitiesCostSystem.instance.GetMaxSoulsAmount() / 5)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
        }
        return canSpecial;
    }

    public bool HasStatus()
    {
        return statusSystem.HasStatus();
    }

    //public void TickSpecialCooldown()
    //{
    //    if (stats.special > 0)
    //    {
    //        stats.special--;
    //    }
    //}


    private void Update()
    {
        switch (state)
        {
            case State.Idle:
                break;
            case State.SlideToTargetAndAttack:
                if (Vector3.Distance(slideToPosition, GetPosition()) > 2f)
                {
                    float slideSpeed = 10f;
                    transform.position += (slideToPosition - GetPosition()) * slideSpeed * Time.deltaTime;
                }
                else
                {
                    // Llego a la posicion del objetivo
                    state = State.AttackingTarget;
                    playerAnims.PlayAnimAttack(() =>
                    {
                        onAttackHit();
                    },
                    () =>
                    {
                        PlayIdleAnim();
                        onAttackComplete();
                    });
                }
                break;
            case State.SlideToPosition:
                if (Vector3.Distance(slideToPosition, GetPosition()) > 2f)
                {
                    float slideSpeed = 10f;
                    transform.position += (slideToPosition - GetPosition()) * slideSpeed * Time.deltaTime;
                }
                else
                {
                    // Reached Target position
                    state = State.Busy;
                    PlayIdleAnim();
                    state = State.Idle;
                    onSlideComplete();
                }
                break;
            case State.AttackingTarget:
                break;
            case State.Busy:
                break;
            case State.SlidingBack:
                if (Vector3.Distance(slideToPosition, GetPosition()) > 2f)
                {
                    float slideSpeed = 10f;
                    transform.position += (slideToPosition - GetPosition()) * slideSpeed * Time.deltaTime;
                }
                else
                {
                    // Reached Target position
                    PlayIdleAnim();
                    state = State.Idle;
                    onSlideComplete();
                }
                break;
        }
    }

    private void SetStateIdle()
    {
        state = State.Idle;
    }

    public void HideSelectionCircle()
    {
        selectionCircleTransform.gameObject.SetActive(false);
    }

    public void ShowSelectionCircle(Color color)
    {
        selectionCircleTransform.gameObject.SetActive(true);
        selectionCircleTransform.GetComponent<SpriteRenderer>().color = color;
    }

    private void DamageFlash()
    {
        materialTintColor = new Color(1, 0, 0, 1f);
        Timing.RunCoroutine(_TintColor(materialTintColor));
    }

    IEnumerator<float> _TintColor(Color color)
    {
        material.SetColor("_Color", materialTintColor);
        yield return Timing.WaitForSeconds(.125f);
        material.SetColor("_Color",baseTint);
        yield break;
    }

    public void DamageKnockback(Vector3 knockbackDir, float knockbackDistance)
    {
        transform.position += knockbackDir * knockbackDistance;
        DamageFlash();
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }

    //private void PlaySlideToTargetAnim()
    //{
    //    SoundManager.PlaySound(SoundManager.Sound.Dash);
    //    if (isPlayerTeam)
    //    {
    //        playerAnims.GetUnitAnimation().PlayAnimForced(slideRightUnitAnim, 1f, null);
    //    }
    //    else
    //    {
    //        playerAnims.GetUnitAnimation().PlayAnimForced(slideLeftUnitAnim, 1f, null);
    //    }
    //}

    //private void PlaySlideBackAnim()
    //{
    //    if (isPlayerTeam)
    //    {
    //        playerAnims.GetUnitAnimation().PlayAnimForced(slideLeftUnitAnim, 1f, null);
    //    }
    //    else
    //    {
    //        playerAnims.GetUnitAnimation().PlayAnimForced(slideRightUnitAnim, 1f, null);
    //    }
    //}

    public void AttackTarget(Vector3 targetPosition, Action onAttackHit, Action onAttackComplete)
    {
        this.onAttackHit = onAttackHit;
        this.onAttackComplete = onAttackComplete;
        slideToPosition = targetPosition + (GetPosition() - targetPosition).normalized * 10f;
        //PlaySlideToTargetAnim();
        state = State.SlideToTargetAndAttack;
    }

    public void SlideBack(Action onSlideComplete)
    {
        this.onSlideComplete = onSlideComplete;
        slideToPosition = startingPosition;
        //PlaySlideBackAnim();
        state = State.SlidingBack;
    }

    public void SlideToPosition(Vector3 targetPosition, Action onSlideComplete)
    {
        this.onSlideComplete = onSlideComplete;
        slideToPosition = targetPosition;
        //PlaySlideToTargetAnim();
        state = State.SlideToPosition;
    }

    public void PlayAnimSpecialAttack(Action OnHit,Action OnComplete)
    {
        playerAnims.PlaySpecialAttack(OnHit, OnComplete);

    }
}
