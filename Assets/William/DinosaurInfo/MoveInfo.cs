using UnityEngine;

[CreateAssetMenu(fileName = "MoveInfo", menuName = "Scriptable Objects/MoveInfo")]
public class MoveInfo : ScriptableObject
{
    public DinosaurType moveType;

    public int moveStrength;
    public int moveCooldown;
    public float moveAccuracy;
    public MoveSpeed moveSpeed;

    public StatusType statusType;
    public float statusChance;

    public string buffsAndDebuff;

    public bool moveCleansesUserStatus;
    public bool moveCleansesUserBuffs;
    public bool moveCleansesOpponentBuffs;

    public float critRate;
    public float critDamageMultiplyer;

    public int multiHit;

    public bool moveCanBeCountered;
    public bool moveIsCounter;
}
