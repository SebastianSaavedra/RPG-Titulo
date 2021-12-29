using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroyOnLoadGameObject : MonoBehaviour
{
    private static DontDestroyOnLoadGameObject instance;
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
