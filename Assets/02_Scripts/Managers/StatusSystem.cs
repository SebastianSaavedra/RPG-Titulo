using System;
using UnityEngine;

public class StatusSystem : MonoBehaviour
{
    public static StatusSystem instance;
    public event EventHandler OnTimerChanged;

    private int remainingDuration;
    private void Awake()
    {
        instance = this;
    }

    public void SetStatusTimer(int duration)
    {
        remainingDuration = duration;
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
        if (OnTimerChanged != null)
        {
            OnTimerChanged(this, EventArgs.Empty);
        }
    }

    public int GetRemainingStatusDuration()
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