using System;
using UnityEngine;

public class StatusAndBuffsSystem : MonoBehaviour
{
    public static StatusAndBuffsSystem instance;
    public event EventHandler OnStatusTimerChanged,OnBuffTimerChanged;

    private int remainingStatusDuration;
    private int remainingBuffsDuration;

    private void Awake()
    {
        instance = this;
    }

    public void SetStatusTimer(int duration)
    {
        remainingStatusDuration = duration;
        if (OnStatusTimerChanged != null)
        {
            OnStatusTimerChanged(this, EventArgs.Empty);
        }
    }
    public void SetBuffsTimer(int duration)
    {
        remainingBuffsDuration = duration;
        if (OnBuffTimerChanged != null)
        {
            OnBuffTimerChanged(this, EventArgs.Empty);
        }
    }

    public void StatusTimerTick()
    {
        if (remainingStatusDuration > 0)
        {
            remainingStatusDuration--;
        }
        if (OnStatusTimerChanged != null)
        {
            OnStatusTimerChanged(this, EventArgs.Empty);
        }
    }

    public void BuffsTimerTick()
    {
        if (remainingBuffsDuration > 0)
        {
            remainingBuffsDuration--;
        }
        if (OnBuffTimerChanged != null)
        {
            OnBuffTimerChanged(this, EventArgs.Empty);
        }
    }

    public int GetRemainingStatusDuration()
    {
        return remainingStatusDuration;
    }

    public int GetRemainingBuffDuration()
    {
        return remainingBuffsDuration;
    }

    public bool HasStatus()
    {
        if (remainingStatusDuration > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public bool HasBuffs()
    {
        if (remainingBuffsDuration > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public string RefreshStatusTimerCounter()
    {
        return remainingStatusDuration.ToString();
    }

    public string RefreshBuffsTimerCounter()
    {
        return remainingBuffsDuration.ToString();
    }
}