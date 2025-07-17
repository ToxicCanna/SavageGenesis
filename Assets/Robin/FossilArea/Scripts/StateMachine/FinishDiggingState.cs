using UnityEngine;

public class FinishDiggingState : BaseState
{
    private MiningStateMachine _miningStateMachine;

    public FinishDiggingState(MiningStateMachine stateMachine) 
    {
        _miningStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        foreach (var fossil in _miningStateMachine.fossilDigOutList)
            Debug.Log($"You get {fossil}!");
    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }
}
