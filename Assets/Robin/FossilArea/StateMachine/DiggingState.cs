using UnityEngine;

public class DiggingState : BaseState
{
    private MiningStateMachine _miningStateMachine;

    public DiggingState(MiningStateMachine stateMachine)
    {
        _miningStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("Start Digging");
        _miningStateMachine.EnableLayer(true, _miningStateMachine.diggingLayer);
    }

    public override void UpdateState()
    {
        _miningStateMachine.playerDigging.Dig();
    }

    public override void ExitState() 
    { 
        //Finish Digging
    }

    //Digging State Unique Methods
    private void FinishDigging()
    {
        Debug.Log("Finish digging");
    }
}
