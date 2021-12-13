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
                        case Character.Type.QuestNpc_1:
                                dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
                            break;
                        case Character.Type.WarriorNPC_1:
                                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                            break;
                        case Character.Type.WarriorNPC_2:
                                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
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
                        case Character.Type.QuestNpc_1:
                                dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
                            break;
                        case Character.Type.WarriorNPC_1:
                                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                            break;
                        case Character.Type.WarriorNPC_2:
                                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
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
                        case Character.Type.QuestNpc_1:
                                dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
                            break;
                        case Character.Type.WarriorNPC_1:
                                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                            break;
                        case Character.Type.WarriorNPC_2:
                                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
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
    public static void Play_Start(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.ShowText("Suyai, ¿así que estas cumpliendo el desafío para finalmente ser reconocida como Machi?");
                dialogue.ShowRightCharacterName("Nehuen");
                dialogue.HideLeftCharacter();
                dialogue.HideLeftCharacterName();
            },() => {
                dialogue.ShowText("La Anciana ya nos ha puesto al corriente, y nos ha pedido que no te dejemos volver hasta que cumplas la tarea que se te ha encomendado, la que consiste en recolectar al menos 6 <q=herb> <c=greenHerb>hierbas.");
            },() => {
                dialogue.ShowText("Aunque no se muy bien como son capaces de diferenciarlas.");
            },() => {
                dialogue.ShowText("He oido que las Machis pueden distinguir facilmente los arbustos de donde poder sacar dichas hierbas.");
            },() => {
                dialogue.ShowText("Dicen que en los arbustos con <c=greenHerb> colores más vivos <c=normal> pueden crecer las plantas curativas, pero para mi, todos son del mismo color.");
            },() => {
                dialogue.ShowText("Ahora que te di ese consejo es hora de que empieces, no quiero quedarme haciendo guardia hasta el anochecer.");
            },() => {
                dialogue.ShowText("Si no sabes por donde empezar, podrias usar WASD o las Flechas direccionales para explorar la zona.");
            },() => {
                dialogue.ShowText("Si ves algo sospechoso, no dudes en inspeccionarlo con SPACE.");
            },() => {
                dialogue.ShowText("Por ultimo, puedes organizarte apretando ENTER.");
            },
            () => {
                dialogue.Hide();
                character.quest.questGoal.QuestStarted();
                QuestManager.instance.quests.Add(character.quest);
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }
    public static void TestDialogue_1(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.ShowText("Ya no hay nada mas que pueda decirte Suyai, ¡Mucha Suerte!");
                dialogue.ShowRightCharacterName(character.name);
                dialogue.HideLeftCharacter();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }
    public static void TestDialogue_2(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.ShowText("Lo siento Suyai, no puedo dejarte avanzar por acá.");
                dialogue.ShowRightCharacterName(character.name);
                dialogue.HideLeftCharacter();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }
    public static void TestDialogue_3(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowRightCharacter(GameAssets.i.warriorNpcDialogueSprite, false);
                dialogue.ShowText("Finalmente, has completado la misión que se te ha encomendado, vamos a la aldea.");
                dialogue.ShowRightCharacterName(character.name);
                dialogue.HideLeftCharacter();
                dialogue.HideLeftCharacterName();
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
                UnityEngine.Application.Quit();
            },
        }, true);
    }
}
