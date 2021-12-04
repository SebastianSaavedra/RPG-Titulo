using UnityEngine;
using CodeMonkey.MonoBehaviours;
using GridPathfindingSystem;
public class GameManager_Overworld : MonoBehaviour
{

    private static GameManager_Overworld instance;

    public static GridPathfinding gridPathfinding;
    [SerializeField] private CameraFollow cameraFollow;
    [SerializeField] private PlayerOverworld playerOverworld;
    [SerializeField] private float cameraZoom = 10f;

    private void Awake()
    {
        instance = this;
        new OverworldManager(playerOverworld);
    }

    private void Start()
    {

        gridPathfinding = new GridPathfinding(new Vector3(-400, -400), new Vector3(400, 400), 5f);
        gridPathfinding.RaycastWalkable();

        OverworldManager.GetInstance().Start(transform);

        cameraFollow.Setup(GetCameraPosition, () => cameraZoom, true, true);
    }

    private void Update()
    {
        OverworldManager.GetInstance().Update();
    }

    private Vector3 GetCameraPosition()
    {
        return playerOverworld.GetPosition();
    }

}
