using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnUI : MonoBehaviour
{
    [SerializeField] TurnSystem turnSystem;
    [SerializeField] TextMeshProUGUI text;
    private void Start()
    {
        turnSystem.OnTurnChanged += TurnSystem_OnTurnChanged;    
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            turnSystem._DebugTurns();
        }
    }

    private void TurnSystem_OnTurnChanged(object sender, System.EventArgs e)
    {
        text.text = turnSystem.RefreshTurnCounter();
    }
}
