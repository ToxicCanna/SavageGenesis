using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConditionsDB
{
    public static void Init()
    {
        foreach (var kvp in Conditions)
        {
            var conditionId = kvp.Key;
            var condition = kvp.Value;

            condition.Id = conditionId;
        }
    }

    public static Dictionary<ConditionID, Condition> Conditions { get; set; } = new Dictionary<ConditionID, Condition>()
    {
        {
            ConditionID.psn,
            new Condition()
            {
                Name = "Poison",
                StartMessage = "has been poisoned!",
                OnAfterTurn = (Enemy enemy) =>
                {
                    enemy.UpdateHP(enemy.MaxHp / 8);
                    enemy.StatusChanges.Enqueue($"The poison in {enemy.Base.Name}'s veins takes its toll.");

                }
            }
        },
        {
            ConditionID.brn,
            new Condition()
            {
                Name = "Burn",
                StartMessage = "got burnt!",
                OnAfterTurn = (Enemy enemy) =>
                {
                    enemy.UpdateHP(enemy.MaxHp / 16);
                    enemy.StatusChanges.Enqueue($"{enemy.Base.Name}'s burn sapped its strength.");

                }
            }
        },
        {
            ConditionID.par,
            new Condition()
            {
                Name = "Paralyzed",
                StartMessage = "has been paralyzed!",

                OnBeforeSkill = (Enemy enemy) =>
                {
                    if (Random.Range(1, 4) == 1)
                    {
                        enemy.StatusChanges.Enqueue($"The {enemy.Base.Name}'s paralysis froze it mid-motion!");
                        return false;
                    }
                    return true;
                }
            }
        },
        {
            ConditionID.slp,
            new Condition()
            {
                Name = "Sleep",
                StartMessage = "fell asleep!",
                OnStart = (Enemy enemy) =>
                {
                    //random sleep time
                    enemy.StatusTurns = Random.Range(1,4);
                },

                OnBeforeSkill = (Enemy enemy) =>
                {
                    if (enemy.StatusTurns <=0)
                    {
                        enemy.CureStatus();
                        enemy.StatusChanges.Enqueue($"The {enemy.Base.Name} woke up!");
                        return true;
                    }
                    enemy.StatusTurns--;
                    enemy.StatusChanges.Enqueue($"The {enemy.Base.Name} is fast asleep.");
                    return false;
                },
            }
        },
        {
            ConditionID.cld,
            new Condition()
            {
                Name = "Cold",
                StartMessage = "is frozen!",
                OnBeforeSkill = (Enemy enemy) =>
                {
                    if (Random.Range(1, 5) == 1)
                    {
                        enemy.CureStatus();
                        enemy.StatusChanges.Enqueue($"The {enemy.Base.Name} braced itself against the cold!");
                        return true;
                    }
                    enemy.StatusChanges.Enqueue($"The {enemy.Base.Name} is too cold to move");
                    return false;
                },
                OnAfterTurn = (Enemy enemy) =>
                {
                    enemy.UpdateHP(enemy.MaxHp / 32);
                    enemy.StatusChanges.Enqueue($"{enemy.Base.Name} shivers as the cold drains its energy.");

                }
            }
        },

        //Volatile Status Conditions
        {
            ConditionID.confusion,
            new Condition()
            {
                Name = "Confusion",
                StartMessage = "became disoriented!",
                OnStart = (Enemy enemy) =>
                {
                    //Confused for up to 4 turns
                    enemy.VolatileStatusTurns = Random.Range(1,5);
                },

                OnBeforeSkill = (Enemy enemy) =>
                {
                    if (enemy.VolatileStatusTurns <=0)
                    {
                        enemy.CureVolatileStatus();
                        enemy.StatusChanges.Enqueue($"The {enemy.Base.Name} regained their composure!");
                        return true;
                    }
                    enemy.VolatileStatusTurns--;

                    if (Random.Range(1,3) == 1)
                    return true;

                    //hurt by confusion
                    enemy.StatusChanges.Enqueue($"{enemy.Base.Name} is disoriented");
                    enemy.UpdateHP(enemy.MaxHp / 8);
                    enemy.StatusChanges.Enqueue($"{enemy.Base.Name} hurt itself while disoriented!");
                    return false;
                },
            }
        },
    };
}
public enum ConditionID
{
    none, psn, brn, slp, par, cld, confusion
}
