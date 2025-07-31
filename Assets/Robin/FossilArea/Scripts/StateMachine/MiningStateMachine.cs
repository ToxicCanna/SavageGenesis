using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MiningStateMachine : BaseStateMachine
{
    public GameObject playerRef;
    public Image loadingImage;
    public GameplayUIManager uiManager;
    public GameObject diggingIcon;
    public float waitInSecAfterFinishDigging = 3f;

    #region Mining States
    //Keep Track of all mining states
    private InitializeState _initialState;
    private DiggingState _diggingState;
    private FinishDiggingState _finishDiggingState;

    //Referencing all mining states
    public InitializeState InitialState => _initialState;
    public DiggingState DiggingState => _diggingState;
    public FinishDiggingState FinishDiggingState => _finishDiggingState;
    #endregion

    #region NonSerialized Input
    [NonSerialized] public FossilLayer fossilLayer;
    [NonSerialized] public DiggingLayer diggingLayer;
    [NonSerialized] public PlayerDigging playerDigging;
    [NonSerialized] public List<Fossil> fossilSpawnedList = new List<Fossil>();
    [NonSerialized] public List<Fossil> fossilDigOutList = new List<Fossil>();
    #endregion

    private void Awake()
    {
        //Initialize all states
        _initialState = new InitializeState(this);
        _diggingState = new DiggingState(this);
        _finishDiggingState = new FinishDiggingState(this);

        //Initialize layers and player digging function
        fossilLayer = GetComponentInChildren<FossilLayer>(true);
        diggingLayer = GetComponentInChildren<DiggingLayer>(true);
        playerDigging = GetComponentInChildren<PlayerDigging>(true);
    }

    private void Start()
    {
        playerRef.GetComponent<PlayerMovement>().enabled = false;
        SetState(InitialState);
    }

    public void EnableLayer(bool isEnabled, BaseMiningLayer layerEnable)
    {
        layerEnable.gameObject.SetActive(isEnabled);
    }
}
