using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayDialogueEventTrigger : MonoBehaviour
{
    public void PlayPrimerPeloton()
    {
        Dialogues.Play_PrimerPeloton(OverworldManager.GetInstance().GetLanceroCharacter());
    }
    public void PlayNPCDesesperado()
    {
        Dialogues.Play_Soldado_Desesperado();
    }
}
