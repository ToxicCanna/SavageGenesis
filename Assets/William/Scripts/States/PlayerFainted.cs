using UnityEngine;

public class PlayerFainted : BaseState
{
    //this is meant to display text when combat finishes. 
    private CombatStateMachine _stateMachine;

    public PlayerFainted(CombatStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("player fainted!");

        _stateMachine.levelInfo.GetSwitchSelector().SetActive(true);
        _stateMachine.levelInfo.GetSwitchSelectorCursor().SetActive(true);

        _stateMachine.levelInfo.GetActionSelector().SetActive(false);
        _stateMachine.levelInfo.GetActionSelectorCursor().SetActive(false);


    }

    public override void ExitState()
    {
        _stateMachine.levelInfo.GetPlayerDinoInventory().SwapCombatSlots(GameManager.Instance.switchFrom, GameManager.Instance.switchTo);

        GameManager.Instance.RefreshPlayerCombatSlotOne();
        GameManager.Instance.goToPlayerFaintedState = false;
        _stateMachine.levelInfo.GetSwitchSelector().SetActive(false);
        _stateMachine.levelInfo.GetSwitchSelectorCursor().SetActive(false);

    }

    public override void UpdateState()
    {

    }

}
