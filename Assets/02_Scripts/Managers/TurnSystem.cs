using System;
using UnityEngine;

public class TurnSystem : MonoBehaviour
{
    public static TurnSystem instance;

    public event EventHandler OnTurnChanged;

    [HideInInspector] public int turns;
    int totalAmount;

    private void Awake()
    {
        instance = this;
    }

    public void SetTurnCount(int turns)
    {
        this.turns += turns;
        if (this.turns >= totalAmount)
        {
            totalAmount = this.turns;
        }
        if (OnTurnChanged != null)
        {
            OnTurnChanged(this, EventArgs.Empty);
        }
    }

    public int GetTurnCount()
    {
        return turns;
    }

    public int GetTotalAmountOfTurns()
    {
        return totalAmount;
    }

    public bool ZeroTurns()
    {
        return turns <= 0;
    }

    public int TurnDecrease()
    {
        if (turns == 0)
        {
            return turns;
        }
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
