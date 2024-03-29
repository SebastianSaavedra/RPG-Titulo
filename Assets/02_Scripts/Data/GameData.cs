using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public enum State
    {
        Intro,
        Starting,
        AlreadyTalkedWithViejaMachi,
        SavingTrenTren,
        TrenTrenSaved,
        FightingCaiCai,
        CaiCaiBeated,
        PeleandoVSPrimerPeloton,
        PeleandoVSSegundoPeloton,
        PeleandoVSTercerPeloton,
        PrimerPelotonVencido,
        SegundoPelotonVencido,
        TercerPelotonVencido,
        Endgame,
        GameOver,
    }

    public enum MapZone
    {
        Aldea,
        BosqueAraucarias,
        Lago,
        BosqueProfundo
    }

    private static bool isInit = false;
    public static List<Character> characterList;
    public static List<Item> itemList;

    public static State state;
    public static MapZone mapZoneState;

    public static bool cutsceneAlreadyWatched;

    public static string[] nombresMasculinosMapucheArray;
    public static string[] nombresFemeninosMapucheArray;

    public static void Init()
    {
        if (isInit)
        {
            return;
        }
        //Debug.Log("Se iniciaron los datos del GAMEDATA");
        isInit = true;
        SoundManager.Initialize();
        //mapZoneState = MapZone.Aldea;
        state = State.Intro;
        nombresMasculinosMapucheArray = new string[] 
        {
            "Nahuel",
            "Eluney",
            "Aukan",
            "Tahiel",
            "Ayun",
            "Lautaro",
            "Quimey",
            "Lihue",
            "Yaco",
            "Auca",
            "Nahuelpan",
            "Quillen",
            "Huenchull�n",
            "Lican ray",
            "Alihuen",
            "Yenien",
            "Kuyen",
            "Nahuelquin",
            "Yamai",
            "Caucaman",
            "Alhue",
            "Linkoyan",
            "Lancuyen",
            "Antilaf",
            "Nulpi",
            "Huapi",
            "Hueicha",
            "Raiquen",
            "Amuyen",
            "Mainque",
            "Cuyen",
            "Chaiten",
            "Huilen",
            "Cobquecura",
            "Cumelen",
            "Ayelen",
        }; 
        nombresFemeninosMapucheArray = new string[]
         {
            "Millaray",
            "Sayen",
            "Aimar�",
            "Ailin",
            "Mailen",
            "Ayel�n",
            "Aneley",
            "Inara",
            "Ailen",
            "Amancay",
            "Lihu�n",
            "Yanara",
            "Lilen",
            "Amaike",
            "Mait�n",
            "Mait�n",
            "Rayen",
            "Aliwe",
            "Antumalen",
            "Yerimen",
            "Ayalen",
            "Ayinray",
            "Guacolda",
            "Yankiray",
            "Pire",
            "Llanca",
         };


        characterList = new List<Character>();

        characterList.Add(new Character(Character.Type.Suyai)
            {
                position = GameAssets.i.Map.Find("Suyai").position,
            });

        characterList.Add(new Character(Character.Type.Antay)
        {
            position = GameAssets.i.Map.Find("Antay").position,
        });

        characterList.Add(new Character(Character.Type.Pedro)
        {
            position = GameAssets.i.Map.Find("Pedro").position,
        });

        characterList.Add(new Character(Character.Type.Arana)
        {
            position = GameAssets.i.Map.Find("Arana").position,
        });

        characterList.Add(new Character(Character.Type.Chillpila)
        {
            position = GameAssets.i.Map.Find("Chillpila").position,
        });

        foreach (Transform mapSpawn in GameAssets.i.Map)
        {
            if (!mapSpawn.gameObject.activeSelf)
            {
                continue;
            }
            CharacterSpawnData characterSpawnData = mapSpawn.GetComponent<CharacterSpawnData>();
            if (characterSpawnData != null)
            {
                characterList.Add(
                    new Character(characterSpawnData.characterType)
                    {
                        position = mapSpawn.position,
                        enemyEncounter = characterSpawnData.enemyEncounter,
                        npcDialogues = characterSpawnData.npcDialogues,
                        quest = characterSpawnData.quest,

                        shopContents = characterSpawnData.shopContents.Clone()
                    }
                );
            }
        }

        itemList = new List<Item>();
        foreach (Transform mapSpawn in GameAssets.i.Map)
        {
            ItemSpawnData itemSpawnData = mapSpawn.GetComponent<ItemSpawnData>();
            if (itemSpawnData != null)
            {
                itemList.Add(new Item(itemSpawnData.itemType, itemSpawnData.amount, mapSpawn.position));
            }
        }

        foreach (Quest quest in GameAssets.i.questArray)
        {
            quest.questGoal.questState = QuestGoal.QUESTSTATE.NONE;
            quest.questGoal._currentAmount = 0;
        }
    }

    public static string GetCharacterName(Character.Type characterType)
    {
        Character character = GetCharacter(characterType);
        if (character != null)
        {
            return character.name;
        }
        else
        {
            return "???";
        }
    }

    public static Character GetCharacter(Character.Type characterType)
    {
        foreach (Character character in characterList)
        {
            if (character.type == characterType)
            {
                return character;
            }
        }
        return null;
    }

    //Encuentro con enemigo/s
    [Serializable]
    public class EnemyEncounter
    {

        public EnemyBattle[] enemyBattleArray;

        [Serializable]
        public struct EnemyBattle
        {
            public Character.Type characterType;
            public Battle.LanePosition lanePosition;
            public EnemyBattle(Character.Type characterType, Battle.LanePosition lanePosition)
            {
                this.characterType = characterType;
                this.lanePosition = lanePosition;
            }
        }
    }

    [Serializable]
    public class NPCDialogues
    {
        public DialoguesList[] dialogues;

        [Serializable]
        public struct DialoguesList
        {
            [TextArea]
            public string[] dialogue;
        }
    }

    //Tienda
    [Serializable]
    public class ShopContents
    {
        public int healingHerbs;

        public ShopContents Clone()
        {
            return new ShopContents
            {
                healingHerbs = healingHerbs
            };
        }
    }
}
