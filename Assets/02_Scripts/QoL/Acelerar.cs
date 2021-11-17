using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acelerar : MonoBehaviour
{
    private static Acelerar instance;

    [Range(.1f, 2f)]
    public float velocidadDeJuego = 1f;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }
    void Update()
    {
        Time.timeScale = velocidadDeJuego;
    }
}
