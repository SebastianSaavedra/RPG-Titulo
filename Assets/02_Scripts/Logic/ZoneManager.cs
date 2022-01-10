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

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        confiner = GameObject.Find("CM vcam1").GetComponent<CinemachineConfiner>();
        player = GameObject.Find("pfPlayer").GetComponent<PlayerOverworld>();
    }

    public void ActivateColliderAldeaToBosque()
    {
        colliderAldeaToBosque.gameObject.SetActive(true);
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
        //Transition FadeOut

    }

    public void TrenTrenBeforeBattle()
    {
        Dialogues.Play_TrenTrenBeforeBattle(OverworldManager.GetInstance().GetTrenTrenCharacter());
    }
}
