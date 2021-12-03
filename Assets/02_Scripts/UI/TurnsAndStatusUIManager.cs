using UnityEngine;
using TMPro;
using DG.Tweening;

public class TurnsAndStatusUIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI turnText,statusText;
    private void Start()
    {
        TurnSystem.instance.OnTurnChanged += TurnSystem_OnTurnChanged;
        StatusSystem.instance.OnTimerChanged += StatusSystem_OnTimerChanged;
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        turnText.text = TurnSystem.instance.RefreshTurnCounter();
    }
    private void StatusSystem_OnTimerChanged(object sender, System.EventArgs e)
    {
        statusText.text = StatusSystem.instance.RefreshTimerCounter();
    }
}
