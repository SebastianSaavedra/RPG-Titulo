using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager instance;
    public event EventHandler OnMoneyChanged, OnHerbsChanged, OnSoulsChanged, OnTattoosChanged, OnHitsChanged;

    //Pedro
    //private int maxMoney = 100;
    private int money;

    //Suyai
    private int maxHerbs = 100;
    private int herbs;

    //Chillquila
    private int maxSouls = 100;
    private int souls;

    //Antay
    private int maxHits = 10;
    private int hitAmounts;

    //Arana
    private int maxTattoos = 5;
    private int tattoos;


    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            return;
        }
    }

    private void Start()
    {
        AddMoney(100);
        AddSouls(50);
        SetTattoosAmount(5);
        SetHitsAmount(10);
    }

    #region Money

    public void AddMoney(int amount)
    {
        this.money += amount;
        if (OnMoneyChanged != null)
        {
            OnMoneyChanged(this, EventArgs.Empty);
        }
    }
    public void PayMoney(int amount)
    {
        money -= amount;
        if (OnMoneyChanged != null)
        {
            OnMoneyChanged(this, EventArgs.Empty);
        }
    }

    public int GetMoneyAmount()
    {
        return money;
    }

    //public int GetMaxMoneyAmount()
    //{
    //    return maxMoney;
    //}
    public string RefreshMoneyCounter()
    {
        return money.ToString();
    }
    #endregion

    #region Herbs
    public void AddHerbs(int amount)
    {
        if (herbs >= maxHerbs)
        {
            herbs = maxHerbs;
        }
        else
        {
            this.herbs += amount;
        }
        if (OnHerbsChanged != null)
        {
            OnHerbsChanged(this, EventArgs.Empty);
        }
    }
    public void ConsumeHerbs(int amount)
    {
        herbs -= amount;
        if (OnHerbsChanged != null)
        {
            OnHerbsChanged(this, EventArgs.Empty);
        }
    }

    public int GetHerbsAmount()
    {
        return herbs;
    }

    public int GetMaxHerbsAmount()
    {
        return maxHerbs;
    }
    public string RefreshHerbsCounter()
    {
        return herbs.ToString();
    }
    #endregion

    #region Souls
    public void AddSouls(int amount)
    {
        if (souls >= maxSouls)
        {
            souls = maxSouls;
        }
        else
        {
            souls += amount;
        }
        if (OnSoulsChanged != null)
        {
            OnSoulsChanged(this, EventArgs.Empty);
        }
    }
    public void ConsumeSouls(int amount)
    {
        souls -= amount;
        if (OnSoulsChanged != null)
        {
            OnSoulsChanged(this, EventArgs.Empty);
        }
    }

    public int GetSoulsAmount()
    {
        return souls;
    }

    public int GetMaxSoulsAmount()
    {
        return maxSouls;
    }
    public string RefreshSoulsCounter()
    {
        return souls.ToString();
    }
    #endregion

    #region Tattoos
    //public int _DebugTattoos()
    //{
    //    if (tattoos >= maxTattoos)
    //    {
    //        tattoos = maxTattoos;
    //    }
    //    else
    //    {
    //        tattoos += 1;
    //    }
    //    if (OnTattoosChanged != null)
    //    {
    //        OnTattoosChanged(this, EventArgs.Empty);
    //    }
    //    return tattoos;
    //}
    public void SetTattoosAmount(int amount)
    {
        this.tattoos = amount;
        if (OnTattoosChanged != null)
        {
            OnTattoosChanged(this, EventArgs.Empty);
        }
    }
    public void ConsumeTattoos(int amount)
    {
        tattoos -= amount;
        if (OnTattoosChanged != null)
        {
            OnTattoosChanged(this, EventArgs.Empty);
        }
    }

    public int GetTattoosAmount()
    {
        return tattoos;
    }

    public int GetMaxTattoosAmount()
    {
        return maxTattoos;
    }
    public string RefreshTattoosCounter()
    {
        return tattoos.ToString();
    }
    #endregion

    #region Hits
    public void SetHitsAmount(int amount)
    {
        if (hitAmounts >= maxHits)
        {
            hitAmounts = maxHits;
        }
        else
        {
            hitAmounts += amount;
        }
        if (OnHitsChanged != null)
        {
            OnHitsChanged(this, EventArgs.Empty);
        }
    }
    public void ConsumeHits(int amount)
    {
        hitAmounts -= amount;
        if (OnHitsChanged != null)
        {
            OnHitsChanged(this, EventArgs.Empty);
        }
    }

    public int GetHitsAmount()
    {
        return hitAmounts;
    }

    public int GetMaxHitsAmount()
    {
        return maxHits;
    }
    public string RefreshHitsCounter()
    {
        return hitAmounts.ToString();
    }
    #endregion

    public void RefreshResourcesUI()
    {
        OnHitsChanged(this, EventArgs.Empty);
        OnHerbsChanged(this, EventArgs.Empty);
        OnMoneyChanged(this, EventArgs.Empty);
        OnSoulsChanged(this, EventArgs.Empty);
        OnTattoosChanged(this, EventArgs.Empty);
    }
}
