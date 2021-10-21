using System;
using UnityEngine;

public class SpecialAbilitiesCostSystem : MonoBehaviour
{
    public static SpecialAbilitiesCostSystem instance;

    public event EventHandler OnMoneyChanged, OnHerbsChanged, OnSoulsChanged,OnTattoosChanged, OnHitsChanged;

    //Pedro
    private int maxMoney = 100;
    private int money;

    //Suyai
    private int maxHerbs = 10;
    private int herbs;

    //Chillquila
    private int maxSouls = 30;
    private int souls;

    //Antay
    private int maxHits = 10;
    private int hitAmounts;

    //Arana
    private int maxTattoos = 5;
    private int tattoos;

    private void Start()
    {
        instance = this;
    }

    #region Money
    public int _DebugMoney()
    {
        if (money >= maxMoney)
        {
            money = maxMoney;
        }
        else
        {
            money += 10;
        }
        if (OnMoneyChanged != null)
        {
            OnMoneyChanged(this, EventArgs.Empty);
        }
        return money;
    }

    public void SetMoneyAmount(int amount)
    {
        this.money = amount;
        if (money >= maxMoney)
        {
            money = maxMoney;
        }
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

    public int GetMaxMoneyAmount()
    {
        return maxMoney;
    }
    public string RefreshMoneyCounter()
    {
        return money.ToString();
    }
    #endregion

    #region Herbs
    public int _DebugHerbs()
    {
        if (herbs >= maxHerbs)
        {
            herbs = maxHerbs;
        }
        else
        {
            herbs += 10;
        }
        if (OnHerbsChanged != null)
        {
            OnHerbsChanged(this, EventArgs.Empty);
        }
        return herbs;
    }
    public void SetHerbsAmount(int amount)
    {
        this.herbs = amount;
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
    public int _DebugSouls()
    {
        if (souls >= maxSouls)
        {
            souls = maxSouls;
        }
        else
        {
            souls += 10;
        }
        if (OnSoulsChanged != null)
        {
            OnSoulsChanged(this, EventArgs.Empty);
        }
        return souls;
    }
    public void SetSoulsAmount(int amount)
    {
        this.souls = amount;
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
    public int _DebugTattoos()
    {
        if (tattoos >= maxTattoos)
        {
            tattoos = maxTattoos;
        }
        else
        {
            tattoos += 1;
        }
        if (OnTattoosChanged != null)
        {
            OnTattoosChanged(this, EventArgs.Empty);
        }
        return tattoos;
    }
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
    public int _DebugHits()
    {
        if (hitAmounts >= maxHits)
        {
            hitAmounts = maxHits;
        }
        else
        {
            hitAmounts += 1;
        }
        if (OnHitsChanged != null)
        {
            OnHitsChanged(this, EventArgs.Empty);
        }
        return hitAmounts;
    }
    public void SetHitsAmount(int amount)
    {
        this.hitAmounts = amount;
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
}
