using System;
using System.Collections.Generic;

public static class Dialogues
{
    public static void TestDialogue(Character character)
    {
        OverworldManager.StopOvermapRunning();
        Character playerCharacter = GameData.GetCharacter(Character.Type.Suyai);
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[0]);
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
                dialogue.Show();
                dialogue.ShowText("testeo testeo testeo testeo");
                dialogue.ShowLeftCharacter(GameAssets.i.playerDialogueSprite,false);
                dialogue.ShowLeftCharacterName(playerCharacter.name);
                dialogue.HideRightCharacter();
                //dialogue.HideText();
                dialogue.HideRightCharacterName();
            },() => {
                dialogue.Show();
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[1]);
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
                dialogue.Show();
                dialogue.ShowText(character.npcDialogues.dialogues[0].dialogue[2]);
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
            },
            () => {
                dialogue.Hide();
                OverworldManager.StartOvermapRunning();
            },
        }, true);
    }
    public static void RandomNPCDialog(Character character)
    {
        OverworldManager.StopOvermapRunning();
        DialogueController dialogue = DialogueController.GetInstance();
        dialogue.SetDialogueActions(new List<Action>() {
            () => {
                dialogue.Show();
                switch (character.type)
                {
                    case Character.Type.NPC_1:
                            dialogue.ShowRightCharacter(GameAssets.i.npc_1DialogueSprite, false);
                        break;
                }
                dialogue.ShowText(character.npcDialogues.dialogues[1].dialogue[UnityEngine.Random.Range(0,character.npcDialogues.dialogues[1].dialogue.Length)]);
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
}
