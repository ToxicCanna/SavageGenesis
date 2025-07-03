using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Skill", menuName = "Skill/Create new Skill")]


public class SkillBase
    : ScriptableObject
{
    [SerializeField] string skillName;

    [TextArea]
    [SerializeField] string skillDescription;

    [SerializeField] EnemyType type;
    [SerializeField] int power;
    [SerializeField] int accuracy;
    [SerializeField] bool alwaysHits;
    [SerializeField] int mana;
    [SerializeField] int priority;
    [SerializeField] SkillCategory category;
    [SerializeField] SkillEffects effects;
    [SerializeField] List<SecondaryEffects> secondaries;
    [SerializeField] SkillTarget target;

    public string Name
    {
        get
        {
            return skillName;
        }
    }
    public string SkillDescription
    {
        get
        {
            return skillDescription;
        }
    }
    public EnemyType Type
    {
        get
        {
            return type;
        }
    }
    public int Power
    {
        get
        {
            return power;
        }
    }
    public int Accuracy
    {
        get
        {
            return accuracy;
        }
    }
    public bool AlwaysHits
    {
        get
        {
            return alwaysHits;
        }
    }
    public int Mana
    {
        get
        {
            return mana;
        }
    }
    public int Priority
    {
        get
        {
            return priority;
        }
    }
    public SkillCategory Category
    {
        get
        {
            return category;
        }
    }
    public SkillEffects Effects
    {
        get
        {
            return effects;
        }
    }
    public List<SecondaryEffects> Secondaries
    {
        get
        {
            return secondaries;
        }
    }
    public SkillTarget Target
    {
        get
        {
            return target;
        }
    }
}


[System.Serializable]

public class SkillEffects
{
    [SerializeField] List<StatBoost> boosts;
    [SerializeField] ConditionID status;
    [SerializeField] ConditionID volatileStatus;

    public List<StatBoost> Boosts
    {
        get { return boosts; }
    }

    public ConditionID Status 
    { 
        get { return status; }
    }

    public ConditionID VolatileStatus
    {
        get { return volatileStatus; }
    }
}

[System.Serializable]

public class SecondaryEffects : SkillEffects
{
    [SerializeField] int chance;
    [SerializeField] SkillTarget target;

    public int Chance
    {
        get { return chance; }
    }
    public SkillTarget Target
    {
        get { return target; }
    }
}

[System.Serializable]

public class StatBoost
{
    public Stat stat;
    public int boost;
}


public enum SkillCategory
{
    Attack, Magic, Status
}

public enum SkillTarget
{
    Foe, Self
}