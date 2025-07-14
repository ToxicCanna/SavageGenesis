using UnityEngine;

public class DiggingLayer : MonoBehaviour, IDiggingArea
{
    public void OnDigging(Vector2 diggingPos, DiggingToolType tool)
    {
        Debug.Log($"Trigger Point: {diggingPos}; Tool Using: {tool}");
    }

    public void FinishDigging()
    {
        Debug.Log("Finish Digging");
    }
}
