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
    [SerializeField] GameObject trenTrenBattleTrigger,caiCaiBattleTrigger,lakeAccess,villageAccess;//, guardia01, guardia02;

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

    //private void Start()
    //{
    //    switch (GameData.state)
    //    {
    //        case GameData.State.TrenTrenSaved:
    //            trenTrenBattleTrigger.SetActive(false);
    //            confiner.m_BoundingShape2D = bosqueProfundoConfiner;
    //            confiner.InvalidatePathCache();
    //            break;
    //    }
    //}

    public GameObject GetTrenTrenBattleTrigger() => trenTrenBattleTrigger;
    public GameObject GetCaiCaiBattleTrigger() => caiCaiBattleTrigger;
    public GameObject GetLakeAccess() => lakeAccess;
    public GameObject GetVilageAccess() => villageAccess;
    //public GameObject GetGuardia01() => guardia01;
    //public GameObject GetGuardia02() => guardia02;
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
        //Transition FadeIn
        confiner.m_BoundingShape2D = bosqueAraucConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = aldeaToBosque.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position - follower.GetOffset();
        }
        GameData.mapZoneState = GameData.MapZone.BosqueAraucarias;
        //Transition FadeOut
    }

    public void BosqueToAldea()
    {
        //Transition FadeIn
        confiner.m_BoundingShape2D = aldeaConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = bosqueToAldea.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position - follower.GetOffset();
        }
        GameData.mapZoneState = GameData.MapZone.Aldea;
        //Transition FadeOut

    }

    public void BosqueToLago()
    {
        //Transition FadeIn
        confiner.m_BoundingShape2D = lagoConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = bosqueToLago.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position - follower.GetOffset();
        }
        GameData.mapZoneState = GameData.MapZone.Lago;
        //Transition FadeOut

    }
    public void BosqueToBosqueP()
    {
        //Transition FadeIn
        confiner.m_BoundingShape2D = bosqueProfundoConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = bosqueToBosqueP.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position - follower.GetOffset();
        }
        GameData.mapZoneState = GameData.MapZone.BosqueProfundo;
        //Transition FadeOut

    }
    public void LagoToBosque()
    {
        //Transition FadeIn
        confiner.m_BoundingShape2D = bosqueAraucConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = lagoToBosque.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position - follower.GetOffset();
        }
        GameData.mapZoneState = GameData.MapZone.BosqueAraucarias;
        //Transition FadeOut

    }
    public void BosquePToBosque()
    {
        //Transition FadeIn
        confiner.m_BoundingShape2D = bosqueAraucConfiner;
        confiner.InvalidatePathCache();
        player.transform.position = bosquePToBosque.position;
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            follower.gameObject.transform.position = player.transform.position - follower.GetOffset();
        }
        GameData.mapZoneState = GameData.MapZone.BosqueAraucarias;
        //Transition FadeOut

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
