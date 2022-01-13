using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;
using CodeMonkey.MonoBehaviours;
using MEC;

public class Battle
{

    private static Battle instance;
    public static Battle GetInstance() => instance;

    public static Character character;
    public static GameData.EnemyEncounter enemyEncounter;
    public static EnemyOverworld enemyOverworld;

    public static void LoadEnemyEncounter(Character character, GameData.EnemyEncounter enemyEncounter)
    {
        UIFade.FadeIn();
        OverworldManager.SaveAllCharacterPositions();
        Battle.character = character;
        Battle.enemyEncounter = enemyEncounter;

        FunctionTimer.Create(OverworldManager.LoadFromOvermapToBattle, UIFade.GetTimer());
    }

    private CameraFollow cameraFollow;
    public List<CharacterBattle> characterBattleList;
    public CharacterBattle activeCharacterBattle;
    public CharacterBattle selectedTargetCharacterBattle;
    public CharacterBattle aiTargetCharacterBattle;

    private static CharacterBattle trenTrenCapturadoCharacterBattle;
    private bool alreadyTrenTrenSpeaked;

    private string enemyCommand;


    private LanePosition lastPlayerActiveLanePosition;
    private LanePosition lastEnemyActiveLanePosition;

    Vector3 middlePosition = GetPosition(LanePosition.Middle, false) + new Vector3(-35, 0);

    public State state;

    public enum LanePosition
    {
        Middle,
        Up,
        Down,
        Captured,
    }

    public enum State
    {
        WaitingForPlayer,
        OnMenu,
        EnemySelection,
        AllySelection,
        DeadAllySelection,
        OnInventory,
        Busy,
    }

    public Battle()
    {
        instance = this;
        characterBattleList = new List<CharacterBattle>();
        state = State.Busy;
    }

    public static void SpawnCharacter(Character.Type characterType, Battle.LanePosition lanePosition, bool isPlayerTeam, Character character)
    {
        Transform characterTransform = Object.Instantiate(GameAssets.i.pfCharacterBattle, GetPosition(lanePosition, isPlayerTeam), Quaternion.identity);
        CharacterBattle characterBattle = characterTransform.GetComponent<CharacterBattle>();
        if (GameData.state == GameData.State.SavingTrenTren && characterType == Character.Type.TrenTren)
        {
            characterBattle.Setup(characterType, lanePosition, GetPosition(lanePosition, isPlayerTeam), true, character.stats, character);
            Transform trenTrenCapturadoObj;
            trenTrenCapturadoObj = Object.Instantiate(GameAssets.i.pfTrenTren, GetPosition(lanePosition, isPlayerTeam), Quaternion.identity);
            trenTrenCapturadoObj.transform.parent = characterTransform.transform;
            trenTrenCapturadoObj.transform.localPosition = Vector3.zero;
            trenTrenCapturadoObj.transform.localScale = new Vector3(1f, 1f, 1f);
            trenTrenCapturadoCharacterBattle = characterBattle;
        }
        else if (GameData.state == GameData.State.FightingCaiCai && characterType == Character.Type.CaiCai)
        {
            characterBattle.Setup(characterType, lanePosition, GetPosition(lanePosition, isPlayerTeam), isPlayerTeam, character.stats, character);
            Transform caicaiTransform;
            caicaiTransform = Object.Instantiate(GameAssets.i.pfColaCaiCai, GetPosition(lanePosition, isPlayerTeam), Quaternion.identity);
            caicaiTransform.transform.parent = characterTransform.transform;
            //caicaiTransform.transform.localPosition = Vector3.zero;
            caicaiTransform.transform.localScale = new Vector3(1f, 1f, 1f);
        }
        else
        {
            characterBattle.Setup(characterType, lanePosition, GetPosition(lanePosition, isPlayerTeam), isPlayerTeam, character.stats, character);
        }
        instance.characterBattleList.Add(characterBattle);
    }

    public static Vector3 GetPosition(LanePosition lanePosition, bool isPlayerTeam)
    {
        float playerTeamMultiplier = isPlayerTeam ? -1f : 1f;

        switch (lanePosition)
        {
            default:
            case LanePosition.Middle: return new Vector3(playerTeamMultiplier * 37.6f, -8.3f);
            case LanePosition.Up: return new Vector3(playerTeamMultiplier * 67.8f, 5f);
            case LanePosition.Down: return new Vector3(playerTeamMultiplier * 57.9f, -28.7f);
            case LanePosition.Captured: return new Vector3(playerTeamMultiplier * 78f, -24f);
        }

    }

    public static LanePosition GetNextLanePosition(LanePosition lanePosition, bool moveUp)
    {
        if (GameData.state == GameData.State.FightingCaiCai)
        {
            if (moveUp)
            {
                switch (lanePosition)
                {
                    default:
                    case LanePosition.Captured: return LanePosition.Down;
                    case LanePosition.Up: return LanePosition.Captured;
                    case LanePosition.Middle: return LanePosition.Up;
                    case LanePosition.Down: return LanePosition.Middle;
                }
            }
            else
            {
                switch (lanePosition)
                {
                    default:
                    case LanePosition.Up: return LanePosition.Middle;
                    case LanePosition.Middle: return LanePosition.Down;
                    case LanePosition.Down: return LanePosition.Captured;
                    case LanePosition.Captured: return LanePosition.Up;
                }
            }

        }
        else
        {
            if (moveUp)
            {
                switch (lanePosition)
                {
                    default:
                    case LanePosition.Up: return LanePosition.Down;
                    case LanePosition.Middle: return LanePosition.Up;
                    case LanePosition.Down: return LanePosition.Middle;
                }
            }
            else
            {
                switch (lanePosition)
                {
                    default:
                    case LanePosition.Up: return LanePosition.Middle;
                    case LanePosition.Middle: return LanePosition.Down;
                    case LanePosition.Down: return LanePosition.Up;
                }
            }
        }
    }

    public void Start(CameraFollow cameraFollow)
    {
        this.cameraFollow = cameraFollow;
        cameraFollow.Setup(() => Vector3.zero, () => 50f, true, true);
        cameraFollow.SetCameraMoveSpeed(10f);
        cameraFollow.SetCameraZoomSpeed(10f);
        ResetCamera();
        //UIFade.Show();
        UIFade.FadeOut();
        SoundManager.PlaySoundLoop(SoundManager.Sound.BattleTheme);

        // Se inicia el combate
        foreach (Character character in GameData.characterList)
        {
            if (character.IsInPlayerTeam())
            {
                LanePosition lanePosition;
                switch (character.type)
                {
                    default:
                    case Character.Type.Suyai:
                        lanePosition = (LanePosition)character.lanePosition;
                        break;
                    case Character.Type.Antay:
                        lanePosition = (LanePosition)character.lanePosition;
                        break;
                    case Character.Type.Pedro:
                        lanePosition = (LanePosition)character.lanePosition;
                        break;
                    case Character.Type.Arana:
                        lanePosition = (LanePosition)character.lanePosition;
                        break;
                    case Character.Type.Chillpila:
                        lanePosition = (LanePosition)character.lanePosition;
                        break;
                }
                SpawnCharacter(character.type, lanePosition, true, character);
            }
            if (GameData.state == GameData.State.SavingTrenTren && character.type == Character.Type.TrenTren)
            {
                Debug.Log("Spawneo a trentren!");
                SpawnCharacter(character.type, LanePosition.Captured, false, character);
            }
            ResourceManager.instance.RefreshResourcesUI();
        }

        GameData.EnemyEncounter enemyEncounter = Battle.enemyEncounter;
        for (int i = 0; i < enemyEncounter.enemyBattleArray.Length; i++)
        {
            GameData.EnemyEncounter.EnemyBattle enemyBattle = enemyEncounter.enemyBattleArray[i];
            SpawnCharacter(enemyBattle.characterType, enemyBattle.lanePosition, false, new Character(enemyBattle.characterType));
        }

        SetActiveCharacterBattle(GetAliveTeamCharacterBattleList(true)[0]);

        lastPlayerActiveLanePosition = GetAliveTeamCharacterBattleList(true)[0].GetLanePosition();
        lastEnemyActiveLanePosition = GetNextLanePosition(GetAliveTeamCharacterBattleList(false)[0].GetLanePosition(), true);

        switch (GameData.state)
        {
            default:
                state = State.WaitingForPlayer;
                break;
            case GameData.State.SavingTrenTren:
                Dialogues.Play_TrenTrenStartingBattle();
                break;
            case GameData.State.FightingCaiCai:
                Dialogues.Play_CaiCaiDuringBattle();
                break;
        }
    }

    private void ResetCamera()
    {
        cameraFollow.SetCameraFollowPosition(Vector3.zero);
        cameraFollow.SetCameraZoom(50f);
    }

    private void SetCamera(Vector3 position, float zoom)
    {
        cameraFollow.SetCameraFollowPosition(position);
        cameraFollow.SetCameraZoom(zoom);
    }

    private void SetActiveCharacterBattle(CharacterBattle characterBattle)
    {
        if (activeCharacterBattle != null)
        {
            activeCharacterBattle.HideSelectionCircle();
        }
        activeCharacterBattle = characterBattle;
        activeCharacterBattle.ShowSelectionCircle(new Color(0, 1, 0, 1));

    }
    /// <summary>
    ///Selecciona un personaje para Atacar
    /// </summary>
    /// <param name="characterBattle"></param>
    public void SetSelectedTargetCharacterBattle(CharacterBattle characterBattle)
    {
        if (selectedTargetCharacterBattle != null)
        {
            selectedTargetCharacterBattle.HideSelectionCircle();
        }
        selectedTargetCharacterBattle = characterBattle;
        selectedTargetCharacterBattle.ShowSelectionCircle(new Color(1, 0, 0, 1));
    }

    /// <summary>
    /// Para cambiar de objetivo
    /// </summary>
    /// <param name="isUp"></param>
    public void UITargetSelect(bool isUp, bool isTeam, bool isAlive)
    {
        if (isAlive)
        {
            SetSelectedTargetCharacterBattle(GetNextCharacterBattle(selectedTargetCharacterBattle.GetLanePosition(), isUp, isTeam));
        }
        else
        {
            SetSelectedTargetCharacterBattle(GetNextDeadCharacterBattle(selectedTargetCharacterBattle.GetLanePosition(), isUp, isTeam));
        }
    }

    public void SelectCapturedTarget()
    {
        SetSelectedTargetCharacterBattle(trenTrenCapturadoCharacterBattle);
    }

    private CharacterBattle GetNextCharacterBattle(LanePosition lanePosition, bool moveUp, bool isPlayerTeam)
    {
        List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(isPlayerTeam);
        if (characterBattleList.Count == 0)
        {
            return null;
        }

        CharacterBattle characterBattle = null;
        do
        {
            // Elige otra linea
            lanePosition = GetNextLanePosition(lanePosition, moveUp);
            // Busca el pj que esta en esa linea
            characterBattle = GetCharacterBattleAt(lanePosition, isPlayerTeam);

            if (characterBattle != null && characterBattle.IsDead())
            {
                characterBattle = null;
            }
        } 
        while (characterBattle == null);

        return characterBattle;
    }

    private CharacterBattle GetNextDeadCharacterBattle(LanePosition lanePosition, bool moveUp, bool isPlayerTeam)
    {
        CharacterBattle characterBattle = null;
        do
        {
            // Elige otra linea
            lanePosition = GetNextLanePosition(lanePosition, moveUp);
            // Busca el pj que esta en esa linea
            characterBattle = GetCharacterBattleAt(lanePosition, isPlayerTeam);
        }
        while (!characterBattle.IsDead());

        return characterBattle;
    }

    private CharacterBattle GetCharacterBattleAt(LanePosition lanePosition, bool isPlayerTeam)
    {
        foreach (CharacterBattle characterBattle in characterBattleList)
        {
            if (characterBattle.IsPlayerTeam() == isPlayerTeam && characterBattle.GetLanePosition() == lanePosition)
            {
                return characterBattle;
            }
        }
        return null;
    }

    private List<CharacterBattle> GetTeamCharacterBattleList(bool isPlayerTeam)
    {
        List<CharacterBattle> character = new List<CharacterBattle>();
        foreach (CharacterBattle characterBattle in characterBattleList)
        {
            if (characterBattle.IsPlayerTeam() == isPlayerTeam)
            {
                character.Add(characterBattle);
            }
        }
        return character;
    }

    public List<CharacterBattle> GetAliveTeamCharacterBattleList(bool isPlayerTeam)
    {
        List<CharacterBattle> character = new List<CharacterBattle>();
        foreach (CharacterBattle characterBattle in characterBattleList)
        {
            if (characterBattle.IsDead())
            {
                continue; // El personaje esta muerto
            }
            if (characterBattle.IsPlayerTeam() == isPlayerTeam)
            {
                character.Add(characterBattle);
            }
        }
        return character;
    }

    public List<CharacterBattle> GetDeadTeamCharacterBattleList(bool isPlayerTeam)
    {
        List<CharacterBattle> character = new List<CharacterBattle>();
        foreach (CharacterBattle characterBattle in characterBattleList)
        {
            if (characterBattle.IsDead() && characterBattle.IsPlayerTeam() == isPlayerTeam)
            {
                character.Add(characterBattle);
            }
        }
        return character;
    }

    public void Update()
    {
        if (state == State.EnemySelection || state == State.AllySelection || state == State.DeadAllySelection)
        {
            if (GameData.state == GameData.State.SavingTrenTren && state == State.AllySelection)
            {
                if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D))
                {
                    SelectCapturedTarget();
                }
            }

            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (state == State.EnemySelection)
                {
                    // Selecciona un pj de arriba
                    UITargetSelect(true, false,true);
                }
                else if (state == State.AllySelection)
                {
                    UITargetSelect(true, true, true);
                }
                else if (state == State.DeadAllySelection)
                {
                    UITargetSelect(true, true, false);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (state == State.EnemySelection)
                {
                    // Selecciona un pj de abajo
                    UITargetSelect(false, false, true);
                }
                else if (state == State.AllySelection)
                {
                    UITargetSelect(false, true, true);
                }
                else if (state == State.DeadAllySelection)
                {
                    UITargetSelect(false, true, false);
                }
            }
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
            {
                ExecuteCommand(BattleUI.instance.command);
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Debug.Log("Retrocedio 1 paso");
                state = State.WaitingForPlayer;
                BattleUI.instance.OpenLastWindow();
                selectedTargetCharacterBattle.HideSelectionCircle();
                selectedTargetCharacterBattle = null;
            }
        }
        Debug.Log(state);
    }

    private void ExecuteCommand(string commandName)
    {
        switch (commandName)
        {
            default:
                break;

            case "Attack":
                _Attack();
                break;

            case "Special":
                _Special();
                break;
        }
    }

    public void _Defense()
    {
        state = State.Busy;
        BattleUI.instance.lastMenuActivated = null;
        FunctionTimer.Create(ChooseNextActiveCharacter, .2f);
        SetCamera(activeCharacterBattle.GetPosition(), 30f);
        activeCharacterBattle.Block(() =>
        {
            ResetCamera();
            activeCharacterBattle.HideSelectionCircle();
        });
    }

    private void _Attack()
    {
        // Ataque basico
        state = State.Busy;
        BattleUI.instance.lastMenuActivated = null;
        BattleUI.instance.battleMenus = BattleUI.BATTLEMENUS.None;
        SetCamera(selectedTargetCharacterBattle.GetPosition() + new Vector3(-5f, 0), 30f);
        activeCharacterBattle.GetComponent<SpriteRenderer>().sortingOrder += 2;

        activeCharacterBattle.AttackTarget(selectedTargetCharacterBattle.GetPosition(), () =>
        {
            SoundManager.PlaySound(SoundManager.Sound.MeleeAtk1);
            int damageBase = activeCharacterBattle.GetAttack();
            int minDamage = (int)(damageBase * 0.8f);
            int maxDamage = (int)(damageBase * 1.2f);
            int damageAmount = Random.Range(minDamage, maxDamage);
            int finalDmg;

            if (Random.Range(0, 100) <= activeCharacterBattle.stats.critChance)       //Critical Hit
            {
                damageAmount *= Random.Range((int)1.2f, (int)1.5f);
                finalDmg = ((damageAmount * damageAmount) / (damageAmount + selectedTargetCharacterBattle.stats.defense));
                //Debug.Log("El daño critico es: " + finalDmg);
                selectedTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, selectedTargetCharacterBattle);
                DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(), finalDmg, true);
                UtilsClass.ShakeCamera(1f, .1f);
            }
            else                                //Normal Hit
            {
                finalDmg = ((damageAmount * damageAmount) / (damageAmount + selectedTargetCharacterBattle.stats.defense));
                selectedTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, selectedTargetCharacterBattle);
                DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(), finalDmg, false);
                UtilsClass.ShakeCamera(.75f, .15f);
            }

            if (selectedTargetCharacterBattle.IsDead())
            {
                if (activeCharacterBattle.GetCharacterType() == Character.Type.Chillpila)
                {
                    ResourceManager.instance.AddSouls(Random.Range((int)(5 * 1.25f), (int)(5 * 1.5f)));
                }
                //WhichMonsterWasKilled(selectedTargetCharacterBattle.GetCharacterType());
            }
            activeCharacterBattle.GetComponent<SpriteRenderer>().sortingOrder -= 2;
        },() =>
        {
            ResetCamera();
            selectedTargetCharacterBattle.HideSelectionCircle();
            activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
        });
    }

    public void EnemyAttack()
    {
        FunctionTimer.Create(() =>
        {
            if (GameData.state == GameData.State.SavingTrenTren)
            {
                aiTargetCharacterBattle = trenTrenCapturadoCharacterBattle;
            }
            else
            {
                aiTargetCharacterBattle = GetAliveTeamCharacterBattleList(true)[Random.Range(0, GetAliveTeamCharacterBattleList(true).Count)];
            }
            SetCamera(aiTargetCharacterBattle.GetPosition() + new Vector3(+5f, 0), 30f);
            activeCharacterBattle.GetComponent<SpriteRenderer>().sortingOrder += 2;
            Vector3 targetPosition;

            if (activeCharacterBattle.GetCharacterType() == Character.Type.Fusilero)
            {
                targetPosition = new Vector3(aiTargetCharacterBattle.GetPosition().x + 30, aiTargetCharacterBattle.GetPosition().y, aiTargetCharacterBattle.GetPosition().z);
            }
            else
            {
                targetPosition = aiTargetCharacterBattle.GetPosition();
            }
            activeCharacterBattle.AttackTarget(targetPosition, () =>
            {
                SoundManager.PlaySound(SoundManager.Sound.MeleeAtk1);
                int damageBase = activeCharacterBattle.GetAttack();
                int damageMin = (int)(damageBase * 0.8f);
                int damageMax = (int)(damageBase * 1.0f);
                int damageAmount = Random.Range(damageMin, damageMax);
                int damageChance = activeCharacterBattle.stats.damageChance;
                if (Random.Range(0, 100) < damageChance)    // probabilidad de golpear al player
                {
                    // Hit
                    if (Random.Range(0, 100) <= activeCharacterBattle.stats.critChance)       //Critical Hit
                    {
                        damageAmount *= Random.Range((int)1.2f, (int)1.5f);
                        int finalDmg = ((damageAmount * damageAmount) / (damageAmount + aiTargetCharacterBattle.stats.defense));
                        //Debug.Log("El daño critico es: " + finalDmg);
                        aiTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, aiTargetCharacterBattle);
                        DamagePopups.Create(aiTargetCharacterBattle.GetPosition(), finalDmg, true);
                        UtilsClass.ShakeCamera(1f, .1f);
                    }
                    else                                //Normal Hit
                    {
                        int finalDmg = ((damageAmount * damageAmount) / (damageAmount + aiTargetCharacterBattle.stats.defense));
                        //Debug.Log("El daño normal es: " + finalDmg);
                        aiTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, aiTargetCharacterBattle);
                        DamagePopups.Create(aiTargetCharacterBattle.GetPosition(), finalDmg, false);
                        UtilsClass.ShakeCamera(.75f, .1f);
                    }
                }
                else
                {
                    // Miss
                    DamagePopups.Create(activeCharacterBattle.GetPosition(), "FALLO", UtilsClass.GetColorFromString("00B4FF"));
                }
            }, () =>
            {
                ResetCamera();
                activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                activeCharacterBattle.GetComponent<SpriteRenderer>().sortingOrder -= 2;
                StatusInfo(activeCharacterBattle);
            });
        }, .3f);
    }

    public void _Special()
    {
        state = State.Busy;
        int number = Random.Range(0, 3);
        switch (activeCharacterBattle.GetCharacterType())
        {
            default:
            case Character.Type.Suyai:      //Healer - Support
                BattleUI.instance.CloseBattleMenus();
                switch (BattleUI.instance.spellName)
                {
                    case "Curar":
                        SetCamera(selectedTargetCharacterBattle.GetPosition() + new Vector3(-5f, 0), 30f);
                        Vector3 slideToPositionToHeal = selectedTargetCharacterBattle.GetPosition() + new Vector3(-8f, 0);
                        activeCharacterBattle.SlideToPosition(slideToPositionToHeal, () =>
                        {
                            activeCharacterBattle.PlayAnimSpecial(() =>
                            {
                                //Heal Individual
                                SoundManager.PlaySound(SoundManager.Sound.Heal);
                                selectedTargetCharacterBattle.Heal((int)(selectedTargetCharacterBattle.GetHealthAmount() * .2f));
                                DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(), ((int)(selectedTargetCharacterBattle.GetHealthAmount() * .2f)).ToString(), Color.green);
                            }, () =>
                            {
                                ResetCamera();
                                activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                            });
                        });
                        ResourceManager.instance.ConsumeHerbs(1);
                        break;

                    case "CuraGrupal":
                        SetCamera(activeCharacterBattle.GetPosition(), 30f);
                        activeCharacterBattle.PlayAnimSpecial(() =>
                        {
                            FunctionTimer.Create(() => {
                                // Heal Grupal
                                SoundManager.PlaySound(SoundManager.Sound.Heal);
                                List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(true);
                                if (GameData.state == GameData.State.SavingTrenTren)
                                {
                                    characterBattleList.Add(trenTrenCapturadoCharacterBattle);
                                }
                                foreach (CharacterBattle characterBattle in characterBattleList)
                                {
                                    characterBattle.Heal(50);
                                    DamagePopups.Create(characterBattle.GetPosition(), "50", Color.green);
                                }
                                ResourceManager.instance.ConsumeHerbs(characterBattleList.Count);
                            }, 1.2f);
                        },
                        () => 
                        {
                            activeCharacterBattle.PlayIdleAnim();
                            ResetCamera();
                            FunctionTimer.Create(ChooseNextActiveCharacter, .2f);
                        });
                        break;

                    case "Revivir":
                        SetCamera(selectedTargetCharacterBattle.GetPosition() + new Vector3(-5f, 0), 30f);
                        Vector3 slideToPositionToRevive = selectedTargetCharacterBattle.GetPosition() + new Vector3(-8f, 0);
                        activeCharacterBattle.SlideToPosition(slideToPositionToRevive, () =>
                        {
                            activeCharacterBattle.PlayAnimSpecial(() =>
                            {
                                //Revive 1 pj
                                SoundManager.PlaySound(SoundManager.Sound.Heal);
                                selectedTargetCharacterBattle.Heal((int)(selectedTargetCharacterBattle.GetMaxHealthAmount() * .25f));
                                selectedTargetCharacterBattle.Revive();
                                DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(), selectedTargetCharacterBattle.GetHealthAmount().ToString(), Color.green);
                            }, () =>
                            {
                                ResetCamera();
                                activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                            });
                        });
                        ResourceManager.instance.ConsumeHerbs(1);
                        break;

                    case "Turnos":
                        SetCamera(activeCharacterBattle.GetPosition(), 30f);
                        activeCharacterBattle.PlayAnimSpecial(() =>
                        {
                            SoundManager.PlaySound(SoundManager.Sound.Heal);
                            TurnSystem.instance.SetTurnCount(3);
                            DamagePopups.Create(activeCharacterBattle.GetPosition(), "¡Conseguiste 3 turnos extras!", Color.white);
                        }, () =>
                        {
                            activeCharacterBattle.PlayIdleAnim();
                            ResetCamera();
                            FunctionTimer.Create(ChooseNextActiveCharacter, .2f);
                        });
                        ResourceManager.instance.ConsumeHerbs(3);
                        break;
                }
                break;

            case Character.Type.Antay:      //Tank
                BattleUI.instance.CloseBattleMenus();

                activeCharacterBattle.SlideToPosition(GetPosition(LanePosition.Middle, false) + new Vector3(-45, 0), () =>
                {
                    activeCharacterBattle.PlayAnimSpecial(() =>
                    {
                        // Dano en area
                        SoundManager.PlaySound(SoundManager.Sound.Buffs);
                        FunctionTimer.Create(() =>
                        {
                            // Buff all
                            List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(true);
                            foreach (CharacterBattle characterBattle in characterBattleList)
                            {
                                characterBattle.Buff(number);
                            }
                            ResourceManager.instance.ConsumeHits(1);
                        }, 1.2f);
                    }, () =>
                    {
                        activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                    });
                });
                break;

            case Character.Type.Pedro:      // Debuffer - Trickster
                BattleUI.instance.CloseBattleMenus();
                SetCamera(middlePosition, 30f);
                activeCharacterBattle.SlideToPosition(middlePosition, () =>
                {
                    activeCharacterBattle.PlayAnimSpecial( () =>
                    {
                        ResetCamera();
                        activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                    }, () =>
                    {
                        // Debuff
                        //SoundManager.PlaySound(SoundManager.Sound.CharacterHit);
                        List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(false);
                        foreach (CharacterBattle characterBattle in characterBattleList)
                        {
                            characterBattle.Debuff(number);
                        }
                        ResourceManager.instance.PayMoney(25);
                    });
                });
                break;

            case Character.Type.Arana:      // DAMAGE DEALER
                BattleUI.instance.CloseBattleMenus();

                SetCamera(selectedTargetCharacterBattle.GetPosition() + new Vector3(-5f, 0), 30f);
                Vector3 slideToPosition = selectedTargetCharacterBattle.GetPosition() + new Vector3(-8f, 0);

                activeCharacterBattle.SlideToPosition(slideToPosition, () =>
                {
                    activeCharacterBattle.PlayAnimSpecial(() =>
                    {
                        UtilsClass.ShakeCamera(2f, .15f);
                        int damageAmount = 100;
                        selectedTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, selectedTargetCharacterBattle);
                        DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(), damageAmount, true);
                    }, () =>
                    {
                        ResetCamera();
                        activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                    });
                });
                ResourceManager.instance.ConsumeTattoos(1);
                break;

            case Character.Type.Chillpila:          // MAGE 
                BattleUI.instance.CloseBattleMenus();

                activeCharacterBattle.SlideToPosition(GetPosition(LanePosition.Middle, false) + new Vector3(-25, 0), () => 
                {
                    activeCharacterBattle.PlayAnimSpecial(() =>
                    {
                        // Dano en area
                        SoundManager.PlaySound(SoundManager.Sound.Thunder);
                        UtilsClass.ShakeCamera(2f, .15f);
                        int damageAmount = 30;
                        List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(false);

                        foreach (CharacterBattle characterBattle in characterBattleList)
                        {
                            
                            characterBattle.Damage(activeCharacterBattle, damageAmount, characterBattle);
                            DamagePopups.Create(characterBattle.GetPosition(), damageAmount, false);
                        }

                        for (int i = 0; i < characterBattleList.Count; i++)
                        {
                            if (characterBattleList[i].IsDead())
                            {
                                ResourceManager.instance.AddSouls(Random.Range((int)(5 * 1.25f), (int)(5 * 1.5f)));
                                //WhichMonsterWasKilled(characterBattleList[i].GetCharacterType());
                            }

                        }
                    }, () =>
                    {
                        activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                    });
                });
                ResourceManager.instance.ConsumeSouls((int)(characterBattleList.Count * 1.5f));
                break;
        }
        BattleUI.instance.lastMenuActivated = null;
        BattleUI.instance.battleMenus = BattleUI.BATTLEMENUS.None;
    }

    public void EnemySpecial()
    {
            aiTargetCharacterBattle = GetAliveTeamCharacterBattleList(true)[Random.Range(0, GetAliveTeamCharacterBattleList(true).Count)];
            Vector3 slideToPosition = aiTargetCharacterBattle.GetPosition() + new Vector3(-8f, 0);
            switch (activeCharacterBattle.GetCharacterType())
            {
                case Character.Type.Fusilero:

                    activeCharacterBattle.SlideToPosition(GetPosition(LanePosition.Middle, false) + new Vector3(-25, 0), () =>
                    {
                        activeCharacterBattle.PlayAnimSpecial(() =>
                        {
                            // Dano en area
                            //SoundManager.PlaySound(SoundManager.Sound.Thunder);
                            UtilsClass.ShakeCamera(1.5f, .175f);
                            int damageAmount = Random.Range(10,26);
                            List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(true);

                            foreach (CharacterBattle characterBattle in characterBattleList)
                            {
                                characterBattle.Damage(activeCharacterBattle, damageAmount, characterBattle);
                                DamagePopups.Create(characterBattle.GetPosition(), damageAmount, false);
                            }
                        }, () =>
                        {
                            activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                        });
                    });

                    break;
                case Character.Type.Lancero:

                    SetCamera(aiTargetCharacterBattle.GetPosition() + new Vector3(-5f, 0), 30f);

                    activeCharacterBattle.SlideToPosition(slideToPosition, () =>
                    {
                        activeCharacterBattle.PlayAnimSpecial(() =>
                        {
                            UtilsClass.ShakeCamera(2f, .15f);
                            int damageAmount = Random.Range(10, 26);
                            aiTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, aiTargetCharacterBattle);
                            DamagePopups.Create(aiTargetCharacterBattle.GetPosition(), damageAmount, true);

                            aiTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, aiTargetCharacterBattle);
                            DamagePopups.Create(aiTargetCharacterBattle.GetPosition(), damageAmount, true);
                        }, () =>
                        {
                            ResetCamera();
                            activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                        });
                    });

                    break;

                case Character.Type.Guirivilo:

                    SetCamera(aiTargetCharacterBattle.GetPosition() + new Vector3(-5f, 0), 30f);

                    activeCharacterBattle.SlideToPosition(slideToPosition, () =>
                    {
                        activeCharacterBattle.PlayAnimSpecial(() =>
                        {
                            UtilsClass.ShakeCamera(2f, .15f);
                            int damageAmount = Random.Range(10, 26);
                            aiTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, aiTargetCharacterBattle);
                            DamagePopups.Create(aiTargetCharacterBattle.GetPosition(), damageAmount, true);

                            aiTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, aiTargetCharacterBattle);
                            DamagePopups.Create(aiTargetCharacterBattle.GetPosition(), damageAmount, true);
                        }, () =>
                        {
                            ResetCamera();
                            activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                        });
                    });

                    break;

                case Character.Type.Piuchen:

                    break;
            }
    }

    public CharacterBattle GetActiveCharacterBattle()
    {
        return activeCharacterBattle;
    }

    private int GetAliveCount(bool isPlayerTeam)
    {
        return GetAliveTeamCharacterBattleList(isPlayerTeam).Count;
    }

    //private void WhichMonsterWasKilled(Character.Type character)
    //{
    //    switch (character)
    //    {
    //        case Character.Type.TrenTren:
    //            //QuestManager.instance.QuestProgress();
    //            GameData.state = GameData.State.TrenTrenSaved;

    //            FunctionTimer.Create(OverworldManager.LoadBackToOvermap, .7f);
    //            break;
    //    }
    //}

    private void ChooseNextActiveCharacter()
    {        
        if (GetAliveTeamCharacterBattleList(false).Count == 0)
        {
            // Se acaba el combate
            //Debug.Log("Se acabo el combate, Ganaste!");
            // Desaparece el pj en el overmap
            if (character.type == Character.Type.TrenTren || character.type == Character.Type.CaiCai)
            {
                character.isDead = false;
            }
            else
            {
                character.isDead = true;
            }
            // Se actualizan los valores de los characters
            foreach (CharacterBattle characterBattle in GetTeamCharacterBattleList(true))
            {
                if (Character.IsUniqueCharacterType(characterBattle.GetCharacterType()))
                {
                    Character uniqueCharacter = GameData.GetCharacter(characterBattle.GetCharacterType());
                    if (uniqueCharacter != null)
                    {
                        uniqueCharacter.stats.health = characterBattle.GetHealthAmount();
                        if (uniqueCharacter.IsInPlayerTeam())
                        {
                            if (uniqueCharacter.stats.health < 1)
                            {
                                uniqueCharacter.stats.health = 1;
                            }
                        }
                    }
                }
            }
            switch (GameData.state)
            {
                case GameData.State.SavingTrenTren:
                    GameData.state = GameData.State.TrenTrenSaved;
                    GameData.cutsceneAlreadyWatched = false;
                    break;
                case GameData.State.FightingCaiCai:
                    GameData.state = GameData.State.CaiCaiBeated;
                    GameData.cutsceneAlreadyWatched = false;
                    break;
                case GameData.State.PeleandoVSPrimerPeloton:
                    GameData.state = GameData.State.PrimerPelotonVencido;
                    break;
                case GameData.State.PeleandoVSSegundoPeloton:
                    GameData.state = GameData.State.SegundoPelotonVencido;
                    break;
                case GameData.State.PeleandoVSTercerPeloton:
                    GameData.state = GameData.State.TercerPelotonVencido;
                    break;
            }
            //Debug.Log("El estado del GameData.state es: " + GameData.state);
            //Debug.Log("El estado del GameData.MapZone es: " + GameData.mapZoneState);
            //SoundManager.PlaySound(SoundManager.Sound.BattleWin);
            UIFade.FadeIn();
            FunctionTimer.Create(OverworldManager.LoadBackToOvermap, UIFade.GetTimer());
            return;
        }

        if (TurnSystem.instance.ZeroTurns())
        {
            //Debug.Log("Se te acabaron los turnos, vuela alto");
            FunctionTimer.Create(OverworldManager.LoadGameOver, .7f);
            return;
        }

        if (GetAliveTeamCharacterBattleList(true).Count == 0)
        {
            // Perdio
            //Debug.Log("Perdiste el combate, ganaron los enemigos :(");

            foreach (CharacterBattle characterBattle in GetTeamCharacterBattleList(true))
            {
                if (Character.IsUniqueCharacterType(characterBattle.GetCharacterType()))
                {
                    Character uniqueCharacter = GameData.GetCharacter(characterBattle.GetCharacterType());
                    if (uniqueCharacter != null)
                    {
                        uniqueCharacter.stats.health = characterBattle.GetHealthAmount();
                    }
                }
            }

            UIFade.FadeIn();
            FunctionTimer.Create(OverworldManager.LoadBackToOvermap, UIFade.GetTimer());
            return;
        }

        // Si es que el turno anterior fue de un aliado
        if (activeCharacterBattle.IsPlayerTeam())
        {
            // Ahora es el turno de un enemigo
            List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(false);
            SetActiveCharacterBattle(
                GetNextCharacterBattle(lastEnemyActiveLanePosition, false, false)
            );
            lastEnemyActiveLanePosition = activeCharacterBattle.GetLanePosition();

            if (GameData.state == GameData.State.SavingTrenTren)
            {
                switch (activeCharacterBattle.GetCharacterType())
                {
                    case Character.Type.Fusilero:
                        EnemyAttack();
                        break;
                    case Character.Type.Lancero:
                        EnemyAttack();
                        break;
                    case Character.Type.Anchimallen:
                        EnemyAttack();
                        break;
                    case Character.Type.Guirivilo:
                        EnemyAttack();
                        break;
                    case Character.Type.Piuchen:
                        EnemyAttack();
                        break;
                    case Character.Type.CaiCai:
                        EnemyAttack();
                        break;
                }
            }
            else
            {
                switch (activeCharacterBattle.GetCharacterType())
                {
                    case Character.Type.Fusilero:
                        EnemyAttackOrSpecial();
                        break;
                    case Character.Type.Lancero:
                        EnemyAttackOrSpecial();
                        break;
                    case Character.Type.Anchimallen:
                        EnemyAttack();
                        break;
                    case Character.Type.Guirivilo:
                        EnemyAttack();
                        break;
                    case Character.Type.Piuchen:
                        EnemyAttack();
                        break;
                    case Character.Type.CaiCai:
                        EnemyAttack();
                        break;
                }
            }
            activeCharacterBattle.TickSpecialCooldown();
        }

        else
        {
            // Inicia turno de aliado
            List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(true);
            //List<CharacterBattle> enemyCharacterBattleList = GetAliveTeamCharacterBattleList(false);
            SetActiveCharacterBattle(GetNextCharacterBattle(lastPlayerActiveLanePosition, false, true));
            lastPlayerActiveLanePosition = activeCharacterBattle.GetLanePosition();
            if (activeCharacterBattle.IsBlocking())
            {
                activeCharacterBattle.LetGoBlock();
            }

            BattleUI.instance.mainBattleMenu.SetActive(true);

            if (GameData.state == GameData.State.SavingTrenTren)
            {
                if (TurnSystem.instance.GetTurnCount() <= TurnSystem.instance.GetTotalAmountOfTurns() * 0.75f)
                {
                    if (!alreadyTrenTrenSpeaked)
                    {
                        Dialogues.Play_TrenTrenDuringBattle();
                        alreadyTrenTrenSpeaked = true;
                    }
                }
                state = State.WaitingForPlayer;
            }
            else
            {
                state = State.WaitingForPlayer;
            }
        }

        // Turn Start
        if (activeCharacterBattle.IsPlayerTeam())
        {
            TurnSystem.instance.TurnDecrease();
            Debug.Log("Turn Start");
        }
    }

    public void EnemyAttackOrSpecial()
    {
        if (activeCharacterBattle.EnemyTrySpendSpecial())
        {
            enemyCommand = "Special";
            EnemySpecial();
        }
        else
        {
            enemyCommand = "Attack";
            EnemyAttack();
        }
    }

    private void StatusInfo(CharacterBattle character)
    {
        if (character.HasStatus())
        {
            character.statusSystem.TimerTick();
        }
    }

    public string GetEnemyCommand()
    {
        return enemyCommand;
    }

    //private void RefreshSelectedTargetCharacterBattle()
    //{
    //    if (selectedTargetCharacterBattle != null && selectedTargetCharacterBattle.IsDead())
    //    {
    //        selectedTargetCharacterBattle.HideSelectionCircle();
    //        selectedTargetCharacterBattle = null;
    //    }
    //    if (selectedTargetCharacterBattle == null)
    //    {
    //        // Select next valid target
    //        List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(false);
    //        if (characterBattleList.Count == 0)
    //        {
    //            // No more targets available
    //            return;
    //        }
    //        else
    //        {
    //            // There are still targets available
    //            CharacterBattle newTargetCharacterBattle = characterBattleList[Random.Range(0, characterBattleList.Count)];
    //            SetSelectedTargetCharacterBattle(newTargetCharacterBattle);
    //        }
    //    }
    //    else
    //    {
    //        SetSelectedTargetCharacterBattle(selectedTargetCharacterBattle);
    //    }
    //}

}
