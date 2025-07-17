using UnityEngine;

public enum DinosaurType {
    None, 
    Predator, 
    Armored, 
    Agile
};
public enum MoveSpeed
{
    SpeedOne,
    SpeedTwo,
    SpeedThree
};

public enum StatusType
{
    None,
    Bleed,
    Stun,
    Disable
};

public enum CombatStates
{ 
    CombatBegining,
    PlayerMakeDecision,
    PlayerAct,
    EnemyAct,
    CombatEnd,
    PlayerLevelUp
};

public enum ActionType
{
    Attack,
    Switch,
    Item
};

public enum SkillSlot
{ 
    One,
    Two,
    Three,
    Four,
    Five
};