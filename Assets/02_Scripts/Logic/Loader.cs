using UnityEngine;
using UnityEngine.SceneManagement;

public static class Loader
{

    public enum Scene
    {
        OverworldScene,
        //Loading,
        BattleScene,
        GameOver,
    }

    public static void LoadTargetScene(Scene scene)
    {
        SceneManager.LoadScene(scene.ToString());
    }

}