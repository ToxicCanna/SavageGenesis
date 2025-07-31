using System;
using UnityEngine;

public class DiggingLayer : BaseMiningLayer, IDiggingArea
{
    public static float durability;

    public virtual void OnDigging(BoxCollider2D collision, DiggingToolType tool, out bool isDugOut)
    {
        isDugOut = false;

        if (tilemap == null || collision == null)
        {
            Debug.Log($"{tilemap.name}, {collision.gameObject.name}. One of these inputs is not assigned!");
            return;
        }

        HandleDigging(collision, tool, out isDugOut);
    }

    private void HandleDigging(BoxCollider2D collision, DiggingToolType tool, out bool isDugOut)
    {
        Vector3Int tilePosition = tilemap.WorldToCell(collision.transform.position);
        Debug.Log("tilemap location: " + tilePosition);

        if (tilemap.GetTile(tilePosition) != null)
        {
            tilemap.SetTile(tilePosition, null);
            isDugOut = true;
        }
        else
            isDugOut = false;
    }

    public virtual void UpdateDurability(DiggingToolType tool)
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
