using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameData
{
    public enum State
    {
        Start,
        FightingHurtMeDaddy,
        DefeatedHurtMeDaddy,
        FightingHurtMeDaddy_2,
        DefeatedHurtMeDaddy_2,
        FightingTank,
        DefeatedTank,
        GoingToTownCenter,
        ArrivedAtTownCenter,
        GoingToAskDoppelGanger,
        GoingToTavern,
        InTavern,
        FightingTavernAmbush,
        SurvivedTavernAmbush,
        HealerJoined,
        LeavingTown,
        GoingToFirstEvilMonsterEncounter,
        GoingToFightEvilMonster,
        FightingEvilMonster_1,
        LostToEvilMonster_1,
        MovingToEvilMonster_2,
        GoingToFightEvilMonster_2,
        FightingEvilMonster_2,
        LostToEvilMonster_2,
        MovingToEvilMonster_3,
        GoingToFightEvilMonster_3,
        FightingEvilMonster_3,
        DefeatedEvilMonster,
        GameOver,
    }

    private static bool isInit = false;
    public static List<Character> characterList;
    //public static List<Character> inPlayerTeamList;
    public static List<Item> itemList;

    public static State state;

    public static void Init()
    {
        if (isInit)
        {
            return;
        }
        //Debug.Log("Se iniciaron los datos del GAMEDATA");
        isInit = true;
        SoundManager.Initialize();
        state = State.Start;
        characterList = new List<Character>();
        //inPlayerTeamList = new List<Character>();

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
        //inPlayerTeamList.Add(new Character(Character.Type.Chillpila));
        //inPlayerTeamList.Add(new Character(Character.Type.Suyai));
        //inPlayerTeamList.Add(new Character(Character.Type.Pedro));
        //inPlayerTeamList.Add(new Character(Character.Type.Arana));
        //inPlayerTeamList.Add(new Character(Character.Type.Antay));

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
                        //shopContents = characterSpawnData.shopContents.Clone()
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


    //public static bool TrySpendHealthPotion()
    //{
    //    if (healthPotionCount > 0)
    //    {
    //        healthPotionCount--;
    //        return true;
    //    }
    //    else
    //    {
    //        return false;
    //    }
    //}






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



    //Tienda
    [Serializable]
    public class ShopContents
    {
        public int healthPotions;

        public ShopContents Clone()
        {
            return new ShopContents
            {
                healthPotions = healthPotions
            };
        }
    }
}
