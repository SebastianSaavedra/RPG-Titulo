using UnityEngine;
using TMPro;
using DG.Tweening;

public class TurnsAndStatusUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText,statusText,buffsText;

    private void Start()
    {
        TurnSystem.instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        StatusAndBuffsSystem.instance.OnStatusTimerChanged += StatusAndBuffsSystem_OnTimerChanged;
        StatusAndBuffsSystem.instance.OnBuffTimerChanged += StatusAndBuffsSystem_OnBuffTimerChanged;
    }

    private void StatusAndBuffsSystem_OnBuffTimerChanged(object sender, System.EventArgs e)
    {
        buffsText.text = StatusAndBuffsSystem.instance.RefreshBuffsTimerCounter();
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        turnText.text = TurnSystem.instance.RefreshTurnCounter();
    }
    private void StatusAndBuffsSystem_OnTimerChanged(object sender, System.EventArgs e)
    {
        statusText.text = StatusAndBuffsSystem.instance.RefreshStatusTimerCounter();
    }
}
