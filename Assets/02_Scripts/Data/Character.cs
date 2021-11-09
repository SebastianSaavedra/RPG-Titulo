using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// Clase que contiene datos de cada personaje
public class Character
{

    public static bool IsUniqueCharacterType(Type type)
    {
        switch (type)
        {
            default:
            case Type.Suyai:
            case Type.Antay:
            case Type.Pedro:
            case Type.Arana:
            case Type.Chillpila:

            case Type.Jefe1:
                return true;
            case Type.TESTENEMY:
                return false;
        }
    }


    public class Stats
    {
        public int healthMax;
        public int health;
        public int attack;
        public int damageChance;
        public int turns;
    }

    public enum Type
    {
        Suyai,     // Prota/Healer Suyai
        Antay,     // Tanque Antay
        Pedro,     // Debuffer Pedro
        Arana,     // Lancero Arana
        Chillpila, // Mago oscuro Chillpila

        TESTENEMY,

        Jefe1,
        //Jefe2,
        //Jefe3,

        Shop,
        //Villager_1,
        //Villager_2,
        //Villager_3,
        //Villager_4,
        //Villager_5,
    }

    public enum SubType
    {
        None,

        Enemy_HurtMeDaddy,
        Enemy_HurtMeDaddy_2,

        Tank_BeforeJoin,
        Tank_Friendly,

        Healer_BeforeJoin,
        Healer_Friendly,

        EvilMonster_1,
        EvilMonster_2,
        EvilMonster_3,
    }

    public Type type;
    public SubType subType;
    public Stats stats;
    public string name;
    public Vector3 position;
    public GameData.EnemyEncounter enemyEncounter;
    public GameData.ShopContents shopContents;
    public bool isDead;
    public bool isInPlayerTeam;     // DEFINE SI ES QUE ESTE CHARACTER VA A SER SPAWNEADO O NO EN EL TEAM DEL PLAYER
    public bool weapon;

    public Character(Type type, SubType subType = SubType.None)
    {
        this.type = type;
        this.subType = subType;
        name = type.ToString();

        stats = new Stats
        {
            attack = 10,
            health = 100,
            healthMax = 100,
            damageChance = 75,
            turns = 1
        };

        switch (type)
        {
            default:
                break;

            /////////////////// Personajes jugables
            
            case Type.Suyai:       // Prota - Healer
                stats = new Stats
                {
                    attack = 20,
                    health = 100,
                    healthMax = 100,
                    turns = 2
                };
                isInPlayerTeam = true;
                break;

            case Type.Chillpila:       // Kalcu - Mago oscuro
                stats = new Stats
                {
                    attack = 20,
                    health = 90,
                    healthMax = 90,
                    turns = 2
                };
                isInPlayerTeam = true;
                break;

            case Type.Pedro:            // Trickster - Debuffer
                stats = new Stats
                {
                    attack = 30,
                    health = 105,
                    healthMax = 105,
                    turns = 2
                };
                isInPlayerTeam = true;
                break;

            case Type.Antay:            // Tank
                stats = new Stats
                {
                    attack = 25,
                    health = 150,
                    healthMax = 150,
                    turns = 3
                };
                //isInPlayerTeam = true;
                break;

            case Type.Arana:            // Lancero - DmgDealer
                stats = new Stats
                {
                    attack = 30,
                    health = 110,
                    healthMax = 110,
                    turns = 3
                };
                //isInPlayerTeam = true;
                break;


            /////////////////// ENEMIGOS
            case Type.TESTENEMY:
                name = "TEST ENEMY";
                stats = new Stats
                {
                    attack = 16,
                    health = 100,
                    healthMax = 100,
                    damageChance = 75,
                };
                break;

            //case Type.Villager_1:
            //case Type.Villager_2:
            //case Type.Villager_3:
            //case Type.Villager_4:
            //case Type.Villager_5:
            //    name = "Villager";
            //    break;

            case Type.Shop:
                name = "Vendor";
                break;
        }
        isDead = false;
    }

    public bool IsInPlayerTeamFunc()
    {
        return isInPlayerTeam;
    }

    public bool IsEnemy()
    {
        switch (type)
        {
            default:
            case Type.Suyai:
            case Type.Antay:
            case Type.Pedro:
            case Type.Arana:
            case Type.Chillpila:
                return false;
            case Type.TESTENEMY:
            case Type.Jefe1:
                return true;
        }
    }

}
