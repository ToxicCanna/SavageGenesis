using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class BaseMiningLayer : MonoBehaviour
{
    public Grid grid;
    
    public MiningStateMachine miningStateMachine;
    [NonSerialized] public Tilemap tilemap;

    protected virtual void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }
}
