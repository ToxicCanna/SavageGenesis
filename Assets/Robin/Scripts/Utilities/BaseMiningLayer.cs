using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class BaseMiningLayer : MonoBehaviour
{
    public Grid grid;
    protected Tilemap tilemap;

    protected virtual void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }
}
