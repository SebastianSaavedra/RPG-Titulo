using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class RandomEnemySplashArt : MonoBehaviour
{
    Image pj;
    private void Awake()
    {
        pj = GetComponent<Image>();
    }
    void OnEnable()
    {
        pj.sprite = GameAssets.i.enemies[Random.Range(0, GameAssets.i.enemies.Count)];
    }
}
