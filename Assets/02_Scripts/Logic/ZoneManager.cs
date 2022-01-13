using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using MEC;

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
        Timing.RunCoroutine(_WaitForFadeToComplete(bosqueAraucConfiner, aldeaToBosquePos.position, GameData.MapZone.BosqueAraucarias));
    }

    public void BosqueToAldea()
    {
        Timing.RunCoroutine(_WaitForFadeToComplete(aldeaConfiner, bosqueToAldeaPos.position, GameData.MapZone.Aldea));
    }

    public void BosqueToLago()
    {
        Timing.RunCoroutine(_WaitForFadeToComplete(lagoConfiner, bosqueToLagoPos.position, GameData.MapZone.Lago));
    }

    public void BosqueToBosqueP()
    {
        Timing.RunCoroutine(_WaitForFadeToComplete(bosqueProfundoConfiner, bosqueToBosquePPos.position, GameData.MapZone.BosqueProfundo));
    }

    public void LagoToBosque()
    {
        Timing.RunCoroutine(_WaitForFadeToComplete(bosqueAraucConfiner, lagoToBosquePos.position, GameData.MapZone.BosqueAraucarias));
    }

    public void BosquePToBosque()
    {
        Timing.RunCoroutine(_WaitForFadeToComplete(bosqueAraucConfiner, bosquePToBosquePos.position, GameData.MapZone.BosqueAraucarias));
    }

    public void TrenTrenBeforeBattle()
    {
        Dialogues.Play_TrenTrenBeforeBattle(OverworldManager.GetInstance().GetTrenTrenCharacter());
    }
    public void CaicaiBeforeBattle()
    {
        Dialogues.Play_CaiCaiBeforeBattle(OverworldManager.GetInstance().GetCaiCaiCharacter());
    }

    IEnumerator<float> _WaitForFadeToComplete(PolygonCollider2D confiner, Vector3 position, GameData.MapZone mapZone)
    {
        OverworldManager.StopOvermapRunning();
        //UIFade.Show();
        UIFade.FadeIn();
        yield return Timing.WaitForSeconds(UIFade.GetTimer());
        this.confiner.m_BoundingShape2D = confiner;
        this.confiner.InvalidatePathCache();
        player.transform.position = position;
        GameData.mapZoneState = mapZone;

        Timing.WaitUntilDone(_FollowerPositionUpdated());

        UIFade.FadeOut();
        yield return Timing.WaitForSeconds(UIFade.GetTimer());
        //UIFade.Hide();
        OverworldManager.ContinueOvermapRunning();
    }

    IEnumerator<float> _FollowerPositionUpdated()
    {
        foreach (FollowerOverworld follower in OverworldManager.GetInstance().GetFollowerList())
        {
            yield return Timing.WaitForOneFrame;
            //follower.gameObject.transform.position = player.transform.position;
            follower.GetAIPath().Teleport(player.transform.position, true);
            follower.GetSeeker().CancelCurrentPathRequest();
        }
    }
}
