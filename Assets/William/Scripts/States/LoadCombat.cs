using UnityEngine;

public class LoadCombat : BaseState
{
    //enemy do an attack
    private CombatStateMachine _stateMachine;

    public LoadCombat(CombatStateMachine stateMachine)
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
        if (GameManager.Instance.loadingCount == 10)
        {
            _stateMachine.JumpToCombatBegining();
        }
    }

}