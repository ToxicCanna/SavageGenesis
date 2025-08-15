using UnityEngine;
using UnityEngine.SceneManagement;
public class EnemyFainted : BaseState
{
    //this is meant to display text when combat finishes. 
    private CombatStateMachine _stateMachine;

    public EnemyFainted(CombatStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("enemy fainted!");


        //swap and bring in enemy 2
        if (!_stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotTwo().IsEmpty() && !_stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotTwo().isFainted)
        {
            _stateMachine.levelInfo.GetEnemyDinoInventory().SwapCombatSlots(DinosaurSlot.One, DinosaurSlot.Two);
            GameManager.Instance.RefreshEnemyCombatSlotOne();
            ChooseNextState();
        }
        else if (!_stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotThree().IsEmpty() && !_stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotThree().isFainted)
        {
            _stateMachine.levelInfo.GetEnemyDinoInventory().SwapCombatSlots(DinosaurSlot.One, DinosaurSlot.Three);
            GameManager.Instance.RefreshEnemyCombatSlotOne();
            ChooseNextState();
        }
        else if (!_stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotFour().IsEmpty() && !_stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotFour().isFainted)
        {
            _stateMachine.levelInfo.GetEnemyDinoInventory().SwapCombatSlots(DinosaurSlot.One, DinosaurSlot.Four);
            GameManager.Instance.RefreshEnemyCombatSlotOne();
            ChooseNextState();
        }
        else
        {
            Debug.Log("YOU WON");
            //change scene
            SceneManager.LoadScene(2);
        }


    }

    public override void ExitState()
    {
        GameManager.Instance.goToEnemyFaintedState = false;
    }

    public override void UpdateState()
    {

    }

    private void ChooseNextState()
    {
        if (GameManager.Instance.goToPlayerFaintedState)
        {
            _stateMachine.JumpToPlayerFaintedState();
        }
        else
        {
            _stateMachine.JumpToPlayerMakeDecisionState();
        }
    }

}
