using UnityEngine;

public class ActionStep : BaseState
{
    //player and enemy do their move
    //Step 1 enemy ai choose two moves
    //Step 2 we check speed, switch->speed3->2>1
    //Step 3 perform fastest action. skip if need be
    //Step 4 repeat till finished all 4 of the slots

    private CombatStateMachine _stateMachine;

    private bool skipPlayerSlotOne; //for both deaths on a turn and 1v1/2v1 situations
    private bool skipPlayerSlotTwo;
    private bool skipEnemySlotOne;
    private bool skipEnemySlotTwo;

    private InventoryDinosaur playerCombatSlotOne;
    private InventoryDinosaur playerCombatSlotTwo;
    private InventoryDinosaur enemyCombatSlotOne;
    private InventoryDinosaur enemyCombatSlotTwo;

    private bool playerSlotOneCountering;
    private bool playerSlotTwoCountering;
    private bool enemySlotOneCountering;
    private bool enemySlotTwoCountering;
    public ActionStep(CombatStateMachine stateMachine)
    {
        _stateMachine = stateMachine;
    }

    public override void EnterState()
    {
        Debug.Log("ActionStep");
        _stateMachine.levelInfo.GetSkillSelector().SetActive(false);
        _stateMachine.levelInfo.GetSkillSelectorCursor().SetActive(false);
        _stateMachine.levelInfo.GetMoveTypeText().SetActive(false);
        _stateMachine.levelInfo.GetMovePowerText().SetActive(false);

        playerCombatSlotOne = _stateMachine.levelInfo.GetPlayerDinoInventory().LoadCombatSlotOne();
        playerCombatSlotTwo = _stateMachine.levelInfo.GetPlayerDinoInventory().LoadCombatSlotTwo(); 
        enemyCombatSlotOne = _stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotOne();
        enemyCombatSlotTwo = _stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotTwo();

        playerSlotOneCountering = false;
        playerSlotTwoCountering = false;
        enemySlotOneCountering = false;
        enemySlotTwoCountering = false;

        if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
        {
            skipPlayerSlotTwo = true;
            skipEnemySlotTwo = true;
        }
        else if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVTwo)
        {
            skipPlayerSlotTwo = true;
        }
        else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVOne)
        {
            skipEnemySlotTwo = true;
        }

        GameManager.Instance.enemy_MoveInfoOne = _stateMachine.levelInfo.GetEnemyDinoInventory().LoadCombatSlotOne().RandomMove();
        if (!skipEnemySlotTwo && !enemyCombatSlotTwo.IsEmpty())
        {
            GameManager.Instance.enemy_MoveInfoTwo = enemyCombatSlotTwo.RandomMove();
        }

        // now we have four moves. 

        // act out switch
        if (GameManager.Instance.playerChoice_ActionType == ActionType.Switch)
        {
            //do switch
            skipPlayerSlotOne = true;
        }
        if (GameManager.Instance.playerChoice_ActionType == ActionType.Item)
        {
            //do item
            skipPlayerSlotOne = true;
        }
        if (!skipPlayerSlotTwo)
        { 
            if (GameManager.Instance.playerChoice_ActionTypeTwo == ActionType.Switch)
            {
                //do switch
                skipPlayerSlotTwo = true;
            }
            if (GameManager.Instance.playerChoice_ActionTypeTwo == ActionType.Item)
            {
                //do item
                skipPlayerSlotOne = true;
            }
        }

        //act out attack
        PerformFastestAttack();
        PerformFastestAttack();
        PerformFastestAttack();
        PerformFastestAttack();

        
    }

    public override void ExitState()
    {
        skipPlayerSlotOne = false;
        skipPlayerSlotTwo = false;
        skipEnemySlotOne = false;
        skipEnemySlotTwo = false;
    }

    public override void UpdateState()
    {

    }

    private void CheckAllActionsCompleted()
    {
        if (skipPlayerSlotOne && skipPlayerSlotTwo && skipEnemySlotOne && skipEnemySlotTwo)
        {
            _stateMachine.JumpToPlayerMakeDecisionState();
        }
    }

    private void PerformFastestAttack()
    {
        //ties are performed in the order of player then enemy

        CheckAllActionsCompleted();
        
        CombatActors currentFastestActor = CombatActors.None;
        MoveSpeed currentFastestMoveSpeed = MoveSpeed.SpeedOne;
        int currentFastestDinoAgility = 0;

        //find fastest actor
        if (!skipPlayerSlotOne && GameManager.Instance.playerChoice_ActionType == ActionType.Attack)
        {
            currentFastestActor = CombatActors.PlayerSlotOne;
            currentFastestMoveSpeed = GameManager.Instance.playerChoice_MoveInfo.moveSpeed;
            currentFastestDinoAgility = playerCombatSlotOne.currentAgility;

        }

        if (!skipPlayerSlotTwo && GameManager.Instance.playerChoice_ActionTypeTwo == ActionType.Attack)
        {
            if (GameManager.Instance.playerChoice_MoveInfoTwo.moveSpeed > currentFastestMoveSpeed)
            {
                currentFastestActor = CombatActors.PlayerSlotTwo;
                currentFastestMoveSpeed = GameManager.Instance.playerChoice_MoveInfoTwo.moveSpeed;
                currentFastestDinoAgility = playerCombatSlotTwo.currentAgility;
            }
            else if (GameManager.Instance.playerChoice_MoveInfoTwo.moveSpeed == currentFastestMoveSpeed)
            {
                if (playerCombatSlotTwo.currentAgility > currentFastestDinoAgility)
                { 
                    currentFastestActor = CombatActors.PlayerSlotTwo;
                    currentFastestMoveSpeed = GameManager.Instance.playerChoice_MoveInfoTwo.moveSpeed;
                    currentFastestDinoAgility = playerCombatSlotTwo.currentAgility;
                }
            }
        }

        if (!skipEnemySlotOne)
        {
            if (GameManager.Instance.enemy_MoveInfoOne.moveSpeed > currentFastestMoveSpeed)
            {
                currentFastestActor = CombatActors.EnemySlotOne;
                currentFastestMoveSpeed = GameManager.Instance.enemy_MoveInfoOne.moveSpeed;
                currentFastestDinoAgility = enemyCombatSlotOne.currentAgility;
            }
            else if (GameManager.Instance.enemy_MoveInfoOne.moveSpeed == currentFastestMoveSpeed)
            {
                if (enemyCombatSlotOne.currentAgility > currentFastestDinoAgility)
                {
                    currentFastestActor = CombatActors.EnemySlotOne;
                    currentFastestMoveSpeed = GameManager.Instance.enemy_MoveInfoOne.moveSpeed;
                    currentFastestDinoAgility = enemyCombatSlotOne.currentAgility;
                }
            }
        }

        if (!skipEnemySlotTwo)
        {
            if (GameManager.Instance.enemy_MoveInfoTwo.moveSpeed > currentFastestMoveSpeed)
            {
                currentFastestActor = CombatActors.EnemySlotTwo;
                currentFastestMoveSpeed = GameManager.Instance.enemy_MoveInfoTwo.moveSpeed;
                currentFastestDinoAgility = enemyCombatSlotTwo.currentAgility;
            }
            else if (GameManager.Instance.enemy_MoveInfoTwo.moveSpeed == currentFastestMoveSpeed)
            {
                if (enemyCombatSlotTwo.currentAgility > currentFastestDinoAgility)
                {
                    currentFastestActor = CombatActors.EnemySlotTwo;
                    currentFastestMoveSpeed = GameManager.Instance.enemy_MoveInfoTwo.moveSpeed;
                    currentFastestDinoAgility = enemyCombatSlotTwo.currentAgility;
                }
            }
        }

        //fastest actor performs attack
        //attacking
        //step 1 check for multiattack
        //
        switch (currentFastestActor)
        {
            case CombatActors.PlayerSlotOne:
                if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
                {
                    playerCombatSlotOne.CalculateBuffDebuffs();
                    enemyCombatSlotOne.CalculateBuffDebuffs();


                }
                else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVOne)
                { }
                else if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVTwo)
                { }
                else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVTwo)
                { }
                break;
            case CombatActors.PlayerSlotTwo:
                if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVOne)
                {
                    //making later
                }
                else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVTwo)
                { 
                    //make later
                }
                
                break;
            case CombatActors.EnemySlotOne:
                if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
                {
                    playerCombatSlotOne.CalculateBuffDebuffs();
                    enemyCombatSlotOne.CalculateBuffDebuffs();

                }
                else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVOne)
                { }
                else if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVTwo)
                { }
                else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVTwo)
                { }

                break;
            case CombatActors.EnemySlotTwo:
                if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVTwo)
                {
                    playerCombatSlotOne.CalculateBuffDebuffs();
                    enemyCombatSlotOne.CalculateBuffDebuffs();
                }
                else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVTwo)
                { }
                break;
            default:
                //something is wrong if it hits here
                Debug.Log("error, you didn't secure/set the game mode correctly");
                break;
        }


    }
}