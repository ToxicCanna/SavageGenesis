using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : BaseStateMachine
{
    [SerializeField] public LevelInfo levelInfo;

    private CombatBegining _combatBeginingState;

    public CombatBegining CombatBeginingState => _combatBeginingState;

    private PlayerMakeDecision _playerMakeDecisionState;

    public PlayerMakeDecision PlayerMakeDecisionState => _playerMakeDecisionState;

    private PlayerAct _playerActState;

    public PlayerAct PlayerActState => _playerActState;

    private EnemyAct _enemyActState;

    public EnemyAct EnemyActState => _enemyActState;

    private CombatEnd _combatEndState;

    public CombatEnd CombatEndState => _combatEndState;

    private PlayerLevelUp _playerLevelUpState;

    public PlayerLevelUp PlayerLevelUpState => _playerLevelUpState;

    private CombatStates combatStates;

    private bool playerActed;
    private bool enemyActed;

    private bool textAnimationFinished;

    private void Awake()
    {
        _combatBeginingState = new CombatBegining(this);
        _playerMakeDecisionState = new PlayerMakeDecision(this);
        _playerActState = new PlayerAct(this);
        _enemyActState = new EnemyAct(this);
        _combatEndState = new CombatEnd(this);
        _playerLevelUpState = new PlayerLevelUp(this);
    }
    private void Start()
    {
        SetState(_combatBeginingState);
        combatStates = CombatStates.CombatBegining;

        playerActed = false;
        enemyActed = false;
        textAnimationFinished = true;
    }

    public void JumpToPlayerMakeDecisionState()
    {
        SetState(_playerMakeDecisionState);
        combatStates = CombatStates.PlayerMakeDecision;
    }

    public void JumpToPlayerActState()
    {
        SetState(_playerActState);
        combatStates = CombatStates.PlayerAct;
        playerActed = true;
    }

    public void JumpToEnemyActState()
    {
        SetState(_enemyActState);
        combatStates = CombatStates.EnemyAct;
        enemyActed = true;
    }

    public void JumpToCombatEndState()
    {
        SetState(_combatEndState);
        combatStates = CombatStates.CombatEnd;
    }

    public void JumpToPlayerLevelUpState()
    {
        SetState(_playerLevelUpState);
        combatStates = CombatStates.PlayerLevelUp;
    }

    public void ResetActedBools()
    {
        playerActed = false;
        enemyActed = false;
    }

    public void PlayerPressedConfirm()
    {
        Debug.Log("confirmpressed");
        if (combatStates == CombatStates.CombatBegining)
        {
            if (textAnimationFinished)
            {
                JumpToPlayerMakeDecisionState();
            }
            else
            { 
                //finish text animation 
            }
        }
        if (combatStates == CombatStates.PlayerMakeDecision)
        {
            //don't do anything and let the selector jump the state here. 
            return;
        }
        if (combatStates == CombatStates.PlayerAct)
        {
            if (textAnimationFinished)
            {
                if (enemyActed)
                {
                    ResetActedBools();
                    JumpToPlayerMakeDecisionState();
                }
                else
                {
                    JumpToEnemyActState();
                }
            }
            else
            { 
                //finish text animation
            }
        }

        if (combatStates == CombatStates.EnemyAct)
        {
            if (textAnimationFinished)
            {
                if (playerActed)
                {
                    ResetActedBools();
                    JumpToPlayerMakeDecisionState();
                }
                else
                {
                    JumpToPlayerActState();
                }
            }
            else
            {
                //finish text animation
            }
        }

    }
}
