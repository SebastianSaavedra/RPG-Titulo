using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class ZoneManager : MonoBehaviour
{
    public static ZoneManager instance;

    CinemachineConfiner confiner;
    PlayerOverworld player;

    [SerializeField] PolygonCollider2D aldeaConfiner, bosqueAraucConfiner, lagoConfiner, bosqueProfundoConfiner;

    [SerializeField] Transform aldeaToBosquePos, bosqueToAldeaPos, bosqueToLagoPos, lagoToBosquePos, bosqueToBosquePPos, bosquePToBosquePos;
    [SerializeField] GameObject colliderAldeaToBosque,colliderBosqueToAldea, colliderBosqueToLago, colliderLagoToBosque, colliderBosqueToBosqueP, colliderBosquePToBosque;

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

    public Transform GetAldeaToBosquePos() => aldeaToBosquePos;
    public Transform GetBosqueToAldeaPos() => bosqueToAldeaPos;
    public Transform GetBosqueToLagoPos() => bosqueToLagoPos;
    public Transform GetLagoToBosquePos() => lagoToBosquePos;
    public Transform GetBosqueToBosquePPos() => bosqueToBosquePPos;
    public Transform GetBosquePToBosquePos() => bosquePToBosquePos;

    public GameObject GetAldeaToBosquePortal() => colliderAldeaToBosque;
    public GameObject GetBosqueToAldeaPortal() => colliderBosqueToAldea;
    public GameObject GetBosqueToLagoPortal() => colliderBosqueToLago;
    public GameObject GetLagoToBosquePortal() => colliderLagoToBosque;
    public GameObject GetBosqueToBosquePPortal() => colliderBosqueToBosqueP;
    public GameObject GetBosquePToBosquePortal() => colliderBosquePToBosque;

    public PolygonCollider2D GetAldeaConfiner() => aldeaConfiner;
    public PolygonCollider2D GetBosqueAraucConfiner() => bosqueAraucConfiner;
    public PolygonCollider2D GetLagoConfiner() => lagoConfiner;
    public PolygonCollider2D GetBosqueProfundoConfiner() => bosqueProfundoConfiner;

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
        player.transform.position = aldeaToBosquePos.position;
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
        player.transform.position = bosqueToAldeaPos.position;
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
        player.transform.position = bosqueToLagoPos.position;
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
        player.transform.position = bosqueToBosquePPos.position;
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
        player.transform.position = lagoToBosquePos.position;
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
        player.transform.position = bosquePToBosquePos.position;
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
