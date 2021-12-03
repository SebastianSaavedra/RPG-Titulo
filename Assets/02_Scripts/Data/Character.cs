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
        public int defense;
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

    public enum LanePosition
    {
        Middle,
        Up,
        Down,
        None,
    }

    //public enum SubType
    //{
    //    None,

    //    Enemy_HurtMeDaddy,
    //    Enemy_HurtMeDaddy_2,

    //    Tank_BeforeJoin,
    //    Tank_Friendly,

    //    Healer_BeforeJoin,
    //    Healer_Friendly,

    //    EvilMonster_1,
    //    EvilMonster_2,
    //    EvilMonster_3,
    //}

    public Type type;
    //public SubType subType;
    public Stats stats;
    public string name;
    public Vector3 position;
    public GameData.EnemyEncounter enemyEncounter;
    public GameData.ShopContents shopContents;
    public bool isDead;
    public LanePosition lanePosition;
    private bool isInPlayerTeam;     // DEFINE SI ES QUE ESTE CHARACTER VA A SER SPAWNEADO O NO EN EL TEAM DEL PLAYER

    public Character(Type type) //, SubType subType = SubType.None
    {
        this.type = type;
        //this.subType = subType;
        name = type.ToString();

        stats = new Stats
        {
            attack = 10,
            health = 100,
            healthMax = 100,
            defense = 1,
            damageChance = 75,
            turns = 1,
        };

        switch (type)
        {
            default:
                break;

            /////////////////// Personajes jugables
            
            case Type.Suyai:       // Prota - Healer
                name = "Suyai";
                stats = new Stats
                {
                    attack = 20,
                    health = 100,
                    healthMax = 100,
                    defense = 1,
                    turns = 2
                };
                isInPlayerTeam = true;
                lanePosition = LanePosition.Middle;
                break;

            case Type.Chillpila:       // Kalcu - Mago oscuro
                name = "Chillpila";
                stats = new Stats
                {
                    attack = 20,
                    health = 90,
                    healthMax = 90,
                    defense = 1,
                    turns = 2
                };
                isInPlayerTeam = true;
                lanePosition = LanePosition.Down;
                break;

            case Type.Pedro:            // Trickster - Debuffer
                name = "Pedro";
                stats = new Stats
                {
                    attack = 25,
                    health = 110,
                    healthMax = 110,
                    defense = 1,
                    turns = 2
                };
                isInPlayerTeam = true;
                lanePosition = LanePosition.Up;
                break;

            case Type.Antay:            // Tank
                name = "Antay";
                stats = new Stats
                {
                    attack = 20,
                    health = 125,
                    healthMax = 125,
                    defense = 2,
                    turns = 3
                };
                //isInPlayerTeam = true;
                lanePosition = LanePosition.None;
                break;

            case Type.Arana:            // Lancero - DmgDealer
                name = "Arana";
                stats = new Stats
                {
                    attack = 30,
                    health = 110,
                    healthMax = 110,
                    defense = 1,
                    turns = 3
                };
                //isInPlayerTeam = true;
                lanePosition = LanePosition.None;
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






            /////////////////// NPC
            //case Type.Villager_1:
            //case Type.Villager_2:
            //case Type.Villager_3:
            //case Type.Villager_4:
            //case Type.Villager_5:
            //    name = "Villager";
            //    break;

            /////////////////// VENDEDORES
            case Type.Shop:
                name = "Vendor";
                break;
        }
        isDead = false;
    }

    public bool IsInPlayerTeam()
    {
        return isInPlayerTeam;
    }

    public bool AssignIsInPlayerTeam(bool isInTeam)
    {
        isInPlayerTeam = isInTeam;
        return isInPlayerTeam;
    }

    public void ChangeLane(LanePosition lane)
    {
        lanePosition = lane;

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
