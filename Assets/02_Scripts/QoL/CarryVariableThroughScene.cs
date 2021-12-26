using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryVariableThroughScene : MonoBehaviour
{
    public static CarryVariableThroughScene instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
    }

}
