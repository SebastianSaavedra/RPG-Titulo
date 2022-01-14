using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Acelerar : MonoBehaviour
{
    [Range(.1f, 2f)]
    public float velocidadDeJuego = 2f;

    void Update()
    {
        if (Input.GetKey(KeyCode.Tab))
        {
            Time.timeScale = velocidadDeJuego;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}
