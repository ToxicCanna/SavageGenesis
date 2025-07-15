using System;
using UnityEngine;

public class MiningStateMachine : BaseStateMachine
{
    #region Mining States
    //Keep Track of all mining states
    private InitializeState _initialState;
    private DiggingState _diggingState;

    //Referencing all mining states
    public InitializeState InitialState => _initialState;
    public DiggingState DiggingState => _diggingState;
    #endregion

    [NonSerialized] public BaseMiningLayer[] miningLayers = new BaseMiningLayer[2];
    [NonSerialized] public PlayerDigging playerDigging;

    private void Awake()
    {
        //Initialize all states
        _initialState = new InitializeState(this);
        _diggingState = new DiggingState(this);

        //Initialize layers and player digging function
        miningLayers[0] = GetComponentInChildren<DiggingLayer>(true);
        miningLayers[1] = GetComponentInChildren<FossilLayer>(true);
        playerDigging = GetComponentInChildren<PlayerDigging>(true);
    }

    private void Start()
    {
        SetState(InitialState);
    }

    public void EnableLayer(bool isEnabled, BaseMiningLayer layerEnable)
    {
        layerEnable.gameObject.SetActive(isEnabled);
    }
}
