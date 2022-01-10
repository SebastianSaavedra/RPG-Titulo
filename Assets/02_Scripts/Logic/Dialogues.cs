using System;
using System.Collections.Generic;

public static class Dialogues
{
    //public static void QuestDialogue(Character character)
    //{
    //    OverworldManager.StopOvermapRunning();
    //    Character playerCharacter = GameData.GetCharacter(Character.Type.Suyai);
    //    DialogueController dialogue = DialogueController.GetInstance();
    //    if (character.quest.questGoal.questState == QuestGoal.QUESTSTATE.NONE)
    //    {
    //        dialogue.SetDialogueActions(new List<Action>() {
    //            () => {
    //                dialogue.Show();
    //                dialogue.ShowText(character.quest._startDialogue);
    //                switch (character.type)
    //                {
    //                    case Character.Type.QuestNpc_1:
    //                            dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
    //                        break;
    //                    case Character.Type.SoldadoMapuche_1:
    //                    case Character.Type.SoldadoMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.ViejaMachi:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.HombreMapuche_1:
    //                    case Character.Type.HombreMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.MujerMapuche_1:
    //                    case Character.Type.MujerMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.NinoMapuche_1:
    //                    case Character.Type.NinoMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.NinaMapuche_1:
    //                    case Character.Type.NinaMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                }
    //                dialogue.ShowRightNameplate();
    //                dialogue.ShowRightCharacterName(character.name);
    //                dialogue.HideLeftCharacter();
    //                dialogue.HideLeftNameplate();
    //                dialogue.HideLeftCharacterName();
    //            },() => {
    //                character.quest.questGoal.QuestStarted();
    //                QuestManager.instance.quests.Add(character.quest);
    //                dialogue.Hide();
    //                OverworldManager.StartOvermapRunning();
    //            },
    //        }, true);

    //    }
    //    else if (character.quest.questGoal.CanComplete())
    //    {
    //        dialogue.SetDialogueActions(new List<Action>() {
    //            () => {
    //                dialogue.Show();
    //                dialogue.ShowText(character.quest._completedDialogue);
    //                switch (character.type)
    //                {
    //                    case Character.Type.QuestNpc_1:
    //                            dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
    //                        break;
    //                    case Character.Type.SoldadoMapuche_1:
    //                    case Character.Type.SoldadoMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.ViejaMachi:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.HombreMapuche_1:
    //                    case Character.Type.HombreMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.MujerMapuche_1:
    //                    case Character.Type.MujerMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.NinoMapuche_1:
    //                    case Character.Type.NinoMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.NinaMapuche_1:
    //                    case Character.Type.NinaMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                }
    //                dialogue.ShowRightNameplate();
    //                dialogue.ShowRightCharacterName(character.name);
    //                dialogue.HideLeftCharacter();
    //                dialogue.HideLeftNameplate();
    //                dialogue.HideLeftCharacterName();
    //            },() => {
    //                dialogue.Hide();
    //                character.quest.questGoal.QuestCompleted();
    //                ResourceManager.instance.AddMoney(character.quest._moneyReward);
    //                OverworldManager.StartOvermapRunning();
    //            },
    //        }, true);

    //    }
    //    else if (character.quest.questGoal.questState == QuestGoal.QUESTSTATE.STARTED)
    //    {
    //        dialogue.SetDialogueActions(new List<Action>() {
    //            () => {
    //                dialogue.Show();
    //                dialogue.ShowText(character.quest._inProgressDialogue);
    //                switch (character.type)
    //                {
    //                    case Character.Type.QuestNpc_1:
    //                            dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
    //                        break;
    //                    case Character.Type.SoldadoMapuche_1:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                    case Character.Type.SoldadoMapuche_2:
    //                            dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
    //                        break;
    //                }
    //                dialogue.ShowRightNameplate();
    //                dialogue.ShowRightCharacterName(character.name);
    //                dialogue.HideLeftCharacter();
    //                dialogue.HideLeftNameplate();
    //                dialogue.HideLeftCharacterName();
    //            },() => {
    //                dialogue.Hide();
    //                OverworldManager.StartOvermapRunning();
    //            },
    //        }, true);

    //    }
    //}

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

    public static void Play_StartViejaMachi(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_ViejaMachi, false);
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(character.name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[1]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[2]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[3]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[4]);
            },() => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_ViejaMachiQuest(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_ViejaMachi, false);
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[0]);
                dialogue.ShowRightCharacterName(character.name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[1]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[2]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[3]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[4]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[5]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[6]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[7]);
            },() => {
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[8]);
            },() => {
                dialogue.Hide();
                character.quest.questGoal.QuestStarted();
                QuestManager.instance.quests.Add(character.quest);
                GameData.state = GameData.State.AlreadyTalkedWithViejaMachi;
                ZoneManager.instance.ActivateColliderAldeaToBosque();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_ViejaMachiQuestPending(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_ViejaMachi, false);
                dialogue.ShowText(character.npcDialogues.dialogues[2].dialogue[0]);
                dialogue.ShowRightCharacterName(character.name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },() => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_SoldadoAdvertenciaAntesDeHablarConMachi(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(character.name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[1]);
            },
            () => {
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[2]);
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_SoldadoAdvertenciaBosque(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[1]);
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[2]);
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[3]);
            },
            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_SoldadoBuenaSuerte(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.ShowText(character.npcDialogues.dialogues[2].dialogue[0]);
                dialogue.ShowRightCharacterName(character.name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Hombre01(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_HombreMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[1]);
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[2]);
            },
            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Hombre01Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_HombreMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Hombre02(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_HombreMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[1]);
            },
            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Hombre02Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_HombreMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Mujer01(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_MujerMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Mujer01Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_MujerMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[1]);
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Mujer02(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_MujerMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[1]);
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[2]);
            },
            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Mujer02Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_MujerMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[1].dialogue[1]);
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Nino01(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_NinoMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },

            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Nino01Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_NinoMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[1]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },

            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Nino02(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_NinoMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },

            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Nino02Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_NinoMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[1]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },

            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Nina01(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_NinaMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[1]);
            },
            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Nina01Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_NinaMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[2]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Nina02(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_NinaMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[0]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[1]);
            },
            () => {
                dialogue.Hide();
                npc.SetTalkForTheFirstTime();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_Nina02Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_NinaMapuche, false);
                dialogue.ShowText(npc.GetCharacter().npcDialogues.dialogues[0].dialogue[2]);
                dialogue.ShowRightCharacterName(npc.GetCharacter().name);
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }





    public static void Play_TrenTrenBeforeBattle(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() 
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false);
                dialogue.ShowText("Tu chica, por favor, ayúdame.");
                dialogue.ShowRightCharacterName("TrenTren");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                GameData.state = GameData.State.SavingTrenTren;
                SoundManager.PlaySound(SoundManager.Sound.BattleTransition);
                Battle.LoadEnemyEncounter(character, character.enemyEncounter);
            },
        }, true);
    }

    public static void ShopDialogue(Character shopCharacter)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() 
        {
            () => 
            {
                dialogue.Show();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftCharacterName();
                dialogue.HideLeftNameplate();
                dialogue.ShowRightCharacter(GameAssets.i.npc_SHOP, false);
                dialogue.ShowRightCharacterName(GameData.GetCharacterName(Character.Type.Shop));
                dialogue.ShowRightNameplate();
                dialogue.ShowText("¡Hola!, ¡te interesa ver lo que tengo?");
            },
            () => 
            {
                dialogue.Hide();

                UI_Shop.Show_Static(shopCharacter.shopContents, () => 
                {
                    //OverworldManager.StartOvermapRunning();
                });
            },
        }, true);
    }
}
