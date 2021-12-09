using System;
using System.Collections.Generic;

public static class Dialogues
{
    public static void QuestDialogue(Character character)
    {
        OverworldManager.StopOvermapRunning();
        Character playerCharacter = GameData.GetCharacter(Character.Type.Suyai);
        DialogueController dialogue = DialogueController.GetInstance();
        if (character.quest.questGoal.questState == QuestGoal.QUESTSTATE.NONE)
        {
            dialogue.SetDialogueActions(new List<Action>() {
                () => {
                    dialogue.Show();
                    dialogue.ShowText(character.quest._startDialogue);
                    switch (character.type)
                    {
                        case Character.Type.NPC_1:
                                dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
                            break;
                    }
                    dialogue.ShowRightCharacterName(character.name);
                    dialogue.HideLeftCharacter();
                    //dialogue.HideText();
                    dialogue.HideLeftCharacterName();
                },() => {
                    character.quest.questGoal.QuestStarted();
                    QuestManager.instance.quests.Add(character.quest);
                    dialogue.Hide();
                    OverworldManager.StartOvermapRunning();
                },
            }, true);

        }
        else if (character.quest.questGoal.CanComplete())
        {
            dialogue.SetDialogueActions(new List<Action>() {
                () => {
                    dialogue.Show();
                    dialogue.ShowText(character.quest._completedDialogue);
                    switch (character.type)
                    {
                        case Character.Type.NPC_1:
                                dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
                            break;
                    }
                    dialogue.ShowRightCharacterName(character.name);
                    dialogue.HideLeftCharacter();
                    //dialogue.HideText();
                    dialogue.HideLeftCharacterName();
                },() => {
                    dialogue.Hide();
                    character.quest.questGoal.QuestCompleted();
                    ResourceManager.instance.AddMoney(character.quest._moneyReward);
                    OverworldManager.StartOvermapRunning();
                },
            }, true);

        }
        else if (character.quest.questGoal.questState == QuestGoal.QUESTSTATE.STARTED)
        {
            dialogue.SetDialogueActions(new List<Action>() {
                () => {
                    dialogue.Show();
                    dialogue.ShowText(character.quest._inProgressDialogue);
                    switch (character.type)
                    {
                        case Character.Type.NPC_1:
                                dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
                            break;
                    }
                    dialogue.ShowRightCharacterName(character.name);
                    dialogue.HideLeftCharacter();
                    //dialogue.HideText();
                    dialogue.HideLeftCharacterName();
                },() => {
                    dialogue.Hide();
                    OverworldManager.StartOvermapRunning();
                },
            }, true);

        }
    }
    //public static void RandomNPCDialog(Character character)
    //{
    //    OverworldManager.StopOvermapRunning();
    //    DialogueController dialogue = DialogueController.GetInstance();
    //    dialogue.SetDialogueActions(new List<Action>() {
    //        () => {
    //            dialogue.Show();
    //            switch (character.type)
    //            {
    //                case Character.Type.NPC_1:
    //                        dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
    //                    break;
    //            }
    //            dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[UnityEngine.Random.Range(0,character.npcDialogues.dialogues[1].dialogue.Length)]);
    //            dialogue.ShowRightCharacterName(character.name);
    //            dialogue.HideLeftCharacter();
    //            dialogue.HideLeftCharacterName();
    //        },
    //        () => {
    //            dialogue.Hide();
    //            OverworldManager.StartOvermapRunning();
    //        },
    //    }, true);
    //}
}
