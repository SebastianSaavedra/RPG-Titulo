using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAnimData : MonoBehaviour
{
    [SerializeField] List<GameObject> rayos = new List<GameObject>();
    [SerializeField] GameObject monedaCara, monedaCruz;

    public List<GameObject> GetRayosList()
    {
        return rayos;
    }

    public GameObject GetMonedaCara()
    {
        return monedaCara;
    }
    public GameObject GetMonedaCruz()
    {
        return monedaCruz;
    }
}
