using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quests/Crear nueva Quest")]
public class Quest : ScriptableObject
{
    [SerializeField] string questName;
    [SerializeField] string description;

    [TextArea]
    [SerializeField] string startDialogue;
    [TextArea]
    [SerializeField] string inProgressDialogue;
    [TextArea]
    [SerializeField] string completedDialogue;

    [SerializeField] int moneyReward;
    [SerializeField] int experienceReward;

    public QuestGoal questGoal;

    public string _questName => questName;
    public string _description => description;
    public string _startDialogue => startDialogue;
    public string _inProgressDialogue => inProgressDialogue;
    public string _completedDialogue => completedDialogue;

    public int _moneyReward => moneyReward;
    public int _experienceReward => experienceReward;

}

[System.Serializable]
public class QuestGoal
{
    public enum QUESTSTATE { NONE, STARTED, COMPLETED }
    public QUESTSTATE questState = QUESTSTATE.NONE;

    [SerializeField] private int requiredAmount;
    [SerializeField] private int currentAmount;

    public int _requiredAmount => requiredAmount;
    public int _currentAmount
    {
        get
        {
            return currentAmount;
        }
        set
        {
            currentAmount = value;
        }
    }

    public void QuestStarted()
    {
        questState = QUESTSTATE.STARTED;
    }

    public void QuestCompleted()
    {
        questState = QUESTSTATE.COMPLETED;
    }

    public bool CanComplete()
    {
        return currentAmount >= requiredAmount;
    }
}
