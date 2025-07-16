using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatStateMachine : BaseStateMachine
{

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

    private void Awake()
    {

        /*
        _friendMovement = GetComponent<FriendMovement>();
        _friendCarry = GetComponent<FriendCarry>();
        _friendFind = GetComponentInChildren<FriendFind>();
        */

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
    }

}
