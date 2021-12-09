using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestManager : MonoBehaviour
{
    public static QuestManager instance;
    
    public List<Quest> quests = new List<Quest>();

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            return;
        }
        Destroy(this.gameObject);
    }

    public void QuestProgress()
    {
        foreach (Quest quest in quests)
        {
            switch (quest._questName)
            {
                case "Matar enemigos":
                    quest.questGoal._currentAmount++;
                    break;
            }
        }
    }
}
