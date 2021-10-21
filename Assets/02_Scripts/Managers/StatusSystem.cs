using System;
using UnityEngine;

public class StatusSystem
{
    public event EventHandler OnTimerChanged;

    private int maxDuration;
    private int remainingDuration;

    public void SetStatusTimer(int maxDuration)
    {
        this.maxDuration = maxDuration;
        remainingDuration = maxDuration;
        if (OnTimerChanged != null)
        {
            OnTimerChanged(this, EventArgs.Empty);
        }
    }
    public void TimerTick()
    {
        if (remainingDuration > 0)
        {
            remainingDuration--;
        }
        Debug.Log("La duración del Status Effect es: " + remainingDuration);
        if (OnTimerChanged != null)
        {
            OnTimerChanged(this, EventArgs.Empty);
        }
    }
    
    public int GetStatusDurationAmount()
    {
        return remainingDuration;
    }

    public bool HasStatus()
    {
        if (remainingDuration > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string RefreshTimerCounter()
    {
        return remainingDuration.ToString();
    }

}
