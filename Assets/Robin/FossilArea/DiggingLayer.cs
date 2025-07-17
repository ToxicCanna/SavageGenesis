using UnityEngine;

public class DiggingLayer : BaseMiningLayer, IDiggingArea
{
    public void OnDigging(Vector2 diggingPos, DiggingToolType tool)
    {
        if (tilemap != null)
        {
            tilemap.SetTile(grid.LocalToCell(diggingPos), null);
        }
        else
            Debug.Log("Tilemap is not found!");
    }
}
