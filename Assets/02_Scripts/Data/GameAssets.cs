using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;
    public static GameAssets i
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("GameAssets"));
            }
            return _i;
        }
    }

    [Header("Mini Assets para Debug")]
    public Sprite s_White;
    public Sprite s_Circle;
    public Material m_White;
    [Space(10)]
    //////////////////////////
    [Header("Assets del juego")]
    public Transform Map;
    public Transform characterBattle;
    public Transform damagePopup;
    public Texture2D t_Ally, t_enemy;
    public RuntimeAnimatorController allyANIM, enemyANIM;



}
