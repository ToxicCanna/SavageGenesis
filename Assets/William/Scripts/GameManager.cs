using UnityEngine;
using UnityEngine.UI;

public class GameManager : Code.Scripts.Managers.Singleton<GameManager>
{
    [SerializeField] private GameMode startingGameMode;
    [SerializeField] private GameMode currentGameMode;

    [SerializeField] private CombatStateMachine _stateMachine;

    [field: SerializeField] public ActionType playerChoice_ActionType { get; set; } //Player's choices. for combat slot one
    [field: SerializeField] public MoveInfo playerChoice_MoveInfo { get; set; }
    [field: SerializeField] public CombatActors playerChoice_MoveOneTarget { get; set; }
    [field: SerializeField] public DinosaurSlot switchFrom { get; set; }
    [field: SerializeField] public DinosaurSlot switchTo { get; set; }

    [field: SerializeField] public ActionType playerChoice_ActionTypeTwo { get; set; } //for combat slot two
    [field: SerializeField] public MoveInfo playerChoice_MoveInfoTwo { get; set; }
    [field: SerializeField] public CombatActors playerChoice_MoveTwoTarget { get; set; }
    [field: SerializeField] public DinosaurSlot switchFromTwo { get; set; }
    [field: SerializeField] public DinosaurSlot switchToTwo { get; set; }

    [field: SerializeField] public MoveInfo enemy_MoveInfoOne { get; set; }
    [field: SerializeField] public MoveInfo enemy_MoveInfoTwo { get; set; }

    [field: SerializeField] public int loadingCount { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGameMode = startingGameMode;
    }

    public GameMode GetCurrentGameMode()
    { 
        return currentGameMode;
    }

    public void PlayerFinishedSelection()
    {
        _stateMachine.JumpToActionStepState();
    }

    public void GenerateRandomMovesSlotOne()
    {
        enemy_MoveInfoOne = _stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotOne().RandomMove();
        Debug.Log("generatemove");
    }

    public void GenerateRandomMovesSlotTwo()
    {
        enemy_MoveInfoTwo = _stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotTwo().RandomMove();
    }

    public void RefreshPlayerCombatSlotOne()
    {
        InventoryDinosaur combatSlotOne = _stateMachine.levelInfo.GetPlayerDinoInventory().LoadCombatSlotOne();
        _stateMachine.levelInfo.GetPlayerNameText().GetComponent<Text>().text = combatSlotOne.nickName;
        _stateMachine.levelInfo.GetPlayerLevelText().GetComponent<Text>().text = "Lvl: " + combatSlotOne.currentLevel;
        _stateMachine.levelInfo.GetPlayerSprite().GetComponent<Image>().sprite = combatSlotOne.dinoInfoRef.dinosaurCombatSprite;
        _stateMachine.levelInfo.GetPlayerExpBar().transform.localScale = new Vector3(combatSlotOne.currentExp / combatSlotOne.currentExpNeeded, 1, 1);
    }

}
