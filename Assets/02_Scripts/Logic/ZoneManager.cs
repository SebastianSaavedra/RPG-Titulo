using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoneManager : MonoBehaviour
{
    public static ZoneManager instance;

    [SerializeField] CinemachineConfiner confiner;
    [SerializeField] PolygonCollider2D aldeaConfiner, bosqueAraucConfiner, lagoConfiner, bosqueProfundoConfiner;
    [SerializeField] PlayerOverworld player;
    [SerializeField] Transform aldeaToBosque, bosqueToAldea, bosqueToLago, lagoToBosque, bosqueToBosqueP, bosquePToBosque,colliderAldeaToBosque;

    [Space(10)]
    [SerializeField] GameObject trenTrenBattleTrigger,caiCaiBattleTrigger,aldeaBattleTriger,guerreroDesesperadoTrigger;

    [Space(10)]
    [SerializeField] List<GameObject> afterTrenTrenBattle = new List<GameObject>();

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        confiner = GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner>();
        player = GameObject.Find("pfPlayer").GetComponent<PlayerOverworld>();
    }

    public GameObject GetTrenTrenBattleTrigger() => trenTrenBattleTrigger;
    public GameObject GetCaiCaiBattleTrigger() => caiCaiBattleTrigger;
    public GameObject GetAldeaBattleTriger() => aldeaBattleTriger;
    public GameObject GetGuerreroDesesperadoTrigger() => guerreroDesesperadoTrigger;
    public Transform GetAldeaToBosque() => aldeaToBosque;
    public Transform GetBosqueToAldea() => bosqueToAldea;
    public Transform GetBosqueToLago() => bosqueToLago;
    public Transform GetLagoToBosque() => lagoToBosque;
    public Transform GetBosqueToBosqueP() => bosqueToBosqueP;
    public Transform GetBosquePToBosque() => bosquePToBosque;
    public PolygonCollider2D GetAldeaConfiner() => aldeaConfiner;
    public PolygonCollider2D GetBosqueAraucConfiner() => bosqueAraucConfiner;
    public PolygonCollider2D GetLagoConfiner() => lagoConfiner;
    public PolygonCollider2D GetBosqueProfundoConfiner() => bosqueProfundoConfiner;

    public void ActivateColliderAldeaToBosque()
    {
        colliderAldeaToBosque.gameObject.SetActive(true);
    }

    public void SetActiveGameobject(GameObject go, bool active)
    {
        go.SetActive(active);
    }
    public void SetActiveGameobject(GameObject go1, GameObject go2, bool active)
    {
        go1.SetActive(active);
        go2.SetActive(active);
    }

    public void SetConfiner(PolygonCollider2D confiner2D)
    {
        confiner.m_BoundingShape2D = confiner2D;
        confiner.InvalidatePathCache();
    }

    public List<GameObject> GetAfterTrenTrenObjects()
    {
        return afterTrenTrenBattle;
    }
    
    public void AldeaToBosque()
    {
        OverworldManager.StopOvermapRunning();
        //Transition FadeIn
        confiner.m_BoundingShape2D = bosqueAraucConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = aldeaToBosque.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position;
        }
        GameData.mapZoneState = GameData.MapZone.BosqueAraucarias;
        //Transition FadeOut
        OverworldManager.ContinueOvermapRunning();
    }

    public void BosqueToAldea()
    {
        OverworldManager.StopOvermapRunning();
        //Transition FadeIn
        confiner.m_BoundingShape2D = aldeaConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = bosqueToAldea.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position;
        }
        GameData.mapZoneState = GameData.MapZone.Aldea;
        //Transition FadeOut
        OverworldManager.ContinueOvermapRunning();
    }

    public void BosqueToLago()
    {
        OverworldManager.StopOvermapRunning();
        //Transition FadeIn
        confiner.m_BoundingShape2D = lagoConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = bosqueToLago.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position;
        }
        GameData.mapZoneState = GameData.MapZone.Lago;
        //Transition FadeOut
        OverworldManager.ContinueOvermapRunning();
    }
    public void BosqueToBosqueP()
    {
        OverworldManager.StopOvermapRunning();
        //Transition FadeIn
        confiner.m_BoundingShape2D = bosqueProfundoConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = bosqueToBosqueP.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position;
        }
        GameData.mapZoneState = GameData.MapZone.BosqueProfundo;
        //Transition FadeOut
        OverworldManager.ContinueOvermapRunning();
    }
    public void LagoToBosque()
    {
        OverworldManager.StopOvermapRunning();
        //Transition FadeIn
        confiner.m_BoundingShape2D = bosqueAraucConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = lagoToBosque.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position;
        }
        GameData.mapZoneState = GameData.MapZone.BosqueAraucarias;
        //Transition FadeOut
        OverworldManager.ContinueOvermapRunning();
    }
    public void BosquePToBosque()
    {
        OverworldManager.StopOvermapRunning();
        //Transition FadeIn
        confiner.m_BoundingShape2D = bosqueAraucConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = bosquePToBosque.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position;
        }
        GameData.mapZoneState = GameData.MapZone.BosqueAraucarias;
        //Transition FadeOut
        OverworldManager.ContinueOvermapRunning();
    }

    public void TrenTrenBeforeBattle()
    {
        Dialogues.Play_TrenTrenBeforeBattle(OverworldManager.GetInstance().GetTrenTrenCharacter());
    }
    public void CaicaiBeforeBattle()
    {
        Dialogues.Play_CaiCaiBeforeBattle(OverworldManager.GetInstance().GetCaiCaiCharacter());
    }
}
