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
    private int health, attack, defense, damageChance, critChance;
    private Action onAttackHit;
    private Action onAttackComplete;
    private Action onSlideComplete;
    //private World_Bar healthWorldBar;
    private HealthSystem healthSystem;
    [HideInInspector] public StatusAndBuffsSystem statusSystem;
    private SpriteRenderer spriteRen;
    private bool canSpecial;
    private bool hasStatus;
    private bool isBlocking;
    private Transform buffArrow, debuffArrow;

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
        buffArrow = transform.Find("BuffArrow");
        debuffArrow = transform.Find("DebuffArrow");
        statusSystem = StatusAndBuffsSystem.instance;
        HideSelectionCircle();
        SetStateIdle();
    }

    private void Start()
    {
        baseTint = GetComponent<SpriteRenderer>().material.color;
        
        switch (characterType)
        {
            case Character.Type.Arana:
                anim.applyRootMotion = true;
                break;
            case Character.Type.Anchimallen:
                anim.applyRootMotion = true;

                Transform anchimallenFire;
                anchimallenFire = Instantiate(GameAssets.i.anchimallenFire, transform.position, Quaternion.identity);
                anchimallenFire.parent = transform;
                anchimallenFire.localPosition = Vector3.zero;
                anchimallenFire.localScale = new Vector3(1f, 1f, 1f);
                break;
            case Character.Type.Piuchen:
            case Character.Type.Guirivilo:
            case Character.Type.Lancero:
            case Character.Type.Fusilero:
                character.stats.special = UnityEngine.Random.Range(2, 5);
                character.stats.specialMax = character.stats.special;
                break;
            case Character.Type.CaiCai:
                character.stats.special = 4;
                character.stats.specialMax = character.stats.special;
                break;
        }
        if (!character.IsInPlayerTeam())
        {
            spriteRen.sortingOrder -= 1;
            Debug.Log(characterType + "Tiene esta cantidad de special " + character.stats.special + "y max " + character.stats.specialMax);
        }
    }

    public void Setup(Character.Type characterType, Battle.LanePosition lanePosition, Vector3 startingPosition, bool isPlayerTeam, Character.Stats stats, Character character)
    {
        this.characterType = characterType;
        this.lanePosition = lanePosition;
        this.startingPosition = startingPosition;
        this.isPlayerTeam = isPlayerTeam;
        this.stats = stats;
        this.character = character;

        //health = stats.health;
        attack = stats.attack;
        defense = stats.defense;
        damageChance = stats.damageChance;
        critChance = stats.critChance;

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

            /////////////////////////
            //CAPTURADOS
            /////////////////////////

            //case Character.Type.TrenTren:
            //    anim.runtimeAnimatorController = GameAssets.i.trentrenBattleAnim;
            //    break;
            case Character.Type.CaiCai:
                anim.runtimeAnimatorController = GameAssets.i.caicaiBattleAnim;
                break;

        }

        if (isPlayerTeam)
        {
            TurnSystem.instance.SetTurnCount(this.stats.turns);
        }
        healthSystem = new HealthSystem(stats.healthMax);
        healthSystem.SetHealthAmount(stats.health);
      
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
        //character.isDead = false;
        playerAnims.PlayAnimIdle();
    }

    public void Damage(CharacterBattle attacker, int damageAmount, CharacterBattle characterAttacked)
    {
        //SoundManager.PlaySound(SoundManager.Sound.CharacterDamaged);
        healthSystem.Damage(damageAmount, characterAttacked);
        DamageFlash();



        if (IsDead())
        {
            if (!IsPlayerTeam())
            {
                // Enemy
                //if (characterType != Character.Type.Jefe1)
                //{
                //    // Don't spawn Flying Body for Evil Monster
                //    Debug.Log("Se murio");
                //    //FlyingBody.Create(GameAssets.i.pfEnemyFlyingBody, GetPosition(), bloodDir);
                //    //SoundManager.PlaySound(SoundManager.Sound.CharacterDead);
                //}
                gameObject.SetActive(false);
            }
            else
            {
                // Player Team
                //SoundManager.PlaySound(SoundManager.Sound.OooohNooo);
            }
            playerAnims.PlayAnimDefeated();
            //playerAnims.GetUnitAnimation().PlayAnimForced(UnitAnim.GetUnitAnim("LyingUp"), 1f, null);
            ////healthWorldBar.Hide();
            //transform.eulerAngles = new Vector3(0, 0, 90);
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        }
        else
        {
            // Knockback
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
                if (!buffArrow.gameObject.activeInHierarchy)
                {
                    buffArrow.gameObject.SetActive(true);
                }
                Debug.Log("El daño de " + characterType + " ahora es:" + stats.attack);
                break;
            case 1:
                stats.attack += 10;
                if (!buffArrow.gameObject.activeInHierarchy)
                {
                    buffArrow.gameObject.SetActive(true);
                }
                Debug.Log("El daño de " + characterType + " ahora es:" + stats.attack);
                break;
            case 2:
                stats.defense += 2;
                if (!buffArrow.gameObject.activeInHierarchy)
                {
                    buffArrow.gameObject.SetActive(true);
                }
                Debug.Log("La defensa de " + characterType + " ahora es:" + stats.defense);
                break;
            case 3:
                stats.critChance += 50;
                if (!buffArrow.gameObject.activeInHierarchy)
                {
                    buffArrow.gameObject.SetActive(true);
                }
                Debug.Log("El critico de " + characterType + " ahora es:" + stats.critChance);
                break;
        }
        statusSystem.SetBuffsTimer(2);
    }

    public void Debuff(int number)
    {
        switch (number)
        {
            default:
                break;
            case 0:
                {
                    stats.attack /= 2;
                    if (!debuffArrow.gameObject.activeInHierarchy)
                    {
                        debuffArrow.gameObject.SetActive(true);
                    }
                    Debug.Log("El daño total del enemigo es: " + stats.attack);
                    break;
                }
            case 1:
                {
                    stats.damageChance /=  2;
                    if (!debuffArrow.gameObject.activeInHierarchy)
                    {
                        debuffArrow.gameObject.SetActive(true);
                    }
                    Debug.Log("La probabilidad de atacar del enemigo es: " + stats.damageChance);
                    break;
                }
            case 2:
                {
                    stats.defense -= 1;
                    if (!debuffArrow.gameObject.activeInHierarchy)
                    {
                        debuffArrow.gameObject.SetActive(true);
                    }
                    Debug.Log("La defensa actual del enemigo es: " + stats.defense);
                    break;
                }
            case 3:
                {
                    stats.critChance /= 2;
                    if (!debuffArrow.gameObject.activeInHierarchy)
                    {
                        debuffArrow.gameObject.SetActive(true);
                    }
                    Debug.Log("La probabilidad de critico actual del enemigo es: " + stats.critChance);
                    break;
                }
        }
        statusSystem.SetStatusTimer(2);
    }

    public void RefreshStats()
    {
        stats.attack = attack;
        stats.defense = defense;
        stats.damageChance = damageChance;
        stats.critChance = critChance;
        if (buffArrow.gameObject.activeInHierarchy)
        {
            buffArrow.gameObject.SetActive(false);
        }
        if (debuffArrow.gameObject.activeInHierarchy)
        {
            debuffArrow.gameObject.SetActive(false);
        }
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

    public int GetSpecial()
    {
        return stats.special;
    }

    public bool EnemyTrySpendSpecial()
    {
        if (stats.special <= 0)
        {
            stats.special = stats.specialMax;
            return true;
        }
        else
        {
            return false;
        }
    }

    public void TickSpecialCooldown()
    {
        if (stats.special > 0)
        {
            stats.special--;
        }
    }

    public bool TrySpendSpecial()
    {
        switch (characterType)
        {
            case Character.Type.Pedro:
                if (ResourceManager.instance.GetMoneyAmount() >= 25)
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
                if (ResourceManager.instance.GetSoulsAmount() >= 6)
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
    public bool HasBuffs()
    {
        return statusSystem.HasBuffs();
    }

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
