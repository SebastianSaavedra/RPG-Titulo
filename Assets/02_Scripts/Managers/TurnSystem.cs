using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using MEC;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem instance;

    public event EventHandler OnTurnChanged;

    [HideInInspector] public int turns;

    private void Awake()
    {
        instance = this;
    }

    public void SetTurnCount(int turns)
    {
        this.turns += turns;
        //Debug.Log(this.turns);
        if (OnTurnChanged != null)
        {
            OnTurnChanged(this, EventArgs.Empty);
        }
    }

    public int _DebugTurns()
    {
        turns = 1;
        if (OnTurnChanged != null)
        {
            OnTurnChanged(this, EventArgs.Empty);
        }
        return turns;
    }

    public int GetTurnCount()
    {
        return turns;
    }
    public bool ZeroTurns()
    {
        return turns <= 0;
    }

    public int TurnDecrease()
    {
        --turns;
        if (OnTurnChanged != null)
        {
            OnTurnChanged(this, EventArgs.Empty);
        }
        return turns;
    }

    public string RefreshTurnCounter()
    {
        return turns.ToString();
    }
}
