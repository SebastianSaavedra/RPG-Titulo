using System;
using System.Collections.Generic;
using CodeMonkey.Utils;

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
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[0]);
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
                dialogue.Hide();
                character.quest.questGoal.QuestStarted();
                QuestManager.instance.quests.Add(character.quest);
                GameData.state = GameData.State.AlreadyTalkedWithViejaMachi;
                ZoneManager.instance.GetAldeaToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToAldeaPortal().SetActive(true);
                ZoneManager.instance.GetBosquePToBosquePortal().SetActive(true);
                ZoneManager.instance.GetBosqueToBosquePPortal().SetActive(true);
                ZoneManager.instance.GetTrenTrenBattleTrigger().SetActive(true);
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
    public static void Play_Soldado_2(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
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
    public static void Play_Soldado_Desesperado()
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                OverworldManager.StopOvermapRunning();
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.GetSuperTextMesh().readDelay = .08f;
                dialogue.ShowText("<j> SUYAI...");
                dialogue.ShowRightCharacterName("Ankatu");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                OverworldManager.StopOvermapRunning();
                dialogue.ShowText("<j> NOS ATACAN… ");
            },
            () => {
                dialogue.ShowText("<j> LA ALDEA…");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = .03f;
                dialogue.ShowText("<j> NUESTRA GENTE!");
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }
    public static void Play_Soldado_2Repeat(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
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

    public static void Play_TrenTrenStartingBattle()
    {
        Battle.GetInstance().state = Battle.State.Busy;
        BattleUI.instance.TalkingInBattle();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false, new UnityEngine.Vector3(901.8f,-412f,0f));
                dialogue.ShowText("Puedo percibir que nunca has estado en un combate, déjame guiarte.");
                dialogue.ShowRightCharacterName("TrenTren",new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.ShowRightNameplate(new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("Tienes un menú radial de combate con múltiples opciones.");
            },
            () => {
                dialogue.ShowText("Si seleccionas arriba, puedes <q=ATACAR> ATACAR o <q=DEFENDER> DEFENDER.");
            },
            () => {
                dialogue.ShowText("Si te defiendes, la defensa de ese personaje aumenta en 1 hasta su proximo turno y no te descuenta el turno.");
            },
            () => {
                dialogue.ShowText("En los combates importantes tendras un contador de turnos que se indica en la esquina superior derecha.");
            },
            () => {
                dialogue.ShowText("Si este llega a 0, pierdes.");
            },
            () => {
                dialogue.ShowText("Si seleccionas derecha puedes usar una <q=HABILIDAD> HABILIDAD.");
            },
            () => {
                dialogue.ShowText("Estás gastarán alguno de los recursos necesarios para ocuparla, así que mientras tengas dichos recursos, ocúpate de probar cada habilidad que tengas a tu disposición.");
            },
            () => {
                dialogue.ShowText("Si seleccionas izquierda puedes entrar en tu <q=INVENTARIO> INVENTARIO para usar algunos ítems en combate. Es importante que revises tu inventario siempre que puedas.");
            },
            () => {
                dialogue.ShowText("Si seleccionas abajo puedes intentar <q=ESCAPAR> ESCAPAR. Pero no siempre lo lograrás, inténtalo cuando no exista otra opción.");
            },
            () => {
                dialogue.ShowText("Ahora derrota a los enemigos que me están atacando, me gustaría poder defenderme a mí mismo, pero estoy bastante debilitado a estas alturas.");
            },
            () => {
                dialogue.Hide();
                BattleUI.instance.FinishedTalkingInBattle();
                Battle.GetInstance().state = Battle.State.WaitingForPlayer;
            },
        }, true);
    }

    public static void Play_TrenTrenDuringBattle()
    {
        Battle.GetInstance().state = Battle.State.Busy;
        BattleUI.instance.TalkingInBattle();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false, new UnityEngine.Vector3(901.8f,-412f,0f));
                dialogue.ShowText("Rápido Suyai, intenta <c=green> curarme <c=normal> con alguna de tus <q=HABILIDAD> habilidades.");
                dialogue.ShowRightCharacterName("TrenTren",new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.ShowRightNameplate(new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                BattleUI.instance.FinishedTalkingInBattle();
                Battle.GetInstance().state = Battle.State.WaitingForPlayer;
            },
        }, true);
    }

    public static void Play_CaiCaiDuringBattle()
    {
        Battle.GetInstance().state = Battle.State.Busy;
        BattleUI.instance.TalkingInBattle();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.caiCaiSprite, false,new UnityEngine.Vector3(901.8f,-412f,0f));
                dialogue.ShowText("Hace tiempo, nosotros velábamos por ustedes");
                dialogue.ShowRightCharacterName("CaiCai",new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.ShowRightNameplate(new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("Creyendo que eran una raza por la que valía la pena garantizar vuestra evolución como especie.");
            },
            () => {
                dialogue.ShowText("Pero se levantaron en armas contra sus propios hermanos.");
            },
            () => {
                dialogue.ShowText("Dañando otras criaturas y a la propia naturaleza que tanto nos costo moldear y construir para ustedes.");
            },
            () => {
                dialogue.ShowText("Eso solo demostró estupidez y que no eran dignos de nuestra preocupación.");
            },
            () => {
                dialogue.ShowText("Por lo que los dejámos a su suerte, esperando que se aniquilaran entre ustedes...");
            },
            () => {
                dialogue.ShowText("Sin embargo, sobrevivieron...");
            },
            () => {
                dialogue.ShowText("Y ahora estas aquí, frente a mí para darme la oportunidad de acabar con ustedes empezando por tí y tu gente.");
            },
            () => {
                dialogue.Hide();
                BattleUI.instance.FinishedTalkingInBattle();
                Battle.GetInstance().state = Battle.State.WaitingForPlayer;
            },
        }, true);
    }

    public static void Play_TrenTrenSaved()
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false, new UnityEngine.Vector3(901.8f,-412f,0f));
                dialogue.ShowText("Muchas gracias Suyai, permíteme presentarme, soy Trentren Vilu, serpiente terrestre.");
                dialogue.ShowRightCharacterName("TrenTren",new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.ShowRightNameplate(new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("Seguramente no lo sabrás, porque hace ya muchísimo tiempo de estos acontecimientos, pero pertenezco a una serie de criaturas ancestrales quienes convivíamos con los hombres.");
            },
            () => {
                dialogue.ShowText("Pero algunos acontecimientos hicieron que nuestros destinos se separaran.");
            },
            () => {
                dialogue.ShowText("Esa es la historia resumida, me gustaría entrar en mas detalles, pero dejemos eso para luego...");
            },
            () => {
                dialogue.ShowText("...ya que, me apena decirlo, pero necesito de tu ayuda nuevamente.");
            },
            () => {
                dialogue.ShowText("Estaba siendo atacado por orden de mi hermano Caicai Vilu, la serpiente marina.");
            },
            () => {
                dialogue.ShowText("Ha de andar por acá cerca, y mientras este por aquí, los mortales están en peligro.");
            },
            () => {
                dialogue.ShowText("Demostraste ser una chica fuerte, así que te pido que lo persuadas para que se retire.");
            },
            () => {
                dialogue.ShowText("Que vuelva a sus dominios en el fondo de los mares, ríos y lagos.");
            },
            () => {
                dialogue.ShowText("No será fácil hacer que te escuche, pero por lo que noté, tienes carácter y sé que lograrás convencerlo...");
            },
            () => {
                dialogue.ShowText("...por las buenas o las malas.");
            },
            () => {
                dialogue.ShowText("Quisiera ser yo quien se encargara de este asunto y no tener que involucrar a otros.");
            },
            () => {
                dialogue.ShowText("Pero debo recuperarme aún para hacerle frente, y eso tomará tiempo.");
            },
            () => {
                dialogue.ShowText("¡Rápido Suyai!");
            },
            () => {
                dialogue.Hide();
                GameData.cutsceneAlreadyWatched = true;
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
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false, new UnityEngine.Vector3(901.8f,-412f,0f));
                dialogue.ShowText("Tú, chica, por favor, ayúdame.");
                dialogue.ShowRightCharacterName("TrenTren",new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.ShowRightNameplate(new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                GameData.state = GameData.State.SavingTrenTren;
                SoundManager.PlaySound(SoundManager.Sound.BattleTransition);
                Battle.LoadEnemyEncounter(character, character.enemyEncounter);
            },
        }, true);
    }

    public static void Play_CaiCaiBeforeBattle(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.caiCaiSprite, false, new UnityEngine.Vector3(901.8f,-412f,0f));
                dialogue.ShowText("Me contaron mis ayudantes que tú fuiste quién salvó a Trentren.");
                dialogue.ShowRightCharacterName("CaiCai",new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.ShowRightNameplate(new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("Te mostraré lo que pasa cuando mortales se involucran en temas superiores a ellos.");
            },
            () => {
                dialogue.Hide();
                GameData.state = GameData.State.FightingCaiCai;
                SoundManager.PlaySound(SoundManager.Sound.BattleTransition);
                Battle.LoadEnemyEncounter(character, character.enemyEncounter);
            },
        }, true);
    }

    public static void Play_PrimerPeloton(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.lanceroOWSprite, false);
                dialogue.ShowText("¡Acá hay otra persona…!");
                dialogue.ShowRightCharacterName("Grupo de fusileros");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                SoundManager.PlaySound(SoundManager.Sound.BattleTransition);
                GameData.state = GameData.State.PeleandoVSPrimerPeloton;
                Battle.LoadEnemyEncounter(character, character.enemyEncounter);
            },
        }, true);
    }

    public static void Play_SegundoPeloton(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.fusileroOWSprite, false);
                dialogue.ShowText("No te creas la gran cosa por haber derrotado a esos novatos, ahora te las verás con hombres de verdad.");
                dialogue.ShowRightCharacterName("Grupo de Lanceros");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                SoundManager.PlaySound(SoundManager.Sound.BattleTransition);
                GameData.state = GameData.State.PeleandoVSSegundoPeloton;
                Battle.LoadEnemyEncounter(character, character.enemyEncounter);
            },
        }, true);
    }

    public static void Play_TercerPeloton(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowLeftCharacter(GameAssets.i.fusileroOWSprite, false);
                dialogue.ShowRightCharacter(GameAssets.i.lanceroOWSprite, false);
                dialogue.ShowText("No hay que subestimarte, ahora te tomaremos en serio.");
                dialogue.ShowRightCharacterName("Grupo de fusileros y Lanceros");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                SoundManager.PlaySound(SoundManager.Sound.BattleTransition);
                GameData.state = GameData.State.PeleandoVSTercerPeloton;
                Battle.LoadEnemyEncounter(character, character.enemyEncounter);
            },
        }, true);
    }

    public static void Play_CaiCaiBeated()
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.caiCaiSprite, false,new UnityEngine.Vector3(901.8f,-412f,0f));
                dialogue.ShowText("Eres lo suficientemente resistente.");
                dialogue.ShowRightCharacterName("CaiCai",new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.ShowRightNameplate(new UnityEngine.Vector3(810.6998f,-241f,0f));
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("Sin embargo, no tengo más tiempo que perder para encargarme de ti.");
            },
            () => {
                dialogue.ShowText("Yo no soy la mayor de tus preocupaciones.");
            },
            () => {
                dialogue.ShowText("Ya pronto se volverán a levantar en armas entre ustedes mortales.");
            },
            () => {
                dialogue.ShowText("Y estaré mirando a lo lejos, esperando la oportunidad para acabar con todos de una buena vez.");
            },
            () => {
                dialogue.HideRightCharacter();
                dialogue.HideRightCharacterName();
                dialogue.HideRightNameplate();
                dialogue.ShowText("Has recibido <c=blue> Escama Marina </c> <q=Escama>, lo puedes encontrar en tu inventario.");
            },
            () => {
                dialogue.Hide();
                Inventory.instance.AddItem(new Item(Item.ItemType.EscamaMarina));
                GameData.cutsceneAlreadyWatched = true;
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_ViejaMachiRescatada()
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_ViejaMachi, false);
                dialogue.GetSuperTextMesh().readDelay = .05f;
                dialogue.ShowText("<j> Suyai, llegaste en el peor momento, se han llevado a algunos de nosotros.");
                dialogue.ShowRightCharacterName("Kuyenray");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("<j> Los que nos quedamos acá en la aldea algunos estamos heridos...");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = .03f;
                dialogue.ShowText("Pero nos recuperaremos. ");
            },
            () => {
                dialogue.ShowText("...<d=10>¿Como estas tú?");
            },
            () => {
                dialogue.HideRightCharacter();
                dialogue.HideRightCharacterName();
                dialogue.HideRightNameplate();
                dialogue.ShowLeftCharacter(GameAssets.i.splashSuyai, false);
                dialogue.ShowLeftCharacterName("Suyai");
                dialogue.ShowLeftNameplate();
                dialogue.ShowText("     (Suyai le cuenta lo sucedido)");
            },
            () => {
                dialogue.ShowRightCharacter(GameAssets.i.npc_ViejaMachi, false);
                dialogue.ShowRightCharacterName("Kuyenray");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
                dialogue.ShowText("<c=greenHerb> ¿Trentren? <c=blue> ¿Caicai? </c> Hace décadas que no escuchaba esos nombres...");
            },
            () => {
                dialogue.ShowText("¿Estás segura de lo que cuentas?...");
            },
            () => {
                dialogue.HideRightCharacter();
                dialogue.HideRightCharacterName();
                dialogue.HideRightNameplate();
                dialogue.ShowLeftCharacter(GameAssets.i.splashSuyai, false);
                dialogue.ShowLeftCharacterName("Suyai");
                dialogue.ShowLeftNameplate();
                dialogue.ShowText("     Le muestras la <q=Escama> <c=blue> Escama Marina");
            },
            () => {
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
                dialogue.ShowRightCharacter(GameAssets.i.npc_ViejaMachi, false);
                dialogue.ShowRightCharacterName("Kuyenray");
                dialogue.ShowRightNameplate();
                dialogue.GetSuperTextMesh().readDelay = 0.08f;
                dialogue.ShowText("ESO… <d=5> ESO ES…");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = 0.05f;
                dialogue.ShowText("Suyai, no puedo creerlo, pero tu historia parece ser cierta.");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = 0.03f;
                dialogue.ShowText("Si es así, no puedes quedarte más tiempo por acá.");
            },
            () => {
                dialogue.ShowText("Vuelve con Trentren y cuéntale acerca de tu encuentro con Caicai.");
            },
            () => {
                dialogue.ShowText("Consigue que te preste su poder para enfrentar a estos enemigos.");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = 0.05f;
                dialogue.ShowText("Si ellos han vuelto, puede que más de estas criaturas ancestrales también lo hayan hecho...");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = 0.03f;
                dialogue.ShowText("De ser así, podrías salir de viaje para encontrar a otras que aún no conoces.");
            },
            () => {
                dialogue.ShowText("Y convencerlas de que se unan a nuestra lucha para traer de vuelta a nuestros amigos y familiares.");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = 0.1f;
                dialogue.ShowText("...");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = 0.05f;
                dialogue.ShowText("Cuídate Suyai...");
            },
            () => {
                dialogue.ShowText("Es una misión importante para alguien como tú, pero no tenemos otra opción.");
            },
            () => {
                dialogue.GetSuperTextMesh().readDelay = 0.03f;
                dialogue.ShowText("Confío en ti.");
            },
            () => {
                dialogue.Hide();
                GameData.state = GameData.State.Endgame;
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_MachiEndGame()
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.npc_ViejaMachi, false);
                dialogue.ShowText("¿Aún sigues por acá Suyai? No podemos perder más tiempo. Debes partir ahora.");
                dialogue.ShowRightCharacterName("Kuyenray");
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

    public static void Play_NPCEndGame()
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.ShowText("Está muy shockeado como para prestarte atención en este momento.");
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
                dialogue.HideRightCharacter();
                dialogue.HideRightCharacterName();
                dialogue.HideRightNameplate();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }

    public static void Play_TrenTrenEndGame()
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>()
        {
            () => {
                dialogue.Show();
                dialogue.HideRightCharacter();
                dialogue.HideRightCharacterName();
                dialogue.HideRightNameplate();
                dialogue.ShowLeftCharacter(GameAssets.i.splashSuyai, false);
                dialogue.ShowLeftCharacterName("Suyai");
                dialogue.ShowLeftNameplate();
                dialogue.ShowText("     Le muestras la <q=Escama> <c=blue> Escama Marina");
            },
            () => {
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false);
                dialogue.ShowRightCharacterName("TrenTren");
                dialogue.ShowRightNameplate();
                dialogue.ShowText("Así que lo lograste Suyai.");
            },
            () => {
                dialogue.ShowText("No estaba seguro de que lo conseguirías.");
            },
            () => {
                dialogue.ShowText("Pero me alegro de que hayas salido con vida.");
            },
            () => {
                dialogue.ShowText("Quiero ser sincero contigo y contarte lo que pasó.");
            },
            () => {
                dialogue.ShowText("Si, <d=5> es cierto que tras ver el egoísmo existente entre los mortales.");
            },
            () => {
                dialogue.ShowText("Egoísmo que los llevo a levantarse en armas entre ustedes...");
            },
            () => {
                dialogue.ShowText("Nosotros nos decepcionamos y alejamos.");
            },
            () => {
                dialogue.ShowText("Pero algunos hemos estado observando a lo lejos y hemos visto como ustedes han ido evolucionando y como han cambiado.");
            },
            () => {
                dialogue.ShowText("Por eso decidí darles una nueva oportunidad y me acerqué para informarles acerca del peligro inminente que se acercaba.");
            },
            () => {
                dialogue.ShowText("Sin embargo, Caicai descubrió mis planes y me atacó debilitándome");
            },
            () => {
                dialogue.ShowText("Por eso no pude avisar a tiempo y los enemigos llegaron a tu aldea.");
            },
            () => {
                dialogue.ShowText("Permíteme pagar mi falta otorgándote mi poder para que puedas ocuparlo en batallas importantes.");
            },
            () => {
                dialogue.ShowText("Aun debo recuperar fuerzas, así que no podrás ocupar mi habilidad aún.");
            },
            () => {
                dialogue.ShowText("Mientras busquemos a otros como yo que quieran prestar su poder a ustedes, los mortales.");
            },
            () => {
                dialogue.Hide();
                UIFade.FadeIn();
                FunctionTimer.Create(OverworldManager.LoadGameOver, UIFade.GetTimer());
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
                dialogue.ShowText("Suyai, encontre unas hierbas en el camino y bueno... quizás a tí te sirvan más que a mí.");
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
