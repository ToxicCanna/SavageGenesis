using UnityEngine;

interface IDiggingArea
{
    void OnDigging(Vector2 diggingPos, DiggingToolType tool);
    void FinishDigging();
}
