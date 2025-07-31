using UnityEngine;

public class DiggingState : BaseState
{
    private MiningStateMachine _miningStateMachine;
    private UpdateDiggingIcon _updateDiggingIcon;

    public DiggingState(MiningStateMachine stateMachine)
    {
        _miningStateMachine = stateMachine;
        _updateDiggingIcon = _miningStateMachine.diggingIcon.GetComponent<UpdateDiggingIcon>();
    }

    public override void EnterState()
    {
        Debug.Log($"Start Digging, diggingArea Stability: {PlayerDigging.durability}");
        _miningStateMachine.EnableLayer(true, _miningStateMachine.diggingLayer);
        _miningStateMachine.loadingImage.gameObject.SetActive(false);
        _miningStateMachine.uiManager.ShowUI(true);
        _miningStateMachine.diggingIcon.SetActive(true);
    }

    public override void UpdateState()
    {
        if (_updateDiggingIcon.gameObject.activeSelf)
            _miningStateMachine.playerDigging.Dig(_updateDiggingIcon.currentToolRange);

        if (PlayerDigging.durability <= 0)
            _miningStateMachine.SetState(_miningStateMachine.FinishDiggingState);
    }

    public override void ExitState() 
    {
        foreach (var fossil in _miningStateMachine.fossilSpawnedList)
        {
            if (fossil.CheckIfDugOut())
                _miningStateMachine.fossilDigOutList.Add(fossil);
        }

        Debug.Log("Finish Digging");
    }
}
