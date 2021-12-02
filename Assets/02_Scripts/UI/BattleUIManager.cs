using UnityEngine;
using TMPro;
using DG.Tweening;

public class BattleUIManager : MonoBehaviour
{
    [SerializeField] TurnSystem turnSystem;
    [SerializeField] TextMeshProUGUI turnText,moneyText,herbsText,soulsText,tattoosText,hammerHitsText,debuffText;
    private void Awake()        //Si a futuro ocurren problemas quizas sea neceasario cambiarlo a start y mandar una corrutina
    {
        ResourceManager.instance.OnMoneyChanged += AbilityCostSystem_OnMoneyChanged;
        ResourceManager.instance.OnHerbsChanged += AbilityCostSystem_OnHerbsChanged;
        ResourceManager.instance.OnSoulsChanged += AbilityCostSystem_OnSoulsChanged;
        ResourceManager.instance.OnTattoosChanged += AbilityCostSystem_OnTattoosChanged;
        ResourceManager.instance.OnHitsChanged += AbilityCostSystem_OnHitsChanged;
        StatusSystem.instance.OnTimerChanged += DebuffTimerSystem_OnTimerChanged;
    }
    private void Start()
    {
        turnSystem.OnTurnChanged += TurnSystem_OnTurnChanged;
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        turnText.text = turnSystem.RefreshTurnCounter();
    }

    private void AbilityCostSystem_OnMoneyChanged(object sender, System.EventArgs e)
    {
        moneyText.text = ResourceManager.instance.RefreshMoneyCounter();
    }
    private void AbilityCostSystem_OnHerbsChanged(object sender, System.EventArgs e)
    {
        herbsText.text = ResourceManager.instance.RefreshHerbsCounter();
    }
    private void AbilityCostSystem_OnSoulsChanged(object sender, System.EventArgs e)
    {
        soulsText.text = ResourceManager.instance.RefreshSoulsCounter();
    }
    private void AbilityCostSystem_OnTattoosChanged(object sender, System.EventArgs e)
    {
        tattoosText.text = ResourceManager.instance.RefreshTattoosCounter();
    }
    private void AbilityCostSystem_OnHitsChanged(object sender, System.EventArgs e)
    {
        hammerHitsText.text = ResourceManager.instance.RefreshHitsCounter();
    }
    private void DebuffTimerSystem_OnTimerChanged(object sender, System.EventArgs e)
    {
        debuffText.text = StatusSystem.instance.RefreshTimerCounter();
    }
}
