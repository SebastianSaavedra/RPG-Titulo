using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using CodeMonkey.Utils;

public class OverworldManager
{

    private static OverworldManager instance;
    public static OverworldManager GetInstance() => instance;

    public static void LoadBackToOvermap()
    {
        SceneManager.instance.LoadTargetScene(SceneManager.Scene.OverworldScene);
    }

    public static void LoadGameOver()
    {
        SceneManager.instance.LoadTargetScene(SceneManager.Scene.GameOver);
    }

    public static void LoadFromOvermapToBattle()
    {
        SceneManager.instance.LoadTargetScene(SceneManager.Scene.BattleScene);
    }

    public static void SaveAllCharacterPositions()
    {
        if (instance == null) return;
        instance.playerOvermap.SaveCharacterPosition();
        foreach (NPCOverworld npcOvermap in instance.npcList) { npcOvermap.SaveCharacterPosition(); }
        foreach (FollowerOverworld followerOvermap in instance.followerList) { followerOvermap.SaveCharacterPosition(); }
        foreach (EnemyOverworld enemyOvermap in instance.enemyList) { enemyOvermap.SaveCharacterPosition(); }
    }

    public event EventHandler<OnOvermapStoppedEventsArgs> OnOvermapStopped;
    public class OnOvermapStoppedEventsArgs : EventArgs
    {
        public int index;
    }

    private PlayerOverworld playerOvermap;

    private Character trenTrenChar, caicaiChar;

    private bool overmapRunning;
    private List<EnemyOverworld> enemyList;
    private List<NPCOverworld> npcList;
    private List<ItemOverworld> itemList;
    private List<FollowerOverworld> followerList;

    public OverworldManager(PlayerOverworld playerOvermap)
    {
        instance = this;
        this.playerOvermap = playerOvermap;
        followerList = new List<FollowerOverworld>();
        npcList = new List<NPCOverworld>();
        enemyList = new List<EnemyOverworld>();
        itemList = new List<ItemOverworld>();
        overmapRunning = true;
    }

    public void Start()
    {
        UnityEngine.SceneManagement.SceneManager.sceneLoaded += SceneManager_sceneLoaded;
        StartOvermapRunning();

        foreach (Character character in GameData.characterList)
        {
            if (character.isDead) continue;
            if (character.IsEnemy())
            {
                if (GameData.state == GameData.State.TrenTrenSaved && character.type == Character.Type.Anchimallen || GameData.state == GameData.State.CaiCaiBeated && character.type == Character.Type.Anchimallen)
                {
                    SpawnEnemy(character);
                }
                else if(character.type != Character.Type.Anchimallen)
                {
                    SpawnEnemy(character);
                    continue;
                }

                if (GameData.state == GameData.State.CaiCaiBeated)
                {
                    if (character.type == Character.Type.Fusilero)
                    {
                        GameObject.Instantiate(GameAssets.i.pfPelotonFusileros, GameAssets.i.pfPelotonFusileros.transform.position, Quaternion.identity);
                    }
                    else if (character.type == Character.Type.Lancero)
                    {
                        GameObject.Instantiate(GameAssets.i.pfPelotonLanceros, GameAssets.i.pfPelotonLanceros.transform.position, Quaternion.identity);
                    }
                    else if (character.type == Character.Type.FusileroYLancero)
                    {
                        GameObject.Instantiate(GameAssets.i.pfPelotoFusileroYLancero, GameAssets.i.pfPelotoFusileroYLancero.transform.position, Quaternion.identity);
                    }
                }
            }

            if (character.IsInPlayerTeam())
            {
                switch (character.type)
                {
                    case Character.Type.Suyai:
                        playerOvermap.Setup(character);
                        break;
                    case Character.Type.Pedro:
                        SpawnFollower(character, new Vector3(-1, 1));
                        break;
                    case Character.Type.Arana:
                        SpawnFollower(character, new Vector3(1, -1));
                        break;
                    case Character.Type.Chillpila:
                        SpawnFollower(character, new Vector3(-1, -1));
                        break;
                    case Character.Type.Antay:
                        SpawnFollower(character, new Vector3(1, 1));
                        break;
                }
            }

            NPCOverworld npcOverworld;
            switch (character.type)
            {
                case Character.Type.QuestNpc_1:
                    character.name = GetRandomString("Masculino");
                    npcOverworld = SpawnNPC(character);
                    break;
                case Character.Type.SoldadoMapuche_2:
                    switch (GameData.state)
                    {
                        default:
                            character.name = GetRandomString("Masculino");
                            npcOverworld = SpawnNPC(character);
                            break;
                        case GameData.State.TrenTrenSaved:
                        case GameData.State.CaiCaiBeated:
                            break;
                    }
                    break;

                case Character.Type.SoldadoMapuche_1:
                case Character.Type.NinoMapuche_1:
                case Character.Type.NinoMapuche_2:
                case Character.Type.HombreMapuche_1:
                case Character.Type.HombreMapuche_2:
                    character.name = GetRandomString("Masculino");
                    npcOverworld = SpawnNPC(character);
                    break;
                case Character.Type.ViejaMachi:
                    character.name = "Kuyenray";
                    npcOverworld = SpawnNPC(character);
                    break;

                case Character.Type.MujerMapuche_1:
                case Character.Type.MujerMapuche_2:
                case Character.Type.NinaMapuche_1:
                case Character.Type.NinaMapuche_2:
                    character.name = GetRandomString("Femenino");
                    npcOverworld = SpawnNPC(character);
                    break;

                case Character.Type.TrenTren:
                    Transform pfTren = GameObject.Instantiate(GameAssets.i.pfTrenTren, character.position, Quaternion.identity);
                    pfTren.localScale = Vector3.one * 3f;
                    trenTrenChar = character;
                    break;
                case Character.Type.CaiCai:
                    if (GameData.state == GameData.State.TrenTrenSaved)
                    {
                        Transform pfCaiCai = GameObject.Instantiate(GameAssets.i.pfCaiCai, character.position, Quaternion.identity);
                        pfCaiCai.localScale = Vector3.one * 3f;
                        caicaiChar = character;
                    }
                    break;



                case Character.Type.Shop:
                    npcOverworld = SpawnNPC(character);
                    break;
            }

            switch (GameData.state)
            {
                case GameData.State.Intro:
                    if (character.type == Character.Type.ViejaMachi)
                    {
                        Dialogues.Play_StartViejaMachi(character);
                        GameData.state = GameData.State.Starting;
                    }
                    break;
            }
        }

        switch (GameData.state)
        {
            case GameData.State.Intro:
            case GameData.State.Starting:
            case GameData.State.AlreadyTalkedWithViejaMachi:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                ZoneManager.instance.SetConfiner(ZoneManager.instance.GetAldeaConfiner());
                ZoneManager.instance.SetActiveGameobject(ZoneManager.instance.GetTrenTrenBattleTrigger(), true);
                //ZoneManager.instance.SetActiveGameobject(ZoneManager.instance.GetVilageAccess(), true);
                break;
            case GameData.State.TrenTrenSaved:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                switch (GameData.mapZoneState)
                {
                    case GameData.MapZone.BosqueAraucarias:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueAraucConfiner());
                        break;
                    case GameData.MapZone.Lago:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                        break;
                    case GameData.MapZone.BosqueProfundo:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueProfundoConfiner());
                        break;
                }
                ZoneManager.instance.SetActiveGameobject(ZoneManager.instance.GetCaiCaiBattleTrigger(), true);
                ZoneManager.instance.SetActiveGameobject(ZoneManager.instance.GetLakeAccess(), true);
                ZoneManager.instance.SetActiveGameobject(ZoneManager.instance.GetVilageAccess(), false);
                foreach (GameObject item in ZoneManager.instance.GetAfterTrenTrenObjects())
                {
                    item.SetActive(false);
                }
                if (!GameData.cutsceneAlreadyWatched)
                {
                    Dialogues.Play_TrenTrenSaved();
                }
                break;
            case GameData.State.CaiCaiBeated:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                ZoneManager.instance.SetActiveGameobject(ZoneManager.instance.GetLakeAccess(), true);
                ZoneManager.instance.SetActiveGameobject(ZoneManager.instance.GetVilageAccess(), true);
                foreach (GameObject item in ZoneManager.instance.GetAfterTrenTrenObjects())
                {
                    item.SetActive(false);
                }
                if (!GameData.cutsceneAlreadyWatched)
                {
                    Dialogues.Play_CaiCaiBeated();
                }
                break;
        }

        foreach (Item item in GameData.itemList)
        {
            if (item.IsDestroyed()) continue;
            SpawnItem(item);
        }
    }

    private void SceneManager_sceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Inventory.instance.ResetBattleItemList();
    }

    public Character GetTrenTrenCharacter()
    {
        return trenTrenChar;
    }
    public Character GetCaiCaiCharacter()
    {
        return caicaiChar;
    }

    string GetRandomString(string genero)
    {
        switch (genero)
        {
            default:
            case "Masculino":
                return GameData.nombresMasculinosMapucheArray[UnityEngine.Random.Range(0, GameData.nombresMasculinosMapucheArray.Length)];
            case "Femenino":
                return GameData.nombresFemeninosMapucheArray[UnityEngine.Random.Range(0, GameData.nombresFemeninosMapucheArray.Length)];
        }
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

    public ItemOverworld GetClosestItem(Vector3 position, float maxDistance = float.MaxValue)
    {
        ItemOverworld closest = null;
        foreach (ItemOverworld itemOvermap in itemList)
        {
            if (Vector3.Distance(position, itemOvermap.GetPosition()) > maxDistance) continue; // Too far
            if (closest == null)
            {
                closest = itemOvermap;
            }
            else
            {
                if (Vector3.Distance(position, itemOvermap.GetPosition()) < Vector3.Distance(position, closest.GetPosition()))
                {
                    closest = itemOvermap;
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

    public List<FollowerOverworld> GetFollowerList()
    {
        return followerList;
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
    public bool IsOvermapRunningNonStatic()
    {
        return instance.overmapRunning;
    }

    public static void StartOvermapRunning()
    {
        instance.overmapRunning = true;
        if (instance.OnOvermapStopped != null)
        {
            instance.OnOvermapStopped(instance, new OnOvermapStoppedEventsArgs { index = 1 });
        }
    }

    public static void StopOvermapRunning()
    {
        instance.overmapRunning = false;
        if (instance.OnOvermapStopped != null)
        {
            instance.OnOvermapStopped(instance, new OnOvermapStoppedEventsArgs { index = 0 });
        }
    }
    public static void ContinueOvermapRunning()
    {
        instance.overmapRunning = true;
        if (instance.OnOvermapStopped != null)
        {
            instance.OnOvermapStopped(instance, new OnOvermapStoppedEventsArgs { index = 1 });
        }

    }
    public void ContinueOvermap()
    {
        instance.overmapRunning = true;
        if (instance.OnOvermapStopped != null)
        {
            instance.OnOvermapStopped(instance, new OnOvermapStoppedEventsArgs { index = 1 });
        }

    }
    public void StopOvermap()
    {
        instance.overmapRunning = false;
        if (instance.OnOvermapStopped != null)
        {
            instance.OnOvermapStopped(instance, new OnOvermapStoppedEventsArgs { index = 0 });
        }
    }

    public static Vector3 GetSuyaiPos()
    {
        Vector3 suyaiPos = instance.playerOvermap.GetPosition();
        return suyaiPos;
    }

    public static void SpawnFollower(Character character, Vector3 followOffset)
    {
        Transform followerTransform = UnityEngine.Object.Instantiate(GameAssets.i.pfFollowerOvermap, GetSuyaiPos() - followOffset, Quaternion.identity);
        FollowerOverworld followerOvermap = followerTransform.GetComponent<FollowerOverworld>();
        followerOvermap.Setup(character, instance.playerOvermap, followOffset);
        instance.followerList.Add(followerOvermap);
    }

    public static void ReplaceFollower(Character pjSale, Character pjEntra, Vector3 followOffset)
    {
        for (int i = 0; i < instance.followerList.Count; i++)
        {
            if (instance.followerList[i].GetCharacter() == pjSale)
            {
                GameObject.Destroy(instance.followerList[i].gameObject);
                instance.followerList.Remove(instance.followerList[i]);
                break;
            }
        }

        Transform followerTransform = UnityEngine.Object.Instantiate(GameAssets.i.pfFollowerOvermap, GetSuyaiPos() - followOffset, Quaternion.identity);
        FollowerOverworld followerOvermap = followerTransform.GetComponent<FollowerOverworld>();
        followerOvermap.Setup(pjEntra, instance.playerOvermap, followOffset);

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
            //case Item.ItemType.Weapon_1:
            //    prefab = GameAssets.i.pfWeaponTEST;
            //    break;
        }
        Transform itemTransform = UnityEngine.Object.Instantiate(prefab, item.GetPosition(), Quaternion.identity);
        ItemOverworld itemOverworld = itemTransform.GetComponent<ItemOverworld>();
        itemOverworld.Setup(item, instance.playerOvermap);
        instance.itemList.Add(itemOverworld);
    }

}
