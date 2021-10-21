using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] TurnSystem turnSystem;
    [SerializeField] SpecialAbilitiesCostSystem abilityCostSystem;
    [SerializeField] TextMeshProUGUI turnText,moneyText,herbsText,soulsText,tattoosText,hammerHitsText,debuffText;
    private void Awake()        //Si a futuro ocurren problemas quizas sea neceasario cambiarlo a start y mandar una corrutina
    {
        abilityCostSystem.OnMoneyChanged += AbilityCostSystem_OnMoneyChanged;
        abilityCostSystem.OnHerbsChanged += AbilityCostSystem_OnHerbsChanged;
        abilityCostSystem.OnSoulsChanged += AbilityCostSystem_OnSoulsChanged;
        abilityCostSystem.OnTattoosChanged += AbilityCostSystem_OnTattoosChanged;
        abilityCostSystem.OnHitsChanged += AbilityCostSystem_OnHitsChanged;
        //statusSystem.OnTimerChanged += DebuffTimerSystem_OnTimerChanged;
    }
    private void Start()
    {
        turnSystem.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            turnSystem._DebugTurns();
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            abilityCostSystem._DebugMoney();
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            abilityCostSystem._DebugHerbs();
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            abilityCostSystem._DebugSouls();
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            abilityCostSystem._DebugTattoos();
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            abilityCostSystem._DebugHits();
        }
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        turnText.text = turnSystem.RefreshTurnCounter();
    }

    private void AbilityCostSystem_OnMoneyChanged(object sender, System.EventArgs e)
    {
        moneyText.text = abilityCostSystem.RefreshMoneyCounter();
    }
    private void AbilityCostSystem_OnHerbsChanged(object sender, System.EventArgs e)
    {
        herbsText.text = abilityCostSystem.RefreshHerbsCounter();
    }
    private void AbilityCostSystem_OnSoulsChanged(object sender, System.EventArgs e)
    {
        soulsText.text = abilityCostSystem.RefreshSoulsCounter();
    }
    private void AbilityCostSystem_OnTattoosChanged(object sender, System.EventArgs e)
    {
        tattoosText.text = abilityCostSystem.RefreshTattoosCounter();
    }
    private void AbilityCostSystem_OnHitsChanged(object sender, System.EventArgs e)
    {
        hammerHitsText.text = abilityCostSystem.RefreshHitsCounter();
    }
    //private void DebuffTimerSystem_OnTimerChanged(object sender, System.EventArgs e)
    //{
    //    debuffText.text = statusSystem.RefreshTimerCounter();
    //}
}
