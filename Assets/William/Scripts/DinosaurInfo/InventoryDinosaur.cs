using UnityEngine;

public class InventoryDinosaur : MonoBehaviour
{
    //this denotes an individual dinosaur

    [SerializeField] private DinosaurInfo dinoInfoRef;
    [SerializeField] private string nickName;

    [SerializeField] private int currentLevel;
    private int currentExpNeeded;
    private int currentStrength;
    private int currentDefense;
    private int currentAgility;
    private int currentMaxHP;
    private int currentHP;
    private bool isFainted;

    private void SetLevel(int level)
    { 
        currentLevel = level;
    }

    private void CalculateStats()
    {
        currentExpNeeded = dinoInfoRef.CalculateExpNeeded(currentLevel);
        currentStrength = dinoInfoRef.CalculateStrength(currentLevel);
        currentDefense = dinoInfoRef.CalculateDefense(currentLevel);
        currentAgility = dinoInfoRef.CalculateAgility(currentLevel);
        currentMaxHP = dinoInfoRef.CalculateMaxHP(currentLevel);
    }

    private void SetCurrentHP(int hpTo)
    { 
        currentHP = hpTo;
        if (currentHP <= 0) { isFainted = true; }
    }

}
