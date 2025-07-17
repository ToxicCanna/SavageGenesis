using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishDiggingState : BaseState
{
    private MiningStateMachine _miningStateMachine;

    public FinishDiggingState(MiningStateMachine stateMachine) 
    {
        _miningStateMachine = stateMachine;
    }

    public override void EnterState()
    {
        _miningStateMachine.diggingIcon.SetActive(false);

        foreach (var fossil in _miningStateMachine.fossilDigOutList)
        {
            Debug.Log($"You get {fossil}!");
        }

        _miningStateMachine.StartCoroutine(BackToOverworldScene(0));

    }

    public override void UpdateState()
    {

    }

    public override void ExitState()
    {

    }

    IEnumerator BackToOverworldScene(int sceneIndex)
    {
        yield return new WaitForSeconds(_miningStateMachine.waitInSecAfterFinishDigging);
        SceneManager.LoadScene(sceneIndex);
    }
}
