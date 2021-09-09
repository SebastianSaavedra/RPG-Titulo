using System;
using UnityEngine;
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
    private bool isPlayerTeam;
    private Vector3 startingPosition;
    private Vector3 slideToPosition;
    private Transform selectionCircleTransform;
    private Battle.LanePosition lanePosition;
    private Character.Type characterType;
    private Character.Stats stats;
    private Action onAttackHit;
    private Action onAttackComplete;
    private Action onSlideComplete;
    private World_Bar healthWorldBar;
    private HealthSystem healthSystem;

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

    public void Setup(Character.Type characterType, Battle.LanePosition lanePosition, Vector3 startingPosition, bool isPlayerTeam, Character.Stats stats)
    {
        this.characterType = characterType;
        this.lanePosition = lanePosition;
        this.startingPosition = startingPosition;
        this.isPlayerTeam = isPlayerTeam;
        this.stats = stats;

        Vector3 healthWorldBarLocalPosition = new Vector3(0, 13.25f);

        switch (characterType)
        {
            case Character.Type.Player:

                //if (GameData.GetCharacter(Character.Type.Player).hasFtnDewArmor)
                //{
                //    Texture2D newSpritesheetTexture = new Texture2D(material.mainTexture.width, material.mainTexture.height, TextureFormat.ARGB32, true);
                //    newSpritesheetTexture.SetPixels((material.mainTexture as Texture2D).GetPixels());
                //    Color[] ftnDewArmorPixels = GameAssets.i.t_FtnDewArmor.GetPixels(0, 0, 512, 128);
                //    newSpritesheetTexture.SetPixels(0, 256, 512, 128, ftnDewArmorPixels);
                //    newSpritesheetTexture.Apply();
                //    material.mainTexture = newSpritesheetTexture;
                //}

                //if (GameData.GetCharacter(Character.Type.Player).hasSwordThousandTruths)
                //{
                //    Texture2D newSpritesheetTexture = new Texture2D(material.mainTexture.width, material.mainTexture.height, TextureFormat.ARGB32, true);
                //    newSpritesheetTexture.SetPixels((material.mainTexture as Texture2D).GetPixels());
                //    Color[] swordThousandTruthsPixels = GameAssets.i.t_SwordThousandTruths.GetPixels(0, 0, 128, 128);
                //    newSpritesheetTexture.SetPixels(0, 128, 128, 128, swordThousandTruthsPixels);
                //    newSpritesheetTexture.Apply();
                //    material.mainTexture = newSpritesheetTexture;
                //}
                break;
            //case Character.Type.Tank:
            //    material.mainTexture = GameAssets.i.enemyTX;
            //    anim.runtimeAnimatorController = GameAssets.i.enemyANIM;
            //    playerAnims.PlayAnimStarter();
            //    transform.localScale = Vector3.one * 1.2f;
            //    break;
            //case Character.Type.Healer:
            //    material.mainTexture = GameAssets.i.t_Healer;
            //    playerAnims.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dDualDagger_Idle, GameAssets.UnitAnimTypeEnum.dDualDagger_Walk, 1f, 1f);
            //    attackUnitAnimType = GameAssets.UnitAnimTypeEnum.dDualDagger_Attack;
            //    transform.localScale = Vector3.one * 1.0f;
            //    break;
            //case Character.Type.EvilMonster:
            //case Character.Type.EvilMonster_2:
            //case Character.Type.EvilMonster_3:
            //    material.mainTexture = GameAssets.i.t_EvilMonster;
            //    playerAnims.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dBareHands_Idle, GameAssets.UnitAnimTypeEnum.dBareHands_Walk, 1f, 1f);
            //    attackUnitAnimType = GameAssets.UnitAnimTypeEnum.dBareHands_Punch;
            //    transform.localScale = Vector3.one * 1.8f;
            //    healthWorldBarLocalPosition.y = 9.5f;
            //    break;
            case Character.Type.TESTENEMY:
                //material.mainTexture = GameAssets.i.enemyTX;
                anim.runtimeAnimatorController = GameAssets.i.enemyANIM;
                playerAnims.PlayAnimStarter();
                //    transform.localScale = Vector3.one * 1.2f;
                break;
                //case Character.Type.Enemy_MinionRed:
                //    material.mainTexture = GameAssets.i.t_EnemyMinionRed;
                //    playerAnims.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dMinion_Idle, GameAssets.UnitAnimTypeEnum.dMinion_Walk, 1f, 1f);
                //    attackUnitAnimType = GameAssets.UnitAnimTypeEnum.dMinion_Attack;
                //    break;
                //case Character.Type.Enemy_Ogre:
                //    material.mainTexture = GameAssets.i.t_Ogre;
                //    playerAnims.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dOgre_Idle, GameAssets.UnitAnimTypeEnum.dOgre_Walk, 1f, 1f);
                //    attackUnitAnimType = GameAssets.UnitAnimTypeEnum.dOgre_Attack;
                //    hitUnitAnimType = UnitAnimType.GetUnitAnimType("dOgre_Hit");
                //    slideLeftUnitAnim = UnitAnim.GetUnitAnim("dOgre_SlideLeft");
                //    slideRightUnitAnim = UnitAnim.GetUnitAnim("dOgre_SlideRight");
                //    healthWorldBarLocalPosition.y = 12;
                //    break;
                //case Character.Type.Enemy_Zombie:
                //    material.mainTexture = GameAssets.i.t_Zombie;
                //    playerAnims.GetAnimatedWalker().SetAnimations(GameAssets.UnitAnimTypeEnum.dZombie_Idle, GameAssets.UnitAnimTypeEnum.dZombie_Walk, 1f, 1f);
                //    attackUnitAnimType = GameAssets.UnitAnimTypeEnum.dZombie_Attack;
                //    break;
        }
        //transform.Find("Body").GetComponent<MeshRenderer>().material = material;

        healthSystem = new HealthSystem(stats.healthMax);
        healthSystem.SetHealthAmount(stats.health);
        healthWorldBar = new World_Bar(transform, healthWorldBarLocalPosition, new Vector3(12 * (stats.healthMax / 100f), 1.6f), Color.grey, Color.red, healthSystem.GetHealthPercent(), UnityEngine.Random.Range(1000, 2000), new World_Bar.Outline { color = Color.black, size = .6f });
        healthSystem.OnHealthChanged += HealthSystem_OnHealthChanged;
        healthSystem.OnDead += HealthSystem_OnDead;

        PlayIdleAnim();
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
        //DamagePopup.Create(GetPosition(), damageAmount, false);
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
                if (characterType != Character.Type.EvilMonster && characterType != Character.Type.EvilMonster_2 && characterType != Character.Type.EvilMonster_3)
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
                stats.special = 0;
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
        material.SetColor("_Tint", materialTintColor);
        healthSystem.Heal(healAmount);
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

    public int GetSpecial()
    {
        return stats.special;
    }

    public bool TrySpendSpecial()
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
                    // Reached Target position
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

        //if (materialTintColor.a > 0)
        //{
        //    float tintFadeSpeed = 6f;
        //    materialTintColor.a -= tintFadeSpeed * Time.deltaTime;
        //    material.SetColor("_Tint", materialTintColor);
        //}
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
        material.SetColor("_Tint", materialTintColor);
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

    public void PlayAnimSpecialAttack(Action OnTrigger,Action OnHit)
    {
        playerAnims.PlaySpecialAttack();
        OnTrigger();
        OnHit();

    }
}
