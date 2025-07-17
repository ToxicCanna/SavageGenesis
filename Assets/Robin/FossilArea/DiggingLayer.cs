using UnityEngine;

public class DiggingLayer : BaseMiningLayer, IDiggingArea
{
    public void OnDigging(Vector2 diggingPos, DiggingToolType tool)
    {
        Debug.Log($"Trigger Point: {diggingPos}; Tool Using: {tool}");

        if (tilemap != null)
            tilemap.SetTile(new Vector3Int((int)diggingPos.x, (int)diggingPos.y, 0), null);
        else
            Debug.Log("Tilemap is not found!");
    }
}
