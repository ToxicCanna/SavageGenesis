using UnityEngine;

public class SecondDiggingLayer : DiggingLayer, IDiggingArea
{
    public override void OnDigging(CircleCollider2D collision, DiggingToolType tool)
    {
        base.OnDigging(collision, tool);
        Debug.Log("Second Layer");
    }
}
