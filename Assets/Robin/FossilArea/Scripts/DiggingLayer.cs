using System;
using UnityEngine;
using UnityEngine.Tilemaps;

public class DiggingLayer : BaseMiningLayer, IDiggingArea
{
    public virtual void DigTile(float x, float y, DiggingToolType tool, out bool isDugOut)
    {
       if (!tilemap)
       {
            isDugOut = false;
            Debug.Log("Tilemap is not assigned!");
            return;
       }

        Vector3Int tilePosition = tilemap.WorldToCell(new Vector3(x, y, 0));
        var tile = tilemap.GetTile(tilePosition);

        if (tile != null)
        {
            tilemap.SetTile(tilePosition, null);
            isDugOut = true;
        }
        else
            isDugOut = false;
    }

    
}
