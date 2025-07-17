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
        _miningStateMachine.loadingImage.gameObject.SetActive(false);
    }

    public override void UpdateState()
    {
        _miningStateMachine.playerDigging.Dig();

        if (_miningStateMachine.diggingLayer.stability <= 0)
        {

        }
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
