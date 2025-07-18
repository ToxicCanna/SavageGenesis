using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CombatBegining : BaseState
{
    //this is meant to display text when combat first begins and only that. 
    private CombatStateMachine _stateMachine;

    private InventoryDinosaur combatSlotOne;
    private InventoryDinosaur enemySlotOne;

    public CombatBegining(CombatStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        combatSlotOne = _stateMachine.levelInfo.GetPlayerDinoInventory().LoadCombatSlotOne();
        enemySlotOne = _stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotOne();

        if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
        {
            

            _stateMachine.levelInfo.GetDialogText().GetComponent<Text>().text = "Battle Start";
            _stateMachine.levelInfo.GetActionSelector().SetActive(false);
            _stateMachine.levelInfo.GetMoveTypeText().SetActive(false);
            _stateMachine.levelInfo.GetMovePowerText().SetActive(false);
            _stateMachine.levelInfo.GetActionSelectorCursor().SetActive(false);

            _stateMachine.levelInfo.GetPlayerNameText().GetComponent<Text>().text = combatSlotOne.nickName;
            _stateMachine.levelInfo.GetPlayerLevelText().GetComponent<Text>().text = "Lvl: " + combatSlotOne.currentLevel;
            _stateMachine.levelInfo.GetPlayerSprite().GetComponent<Image>().sprite = combatSlotOne.dinoInfoRef.dinosaurCombatSprite;
            _stateMachine.levelInfo.GetPlayerExpBar().transform.localScale = new Vector3(combatSlotOne.currentExp / combatSlotOne.currentExpNeeded, 1, 1);

            _stateMachine.levelInfo.GetEnemyNameText().GetComponent<Text>().text = enemySlotOne.nickName;
            _stateMachine.levelInfo.GetEnemyLevelText().GetComponent<Text>().text = "Lvl: " + enemySlotOne.currentLevel;
            _stateMachine.levelInfo.GetEnemySprite().GetComponent<Image>().sprite = enemySlotOne.dinoInfoRef.dinosaurCombatSprite;

        }

    }

    public override void ExitState()
    {
        _stateMachine.levelInfo.GetDialogText().SetActive(false);
    }

    public override void UpdateState()
    {

    }


}
