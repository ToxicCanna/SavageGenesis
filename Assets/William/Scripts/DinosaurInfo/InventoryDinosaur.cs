using UnityEngine;

public class InventoryDinosaur : MonoBehaviour
{
    //this denotes an individual dinosaur

    [field: SerializeField] public DinosaurInfo dinoInfoRef { get; private set; }
    [field: SerializeField] public string nickName { get; private set; }
    [field: SerializeField] public int currentLevel { get; private set; }

    [field: SerializeField] public StatusType currentStatus { get; private set; }

    [SerializeField] private bool isEmpty;
    [SerializeField] private bool usedByEnemy;

    private int origionalStrength;
    private int origionalDefense;
    private int origionalAgility;

    private int totalStrengthBuffDebuff;
    private int totalDefenseBuffDebuff;
    private int totalAgilityBuffDebuff;

    [field: SerializeField] public int currentExpNeeded { get; private set; }
    [field: SerializeField] public int currentStrength { get; private set; }
    [field: SerializeField] public int currentDefense { get; private set; }
    [field: SerializeField] public int currentAgility { get; private set; }
    [field: SerializeField] public int currentMaxHP { get; private set; }
    [field: SerializeField] public int currentHP { get; private set; }
    [field: SerializeField] public int currentExp { get; private set; }

    private bool isFainted;

    [field: SerializeField] public MoveInfo moveOne { get; private set; }
    [field: SerializeField] public MoveInfo moveTwo { get; private set; }
    [field: SerializeField] public MoveInfo moveThree { get; private set; }
    [field: SerializeField] public MoveInfo moveFour { get; private set; }
    [field: SerializeField] public MoveInfo moveFive { get; private set; }

    [field: SerializeField] public bool moveTwoEmpty { get; private set; } //move Two onward is empty
    [field: SerializeField] public bool moveThreeEmpty { get; private set; } //move Three onward is empty and so on
    [field: SerializeField] public bool moveFourEmpty { get; private set; }
    [field: SerializeField] public bool moveFiveEmpty { get; private set; }


    void Start()
    {
        if (!isEmpty)
        {
            Reset();
        }

        GameManager.Instance.loadingCount++;

    }

    public void Reset()
    {
        
        CalculateStats();
        ResetBuffDebuffs();
        currentHP = currentMaxHP;
        CheckNickName();
    }


    private void SetLevel(int level)
    { 
        currentLevel = level;
    }

    private void CalculateStats()
    {
        if (!usedByEnemy)
        {
            currentExpNeeded = dinoInfoRef.CalculateExpNeeded(currentLevel);
        }
        origionalStrength = dinoInfoRef.CalculateStrength(currentLevel);
        origionalDefense = dinoInfoRef.CalculateDefense(currentLevel);
        origionalAgility = dinoInfoRef.CalculateAgility(currentLevel);
        currentMaxHP = dinoInfoRef.CalculateMaxHP(currentLevel);
    }

    public void CalculateBuffDebuffs()
    {
        if (totalStrengthBuffDebuff == 0)
        {
            currentStrength = origionalStrength;
        }
        else if (totalStrengthBuffDebuff < 0)
        {
            currentStrength = Mathf.RoundToInt(2/(2 + (totalStrengthBuffDebuff * -1)) * origionalStrength);
        } 
        else
        {
            currentStrength = Mathf.RoundToInt((2 + totalStrengthBuffDebuff) / 2 * origionalStrength);
        }

        if (totalDefenseBuffDebuff == 0)
        {
            currentDefense = origionalDefense;
        }
        else if (totalDefenseBuffDebuff < 0)
        {
            currentDefense = Mathf.RoundToInt(2 / (2 + (totalDefenseBuffDebuff * -1)) * origionalDefense);
        }
        else
        {
            currentDefense = Mathf.RoundToInt((2 + totalDefenseBuffDebuff) / 2 * origionalDefense);
        }

        if (totalAgilityBuffDebuff == 0)
        {
            currentAgility = origionalAgility;
        }
        else if (totalDefenseBuffDebuff < 0)
        {
            currentAgility = Mathf.RoundToInt(2 / (2 + (totalAgilityBuffDebuff * -1)) * origionalAgility);
        }
        else
        {
            currentAgility = Mathf.RoundToInt((2 + totalAgilityBuffDebuff) / 2 * origionalAgility);
        }
    }

    private void ResetBuffDebuffs()
    {
        currentStrength = origionalStrength;

        currentDefense = origionalDefense;

        currentAgility = origionalAgility;

        totalStrengthBuffDebuff = 0;
        totalDefenseBuffDebuff = 0;
        totalAgilityBuffDebuff = 0;
    }

    public void BuffDebuff(int strengthChange, int defenseChange, int agilityChange)
    {
        totalStrengthBuffDebuff += strengthChange;
        totalDefenseBuffDebuff += defenseChange;
        totalAgilityBuffDebuff += agilityChange;
    }

    private void SetCurrentHP(int hpTo)
    { 
        currentHP = hpTo;
        if (currentHP <= 0) { isFainted = true; }
    }

    public void TakeDamage(int damage)
    {
        SetCurrentHP(currentHP - damage);
    }

    public void Regen()
    {
        SetCurrentHP(Mathf.RoundToInt(currentMaxHP * 0.1f + currentHP));
    }
    private void CheckNickName()
    {
        if (nickName.Length == 0) {
            nickName = dinoInfoRef.dinosaurName;
        }
    }

    public bool IsEmpty()
    {
        return isEmpty;
    }

    public MoveInfo GetMove(int slotNum)
    {
        if (slotNum == 1)
        {
            return moveOne;
        }

        switch (slotNum)
        {
            case 1:
                return moveOne;
            case 2:
                return moveTwo;
            case 3:
                return moveThree;
            case 4:
                return moveFour;
            case 5:
                return moveFive;
            default:
                Debug.Log("error, weird slotNum entered");
                return null;
        }
    }
    public MoveInfo RandomMove()
    {
        if (moveTwoEmpty)
        {
            return moveOne;
        }
        else if (moveThreeEmpty)
        {
            return GetMove(Random.Range(1, 3));
        }
        else if (moveFourEmpty)
        {
            return GetMove(Random.Range(1, 4));
        }
        else if (moveFiveEmpty)
        {
            return GetMove(Random.Range(1, 5));
        }
        else
        {
            return GetMove(Random.Range(1, 6));
        }
    }

    public void InflictStatus(StatusType st)
    {
        currentStatus = st;
    }


}
