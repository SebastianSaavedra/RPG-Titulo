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
    Character character;
    private int health, attack, defense, damageChance;
    private Action onAttackHit;
    private Action onAttackComplete;
    private Action onSlideComplete;
    //private World_Bar healthWorldBar;
    private HealthSystem healthSystem;
    [HideInInspector] public StatusSystem statusSystem;
    private SpriteRenderer spriteRen;
    private bool canSpecial;
    private bool hasStatus;
    private bool isBlocking;

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
        spriteRen = gameObject.GetComponent<SpriteRenderer>();
        materialTintColor = new Color(1, 0, 0, 0);
        selectionCircleTransform = transform.Find("SelectionCircle");
        statusSystem = StatusSystem.instance;
        HideSelectionCircle();
        SetStateIdle();
    }

    private void Start()
    {
        baseTint = GetComponent<SpriteRenderer>().material.color;
    }

    public void Setup(Character.Type characterType, Battle.LanePosition lanePosition, Vector3 startingPosition, bool isPlayerTeam, Character.Stats stats, Character character)
    {
        this.characterType = characterType;
        this.lanePosition = lanePosition;
        this.startingPosition = startingPosition;
        this.isPlayerTeam = isPlayerTeam;
        this.stats = stats;
        this.character = character;

        health = stats.health;
        attack = stats.attack;
        defense = stats.defense;
        damageChance = stats.damageChance;

        if (!isPlayerTeam)
        {
            spriteRen.flipX = false;
        }

        switch (characterType)
        {
            case Character.Type.Suyai:
                anim.runtimeAnimatorController = GameAssets.i.suyaiBATTLEANIM;
                //si es q el pj tiene una armadura es posible poner un if y cambiar de animator
                break;
            case Character.Type.Antay:
                anim.runtimeAnimatorController = GameAssets.i.antayBATTLEANIM;
                break;
            case Character.Type.Pedro:
                anim.runtimeAnimatorController = GameAssets.i.pedroBATTLEANIM;
                break;
            case Character.Type.Chillpila:
                anim.runtimeAnimatorController = GameAssets.i.chillpilaBATTLEANIM;
                break;
            case Character.Type.Arana:
                anim.runtimeAnimatorController = GameAssets.i.aranaBATTLEANIM;
                break;

            /////////////////////////
            //ENEMIGOS
            /////////////////////////

            case Character.Type.TESTENEMY:
                anim.runtimeAnimatorController = GameAssets.i.testEnemyANIM;
                break;
            case Character.Type.Fusilero:
                anim.runtimeAnimatorController = GameAssets.i.fusileroANIM;
                break;
            case Character.Type.Lancero:
                anim.runtimeAnimatorController = GameAssets.i.lanceroANIM;
                break;
            case Character.Type.Anchimallen:
                anim.runtimeAnimatorController = GameAssets.i.anchimallenBattleANIM;
                break;
            case Character.Type.Guirivilo:
                anim.runtimeAnimatorController = GameAssets.i.guiriviloANIM;
                break;
            case Character.Type.Piuchen:
                anim.runtimeAnimatorController = GameAssets.i.piuchenANIM;
                break;
        }

        if (isPlayerTeam)
        {
            TurnSystem.instance.SetTurnCount(this.stats.turns);
        }
        healthSystem = new HealthSystem(stats.healthMax);
        healthSystem.SetHealthAmount(stats.health);
        //
        //healthWorldBar = new World_Bar(transform, healthWorldBarLocalPosition, new Vector3(12 * (stats.healthMax / 100f), 1.6f), Color.grey, Color.red, healthSystem.GetHealthPercent(), UnityEngine.Random.Range(1000, 2000), new World_Bar.Outline { color = Color.black, size = .6f });
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        //healthSystem.OnDead += HealthSystem_OnDead;

        PlayIdleAnim();
    }

    //IEnumerator<float> _WaitUntilAnimComplete(string name)
    //{
    //    anim.Play(name);
    //    yield return Timing.WaitForSeconds(anim.GetCurrentAnimatorStateInfo(0).length);
    //    PlayIdleAnim();
    //    yield break;
    //}

    //private void HealthSystem_OnDead(object sender, EventArgs e)
    //{
    //    healthWorldBar.Hide();
    //}

    private void HealthSystem_OnHealthChanged(object sender, EventArgs e)
    {
        if (isPlayerTeam == true)
        {
            InfoMenuWindow.instance.OnHealthChanged(character,healthSystem);
        }
    }

    public bool IsBlocking()
    {
        return isBlocking;
    }

    public void Block(Action onDefense)
    {
        isBlocking = true;
        stats.defense += 1;
        //Animación de bloquear
        Debug.Log("Se puso en modo bloqueo");
        TurnSystem.instance.SetTurnCount(1);
        onDefense();
    }

    public void LetGoBlock()
    {
        isBlocking = false;
        stats.defense -= 1;
        playerAnims.PlayAnimIdle();
        Debug.Log("Salio del modo bloqueo");
    }

    public void Revive()
    {
        //Revive Anim
        Debug.Log(character.name + " fue revivido");
        transform.eulerAngles = new Vector3(0, 0, 0);
    }

    public void Damage(CharacterBattle attacker, int damageAmount, CharacterBattle characterAttacked)
    {
        //Vector3 bloodDir = (GetPosition() - attacker.GetPosition()).normalized;
        //Blood_Handler.SpawnBlood(GetPosition(), bloodDir);

        //SoundManager.PlaySound(SoundManager.Sound.CharacterDamaged);
        healthSystem.Damage(damageAmount, characterAttacked);
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
            //healthWorldBar.Hide();
            transform.eulerAngles = new Vector3(0, 0, 90);
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
                Debug.Log("El daño de ahora es: " + stats.attack);
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
        statusSystem.SetStatusTimer(6);
    }
     
    public void StatusIcons(Sprite sprite)
    {

    }

    public void RefreshStats()
    {
        stats.attack = attack;
        stats.damageChance = damageChance;
        //healthSystem.SetHealthAmount(health);
    }

    public bool IsDead()
    {
        return healthSystem.IsDead();
    }

    public int GetHealthAmount() => healthSystem.GetHealthAmount();
    public int GetMaxHealthAmount() => healthSystem.GetMaxHealthAmount();

    public void PlayIdleAnim()
    {
        playerAnims.PlayAnimIdle();
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
                if (ResourceManager.instance.GetMoneyAmount() > 0)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
            case Character.Type.Suyai:
                if (ResourceManager.instance.GetHerbsAmount() >= 1)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
            case Character.Type.Antay:
                if (ResourceManager.instance.GetHitsAmount() >= 1)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
            case Character.Type.Arana:
                if (ResourceManager.instance.GetTattoosAmount() >= 1)
                {
                    canSpecial = true;
                }
                else
                {
                    canSpecial = false;
                }
                break;
            case Character.Type.Chillpila:
                if (ResourceManager.instance.GetSoulsAmount() >= ResourceManager.instance.GetMaxSoulsAmount() / 5)
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
        yield return Timing.WaitForSeconds(.133f);
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

    public void PlayAnimSpecial(Action OnHit,Action OnComplete)
    {
        playerAnims.PlaySpecialAttack(OnHit, OnComplete);

    }
}
