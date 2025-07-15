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

    [NonSerialized] public FossilLayer fossilLayer;
    [NonSerialized] public DiggingLayer diggingLayer;
    [NonSerialized] public PlayerDigging playerDigging;

    private void Awake()
    {
        //Initialize all states
        _initialState = new InitializeState(this);
        _diggingState = new DiggingState(this);

        //Initialize layers and player digging function
        fossilLayer = GetComponentInChildren<FossilLayer>(true);
        diggingLayer = GetComponentInChildren<DiggingLayer>(true);
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
