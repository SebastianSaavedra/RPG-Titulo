using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class OverworldManager
{

    private static OverworldManager instance;
    public static OverworldManager GetInstance() => instance;

    public static void LoadBackToOvermap()
    {
        Loader.Load(Loader.Scene.OverworldScene);
    }

    public static void LoadFromOvermapToBattle()
    {
        Loader.Load(Loader.Scene.BattleScene);
    }

    public static void SaveAllCharacterPositions()
    {
        if (instance == null) return;
        instance.playerOvermap.SaveCharacterPosition();
        foreach (NPCOverworld npcOvermap in instance.npcList) { npcOvermap.SaveCharacterPosition(); }
        foreach (FollowerOverworld followerOvermap in instance.followerList) { followerOvermap.SaveCharacterPosition(); }
        foreach (EnemyOverworld enemyOvermap in instance.enemyList) { enemyOvermap.SaveCharacterPosition(); }
    }

    public event EventHandler OnOvermapStopped;

    private PlayerOverworld playerOvermap;

    private bool overmapRunning;
    private List<EnemyOverworld> enemyList;
    private List<NPCOverworld> npcList;
    private List<FollowerOverworld> followerList;

    public OverworldManager(PlayerOverworld playerOvermap)
    {
        instance = this;
        this.playerOvermap = playerOvermap;
        followerList = new List<FollowerOverworld>();
        npcList = new List<NPCOverworld>();
        enemyList = new List<EnemyOverworld>();
        overmapRunning = true;
    }

    public void Start(Transform transform)
    {
        StartOvermapRunning();

        foreach (Character character in GameData.characterList)
        {
            if (character.isDead) continue;
            if (character.IsEnemy())
            {
                SpawnEnemy(character);
                continue;
            }

            if (character.IsInPlayerTeam())
            {
                switch (character.type)
                {
                    case Character.Type.Suyai:
                        playerOvermap.Setup(character);
                        break;
                    case Character.Type.Pedro:
                        SpawnFollower(character, new Vector3(-1, -1));
                        break;
                    case Character.Type.Arana:
                        SpawnFollower(character, new Vector3(1, 1));
                        break;
                    case Character.Type.Chillpila:
                        SpawnFollower(character, new Vector3(1, 1));
                        break;
                    case Character.Type.Antay:
                        SpawnFollower(character, new Vector3(-1, -1));
                        break;
                }
            }
                switch (character.type)
            {
                    //case Character.Type.Shop:
                    //case Character.Type.TavernAmbush:
                    //case Character.Type.TavernAmbush_2:
                    //case Character.Type.TavernAmbush_3:
                    //    SpawnNPC(character);
                    //    break;
                    //case Character.Type.Villager_1:
                    //case Character.Type.Villager_2:
                    //case Character.Type.Villager_3:
                    //case Character.Type.Villager_4:
                    //case Character.Type.Villager_5:
                    //NPCOverworld npcOverworld = SpawnNPC(character);
                    //foreach (Transform child in transform.Find("Map"))
                    //{
                    //    if (Vector3.Distance(npcOvermap.GetPosition(), child.position) < 1f)
                    //    {
                    //        CharacterSetLastMoveDirData characterSetLastMoveDirData = child.GetComponent<CharacterSetLastMoveDirData>();
                    //        //npcOvermap.SetLastMoveDir(characterSetLastMoveDirData.lastMoveDir);
                    //    }
                    //}
                    //break;
            }
        }


        foreach (Item item in GameData.itemList)
        {
            if (item.IsDestroyed()) continue;
            SpawnItem(item);
        }

        // Starting state
        //switch (GameData.state)
        //{
        //    case GameData.State.Start:
        //        Cutscenes.Play_Start();
        //        break;
        //    case GameData.State.DefeatedHurtMeDaddy:
        //    case GameData.State.DefeatedHurtMeDaddy_2:
        //        Window_QuestPointer.Create(GetNPC(GameData.GetCharacter(Character.Type.Tank)).GetPosition() + new Vector3(0, 10), Color.yellow, Color.yellow, crossSprite: GameAssets.i.s_ExclamationPoint);
        //        break;
        //    case GameData.State.DefeatedTank:
        //        Cutscenes.Play_Tank_AfterJoin();
        //        break;
        //    case GameData.State.SurvivedTavernAmbush:
        //        Cutscenes.Play_SurvivedTavernAmbush();
        //        break;
        //    case GameData.State.HealerJoined:
        //        Window_QuestPointer.Create(GameAssets.i.Map.Find("shop").position, Color.white, Color.white);
        //        break;
        //    case GameData.State.LostToEvilMonster_1:
        //        playerOvermap.SetPosition(GameAssets.i.Map.Find("dungeonPlayer").position);
        //        GetFollower(GameData.GetCharacter(Character.Type.Tank)).SetPosition(GameAssets.i.Map.Find("dungeonTank").position);
        //        GetFollower(GameData.GetCharacter(Character.Type.Sleezer)).SetPosition(GameAssets.i.Map.Find("dungeonSleezer").position);
        //        GetFollower(GameData.GetCharacter(Character.Type.Healer)).SetPosition(GameAssets.i.Map.Find("dungeonHealer").position);

        //        Cutscenes.Play_LostToEvilMonster_1();
        //        break;
        //    case GameData.State.MovingToEvilMonster_2:
        //        Window_QuestPointer.Create(GameAssets.i.Map.Find("evilMonster_2").position, Color.white, Color.white);
        //        break;
        //    case GameData.State.LostToEvilMonster_2:
        //        playerOvermap.SetPosition(GameAssets.i.Map.Find("dungeonPlayer").position);
        //        GetFollower(GameData.GetCharacter(Character.Type.Tank)).SetPosition(GameAssets.i.Map.Find("dungeonTank").position);
        //        GetFollower(GameData.GetCharacter(Character.Type.Sleezer)).SetPosition(GameAssets.i.Map.Find("dungeonSleezer").position);
        //        GetFollower(GameData.GetCharacter(Character.Type.Healer)).SetPosition(GameAssets.i.Map.Find("dungeonHealer").position);

        //        Cutscenes.Play_LostToEvilMonster_2();
        //        break;
        //    case GameData.State.MovingToEvilMonster_3:
        //        Window_QuestPointer.Create(GameAssets.i.Map.Find("evilMonster_3").position, Color.white, Color.white);
        //        break;
        //    case GameData.State.DefeatedEvilMonster:
        //        Cutscenes.Play_DefeatedEvilMonster();
        //        break;
        //}

        //transform.Find("Map").Find("Hitboxes").Find("TavernEntryHitbox").gameObject.SetActive(((int)GameData.state) < ((int)GameData.State.DefeatedTank));
        //transform.Find("Map").Find("Hitboxes").Find("HurtMeDaddyHitbox").gameObject.SetActive(((int)GameData.state) < ((int)GameData.State.DefeatedHurtMeDaddy));
        //transform.Find("Map").Find("Hitboxes").Find("HurtMeDaddy2Hitbox").gameObject.SetActive(((int)GameData.state) < ((int)GameData.State.DefeatedHurtMeDaddy_2));
        //transform.Find("Map").Find("Hitboxes").Find("CastleBlockageHitbox").gameObject.SetActive(((int)GameData.state) < ((int)GameData.State.LostToEvilMonster_2));

        //transform.Find("Map").Find("VillagerBlocker").gameObject.SetActive(((int)GameData.state) < ((int)GameData.State.DefeatedTank));

        //transform.Find("Map").Find("Blockage").gameObject.SetActive(((int)GameData.state) < ((int)GameData.State.LostToEvilMonster_2));

        ////transform.Find("Map").Find("Hitboxes").Find("HurtMeDaddyHitbox").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        ////transform.Find("Map").Find("Hitboxes").Find("HurtMeDaddy2Hitbox").GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);

        //foreach (Transform hitboxTransform in transform.Find("Map").Find("Hitboxes"))
        //{
        //    hitboxTransform.GetComponent<SpriteRenderer>().color = new Color(0, 0, 0, 0);
        //}
    }

    public void Update()
    {
        //    switch (GameData.state)
        //    {
        //        case GameData.State.GoingToTownCenter:
        //            List<Vector3> positionList = new List<Vector3> {
        //            GameAssets.i.Map.Find("townCenter").position,
        //            GameAssets.i.Map.Find("townCenter_2").position,
        //            GameAssets.i.Map.Find("townCenter_3").position,
        //        };
        //            bool reachedPosition = false;
        //            foreach (Vector3 position in positionList)
        //            {
        //                if (Vector3.Distance(playerOvermap.GetPosition(), position) < 30f)
        //                {
        //                    reachedPosition = true;
        //                }
        //            }
        //            if (reachedPosition)
        //            {
        //                GameData.state = GameData.State.ArrivedAtTownCenter;
        //                //Cutscenes.Play_ArrivedAtTownCenter();
        //            }
        //            break;
        //        case GameData.State.GoingToTavern:
        //            Vector3 tavernPosition = GameAssets.i.Map.Find("tavern").position;
        //            if (Vector3.Distance(playerOvermap.GetPosition(), tavernPosition) < 20f)
        //            {
        //                GameData.state = GameData.State.InTavern;
        //                //Cutscenes.Play_Tavern();
        //            }
        //            break;
        //        case GameData.State.LeavingTown:
        //            //Vector3 leaveTownPosition = GameAssets.i.Map.Find("letsLeaveTown").position;
        //            positionList = new List<Vector3> {
        //            GameAssets.i.Map.Find("letsLeaveTown").position,
        //            GameAssets.i.Map.Find("letsLeaveTown_2").position,
        //            GameAssets.i.Map.Find("letsLeaveTown_3").position,
        //        };
        //            reachedPosition = false;
        //            foreach (Vector3 position in positionList)
        //            {
        //                if (Vector3.Distance(playerOvermap.GetPosition(), position) < 30f)
        //                {
        //                    reachedPosition = true;
        //                }
        //            }
        //            if (reachedPosition)
        //            {
        //                GameData.state = GameData.State.GoingToFirstEvilMonsterEncounter;
        //                Window_QuestPointer.DestroyPointer(GameAssets.i.Map.Find("letsLeaveTown").position);

        //                Window_QuestPointer.Create(GameAssets.i.Map.Find("evilMonster").position, Color.white, Color.white);
        //            }
        //            break;
        //        case GameData.State.GoingToFirstEvilMonsterEncounter:
        //            Vector3 evilMonsterPosition = GetEnemy(Character.Type.EvilMonster).GetPosition();
        //            if (Vector3.Distance(playerOvermap.GetPosition(), evilMonsterPosition) < 90f)
        //            {
        //                GameData.state = GameData.State.GoingToFightEvilMonster;
        //                Window_QuestPointer.DestroyPointer(GameAssets.i.Map.Find("evilMonster").position);

        //                Cutscenes.Play_SpottedEvilMonster();
        //            }
        //            break;
        //        case GameData.State.MovingToEvilMonster_2:
        //            evilMonsterPosition = GetEnemy(Character.Type.EvilMonster_2).GetPosition();
        //            if (Vector3.Distance(playerOvermap.GetPosition(), evilMonsterPosition) < 80f)
        //            {
        //                GameData.state = GameData.State.GoingToFightEvilMonster_2;
        //                Window_QuestPointer.DestroyPointer(GameAssets.i.Map.Find("evilMonster_2").position);
        //            }
        //            break;
        //        case GameData.State.MovingToEvilMonster_3:
        //            evilMonsterPosition = GetEnemy(Character.Type.EvilMonster_3).GetPosition();
        //            if (Vector3.Distance(playerOvermap.GetPosition(), evilMonsterPosition) < 80f)
        //            {
        //                GameData.state = GameData.State.GoingToFightEvilMonster_3;
        //                Window_QuestPointer.DestroyPointer(GameAssets.i.Map.Find("evilMonster_3").position);
        //            }
        //            break;
        //    }
        //}

        //public void SpawnSmoke(Vector3 position, float timer, Vector3 localScale)
        //{
        //    FunctionTimer.Create(() => {
        //        Transform smokeTransform = UnityEngine.Object.Instantiate(GameAssets.i.pfSmokePuff, position, Quaternion.identity);
        //        smokeTransform.localScale = localScale;
        //    }, timer);
    }

public NPCOverworld GetClosestNPC(Vector3 position, float maxDistance = float.MaxValue)
    {
        NPCOverworld closest = null;
        foreach (NPCOverworld npcOvermap in npcList)
        {
            if (Vector3.Distance(position, npcOvermap.GetPosition()) > maxDistance) continue; // Too far
            if (closest == null)
            {
                closest = npcOvermap;
            }
            else
            {
                if (Vector3.Distance(position, npcOvermap.GetPosition()) < Vector3.Distance(position, closest.GetPosition()))
                {
                    closest = npcOvermap;
                }
            }
        }
        return closest;
    }

    public PlayerOverworld GetPlayer()
    {
        return playerOvermap;
    }

    public NPCOverworld GetNPC(Character character)
    {
        foreach (NPCOverworld npc in npcList)
        {
            if (npc.GetCharacter() == character)
            {
                return npc;
            }
        }
        return null;
    }

    public FollowerOverworld GetFollower(Character character)
    {
        foreach (FollowerOverworld follower in followerList)
        {
            if (follower.GetCharacter() == character)
            {
                return follower;
            }
        }
        return null;
    }

    public EnemyOverworld GetEnemy(Character.Type characterType)
    {
        foreach (EnemyOverworld enemyOvermap in enemyList)
        {
            if (enemyOvermap.GetCharacter().type == characterType)
            {
                return enemyOvermap;
            }
        }
        return null;
    }

    public bool TryPlayerInteract()
    {
        NPCOverworld npcOvermap = GetClosestNPC(playerOvermap.GetPosition(), 20f);
        if (npcOvermap != null)
        {
            //UIBlack.Show();
            StopOvermapRunning();
            return true;
        }
        return false;
    }

    public static bool IsOvermapRunning()
    {
        return instance.overmapRunning;
    }

    public static void StartOvermapRunning()
    {
        instance.overmapRunning = true;
    }

    public static void StopOvermapRunning()
    {
        instance.overmapRunning = false;
        if (instance.OnOvermapStopped != null) instance.OnOvermapStopped(instance, EventArgs.Empty);
    }
    public static void ContinueOvermapRunning()
    {
        instance.overmapRunning = true;
        if (instance.OnOvermapStopped != null)
        {
            instance.OnOvermapStopped(instance, EventArgs.Empty);
        }

    }
    public void ContinueOvermap()
    {
        instance.overmapRunning = true;
        if (instance.OnOvermapStopped != null)
        {
            instance.OnOvermapStopped(instance, EventArgs.Empty);
        }

    }
    public void StopOvermap()
    {
        instance.overmapRunning = false;
        if (instance.OnOvermapStopped != null) instance.OnOvermapStopped(instance, EventArgs.Empty);
    }

    public static void SpawnFollower(Character character, Vector3 followOffset)
    {
        Transform followerTransform = UnityEngine.Object.Instantiate(GameAssets.i.pfFollowerOvermap, character.position, Quaternion.identity);
        FollowerOverworld followerOvermap = followerTransform.GetComponent<FollowerOverworld>();
        followerOvermap.Setup(character, instance.playerOvermap, followOffset);
        instance.followerList.Add(followerOvermap);
    }

    public static void SpawnEnemy(Character character)
    {
        Transform enemyTransform = UnityEngine.Object.Instantiate(GameAssets.i.pfEnemyOvermap, character.position, Quaternion.identity);
        EnemyOverworld enemyOvermap = enemyTransform.GetComponent<EnemyOverworld>();
        enemyOvermap.Setup(character, instance.playerOvermap);
        instance.enemyList.Add(enemyOvermap);
    }

    public static NPCOverworld SpawnNPC(Character character)
    {
        Transform npcTransform = UnityEngine.Object.Instantiate(GameAssets.i.pfNPCOvermap, character.position, Quaternion.identity);
        NPCOverworld npcOvermap = npcTransform.GetComponent<NPCOverworld>();
        npcOvermap.Setup(character, instance.playerOvermap);
        instance.npcList.Add(npcOvermap);
        return npcOvermap;
    }

    public static void SpawnItem(Item item)
    {
        Transform prefab;
        switch (item.GetItemType())
        {
            default:
            case Item.ItemType.MedicinalHerbs:
                prefab = GameAssets.i.pfMedicinalHerbs;
                break;
        }
        Transform itemTransform = UnityEngine.Object.Instantiate(prefab, item.GetPosition(), Quaternion.identity);
        ItemOverworld itemOvermap = itemTransform.GetComponent<ItemOverworld>();
        itemOvermap.Setup(item, instance.playerOvermap);
    }

}
