using UnityEngine;

public class EnemyAct : BaseState
{
    //enemy do an attack
    private CombatStateMachine _stateMachine;

    public EnemyAct(CombatStateMachine stateMachine)
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
