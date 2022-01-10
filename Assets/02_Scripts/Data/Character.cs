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
            case Type.TrenTren:
            case Type.CaiCai:
                return true;
            case Type.TESTENEMY:
            case Type.Fusilero:
            case Type.Lancero:
            case Type.Anchimallen:
            case Type.Guirivilo:
            case Type.Piuchen:
                return false;
        }
    }


    public class Stats
    {
        public int healthMax;
        public int health;
        public int attack;
        public int defense;
        public int critChance;
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
        Fusilero,
        Lancero,
        Anchimallen,
        Guirivilo,
        Piuchen,


        Jefe1,
        //Jefe2,
        //Jefe3,

        Shop,
        QuestNpc_1,
        SoldadoMapuche_1,
        SoldadoMapuche_2,
        ViejaMachi,
        HombreMapuche_1,
        HombreMapuche_2,
        MujerMapuche_1,
        MujerMapuche_2,
        NinoMapuche_1,
        NinoMapuche_2,
        NinaMapuche_1,
        NinaMapuche_2,
        TrenTren,
        CaiCai,





    }

    public enum LanePosition
    {
        Middle,
        Up,
        Down,
        None,
    }

    public Type type;
    public Stats stats;
    public string name;
    public HealthSystem healthSystem;
    public Vector3 position;
    public GameData.EnemyEncounter enemyEncounter;
    public GameData.ShopContents shopContents;
    public GameData.NPCDialogues npcDialogues;
    public Quest quest;
    public bool isDead;
    public LanePosition lanePosition;
    private bool firstTimeTalking;
    private bool isInPlayerTeam;     // DEFINE SI ES QUE ESTE CHARACTER VA A SER SPAWNEADO O NO EN EL TEAM DEL PLAYER

    public Character(Type type)
    {
        this.type = type;
        name = type.ToString();

        stats = new Stats
        {
            attack = 10,
            health = 100,
            healthMax = 100,
            defense = 1,
            critChance = 5,
            damageChance = 95,
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
                    critChance = 5,
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
                    critChance = 5,
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
                    health = 100,
                    healthMax = 100,
                    defense = 1,
                    critChance = 5,
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
                    critChance = 5,
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
                    critChance = 5,
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
                    attack = 10,
                    health = 50,
                    healthMax = 50,
                    defense = 0,
                    critChance = 5,
                    damageChance = 90,
                };
                break;

            case Type.Fusilero:
                stats = new Stats
                {
                    attack = 18,
                    health = 65,
                    healthMax = 65,
                    defense = 0,
                    critChance = 5,
                    damageChance = 95,
                };
                break;

            case Type.Lancero:
                stats = new Stats
                {
                    attack = 20,
                    health = 75,
                    healthMax = 75,
                    defense = 1,
                    critChance = 5,
                    damageChance = 95,
                };
                break;

            case Type.Anchimallen:
                stats = new Stats
                {
                    attack = 18,
                    health = 75,
                    healthMax = 75,
                    defense = 0,
                    critChance = 5,
                    damageChance = 95,
                };
                break;

            case Type.Guirivilo:
                stats = new Stats
                {
                    attack = 20,
                    health = 75,
                    healthMax = 75,
                    defense = 1,
                    critChance = 5,
                    damageChance = 95,
                };
                break;

            case Type.Piuchen:
                stats = new Stats
                {
                    attack = 22,
                    health = 80,
                    healthMax = 80,
                    defense = 1,
                    critChance = 5,
                    damageChance = 95,
                };
                break;

            case Type.TrenTren:
                stats = new Stats
                {
                    health = 1000,
                    healthMax = 1000,
                };
                break;






            ///////////////// NPC
            case Type.QuestNpc_1:
                name = "Elpe huen";
                break;

            /////////////////// VENDEDORES
            case Type.Shop:
                name = "Vendedor";
                break;
        }
        isDead = false;
    }

    public bool IsFirstTimeTalking()
    {
        return firstTimeTalking;
    }
    public void AssignFirstTimeTalking()
    {
        firstTimeTalking = true;
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

    public void SetHealthSystem(HealthSystem healthSystem)
    {
        this.healthSystem = healthSystem;
    }

    public HealthSystem GetHealthSystem()
    {
        return healthSystem;
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
            case Type.Fusilero:
            case Type.Lancero:
            case Type.Jefe1:
            case Type.Anchimallen:
            case Type.Guirivilo:
            case Type.Piuchen:
                return true;
        }
    }

}
