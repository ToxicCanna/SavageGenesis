using UnityEngine;

public class InitializeState : BaseState
{
    private MiningStateMachine _miningStateMachine;

    public InitializeState(MiningStateMachine stateMachine)
    {
        _miningStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("Initializing");
        
        _miningStateMachine.EnableLayer(false, _miningStateMachine.miningLayers[0]);
        _miningStateMachine.EnableLayer(true, _miningStateMachine.miningLayers[1]);
    }

    public override void UpdateState()
    {
        _miningStateMachine.SetState(_miningStateMachine.DiggingState);
    }

    public override void ExitState() 
    {
        Debug.Log("Initialization Complete");
    }
}
