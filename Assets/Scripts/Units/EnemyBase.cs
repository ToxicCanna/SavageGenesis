using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Enemy", menuName = "Enemy/Create new Enemy")]

public class EnemyBase : ScriptableObject
{
    [SerializeField] string enemyName;

    [TextArea]
    [SerializeField] string description;

    [SerializeField] Sprite playerSprite;
    [SerializeField] Sprite enemySprite;

    [SerializeField] EnemyType type1;
    [SerializeField] EnemyType type2;

    // base stats
    [SerializeField] int maxHp;
    [SerializeField] int attack;
    [SerializeField] int defense;
    [SerializeField] int focus;
    [SerializeField] int resist;
    [SerializeField] int dexterity;

    [SerializeField] int expYield;
    [SerializeField] GrowthRate growthRate;

    [SerializeField] List<LearnableSkill> learnableSkills;

    public int GetExpForLevel(int level)
    {
        if (growthRate == GrowthRate.Fast)
        {
            return 4 * (level * level * level) / 5;
        }
        else if (growthRate == GrowthRate.MediumFast)
        {
            return level * level * level;
        }

        return -1;
    }

    public Sprite PlayerSprite
    {
        get
        {
            return playerSprite;
        }
    }

    public Sprite EnemySprite
    {
        get
        {
            return enemySprite;
        }
    }

    public EnemyType Type1
    {
        get
        {
            return type1;
        }
    }

    public EnemyType Type2
    {
        get
        {
            return type2;
        }
    }

    public string Name
    {
        get
        {
            return enemyName;
        }
    }

    public string Description
    {
        get
        {
            return description;
        }
    }

    public int MaxHp
    {
        get
        {
            return maxHp;
        }
    }
    public int Attack
    {
        get
        {
            return attack;
        }
    }
    public int Defense
    {
        get
        {
            return defense;
        }
    }
    public int Focus
    {
        get
        {
            return focus;
        }
    }
    public int Resist
    {
        get
        {
            return resist;
        }
    }
    public int Dexterity
    {
        get
        {
            return dexterity;
        }
    }
    public List<LearnableSkill> LearnableSkills
    {
        get
        {
            return learnableSkills;
        }
    }

    public int ExpYield => expYield;

    public GrowthRate GrowthRate => growthRate;
}

[System.Serializable]

public class LearnableSkill
{
    [SerializeField] SkillBase skillBase;
    [SerializeField] int level;

    public SkillBase Base
    {
        get
        {
            return skillBase;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
    }
}

public enum EnemyType
{
    None,
    Mortal,
    Fire,
    Water,
    Grass,
    Earth,
    Wind,
    Thunder,
    Undead,
    Life,
    Demon
}

public enum GrowthRate
{
    Fast, MediumFast
}

public enum Stat
{
    Attack,
    Defense,
    Focus,
    Resist,
    Dexterity,

    //secondary stats
    Accuracy,
    Evasion
}

public class WeaknessChart
{
    static float[][] chart =
    {
        //                 Mortal,Fire,Water,Grass,Earth,Wind,Thunder,Undead,Life,Demon
        /*Mortal*/new float [] { 1f, 1f, 1f, 1f, 1f, 1f, 1f, 0.5f, 0.5f, 0.5f },

        /*Fire*/new float [] { 1f, 1f, 0.5f, 2f, 0.5f, 2f, 1f, 2f, 1f, 0.75f },

        /*Water*/new float [] { 1f, 2f, 1f, 0.5f, 2f, 1f, .5f, 1.5f, 1.5f, 0.75f },

        /*Grass*/new float [] { 1f, 0.5f, 2f, 1f, 1f, 0.5f, 2f, 1f, 2f, 0.75f },

        /*Earth*/new float [] { 1f, 2f, 0.5f, 1f, 1f, 0.5f, 2f, 1.25f, 1.25f, 0.75f },

        /*Wind*/new float [] { 1f, 0.5f, 1f, 2f, 2f, 1f, 0.5f, 1f, 1.5f, 0.75f },

        /*Thunder*/new float [] { 1f, 1f, 2f, 0.5f, 0.5f, 2f, 1f, 1.5f, 1f, 0.75f },

        /*Undead*/new float [] { 2f, 0.5f, 0.75f, 1f, 1f, 1f, 1f, 1f, 2f, 0.5f },

        /*Life*/new float [] { 2f, 1f, 1.5f, 2f, 1f, 1f, 1f, 2f, 1f, 2f },

        /*Demon*/new float [] { 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 2f, 3f }
    };

    public static float GetEffect(EnemyType attackType, EnemyType defenseType)
    {
        if (attackType == EnemyType.None || defenseType == EnemyType.None)
        {
            return 1;
        }

        int row = (int)attackType - 1;
        int col = (int)defenseType - 1;

        return chart[row][col];
    }
}