using UnityEngine;

public class GameManager : Code.Scripts.Managers.Singleton<GameManager>
{
    [SerializeField] private GameMode startingGameMode;
    [SerializeField] private GameMode currentGameMode;

    [field: SerializeField] public ActionType playerChoice_ActionType { get; set; }
    [field: SerializeField] public MoveInfo playerChoice_MoveInfo { get; set; }
    [field: SerializeField] public InventoryDinosaur switchFrom { get; set; }
    [field: SerializeField] public InventoryDinosaur switchTo { get; set; }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        currentGameMode = startingGameMode;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameMode GetCurrentGameMode()
    { 
        return currentGameMode;
    }
}
