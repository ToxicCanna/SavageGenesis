using UnityEngine;

public class PlayerMakeDecision : BaseState
{
    //player choose options between UI
    private CombatStateMachine _stateMachine;

    public PlayerMakeDecision(CombatStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        _stateMachine.levelInfo.GetActionSelector().SetActive(true);
        _stateMachine.levelInfo.GetActionSelectorCursor().SetActive(true);
    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }

}
