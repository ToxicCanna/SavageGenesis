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
        _miningStateMachine.EnableLayer(false, _miningStateMachine.diggingLayer);
        _miningStateMachine.loadingImage.gameObject.SetActive(true);
    }

    public override void UpdateState()
    {
        if (_miningStateMachine.fossilLayer.isInitializationComplete)
            _miningStateMachine.SetState(_miningStateMachine.DiggingState);
    }

    public override void ExitState() 
    {
        Debug.Log("Initialization Complete");
    }
}
