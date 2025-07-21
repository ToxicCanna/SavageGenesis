using UnityEngine;

public class GameManager : Code.Scripts.Managers.Singleton<GameManager>
{
    [SerializeField] private GameMode startingGameMode;
    [SerializeField] private GameMode currentGameMode;

    [SerializeField] private CombatStateMachine _stateMachine;

    [field: SerializeField] public ActionType playerChoice_ActionType { get; set; } //Player's choices. for combat slot one
    [field: SerializeField] public MoveInfo playerChoice_MoveInfo { get; set; }
    [field: SerializeField] public CombatActors playerChoice_MoveOneTarget { get; set; }
    [field: SerializeField] public InventoryDinosaur switchFrom { get; set; }
    [field: SerializeField] public InventoryDinosaur switchTo { get; set; }

    [field: SerializeField] public ActionType playerChoice_ActionTypeTwo { get; set; } //for combat slot two
    [field: SerializeField] public MoveInfo playerChoice_MoveInfoTwo { get; set; }
    [field: SerializeField] public CombatActors playerChoice_MoveTwoTarget { get; set; }
    [field: SerializeField] public InventoryDinosaur switchFromTwo { get; set; }
    [field: SerializeField] public InventoryDinosaur switchToTwo { get; set; }

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
}
