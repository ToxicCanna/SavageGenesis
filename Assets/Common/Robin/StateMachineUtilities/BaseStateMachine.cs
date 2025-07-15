using UnityEngine;

public class BaseStateMachine : MonoBehaviour
{
    protected BaseState CurrentState;

    public virtual void SetState(BaseState newState)
    {
        CurrentState?.ExitState();
        CurrentState = newState;
        CurrentState.EnterState();
    }

    public virtual void Update()
    {
        CurrentState?.UpdateState();
    }
}
