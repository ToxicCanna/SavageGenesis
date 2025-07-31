using UnityEngine;

interface IDiggingArea
{
    void OnDigging(BoxCollider2D collision, DiggingToolType tool, out bool isDugOut);
    void UpdateDurability(DiggingToolType tool);
}
