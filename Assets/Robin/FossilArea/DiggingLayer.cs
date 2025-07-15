using UnityEngine;

public class DiggingLayer : BaseMiningLayer, IDiggingArea
{
    public void OnDigging(Vector2 diggingPos, DiggingToolType tool)
    {
        Debug.Log($"Trigger Point: {diggingPos}; Tool Using: {tool}");
    }
}
