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

    public static void LoadEnemyEncounter(Character character, GameData.EnemyEncounter enemyEncounter)
    {
        OverworldManager.SaveAllCharacterPositions();
        Battle.character = character;
        Battle.enemyEncounter = enemyEncounter;
        
        FunctionTimer.Create(OverworldManager.LoadFromOvermapToBattle, .7f);
    }

    private CameraFollow cameraFollow;
    public List<CharacterBattle> characterBattleList;
    public CharacterBattle activeCharacterBattle;
    public CharacterBattle selectedTargetCharacterBattle;

    private LanePosition lastPlayerActiveLanePosition;
    private LanePosition lastEnemyActiveLanePosition;

    Vector3 middlePosition = GetPosition(LanePosition.Middle, false) + new Vector3(-35, 0);

    public State state;

    public enum LanePosition
    {
        Middle,
        Up,
        Down,
    }

    public enum State
    {
        WaitingForPlayer,
        OnMenu,
        EnemySelection,
        AllySelection,
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
        characterBattle.Setup(characterType, lanePosition, GetPosition(lanePosition, isPlayerTeam), isPlayerTeam, character.stats,character);
        instance.characterBattleList.Add(characterBattle);
    }

    public static Vector3 GetPosition(LanePosition lanePosition, bool isPlayerTeam)
    {
        float playerTeamMultiplier = isPlayerTeam ? -1f : 1f;

        switch (lanePosition)
        {
            default:
            case LanePosition.Middle: return new Vector3(playerTeamMultiplier * 37.6f, -17.2f);
            case LanePosition.Up: return new Vector3(playerTeamMultiplier * 67.8f, -6.2f);
            case LanePosition.Down: return new Vector3(playerTeamMultiplier * 57.9f, -37.6f);
        }

    }

    public static LanePosition GetNextLanePosition(LanePosition lanePosition, bool moveUp)
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

    public void Start(CameraFollow cameraFollow)
    {
        this.cameraFollow = cameraFollow;
        cameraFollow.Setup(() => Vector3.zero, () => 50f, true, true);
        cameraFollow.SetCameraMoveSpeed(10f);
        cameraFollow.SetCameraZoomSpeed(10f);
        ResetCamera();
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

        state = State.WaitingForPlayer;
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
    /// Para cambiar de objetivo (Esta funcion debe ser usada desde el script BattleWindow)
    /// </summary>
    /// <param name="isUp"></param>
    public void UITargetSelect(bool isUp, bool isTeam)
    {
        SetSelectedTargetCharacterBattle(GetNextCharacterBattle(selectedTargetCharacterBattle.GetLanePosition(), isUp, isTeam));
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
            // Get next lane position
            lanePosition = GetNextLanePosition(lanePosition, moveUp);
            // Find character at that position
            characterBattle = GetCharacterBattleAt(lanePosition, isPlayerTeam);
            if (characterBattle != null && characterBattle.IsDead()) characterBattle = null; // Ignore dead
        } while (characterBattle == null);

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
        if (state == State.EnemySelection || state == State.AllySelection)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W))
            {
                if (state == State.EnemySelection)
                {
                    // Selecciona un pj de arriba
                    UITargetSelect(true, false);
                }
                else if (state == State.AllySelection)
                {
                    UITargetSelect(true, true);
                }
            }

            if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S))
            {
                if (state == State.EnemySelection)
                {
                    // Selecciona un pj de abajo
                    UITargetSelect(false, false);
                }
                else if (state == State.AllySelection)
                {
                    UITargetSelect(false, true);
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

        //if (BattleUI.instance.battleMenus == BattleUI.BATTLEMENUS.Submenu)
        //{
        //    if (Input.GetKeyDown(KeyCode.Escape))
        //    {
        //        if (state == State.EnemySelection || state == State.AllySelection)
        //        {
        //            Debug.Log("Retrocedio 1 paso");
        //            state = State.WaitingForPlayer;
        //            BattleUI.instance.OpenLastWindow();
        //            selectedTargetCharacterBattle.HideSelectionCircle();
        //            selectedTargetCharacterBattle = null;
        //        }
        //        else
        //        {
        //            Debug.Log("Retrocedio 1 paso");
        //            state = State.WaitingForPlayer;
        //            BattleUI.instance.lastMenuActivated.SetActive(false);
        //            BattleUI.instance.SetSelectedGameObjectEventSystem();
        //        }
        //    }
        //}
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
        activeCharacterBattle.AttackTarget(selectedTargetCharacterBattle.GetPosition(), () =>
        {
            //SoundManager.PlaySound(SoundManager.Sound.CharacterHit);
            int damageBase = activeCharacterBattle.GetAttack();
            int minDamage = (int)(damageBase * 0.8f);
            int maxDamage = (int)(damageBase * 1.2f);
            int damageAmount = Random.Range(minDamage, maxDamage);
            selectedTargetCharacterBattle.Damage(activeCharacterBattle,damageAmount,selectedTargetCharacterBattle);
            DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(), damageAmount, false);
            UtilsClass.ShakeCamera(.75f, .15f);
            if (selectedTargetCharacterBattle.IsDead())
            {
                if (activeCharacterBattle.GetCharacterType() == Character.Type.Chillpila)
                {
                    ResourceManager.instance.AddSouls(Random.Range((int)(5 * 1.25f), (int)(5 * 1.5f)));
                }
                TestEvilMonsterKilled(selectedTargetCharacterBattle.GetCharacterType());
            }
        }, () =>
        {
            ResetCamera();
            selectedTargetCharacterBattle.HideSelectionCircle();
            activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
        });
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
                            activeCharacterBattle.PlayAnimSpecialAttack(() =>
                            {
                                ResetCamera();
                                activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                            }, () =>
                            {
                                //Heal Individual
                                //SoundManager.PlaySound(SoundManager.Sound.Heal);
                                selectedTargetCharacterBattle.Heal((int)(selectedTargetCharacterBattle.GetHealthAmount() * .2f));
                                DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(),((int)(selectedTargetCharacterBattle.GetHealthAmount() * .2f)).ToString(), Color.green);
                            });
                        });
                        ResourceManager.instance.ConsumeHerbs(1);
                        break;

                    case "CuraGrupal":
                        SetCamera(activeCharacterBattle.GetPosition(), 30f);
                        activeCharacterBattle.PlayAnimSpecialAttack(() =>
                        {
                            activeCharacterBattle.PlayIdleAnim();
                            ResetCamera();
                            FunctionTimer.Create(ChooseNextActiveCharacter, .2f);
                        },
                        () => { });

                        FunctionTimer.Create(() => {
                            // Heal Grupal
                            //SoundManager.PlaySound(SoundManager.Sound.Heal);
                            List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(true);
                            foreach (CharacterBattle characterBattle in characterBattleList)
                            {
                                characterBattle.Heal(50);
                                DamagePopups.Create(characterBattle.GetPosition(), "50", Color.green);
                            }
                            ResourceManager.instance.ConsumeHerbs(characterBattleList.Count);
                        }, 1.2f);
                        break;

                    case "Revivir":
                        SetCamera(selectedTargetCharacterBattle.GetPosition() + new Vector3(-5f, 0), 30f);
                        Vector3 slideToPositionToRevive = selectedTargetCharacterBattle.GetPosition() + new Vector3(-8f, 0);
                        activeCharacterBattle.SlideToPosition(slideToPositionToRevive, () =>
                        {
                            activeCharacterBattle.PlayAnimSpecialAttack(() =>
                            {
                                ResetCamera();
                                activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                            }, () =>
                            {
                                //Revive 1 pj
                                //SoundManager.PlaySound(SoundManager.Sound.Heal);
                                selectedTargetCharacterBattle.Heal((int)(selectedTargetCharacterBattle.GetMaxHealthAmount() * .25f));
                                DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(), selectedTargetCharacterBattle.GetHealthAmount().ToString(), Color.green);
                            });
                        });
                        ResourceManager.instance.ConsumeHerbs(1);
                        break;

                    case "Turnos":
                        SetCamera(activeCharacterBattle.GetPosition(), 30f);
                        activeCharacterBattle.PlayAnimSpecialAttack(() =>
                        {
                            activeCharacterBattle.PlayIdleAnim();
                            ResetCamera();
                            FunctionTimer.Create(ChooseNextActiveCharacter, .2f);
                        }, () =>
                        {
                            //SoundManager.PlaySound(SoundManager.Sound.Heal);
                            TurnSystem.instance.SetTurnCount(3);
                            DamagePopups.Create(activeCharacterBattle.GetPosition(),"¡Conseguiste 3 turnos extras!", Color.white);
                        });
                        ResourceManager.instance.ConsumeHerbs(3);
                        break;
                }
                break;

            case Character.Type.Antay:      //Tank
                BattleUI.instance.CloseBattleMenus();

                SetCamera(activeCharacterBattle.GetPosition(), 30f);
                activeCharacterBattle.PlayAnimSpecialAttack(() =>
                {
                    activeCharacterBattle.PlayIdleAnim();
                    ResetCamera();
                    FunctionTimer.Create(ChooseNextActiveCharacter, .2f);
                },
                () => { });

                FunctionTimer.Create(() => {
                    // Buff all
                    //SoundManager.PlaySound(SoundManager.Sound.Buff);
                    List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(true);
                    foreach (CharacterBattle characterBattle in characterBattleList)
                    {
                        characterBattle.Buff(number);
                    }
                    ResourceManager.instance.ConsumeHits(1);
                }, 1.2f);
                break;

            case Character.Type.Pedro:      // Debuffer - Trickster
                BattleUI.instance.CloseBattleMenus();
                SetCamera(middlePosition, 30f);
                activeCharacterBattle.SlideToPosition(middlePosition, () =>
                {
                    activeCharacterBattle.PlayAnimSpecialAttack(() =>
                    {
                        // Debuff
                        //SoundManager.PlaySound(SoundManager.Sound.CharacterHit);
                        List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(false);
                        foreach (CharacterBattle characterBattle in characterBattleList)
                        {
                            characterBattle.Debuff(number);
                        }
                        ResourceManager.instance.PayMoney(25);
                    }, () =>
                    {
                        ResetCamera();
                        activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                    });
                });
                break;

            case Character.Type.Arana:      // DAMAGE DEALER
                BattleUI.instance.CloseBattleMenus();
                SetCamera(selectedTargetCharacterBattle.GetPosition() + new Vector3(-5f, 0), 30f);
                Vector3 slideToPosition = selectedTargetCharacterBattle.GetPosition() + new Vector3(-8f, 0);
                activeCharacterBattle.SlideToPosition(slideToPosition, () =>
                {
                    activeCharacterBattle.PlayAnimSpecialAttack(() =>
                    {
                        ResetCamera();
                        activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                    },() =>
                    {
                        // Un Golpe Fuerte
                        //SoundManager.PlaySound(SoundManager.Sound.CharacterHit);
                        UtilsClass.ShakeCamera(2f, .15f);
                        int damageAmount = 100;
                        selectedTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, selectedTargetCharacterBattle);
                        DamagePopups.Create(selectedTargetCharacterBattle.GetPosition(), damageAmount, false);
                        if (selectedTargetCharacterBattle.IsDead())
                        {
                            TestEvilMonsterKilled(selectedTargetCharacterBattle.GetCharacterType());
                        }
                    });
                });
                ResourceManager.instance.ConsumeTattoos(1);
                break;

            case Character.Type.Chillpila:          // MAGE 
                BattleUI.instance.CloseBattleMenus();
                activeCharacterBattle.SlideToPosition(GetPosition(LanePosition.Middle, false) + new Vector3(-15, 0), () => {
                    activeCharacterBattle.PlayAnimSpecialAttack(() => {
                        activeCharacterBattle.SlideBack(() => FunctionTimer.Create(ChooseNextActiveCharacter, .2f));
                    }, () => {
                        // Dano en area
                        //SoundManager.PlaySound(SoundManager.Sound.GroundPound);
                        UtilsClass.ShakeCamera(2f, .15f);
                        int damageAmount = 30;
                        List<CharacterBattle> characterBattleList = GetAliveTeamCharacterBattleList(false);
                        foreach (CharacterBattle characterBattle in characterBattleList)
                        {
                            characterBattle.Damage(activeCharacterBattle, damageAmount,characterBattle);
                            DamagePopups.Create(characterBattle.GetPosition(), damageAmount, false);
                        }
                        for (int i = 0; i < characterBattleList.Count; i++)
                        {
                            if (characterBattleList[i].IsDead())
                            {
                                ResourceManager.instance.AddSouls(Random.Range((int)(5 * 1.25f), (int)(5 * 1.5f)));
                                TestEvilMonsterKilled(characterBattleList[i].GetCharacterType());
                            }

                        }
                    });
                });
                ResourceManager.instance.ConsumeSouls((int)(characterBattleList.Count * 1.5f));
                break;
        }
        BattleUI.instance.lastMenuActivated = null;
        BattleUI.instance.battleMenus = BattleUI.BATTLEMENUS.None;
    }

    public CharacterBattle GetActiveCharacterBattle()
    {
        return activeCharacterBattle;
    }

    private int GetAliveCount(bool isPlayerTeam)
    {
        return GetAliveTeamCharacterBattleList(isPlayerTeam).Count;
    }

    private void TestEvilMonsterKilled(Character.Type character)
    {
        switch (character)
        {
            case Character.Type.TESTENEMY:
                QuestManager.instance.QuestProgress();
                //GameData.state = GameData.State.LostToEvilMonster_1;
                break;
        }
    }

    private void ChooseNextActiveCharacter()
    {
        if (TurnSystem.instance.ZeroTurns())
        {
            Debug.Log("Se te acabaron los turnos, vuela alto");
            FunctionTimer.Create(OverworldManager.LoadBackToOvermap, .7f);
            return;
        }
        
        // Selecciona un enemigo
        if (GetAliveTeamCharacterBattleList(false).Count == 0)
        {
            // Se acaba el combate
            Debug.Log("Se acabo el combate, Ganaste!");
            // Desaparece el pj en el overmap
            character.isDead = true;
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
            //Debug.Log("El estado del GameData.state es: " + GameData.state);
            //switch (GameData.state)
            //{
            //    case GameData.State.Start:
            //        break;
            //    case GameData.State.FightingHurtMeDaddy:
            //        GameData.state = GameData.State.DefeatedHurtMeDaddy;
            //        break;
            //    case GameData.State.FightingHurtMeDaddy_2:
            //        GameData.state = GameData.State.DefeatedHurtMeDaddy_2;
            //        break;
            //    case GameData.State.FightingTank:
            //        GameData.state = GameData.State.DefeatedTank;
            //        // Heal Tank
            //        character.isDead = false;
            //        character.AssignIsInPlayerTeam(true);
            //        character.stats.health = character.stats.healthMax;
            //        // Heal Player
            //        Character uniqueCharacter = GameData.GetCharacter(Character.Type.Suyai);
            //        uniqueCharacter.stats.health = uniqueCharacter.stats.healthMax;
            //        break;
            //    case GameData.State.FightingTavernAmbush:
            //        GameData.state = GameData.State.SurvivedTavernAmbush;
            //        break;
            //    case GameData.State.FightingEvilMonster_1:
            //        // Player Lost to Evil Monster
            //        GameData.GetCharacter(Character.Type.Suyai).stats.health = 1;
            //        GameData.GetCharacter(Character.Type.Antay).stats.health = 1;
            //        GameData.GetCharacter(Character.Type.Pedro).stats.health = 1;
            //        GameData.state = GameData.State.LostToEvilMonster_1;
            //        break;
            //    case GameData.State.FightingEvilMonster_2:
            //        // Player Lost to Evil Monster
            //        GameData.state = GameData.State.LostToEvilMonster_2;
            //        break;
            //    case GameData.State.FightingEvilMonster_3:
            //        // Player Defeated Evil Monster
            //        GameData.state = GameData.State.DefeatedEvilMonster;
            //        break;
            //}
            //SoundManager.PlaySound(SoundManager.Sound.BattleWin);
            FunctionTimer.Create(OverworldManager.LoadBackToOvermap, .7f);
            return;
        }
        if (GetAliveTeamCharacterBattleList(true).Count == 0)
        {
            // Perdio
            Debug.Log("Perdiste el combate, ganaron los enemigos :(");

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

            FunctionTimer.Create(OverworldManager.LoadBackToOvermap, .7f);
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

            FunctionTimer.Create(() => 
            {
                CharacterBattle aiTargetCharacterBattle = GetAliveTeamCharacterBattleList(true)[Random.Range(0, GetAliveTeamCharacterBattleList(true).Count)];
                SetCamera(aiTargetCharacterBattle.GetPosition() + new Vector3(+5f, 0), 30f);
                activeCharacterBattle.AttackTarget(aiTargetCharacterBattle.GetPosition(), () => 
                {
                    //SoundManager.PlaySound(SoundManager.Sound.CharacterHit);
                    int damageBase = activeCharacterBattle.GetAttack();
                    int damageMin = (int)(damageBase * 0.8f);
                    int damageMax = (int)(damageBase * 1.0f);
                    int damageAmount = Random.Range(damageMin, damageMax);
                    int damageChance = activeCharacterBattle.stats.damageChance;
                    if (Random.Range(0, 100) < damageChance)    // probabilidad de golpear al player
                    {
                        // Hit
                        if (Random.Range(0, 100) <= 5)       //Critical Hit
                        {
                            damageAmount *= (int)Random.Range((int)1.2f,(int)1.5f);
                            aiTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, aiTargetCharacterBattle);
                            DamagePopups.Create(aiTargetCharacterBattle.GetPosition(),damageAmount,true);
                            UtilsClass.ShakeCamera(1f, .1f);
                        }
                        else                                //Normal Hit
                        {
                            aiTargetCharacterBattle.Damage(activeCharacterBattle, damageAmount, aiTargetCharacterBattle);
                            DamagePopups.Create(aiTargetCharacterBattle.GetPosition(), damageAmount, false);
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
                    StatusInfo(activeCharacterBattle);
                });
            }, .3f);
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

            BattleUI.instance.radialMenu.SetActive(true);
            state = State.WaitingForPlayer;
        }

        // Turn Start
        if (activeCharacterBattle.IsPlayerTeam())
        {
            TurnSystem.instance.TurnDecrease();
            Debug.Log("Turn Start");
        }
    }

    private void StatusInfo(CharacterBattle character)
    {
        if (character.HasStatus())
        {
            character.statusSystem.TimerTick();
        }
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
