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
    private NPCOverworld viejaMachi;

    private Character trenTrenChar, caicaiChar,fusilero,lancero,fusileroYLancero;

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
        //UIFade.Show();
        UIFade.FadeOut();
        SoundManager.PlaySoundLoop(SoundManager.Sound.OverworldTheme);

        foreach (Character character in GameData.characterList)
        {
            if (character.isDead) continue;
            if (character.IsEnemy())
            {
                switch (character.type)
                {
                    default:
                        SpawnEnemy(character);
                        break;
                    case Character.Type.Fusilero:
                        switch (GameData.state)
                        {
                            case GameData.State.PrimerPelotonVencido:
                            case GameData.State.SegundoPelotonVencido:
                            case GameData.State.CaiCaiBeated:
                                SpawnEnemy(character);
                                Transform pfFusilero = GameObject.Instantiate(GameAssets.i.pfPelotonFusileros, character.position, Quaternion.identity);
                                pfFusilero.localScale = Vector3.one * 1f;
                                fusilero = character;
                                break;
                        }
                        break;
                    case Character.Type.Lancero:
                        switch (GameData.state)
                        {
                            case GameData.State.PrimerPelotonVencido:
                            case GameData.State.SegundoPelotonVencido:
                            case GameData.State.CaiCaiBeated:
                                SpawnEnemy(character);
                                Transform pfLancero = GameObject.Instantiate(GameAssets.i.pfPelotonLanceros, character.position, Quaternion.identity);
                                pfLancero.localScale = Vector3.one * 1f;
                                lancero = character;
                                break;
                        }
                        break;
                    case Character.Type.FusileroYLancero:
                        switch (GameData.state)
                        {
                            case GameData.State.PrimerPelotonVencido:
                            case GameData.State.SegundoPelotonVencido:
                            case GameData.State.CaiCaiBeated:
                                SpawnEnemy(character);
                                Transform pfFusileroYLancero = GameObject.Instantiate(GameAssets.i.pfPelotoFusileroYLancero, character.position, Quaternion.identity);
                                pfFusileroYLancero.localScale = Vector3.one * 1f;
                                fusileroYLancero = character;
                                break;
                        }
                        break;
                    case Character.Type.Anchimallen:
                        switch (GameData.state)
                        {
                            case GameData.State.TercerPelotonVencido:
                            case GameData.State.Endgame:
                            case GameData.State.CaiCaiBeated:
                            case GameData.State.TrenTrenSaved:
                                SpawnEnemy(character);
                                break;
                        }
                        break;
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
                        case GameData.State.TercerPelotonVencido:
                            break;
                    }
                    break;
                case Character.Type.SoldadoDesesperado:
                    switch (GameData.state)
                    {
                        case GameData.State.CaiCaiBeated:
                            character.name = GetRandomString("Masculino");
                            npcOverworld = SpawnNPC(character);
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
                    viejaMachi = npcOverworld;
                    break;

                case Character.Type.MujerMapuche_1:
                case Character.Type.MujerMapuche_2:
                case Character.Type.NinaMapuche_1:
                case Character.Type.NinaMapuche_2:
                    character.name = GetRandomString("Femenino");
                    npcOverworld = SpawnNPC(character);
                    break;

                case Character.Type.TrenTren:
                    npcOverworld = SpawnNPC(character);
                    Transform pfTren = GameObject.Instantiate(GameAssets.i.pfTrenTren, character.position, Quaternion.identity);
                    pfTren.parent = npcOverworld.gameObject.transform;
                    pfTren.localPosition = new Vector3(7f, 1f,1f);
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
                GameData.mapZoneState = GameData.MapZone.Aldea;
                ZoneManager.instance.SetConfiner(ZoneManager.instance.GetAldeaConfiner());
                break;
            case GameData.State.TrenTrenSaved:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                if (GameData.cutsceneAlreadyWatched)
                {
                    switch (GameData.mapZoneState)
                    {
                        case GameData.MapZone.BosqueAraucarias:
                            ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueAraucConfiner());
                            break;
                        case GameData.MapZone.Aldea:
                            ZoneManager.instance.SetConfiner(ZoneManager.instance.GetAldeaConfiner());
                            break;
                        case GameData.MapZone.Lago:
                            ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                            break;
                        case GameData.MapZone.BosqueProfundo:
                            ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueProfundoConfiner());
                            break;
                    }
                }
                ZoneManager.instance.GetCaiCaiBattleTrigger().SetActive(true);
                ZoneManager.instance.GetBosqueToLagoPortal().SetActive(true);
                ZoneManager.instance.GetLagoToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosquePToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToBosquePPortal().SetActive(true);
                foreach (GameObject item in ZoneManager.instance.GetAfterTrenTrenObjects())
                {
                    item.SetActive(false);
                }
                if (!GameData.cutsceneAlreadyWatched)
                {
                    GameData.mapZoneState = GameData.MapZone.BosqueProfundo;
                    ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueProfundoConfiner());
                    Dialogues.Play_TrenTrenSaved();
                }
                break;
            case GameData.State.CaiCaiBeated:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                if (GameData.cutsceneAlreadyWatched)
                {
                    switch (GameData.mapZoneState)
                    {
                        case GameData.MapZone.BosqueAraucarias:
                            ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueAraucConfiner());
                            break;
                        case GameData.MapZone.Aldea:
                            ZoneManager.instance.SetConfiner(ZoneManager.instance.GetAldeaConfiner());
                            break;
                        case GameData.MapZone.Lago:
                            ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                            break;
                        case GameData.MapZone.BosqueProfundo:
                            ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueProfundoConfiner());
                            break;
                    }
                }
                ZoneManager.instance.GetAldeaBattleTriger().SetActive(true);
                ZoneManager.instance.GetBosqueToLagoPortal().SetActive(true);
                ZoneManager.instance.GetLagoToBosquePortal().SetActive(true);
                ZoneManager.instance.GetAldeaToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToAldeaPortal().SetActive(true);
                ZoneManager.instance.GetBosquePToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToBosquePPortal().SetActive(true);
                foreach (GameObject item in ZoneManager.instance.GetAfterTrenTrenObjects())
                {
                    item.SetActive(false);
                }
                if (!GameData.cutsceneAlreadyWatched)
                {
                    GameData.mapZoneState = GameData.MapZone.Lago;
                    ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                    ZoneManager.instance.GetGuerreroDesesperadoTrigger().SetActive(true);
                    Dialogues.Play_CaiCaiBeated();
                }
                break;

            case GameData.State.PrimerPelotonVencido:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                GameData.mapZoneState = GameData.MapZone.Aldea;
                switch (GameData.mapZoneState)
                {
                    case GameData.MapZone.BosqueAraucarias:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueAraucConfiner());
                        break;
                    case GameData.MapZone.Aldea:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetAldeaConfiner());
                        break;
                    case GameData.MapZone.Lago:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                        break;
                    case GameData.MapZone.BosqueProfundo:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueProfundoConfiner());
                        break;
                }
                foreach (GameObject item in ZoneManager.instance.GetAfterTrenTrenObjects())
                {
                    item.SetActive(false);
                }
                Dialogues.Play_SegundoPeloton(fusilero);
                break;

            case GameData.State.SegundoPelotonVencido:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                GameData.mapZoneState = GameData.MapZone.Aldea;
                switch (GameData.mapZoneState)
                {
                    case GameData.MapZone.BosqueAraucarias:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueAraucConfiner());
                        break;
                    case GameData.MapZone.Aldea:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetAldeaConfiner());
                        break;
                    case GameData.MapZone.Lago:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                        break;
                    case GameData.MapZone.BosqueProfundo:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueProfundoConfiner());
                        break;
                }
                foreach (GameObject item in ZoneManager.instance.GetAfterTrenTrenObjects())
                {
                    item.SetActive(false);
                }
                Dialogues.Play_TercerPeloton(fusileroYLancero);
                break;

            case GameData.State.TercerPelotonVencido:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                GameData.mapZoneState = GameData.MapZone.Aldea;
                switch (GameData.mapZoneState)
                {
                    case GameData.MapZone.BosqueAraucarias:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueAraucConfiner());
                        break;
                    case GameData.MapZone.Aldea:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetAldeaConfiner());
                        break;
                    case GameData.MapZone.Lago:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                        break;
                    case GameData.MapZone.BosqueProfundo:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueProfundoConfiner());
                        break;
                }
                ZoneManager.instance.GetBosqueToLagoPortal().SetActive(true);
                ZoneManager.instance.GetLagoToBosquePortal().SetActive(true);
                ZoneManager.instance.GetAldeaToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToAldeaPortal().SetActive(true);
                ZoneManager.instance.GetBosquePToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToBosquePPortal().SetActive(true);
                foreach (GameObject item in ZoneManager.instance.GetAfterTrenTrenObjects())
                {
                    item.SetActive(false);
                }
                Dialogues.Play_ViejaMachiRescatada();
                break;

            case GameData.State.Endgame:
                UtilsClass.WaitOneFrame();
                Debug.Log("El estado en el overworld es: " + GameData.state);
                switch (GameData.mapZoneState)
                {
                    case GameData.MapZone.BosqueAraucarias:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueAraucConfiner());
                        break;
                    case GameData.MapZone.Aldea:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetAldeaConfiner());
                        break;
                    case GameData.MapZone.Lago:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetLagoConfiner());
                        break;
                    case GameData.MapZone.BosqueProfundo:
                        ZoneManager.instance.SetConfiner(ZoneManager.instance.GetBosqueProfundoConfiner());
                        break;
                }
                ZoneManager.instance.GetBosqueToLagoPortal().SetActive(true);
                ZoneManager.instance.GetLagoToBosquePortal().SetActive(true);
                ZoneManager.instance.GetAldeaToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToAldeaPortal().SetActive(true);
                ZoneManager.instance.GetBosquePToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToBosquePPortal().SetActive(true);
                foreach (GameObject item in ZoneManager.instance.GetAfterTrenTrenObjects())
                {
                    item.SetActive(false);
                }
                break;
        }

        Debug.Log("El estado del GameData.MapZone es: " + GameData.mapZoneState);
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
    public Character GetFusileroCharacter()
    {
        return fusilero;
    }
    public Character GetLanceroCharacter()
    {
        return lancero;
    }
    public Character GetFusileroYLanceroCharacter()
    {
        return fusileroYLancero;
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
            case Item.ItemType.Fogata:
                prefab = GameAssets.i.pfFogata;
                break;
        }
        Transform itemTransform = UnityEngine.Object.Instantiate(prefab, item.GetPosition(), Quaternion.identity);
        ItemOverworld itemOverworld = itemTransform.GetComponent<ItemOverworld>();
        itemOverworld.Setup(item, instance.playerOvermap);
        instance.itemList.Add(itemOverworld);
    }

}
