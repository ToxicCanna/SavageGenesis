using UnityEngine;

interface IDiggingArea
{
    void OnDigging(CircleCollider2D collision, DiggingToolType tool);
}
