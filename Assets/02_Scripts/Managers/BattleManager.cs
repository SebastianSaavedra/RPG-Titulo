using UnityEngine;
using CodeMonkey.MonoBehaviours;

public class BattleManager : MonoBehaviour
{

    [SerializeField] private CameraFollow cameraFollow;

    private void Awake()
    {
        new Battle();
    }

    private void Start()
    {
        Battle.GetInstance().Start(cameraFollow);
    }

    private void Update()
    {
        Battle.GetInstance().Update();
    }

}
