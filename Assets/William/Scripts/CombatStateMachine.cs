using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : BaseStateMachine
{
    [SerializeField] public LevelInfo levelInfo;

    private LoadCombat _loadCombatState;

    public LoadCombat LoadCombatState => _loadCombatState;

    private CombatBegining _combatBeginingState;

    public CombatBegining CombatBeginingState => _combatBeginingState;

    private PlayerMakeDecision _playerMakeDecisionState;

    public PlayerMakeDecision PlayerMakeDecisionState => _playerMakeDecisionState;

    private ActionStep _actionStepState;

    public ActionStep ActionStepState => _actionStepState;

    private EnemyFainted _enemyFaintedState;

    public EnemyFainted EnemyFaintedState => _enemyFaintedState;

    private PlayerFainted _playerFaintedState;

    public PlayerFainted PlayerFaintedState => _playerFaintedState;

    private CombatEnd _combatEndState;

    public CombatEnd CombatEndState => _combatEndState;

    private PlayerLevelUp _playerLevelUpState;

    public PlayerLevelUp PlayerLevelUpState => _playerLevelUpState;

    private CombatStates combatStates;

    private bool playerActed;
    private bool enemyActed;

    public bool textAnimationFinished;

    private void Awake()
    {
        _loadCombatState = new LoadCombat(this);
        _combatBeginingState = new CombatBegining(this);
        _playerMakeDecisionState = new PlayerMakeDecision(this);
        _actionStepState = new ActionStep(this);
        _enemyFaintedState = new EnemyFainted(this);
        _playerFaintedState = new PlayerFainted(this);
        _combatEndState = new CombatEnd(this);
        _playerLevelUpState = new PlayerLevelUp(this);
    }
    private void Start()
    {
        SetState(_loadCombatState);
        combatStates = CombatStates.LoadCombat;

        playerActed = false;
        enemyActed = false;
        textAnimationFinished = true;
    }

    public void JumpToCombatBegining()
    {
        SetState(_combatBeginingState);
        combatStates = CombatStates.CombatBegining;
    }

    public void JumpToPlayerMakeDecisionState()
    {
        SetState(_playerMakeDecisionState);
        combatStates = CombatStates.PlayerMakeDecision;
    }

    public void JumpToActionStepState()
    {
        SetState(_actionStepState);
        combatStates = CombatStates.ActionStep;
    }

    public void JumpToEnemyFaintedState()
    {
        SetState(_enemyFaintedState);
        combatStates = CombatStates.EnemyFainted;
    }

    public void JumpToPlayerFaintedState()
    {
        SetState(_playerFaintedState);
        combatStates = CombatStates.PlayerFainted;
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
                textAnimationFinished = true;
            }
        }
        if (combatStates == CombatStates.PlayerMakeDecision)
        {
            //don't do anything and let the selector jump the state here. 
            return;
        }
        if (combatStates == CombatStates.ActionStep)
        {
            
        }


    }
}
