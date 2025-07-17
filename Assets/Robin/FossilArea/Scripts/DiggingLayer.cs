using System.Collections;
using UnityEngine;

public class DiggingLayer : BaseMiningLayer, IDiggingArea
{
    [Min(0.1f)]
    public float durability = 50f;

    [SerializeField] private float diggingDelay = 0.5f;

    public void OnDigging(CircleCollider2D collision, DiggingToolType tool)
    {
        if (tilemap == null || collision == null)
        {
            Debug.Log($"{tilemap.name}, {collision.gameObject.name}. One of these inputs is not assigned!");
            return;
        }

        StartCoroutine(HandleDigging(collision, tool));
    }

    private IEnumerator HandleDigging(CircleCollider2D collision, DiggingToolType tool)
    {
        Vector2 center = collision.bounds.center;
        float radius = collision.radius * collision.transform.lossyScale.x;
        BoundsInt bounds = tilemap.cellBounds;
        
        Debug.Log("Digging");
        miningStateMachine.diggingIcon.SetActive(false);

        yield return new WaitForSeconds(diggingDelay);

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
        miningStateMachine.diggingIcon.SetActive(true);
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
}
