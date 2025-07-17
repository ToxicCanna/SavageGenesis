using UnityEngine;
using UnityEngine.Tilemaps;

public abstract class BaseMiningLayer : MonoBehaviour
{
    protected Tilemap tilemap;

    protected virtual void Start()
    {
        tilemap = GetComponentInChildren<Tilemap>();
    }
}
