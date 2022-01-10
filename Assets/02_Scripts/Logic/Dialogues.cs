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
    public static void Play_Soldado_Agitado(NPCOverworld npc)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.ShowText("<j> SUYAI, <d = 1> NOS ATACAN… <d/> <d = 1> LA ALDEA… <d/> <d = 1> NUESTRA GENTE");
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
    public static void Play_TrenTrenDuringBattle()
    {
        Battle.GetInstance().state = Battle.State.Busy;
        BattleUI.instance.TalkingInBattle();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false);
                dialogue.ShowText("Puedo percibir que nunca has estado en un combate déjame guiarte.");
                dialogue.ShowRightCharacterName("TrenTren");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("Tienes un menú radial de combate con múltiples opciones");
            },
            () => {
                dialogue.ShowText("Si seleccionas arriba, puedes (ICONO) ATACAR o (ICONO) DEFENDER");
            },
            () => {
                dialogue.ShowText("Si seleccionas derecha puedes usar una (ICONO) HABILIDAD.");
            },
            () => {
                dialogue.ShowText("Estas gastaran alguno de los recursos necesarios para ocuparla, así que mientras tengas dichos recursos, ocúpate de probar cada habilidad que tengas a tu disposición.");
            },
            () => {
                dialogue.ShowText("Si seleccionas izquierda puedes entrar en tu (ICONO) INVENTARIO para usar algunos ítems en combate. Es importante que revises tu inventario siempre que puedas");
            },
            () => {
                dialogue.ShowText("Si seleccionas abajo puedes intentar (ICONO) ESCAPAR. Pero no siempre lo lograras, inténtalo cuando no exista otra opción.");
            },
            () => {
                dialogue.ShowText("Ahora derrota a los enemigos que me están atacando, me gustaría poder defenderme a mi mismo, pero estoy bastante debilitado a estas alturas.");
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
                dialogue.ShowRightCharacter(GameAssets.i.caiCaiSprite, false);
                dialogue.ShowText("Hace tiempo, nosotros velábamos por ustedes");
                dialogue.ShowRightCharacterName("CaiCai");
                dialogue.ShowRightNameplate();
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
                dialogue.ShowText("Eso solo demostró estupidez y que no eran dignos de nuestra preocupación");
            },
            () => {
                dialogue.ShowText("Por lo que los dejamos a su suerte, esperando que se aniquilaran entre ustedes...");
            },
            () => {
                dialogue.ShowText("Sin embargo, sobrevivieron...");
            },
            () => {
                dialogue.ShowText("Y ahora estas aquí, frente a mi para darme la oportunidad de acabar con ustedes empezando por ti y tu gente.");
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
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false);
                dialogue.ShowText("Muchas gracias Suyai, permíteme presentarme, soy Trentren Vilu, serpiente terrestre. ");
                dialogue.ShowRightCharacterName("TrenTren");
                dialogue.ShowRightNameplate();
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
                dialogue.ShowText("No será fácil hacer que te escuche, pero por lo que note, tienes carácter y sé que lograras convencerlo...");
            },
            () => {
                dialogue.ShowText("...por las buenas o las malas");
            },
            () => {
                dialogue.ShowText("Quisiera ser yo quien se encargará de este asunto y no tener que involucrar a otros");
            },
            () => {
                dialogue.ShowText("Pero debo recuperarme aun para hacerle frente, y eso tomara tiempo.");
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
                dialogue.ShowRightCharacter(GameAssets.i.trenTrenSprite, false);
                dialogue.ShowText("Tu chica, por favor, ayúdame.");
                dialogue.ShowRightCharacterName("TrenTren");
                dialogue.ShowRightNameplate();
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
                dialogue.ShowRightCharacter(GameAssets.i.caiCaiSprite, false);
                dialogue.ShowText("Me contaron mis ayudantes que tu fuiste quien salvo a Trentren.");
                dialogue.ShowRightCharacterName("CaiCai");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("Te mostrare lo que pasa cuando mortales se involucran en temas superiores a ellos.");
            },
            () => {
                dialogue.Hide();
                GameData.state = GameData.State.FightingCaiCai;
                SoundManager.PlaySound(SoundManager.Sound.BattleTransition);
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
                dialogue.ShowRightCharacter(GameAssets.i.caiCaiSprite, false);
                dialogue.ShowText("Eres lo suficientemente resistente.");
                dialogue.ShowRightCharacterName("CaiCai");
                dialogue.ShowRightNameplate();
                dialogue.HideLeftCharacter();
                dialogue.HideLeftNameplate();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.ShowText("Sin embargo, no tengo mas tiempo que perder para encargarme de ti.");
            },
            () => {
                dialogue.ShowText("Yo no soy la mayor de tus preocupaciones.");
            },
            () => {
                dialogue.ShowText("Ya pronto se volverán a levantar en armas entre ustedes mortales");
            },
            () => {
                dialogue.ShowText("Y estaré mirando a lo lejos, esperando la oportunidad para acabar con todos de una buena vez.");
            },
            () => {
                dialogue.Hide();
                Inventory.instance.AddItem(new Item(Item.ItemType.EscamaMarina));
                GameData.cutsceneAlreadyWatched = true;
                OverworldManager.StartOvermapRunning();
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
