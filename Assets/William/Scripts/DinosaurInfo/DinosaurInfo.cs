using UnityEngine;

[CreateAssetMenu(fileName = "DinosaurInfo", menuName = "Scriptable Objects/DinosaurInfo")]
public class DinosaurInfo : ScriptableObject
{
    public int dexNumber;
    public string dinosaurName;

    public DinosaurType dinosaurTypeOne;
    public DinosaurType dinosaurTypeTwo;

    public int baseExp;
    public float expNeededRate;

    public int baseStrength;
    public float strengthGrowthRate;

    public int baseDefense;
    public float defenseGrowthRate;

    public int baseAgility;
    public float agilityGrowthRate;

    public int baseMaxHP;
    public float maxHpGrowthRate;

    public Sprite dinosaurCombatSprite;
    public Sprite dinosaurInventorySprite;

    public virtual int CalculateExpNeeded(int currentLevel)
    {
        return Mathf.RoundToInt(expNeededRate * (currentLevel^3));
    }
    public virtual int CalculateStrength(int currentLevel)
    {
        return Mathf.RoundToInt((baseStrength * 2 + strengthGrowthRate) * currentLevel / 100 + 5);
    }

    public virtual int CalculateDefense(int currentLevel)
    {
        return Mathf.RoundToInt((baseDefense * 2 + defenseGrowthRate) * currentLevel / 100 + 5);
    }

    public virtual int CalculateAgility(int currentLevel)
    {
        return Mathf.RoundToInt((baseAgility * 2 + agilityGrowthRate) * currentLevel /100 + 5 );
    }
    public virtual int CalculateMaxHP(int currentLevel)
    {
        return Mathf.RoundToInt((baseMaxHP * 2 + maxHpGrowthRate) * currentLevel /100 + currentLevel + 10);
    }

}
