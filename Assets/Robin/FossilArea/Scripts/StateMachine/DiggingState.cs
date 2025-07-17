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
        Debug.Log($"Start Digging, diggingArea Stability: {_miningStateMachine.diggingLayer.stability}");
        _miningStateMachine.EnableLayer(true, _miningStateMachine.diggingLayer);
        _miningStateMachine.loadingImage.gameObject.SetActive(false);
        _miningStateMachine.diggingIcon.SetActive(true);
    }

    public override void UpdateState()
    {
        _miningStateMachine.playerDigging.Dig();

        if (_miningStateMachine.diggingLayer.stability <= 0)
            _miningStateMachine.SetState(_miningStateMachine.FinishDiggingState);
    }

    public override void ExitState() 
    {
        foreach (var fossil in _miningStateMachine.fossilSpawnedList)
        {
            if (fossil.isDigOut)
                _miningStateMachine.fossilDigOutList.Add(fossil);
        }

        Debug.Log("Finish Digging");
    }
}
