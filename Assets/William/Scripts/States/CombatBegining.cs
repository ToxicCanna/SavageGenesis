using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

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

    }

    public override void ExitState()
    {

    }

    public override void UpdateState()
    {

    }

}
