using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]

public class Enemy
{
    [SerializeField] EnemyBase _base;
    [SerializeField] int level;

    public EnemyBase Base
    {
        get
        {
            return _base;
        }
    }
    public int Level
    {
        get
        {
            return level;
        }
    }

    public int Exp
    {
        get; set;
    }

    public int HP
    {
        get; set;
    }

    public List<Skill> Skills
    {
        get; set;
    }

    public Skill CurrentSkill
    {
        get; set;
    }

    public Dictionary<Stat, int> Stats
    {
        get; private set;
    }

    public Dictionary<Stat, int> StatBoosts
    {
        get; private set;
    }

    public Condition Status { get; private set; }
    public int StatusTurns { get; set; }

    public Condition VolatileStatus { get; private set; }
    public int VolatileStatusTurns { get; set; }

    public Queue<string> StatusChanges { get; private set; } = new Queue<string>();

    public bool HpChanged { get; set; }

    public event System.Action OnStatusChanged;

    public void Init()
    {

        //Generate Skills
        Skills = new List<Skill>();
        foreach (var skill in Base.LearnableSkills)
        {
            if (skill.Level <= Level)
                Skills.Add(new Skill(skill.Base));
        }

        Exp = Base.GetExpForLevel(Level);

        CalculateStats();
        HP = MaxHp;

        ResetStatBoost();
        Status = null;
        VolatileStatus = null;
    }

    void CalculateStats()
    {
        Stats = new Dictionary<Stat, int>();
        Stats.Add(Stat.Attack, Mathf.FloorToInt((Base.Attack * Level) / 100f) + 5);
        Stats.Add(Stat.Defense, Mathf.FloorToInt((Base.Defense * Level) / 100f) + 5);
        Stats.Add(Stat.Focus, Mathf.FloorToInt((Base.Focus * Level) / 100f) + 5);
        Stats.Add(Stat.Dexterity, Mathf.FloorToInt((Base.Dexterity * Level) / 100f) + 5);
        Stats.Add(Stat.Resist, Mathf.FloorToInt((Base.Resist * Level) / 100f) + 5);

        MaxHp = Mathf.FloorToInt((Base.MaxHp * Level) / 100f) + 10 + Level;
    }

    void ResetStatBoost()
    {
        StatBoosts = new Dictionary<Stat, int>()
        {
            {Stat.Attack, 0},
            {Stat.Defense, 0},
            {Stat.Focus, 0},
            {Stat.Dexterity, 0},
            {Stat.Resist, 0},
            {Stat.Accuracy, 0},
            {Stat.Evasion, 0}
        };
    }

    int GetStat(Stat stat)
    {
        int statVal = Stats[stat];

        //apply stat boosts
        int boost = StatBoosts[stat];
        var boostValues = new float[] { 1f, 1.5f, 2f, 2.5f, 3f, 3.5f, 4f };

        if (boost >= 0)
        {
            statVal = Mathf.FloorToInt(statVal * boostValues[boost]);
        }
        else
        {
            statVal = Mathf.FloorToInt(statVal / boostValues[-boost]);
        }

        return statVal;
    }

    public void ApplyBoosts(List<StatBoost> statBoosts)
    {
        foreach (var statBoost in statBoosts)
        {
            var stat = statBoost.stat;
            var boost = statBoost.boost;

            StatBoosts[stat] = Mathf.Clamp(StatBoosts[stat] + boost, -6, 6);

            if (boost > 0)
            {
                StatusChanges.Enqueue($"{Base.Name}'s {stat} increased!");
            }
            else
            {
                StatusChanges.Enqueue($"{Base.Name}'s {stat} decreased!");
            }

            Debug.Log($"{stat} has been boosted to {StatBoosts[stat]}");
        }
    }

    public bool CheckForLevelUp()
    {
        if (Exp >= Base.GetExpForLevel(level + 1))
        {
            ++level;
            return true;
        }
        else
        {
            return false;
        }
    }

    public int MaxHp
    {
        get; private set;
    }

    public int Attack
    {
        get
        {
            return GetStat(Stat.Attack);
        }
    }

    public int Defense
    {
        get
        {
            return GetStat(Stat.Defense);
        }
    }

    public int Focus
    {
        get
        {
            return GetStat(Stat.Focus);
        }
    }

    public int Resist
    {
        get
        {
            return GetStat(Stat.Resist);
        }
    }

    public int Dexterity
    {
        get
        {
            return GetStat(Stat.Dexterity);
        }
    }

    public DamageRecap TakeDamage(Skill skill, Enemy attacker)
    {
        float critical = 1f;
        if (Random.value * 100f <= Focus)
        {
            critical = 1.75f;
        }

        float type = WeaknessChart.GetEffect(skill.Base.Type, this.Base.Type1) * WeaknessChart.GetEffect(skill.Base.Type, this.Base.Type2);

        var damageRecap = new DamageRecap()
        {
            TypeEffect = type,
            Critical = critical,
            Dead = false
        };

        float attack = (skill.Base.Category == SkillCategory.Magic)? attacker.Focus : attacker.Attack;
        float defense = (skill.Base.Category == SkillCategory.Magic)? Resist : Defense;

        float modifiers = Random.Range(0.85f, 1.15f) * type * critical;
        float a = (2 * attacker.Level + 10) / 250f;
        float d = a * skill.Base.Power * ((float)attack / defense) + 2;
        int damage = Mathf.FloorToInt(d * modifiers);

        UpdateHP(damage);

        return damageRecap;
    }

    public void UpdateHP(int damage)
    {
        HP = Mathf.Clamp(HP - damage, 0, MaxHp);
        HpChanged = true;
    }
    public void HealHP(int amount)
    {
        HP = Mathf.Clamp(HP + amount, 0, MaxHp);
        HpChanged = true;
    }
    public void SetStatus(ConditionID conditionId)
    {
        if (Status != null) return;

        Status = ConditionsDB.Conditions[conditionId];
        Status?.OnStart?.Invoke(this);
        StatusChanges.Enqueue($"{Base.Name} {Status.StartMessage}");
        OnStatusChanged?.Invoke();
    }

    public void CureStatus()
    {
        Status = null;
        OnStatusChanged?.Invoke();
    }

    public void SetVolatileStatus(ConditionID conditionId)
    {
        if (VolatileStatus != null) return;

        VolatileStatus = ConditionsDB.Conditions[conditionId];
        VolatileStatus?.OnStart?.Invoke(this);
        StatusChanges.Enqueue($"{Base.Name} {VolatileStatus.StartMessage}");
    }

    public void CureVolatileStatus()
    {
        VolatileStatus = null;
    }

    public Skill GetRandomSkill()
    {
        var skillsWithMana = Skills.Where(x => x.Mana > 0).ToList();
        int r = Random.Range(0, skillsWithMana.Count);
        return skillsWithMana[r];
    }

    public bool OnBeforeSkill()
    {
        bool canPerformSkill = true;
        if (Status?.OnBeforeSkill != null)
        {
            if (!Status.OnBeforeSkill(this))
            {
                canPerformSkill = false;
            }
        }

        if (VolatileStatus?.OnBeforeSkill != null)
        {
            if (!VolatileStatus.OnBeforeSkill(this))
            {
                canPerformSkill = false;
            }
        }

        return canPerformSkill;
    }

    public void OnAfterTurn()
    {
        Status?.OnAfterTurn?.Invoke(this);
        VolatileStatus?.OnAfterTurn?.Invoke(this);
    }

    public void OnBattleOver()
    {
        VolatileStatus = null;
        ResetStatBoost();
    }
}

public class DamageRecap
{
    public bool Dead
    {
        get; set;
    }
    public float Critical
    {
        get; set;
    }
    public float TypeEffect
    {
        get; set;
    }
}
