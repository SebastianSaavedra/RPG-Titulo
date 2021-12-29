using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acelerar : MonoBehaviour
{
    [Range(.1f, 2f)]
    public float velocidadDeJuego = 1f;

    void Update()
    {
        Time.timeScale = velocidadDeJuego;
    }
}
