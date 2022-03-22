using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CodeMonkey.Utils;

public class PlayDialogueEventTrigger : MonoBehaviour
{
    public void PlayPrimerPeloton()
    {
        Dialogues.Play_PrimerPeloton(OverworldManager.GetInstance().GetLanceroCharacter());
    }
    public void PlayNPCDesesperado()
    {
        FunctionTimer.Create(Dialogues.Play_Soldado_Desesperado,1.15f);
        //Dialogues.Play_Soldado_Desesperado();
    }
}
