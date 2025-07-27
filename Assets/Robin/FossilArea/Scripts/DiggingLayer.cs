using System;
using UnityEngine;

public class DiggingLayer : BaseMiningLayer, IDiggingArea
{
    [SerializeField] private float diggingDelay = 0.5f;
    [NonSerialized] public float durability = 50f;

    private bool _isDigging = false;

    /*protected override void Start()
    {
        base.Start();
        durability = miningStateMachine.uiManager.maxDurability;
    }*/

    public virtual void OnDigging(CircleCollider2D collision, DiggingToolType tool)
    {
        if (tilemap == null || collision == null)
        {
            Debug.Log($"{tilemap.name}, {collision.gameObject.name}. One of these inputs is not assigned!");
            return;
        }

        if (!_isDigging)
        {
            _isDigging = true;
            HandleDigging(collision, tool);
            Invoke(nameof(ResetDigging), diggingDelay);
        }
        else
            Debug.Log("Digging Delaying");
    }

    private void HandleDigging(CircleCollider2D collision, DiggingToolType tool)
    {
        Vector2 center = collision.bounds.center;
        float radius = collision.radius * collision.transform.lossyScale.x;
        BoundsInt bounds = tilemap.cellBounds;

        for (int x = bounds.xMin; x < bounds.xMax; x++)
        {
            for (int y = bounds.yMin; y < bounds.yMax; y++)
            {
                Vector3Int tilePosition = new Vector3Int(x, y, 0);
                Vector3 worldPosition = tilemap.CellToWorld(tilePosition) + tilemap.cellSize / 2;

                // Check if the tile is within the circle
                if (Vector2.Distance(center, worldPosition) <= radius)
                {
                    tilemap.SetTile(tilePosition, null);
                }
            }
        }

        UpdateDurability(tool);
    }

    private void UpdateDurability(DiggingToolType tool)
    {
        switch (tool)
        {
            case 0:
                durability -= DiggingToolStrength.diggingStrength[0];
                break;
            case (DiggingToolType)1:
                durability -= DiggingToolStrength.diggingStrength[1];
                break;
            default:
                durability -= 1f;
                break;
        }

        Debug.Log($"Current stability: {durability}");

        if (miningStateMachine.uiManager != null)
        {
            miningStateMachine.uiManager.SetDurability(durability);
            //Debug.Log($"[Digging] Stability now {stability}");
        }
    }

    private void ResetDigging()
    {
        _isDigging = false;
    }
}
