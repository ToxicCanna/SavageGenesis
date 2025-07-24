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

            Debug.Log("OnevOne");
        }
        else if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVTwo)
        {
            skipPlayerSlotTwo = true;

            Debug.Log("OnevTwo");
        }
        else if (GameManager.Instance.GetCurrentGameMode() == GameMode.TwoVOne)
        {
            skipEnemySlotTwo = true;
            Debug.Log("TwovOne");
        }

        GameManager.Instance.GenerateRandomMovesSlotOne();
        if (!skipEnemySlotTwo && !enemyCombatSlotTwo.IsEmpty())
        {
            GameManager.Instance.GenerateRandomMovesSlotTwo();
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

        _stateMachine.JumpToPlayerMakeDecisionState();
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

    private bool CheckAllActionsCompleted()
    {
        if (skipPlayerSlotOne && skipPlayerSlotTwo && skipEnemySlotOne && skipEnemySlotTwo)
        {
            return true;
        }
        return false;
    }

    private int CalculateDamage(InventoryDinosaur attackFrom, InventoryDinosaur attackTo, MoveInfo attackMove, float targetMultiplier)
    {
        bool critHit = Random.Range(1, 101) < attackMove.critRate;
        float critMulti = 1f;
        if (critHit)
        {
            critMulti = attackMove.critDamageMultiplyer;
        }
        float STAB = 1f;
        if (attackFrom.GetType() == attackMove.GetType())
        {
            STAB = 1.5f;
        }
        float randomFactor = Random.Range(0.85f, 1.01f);

        int damage = Mathf.RoundToInt((((((2 * attackFrom.currentLevel) / 5) + 2) * attackMove.moveStrength * attackFrom.currentStrength / attackTo.currentDefense/50)+2) * targetMultiplier* critMulti * STAB *randomFactor);
        Debug.Log("damage: "+ damage);

        return damage;
    }

    private void PerformFastestAttack()
    {
        //ties are performed in the order of player then enemy



        if (CheckAllActionsCompleted())
        {
            return;
        }
        
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
        //step 1 apply counter to self if countering
        //step 2 check for multiattack
        //step 3 do damage, including crit and everything else buffs etc. 
        //step 4 if hit someone countering and move can be countered, get countered
        //step 5 apply status
        //step 6 apply buffdebuff
        //step 7 regen
        MoveInfo currentMove;
        switch (currentFastestActor)
        {
            case CombatActors.PlayerSlotOne:
                currentMove = GameManager.Instance.playerChoice_MoveInfo;
                if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
                {
                    playerCombatSlotOne.CalculateBuffDebuffs();
                    enemyCombatSlotOne.CalculateBuffDebuffs();

                    if (currentMove.moveIsCounter)
                    {
                        playerSlotOneCountering = true;
                        skipPlayerSlotOne = true;
                        return;
                    }
                    if (enemySlotOneCountering && currentMove.moveCanBeCountered)
                    {
                        
                        playerCombatSlotOne.TakeDamage(CalculateDamage(playerCombatSlotOne, enemyCombatSlotOne, currentMove, 1f));

                        //use dotween here later
                        _stateMachine.levelInfo.GetPlayerHealthBar().transform.localScale = new Vector3((float) playerCombatSlotOne.currentHP / playerCombatSlotOne.currentMaxHP, 1, 1);
                        

                        if (Random.Range(1, 101) < currentMove.statusChance * 100 && playerCombatSlotOne.currentStatus == StatusType.None)
                        {
                            playerCombatSlotOne.InflictStatus(currentMove.statusType);
                        }

                        if (Random.Range(1, 101) < currentMove.statChangeChance * 100)
                        {
                            playerCombatSlotOne.BuffDebuff(currentMove.strengthBuffDebuff, currentMove.defenseBuffDebuff, currentMove.agilityBuffDebuff);
                        }

                        if (Random.Range(1, 101) < currentMove.statChangeChance * 100)
                        {
                            playerCombatSlotOne.BuffDebuff(currentMove.strengthBuffDebuffUser, currentMove.defenseBuffDebuffUser, currentMove.agilityBuffDebuffUser);
                        }

                        if (!playerCombatSlotOne.isFainted && currentMove.regenUser)
                        {
                            playerCombatSlotOne.Regen();
                        }

                    }
                    else
                    { 
                        for (int i = 0; i < GameManager.Instance.playerChoice_MoveInfo.multiHit; i++)
                        {
                            enemyCombatSlotOne.TakeDamage(CalculateDamage(playerCombatSlotOne, enemyCombatSlotOne, currentMove, 1f));
                        
                            //use dotween here later
                            _stateMachine.levelInfo.GetEnemyHealthBar().transform.localScale = new Vector3((float) enemyCombatSlotOne.currentHP / enemyCombatSlotOne.currentMaxHP, 1, 1);

                            if (Random.Range(1, 101) < currentMove.statusChance * 100 && playerCombatSlotOne.currentStatus == StatusType.None)
                            {
                                enemyCombatSlotOne.InflictStatus(currentMove.statusType);
                            }

                            if (Random.Range(1, 101) < currentMove.statChangeChance * 100)
                            {
                                enemyCombatSlotOne.BuffDebuff(currentMove.strengthBuffDebuff, currentMove.defenseBuffDebuff, currentMove.agilityBuffDebuff);
                            }

                            if (Random.Range(1, 101) < currentMove.statChangeChance * 100)
                            {
                                playerCombatSlotOne.BuffDebuff(currentMove.strengthBuffDebuffUser, currentMove.defenseBuffDebuffUser, currentMove.agilityBuffDebuffUser);
                            }

                            if (!playerCombatSlotOne.isFainted && currentMove.regenUser)
                            {
                                playerCombatSlotOne.Regen();
                            }
                        }

                    }

                    skipPlayerSlotOne = true;
                    
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
                currentMove = GameManager.Instance.enemy_MoveInfoOne;
                if (GameManager.Instance.GetCurrentGameMode() == GameMode.OneVOne)
                {
                    playerCombatSlotOne.CalculateBuffDebuffs();
                    enemyCombatSlotOne.CalculateBuffDebuffs();

                    if (currentMove.moveIsCounter)
                    {
                        enemySlotOneCountering = true;
                        skipEnemySlotOne = true;
                        return;
                    }
                    if (playerSlotOneCountering && currentMove.moveCanBeCountered) //player is countering
                    {

                        enemyCombatSlotOne.TakeDamage(CalculateDamage(enemyCombatSlotOne, playerCombatSlotOne, currentMove, 1f));

                        //use dotween here later
                        _stateMachine.levelInfo.GetEnemyHealthBar().transform.localScale = new Vector3((float) enemyCombatSlotOne.currentHP / enemyCombatSlotOne.currentMaxHP, 1, 1);


                        if (Random.Range(1, 101) < currentMove.statusChance * 100 && enemyCombatSlotOne.currentStatus == StatusType.None)
                        {
                            enemyCombatSlotOne.InflictStatus(currentMove.statusType);
                        }

                        if (Random.Range(1, 101) < currentMove.statChangeChance * 100)
                        {
                            enemyCombatSlotOne.BuffDebuff(currentMove.strengthBuffDebuff, currentMove.defenseBuffDebuff, currentMove.agilityBuffDebuff);
                        }

                        if (Random.Range(1, 101) < currentMove.statChangeChance * 100)
                        {
                            enemyCombatSlotOne.BuffDebuff(currentMove.strengthBuffDebuffUser, currentMove.defenseBuffDebuffUser, currentMove.agilityBuffDebuffUser);
                        }

                        if (!enemyCombatSlotOne.isFainted && currentMove.regenUser)
                        {
                            enemyCombatSlotOne.Regen();
                        }

                    }
                    else
                    {
                        for (int i = 0; i < currentMove.multiHit; i++)
                        {
                            playerCombatSlotOne.TakeDamage(CalculateDamage(enemyCombatSlotOne, playerCombatSlotOne, currentMove, 1f));

                            //use dotween here later
                            _stateMachine.levelInfo.GetPlayerHealthBar().transform.localScale = new Vector3((float) playerCombatSlotOne.currentHP / playerCombatSlotOne.currentMaxHP, 1, 1);

                            if (Random.Range(1, 101) < currentMove.statusChance * 100 && playerCombatSlotOne.currentStatus == StatusType.None)
                            {
                                playerCombatSlotOne.InflictStatus(currentMove.statusType);
                            }

                            if (Random.Range(1, 101) < currentMove.statChangeChance * 100)
                            {
                                playerCombatSlotOne.BuffDebuff(currentMove.strengthBuffDebuff, currentMove.defenseBuffDebuff, currentMove.agilityBuffDebuff);
                            }

                            if (Random.Range(1, 101) < currentMove.statChangeChance * 100)
                            {
                                enemyCombatSlotOne.BuffDebuff(currentMove.strengthBuffDebuffUser, currentMove.defenseBuffDebuffUser, currentMove.agilityBuffDebuffUser);
                            }

                            if (!enemyCombatSlotOne.isFainted && currentMove.regenUser)
                            {
                                enemyCombatSlotOne.Regen();
                            }
                        }

                    }

                    skipEnemySlotOne = true;
                    CheckAllActionsCompleted();

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