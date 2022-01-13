using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityAnimData : MonoBehaviour
{
    [SerializeField] List<GameObject> rayos = new List<GameObject>();
    [SerializeField] GameObject monedaCara, monedaCruz;
    [SerializeField] GameObject mineralCobre, mineralBronce, mineralPlata, mineralOro;

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

    public GameObject GetMineralCobre()
    {
        return mineralCobre;
    }

    public GameObject GetMineralBronce()
    {
        return mineralBronce;
    }

    public GameObject GetMineralPlata()
    {
        return mineralPlata;
    }

    public GameObject GetMineralOro()
    {
        return mineralOro;
    }


}
