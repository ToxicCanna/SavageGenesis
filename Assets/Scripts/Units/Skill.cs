using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skill
{
    public SkillBase Base
    {
        get; set;
    }
    public int Mana
    {
        get; set;
    }
    public Skill(SkillBase eBase)
    {
        Base = eBase;
        Mana = eBase.Mana;
    }
}
