using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatBegining : BaseState
{
    //this is meant to display text when combat first begins and only that. 
    private CombatStateMachine _stateMachine;

    public CombatBegining(CombatStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        _stateMachine.levelInfo.GetDialogText().GetComponent<Text>().text = "Battle Start, You're against a __";
        _stateMachine.levelInfo.GetActionSelector().SetActive(false);
        _stateMachine.levelInfo.GetMoveTypeText().SetActive(false);
        _stateMachine.levelInfo.GetMovePowerText().SetActive(false);
        _stateMachine.levelInfo.GetActionSelectorCursor().SetActive(false);
    }

    public override void ExitState()
    {
        _stateMachine.levelInfo.GetDialogText().SetActive(false);
    }

    public override void UpdateState()
    {

    }


}
